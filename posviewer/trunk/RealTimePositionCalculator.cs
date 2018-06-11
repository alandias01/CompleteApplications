using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Maple.Utilities;
using Maple.Dtc.Views;
using Maple.Global1.G1BizObjects;
using G1 = Maple.Global1.G1BizObjects;
using Maple.Occ.OccBizObjects;
using System.Threading;
using Maple.NewtonDTCBizObjects;

namespace Maple.Dtc
{
    public class RealTimePositionCalculator
    {
        private PositionFactory pf = new PositionFactory();
        private StartingPositionFactory g1pFact = new StartingPositionFactory();
        private DtcPositionViewFactory dvFact = new DtcPositionViewFactory();
        private IncomingDeliveryOrderFactory idFact = new IncomingDeliveryOrderFactory();
        private RecalledStockViewFactory rsf = new RecalledStockViewFactory();
        private StockViewFactory sf = new StockViewFactory();
        private StockPriceViewFactory spf = new StockPriceViewFactory();
        private AllocationViewFactory avf = new AllocationViewFactory();
        private ReturnViewFactory rvf = new ReturnViewFactory();

        public List<PositionObject> OccBodPositions = new List<PositionObject>();

        private List<StartingPositionObject> G1BodPositions = new List<StartingPositionObject>();
        private Dictionary<string, List<StartingPositionObject>> G1NonNpbPositions = new Dictionary<string, List<StartingPositionObject>>();
        private List<StartingPositionObject> G1NpbPositions = new List<StartingPositionObject>();

        public Dictionary<string, DtcPositionViewObject> DtcBodPositions = new Dictionary<string, DtcPositionViewObject>();

        public List<IncomingDeliveryOrderObject> AllDtcActivity = new List<IncomingDeliveryOrderObject>();
        public List<IncomingDeliveryOrderObject> DtcActivity = new List<IncomingDeliveryOrderObject>();
        public List<IncomingDeliveryOrderObject> DtcPendingActivity = new List<IncomingDeliveryOrderObject>();
        public List<IncomingDeliveryOrderObject> DtcNpbActivity = new List<IncomingDeliveryOrderObject>();

        public List<G1RealTimeActivityObject> AllG1Activity = new List<G1RealTimeActivityObject>();
        public List<G1RealTimeActivityObject> G1Activity = new List<G1RealTimeActivityObject>();

        public Dictionary<string, RealTimePositionObject> RealTimePositions = new Dictionary<string, RealTimePositionObject>();

        public Dictionary<RecalledStockViewObject, int> RecalledStocks = new Dictionary<RecalledStockViewObject, int>();
        public Dictionary<string, StockViewObject> Stocks = new Dictionary<string, StockViewObject>();
        public Dictionary<string, StockPriceViewObject> StockPrices = new Dictionary<string, StockPriceViewObject>();

        public int MaxDtcId { get; set; }
        public int MaxG1Id { get; set; }

        public DateTime ProcessDate { get; set; }
        object a = "";
        object b = "";


        public RealTimePositionCalculator()
        {
            //initialize some data
            try
            {
                //load the recall list
                rsf.Load(RecalledStocks);

                //load the stock list               
                sf.Load(Stocks);


                //load stock prices
                spf.Load(StockPrices);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //when the program first starts up, calculate your position
        public void CalculateStartingPosition(DateTime dte)
        {
            ProcessDate = dte;

            try
            {
                LoadMorningPositions();
                //                LoadG1Activity(dte);
                LoadDtcActivity();

                CalculatePosition();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //when a new order comes in, recalculate your Positions. There is no need
        //to reload g1 data or anything like that.
        public RealTimePositionObject CalculatePosition(IncomingDeliveryOrderObject d)
        {
            RealTimePositionObject pos = null;

            if (d.Id > MaxDtcId)
            {
                MaxDtcId = d.Id;
            }

            //we only care about real msgs now.
            if (d.DtcStatusIndicator == " " || d.DtcStatusIndicator == "X")
            {
                //Non 5239 activity                
                if (d.Receiver != Settings.Account.Padded() && d.Deliverer != Settings.Account.Padded())
                    return null;

                //Remove records with an actioncode of 0                
                if (d.ActionCode == "0")
                    return null;

                //Records that are receives but 5239 is not the receiver, same for deliver                
                if ((d.Receiver != Settings.Account.Padded() && d.DelivererReceiverIndicator == "R")
                    || (d.Deliverer != Settings.Account.Padded() && d.DelivererReceiverIndicator == "D"))
                {
                    return null;
                }

                /* Dont care about NPB anymore
                //Remove the NPB activity
                if (d.Deliverer == "00000238" && d.Receiver == "00005239" && d.ReasonCode == "010")
                    return null;

                if (d.Deliverer == "00005239" && d.Receiver == "00000238" && d.ReasonCode == "020")
                    return null;
                */

                //check to see if we already have activity for this cusip
                RealTimePositions.TryGetValue(d.Cusip, out pos);

                //first activity for this cusip
                if (pos == null)
                {
                    pos = new RealTimePositionObject(d.DateAndTimeStamp.Value.Date, Settings.Account.ToInt(),
                                                                                    "", d.Cusip, d.CusipDescription, 0, 0,
                                                                                    d.DateAndTimeStamp.Value,
                                                                                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "");

                    //DtcActivity has all NPB ripped out
                    pos.DtcNonNpbActivityQuantity = d.ShareQuantity_Signed.Value;
                    pos.DtcNonNpbActivityLoanValue = d.MoneyValue_Signed.Value;
                    pos.Ticker = d.CusipDescription;
                    pos.DtcActivity.Add(d);

                    pos.LastActivityDate = d.DateAndTimeStamp.Value;

                    //Add activity to dtcactivity so you do now apply it twice
                    DtcActivity.Add(d);

                    RealTimePositions.Add(pos.Cusip, pos);
                }
                //already got some activity for this cusip
                else
                {
                    pos.DtcNonNpbActivityQuantity += d.ShareQuantity_Signed.Value;
                    pos.DtcNonNpbActivityLoanValue += d.MoneyValue_Signed.Value;
                    pos.DtcActivity.Add(d);

                    if (d.DateAndTimeStamp > pos.LastActivityDate)
                    {
                        pos.LastActivityDate = d.DateAndTimeStamp.Value;
                    }

                    //Add activity to dtcactivity so you do not apply it twice
                    DtcActivity.Add(d);
                }
                GetExtraData(pos, false);
            }
            else 
            {
                // JP 10/16/12
                
                if (!RealTimePositions.ContainsKey(d.Cusip))
                {
                    // no position for this cusip, but there is pending activity so add a 0 position entry
                    RealTimePositionObject newPosition = new RealTimePositionObject(d.DateAndTimeStamp.Value.Date, Settings.Account.ToInt(),
                                                                                    "", d.Cusip, d.CusipDescription, 0,
                                                                                    0, d.DateAndTimeStamp.Value,
                                                                                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "");

                    //DtcActivity has all NPB ripped out
                    newPosition.DtcNonNpbActivityQuantity = 0;//d.ShareQuantity_Signed.Value;
                    newPosition.DtcNonNpbActivityLoanValue = 0;//d.MoneyValue_Signed.Value;
                    newPosition.Ticker = d.CusipDescription;
                    newPosition.DtcActivity.Add(d);

                    newPosition.LastActivityDate = d.DateAndTimeStamp.Value;

                    RealTimePositions.Add(newPosition.Cusip, newPosition);
                }
            }

            return pos;
        }


        //calculates Position based on start up data
        private void CalculatePosition()
        {
            RealTimePositions.Clear();

            //Build a position from todays activity
            foreach (IncomingDeliveryOrderObject d in DtcActivity)
            {
                RealTimePositionObject found = null;

                RealTimePositions.TryGetValue(d.Cusip, out found);


                //first activity for this cusip
                if (found == null)
                {
                    RealTimePositionObject newPosition = new RealTimePositionObject(d.DateAndTimeStamp.Value.Date, Settings.Account.ToInt(),
                                                                                    "", d.Cusip, d.CusipDescription, 0,
                                                                                    0, d.DateAndTimeStamp.Value,
                                                                                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "");

                    //DtcActivity has all NPB ripped out
                    newPosition.DtcNonNpbActivityQuantity = d.ShareQuantity_Signed.Value;
                    newPosition.DtcNonNpbActivityLoanValue = d.MoneyValue_Signed.Value;
                    newPosition.Ticker = d.CusipDescription;
                    newPosition.DtcActivity.Add(d);

                    newPosition.LastActivityDate = d.DateAndTimeStamp.Value;

                    RealTimePositions.Add(newPosition.Cusip, newPosition);
                }
                //already got some activity for this cusip
                else
                {
                    found.DtcNonNpbActivityQuantity += d.ShareQuantity_Signed.Value;
                    found.DtcNonNpbActivityLoanValue += d.MoneyValue_Signed.Value;
                    found.DtcActivity.Add(d);

                    if (d.DateAndTimeStamp > found.LastActivityDate)
                    {
                        found.LastActivityDate = d.DateAndTimeStamp.Value;
                    }
                }
            }

            //now add the morning position to the list
            foreach (DtcPositionViewObject pos in DtcBodPositions.Values)
            {
                RealTimePositionObject found = null;

                RealTimePositions.TryGetValue(pos.CUSIP, out found);

                //this cusip had no activity today
                if (found == null)
                {
                    RealTimePositions.Add(pos.CUSIP, new RealTimePositionObject(pos));
                }
                //already got some activity for this cusip, update its bod quantity
                else
                {
                    found.BodShareQuantity = pos.ShareQuantity;
                }
            }

            
            //now subtract NPB from DTC
            foreach (StartingPositionObject pos in G1NpbPositions)
            {
                RealTimePositionObject found = null;

                RealTimePositions.TryGetValue(pos.Cusip, out found);

                if (found != null)
                {
                    found.G1BodNpbQuantity = (int)pos.Quantity.Value;
                    found.G1BodNpbLoanValue = pos.LoanValue.Value;
                }
            }

            /*
            //sum up the npb data from todays DTC activity
            foreach (IncomingDeliveryOrderObject npb in DtcNpbActivity)
            {
                RealTimePositionObject found = RealTimePositions.Find(delegate(RealTimePositionObject p)
                { return p.Cusip == npb.Cusip; });

                if (found != null)
                {
                    found.DtcNpbActivityQuantity += (int)npb.ShareQuantity_Signed.Value;
                    found.DtcNpbActivityLoanValue += npb.MoneyValue_Signed.Value;
                }
            }
            */

            //Get extra data 
            foreach (RealTimePositionObject rtPos in RealTimePositions.Values)
            {
                GetExtraData(rtPos, true);
            }

            UpdateConduitInfo(RealTimePositions.Values.ToList());

            
            // JP 10/16/12: add pending position with 0 qty
            // pending
            foreach (IncomingDeliveryOrderObject d in DtcPendingActivity)
            {
                if (!RealTimePositions.ContainsKey(d.Cusip))
                {
                    // no position for this cusip, but there is pending activity so add a 0 position entry
                    RealTimePositionObject newPosition = new RealTimePositionObject(d.DateAndTimeStamp.Value.Date, Settings.Account.ToInt(),
                                                                                    "", d.Cusip, d.CusipDescription, 0,
                                                                                    0, d.DateAndTimeStamp.Value,
                                                                                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "");

                    //DtcActivity has all NPB ripped out
                    newPosition.DtcNonNpbActivityQuantity = 0;//d.ShareQuantity_Signed.Value;
                    newPosition.DtcNonNpbActivityLoanValue = 0;//d.MoneyValue_Signed.Value;
                    newPosition.Ticker = d.CusipDescription;
                    newPosition.DtcActivity.Add(d);

                    newPosition.LastActivityDate = d.DateAndTimeStamp.Value;

                    RealTimePositions.Add(newPosition.Cusip, newPosition);
                }
            }
        }

        //TODO: Get a better name for this
        private void GetExtraData(RealTimePositionObject rtPos, bool firstLoad)
        {
            Dictionary<string, int> tradeCats = new Dictionary<string, int>();

            DateTime lastBDate = DateTime.MinValue;
            DateTime lastLDate = DateTime.MinValue;

            //Rates
            double bRate = 0;
            double lRate = 0;
            double bLastRate = 0;
            double lLastRate = 0;


            //Loan Values
            double bValue = 0;
            double lValue = 0;

            //ShareQty
            int bQty = 1;
            int lQty = 1;
            int bLastQty = 1;
            int lLastQty = 1;

            //Number of Deals
            int bCount = 0;
            int lCount = 0;

            List<StartingPositionObject> found = new List<StartingPositionObject>();
            //            List<G1PositionObject> found = G1NonNpbPositions.FindAll(delegate(G1PositionObject p)
            //            { return p.Cusip.ToUpper() == rtPos.Cusip.ToUpper(); });

            if (G1NonNpbPositions.ContainsKey(rtPos.Cusip.ToUpper()))
            {
                found = G1NonNpbPositions[rtPos.Cusip.ToUpper()];
            }

            if (found.Count > 0)
            {
                foreach (StartingPositionObject g in found)
                {
                    //get all the data you need
                    if (!tradeCats.ContainsKey(g.TradeCategory) && (rtPos.TradeCategory == null || !rtPos.TradeCategory.Contains(g.TradeCategory)))
                    {
                        tradeCats.Add(g.TradeCategory, 0);
                    }

                    if (g.BorrowLoanIndicator == "B")
                    {
                        bRate += (double)g.Rate * (int)Math.Abs(g.Quantity.Value);
                        bQty += (int)Math.Abs(g.Quantity.Value);
                        bValue += (double)Math.Abs(g.LoanValue.Value);
                        bCount++;
                        rtPos.NumBorrows++;

                        if (g.TradeDate.Value.Date >= lastBDate.Date && (int)Math.Abs(g.Quantity.Value) > 0)
                        {
                            if (g.TradeDate.Value.Date > lastBDate.Date)
                            {
                                bLastRate = 0;
                                bLastQty = 1;
                            }

                            bLastRate += (double)g.Rate * (int)Math.Abs(g.Quantity.Value);
                            bLastQty += (int)Math.Abs(g.Quantity.Value);
                            lastBDate = g.TradeDate.Value;
                        }

                    }
                    else
                    {
                        lRate += (double)g.Rate * (int)Math.Abs(g.Quantity.Value);
                        lQty += (int)Math.Abs(g.Quantity.Value);
                        lValue += (double)Math.Abs(g.LoanValue.Value);
                        lCount++;

                        try
                        {
                            if (g.TradeDate.Value.Date >= lastLDate.Date && (int)Math.Abs(g.Quantity.Value) > 0)
                            {
                                if (g.TradeDate.Value.Date > lastLDate.Date)
                                {
                                    lLastRate = 0;
                                    lLastQty = 1;
                                }

                                lLastRate += (double)g.Rate * (int)Math.Abs(g.Quantity.Value);
                                lLastQty += (int)Math.Abs(g.Quantity.Value);
                                lastLDate = g.TradeDate.Value;
                            }
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                    }
                }

                //figure out the trade category
                foreach (string s in tradeCats.Keys)
                {
                    rtPos.TradeCategory += s + " ";
                }

                rtPos.TradeCategory.Trim();

                try
                {
                    rtPos.HistoricalBorrowRate = bRate / bQty;
                    rtPos.HistoricalLoanRate = lRate / lQty;

                    rtPos.BorrowRate = bLastRate / bLastQty;
                    rtPos.LoanRate = lLastRate / lLastQty;
                }
                catch (Exception ex)
                {
                    //Trace.WriteLine(ex, TraceEnum.LoggedError);
                }
            }

            MarkRecall(rtPos, firstLoad);
            UpdateStockInfo(rtPos);
            UpdateStockPrice(rtPos);
            //UpdateConduitInfo(rtPos);
            UpdateDtcInfo(rtPos);
            UpdateOccInfo(rtPos);
        }


        private void LoadMorningPositions()
        {
            //changed this since nthBizday takes holidays into account, but positions table doesnt            
            DateTime dtcDate = Utils.GetNthBusinessDay(ProcessDate, -1);
            DateTime g1Date = DateTime.Today; // Utils.GetLastWeekDay(ProcessDate);

            StartingPositionParam g1p = new StartingPositionParam();
            g1p.FileDate_Date.AddParamValue(g1Date);
            g1p.ClearingNo.AddParamValue(Settings.Account);

            //load all the morning positions
            //g1pFact.LoadMinimal(G1BodPositions, g1p);
            g1pFact.Load(G1BodPositions, g1p);

            //make sure there are some positions
            if (G1BodPositions.Count < 50)
            {
                //                throw new Exception("Problem loading G1 positions. Only found " + G1BodPositions.Count);
            }

            //get the non NPB data
            List<StartingPositionObject> tmp = G1BodPositions.FindAll(p => p.TradeCategory != "NPB");

            foreach (StartingPositionObject g in tmp)
            {
                if (G1NonNpbPositions.ContainsKey(g.Cusip))
                {
                    G1NonNpbPositions[g.Cusip].Add(g);
                }
                else
                {
                    List<StartingPositionObject> t = new List<StartingPositionObject>();
                    t.Add(g);
                    G1NonNpbPositions.Add(g.Cusip, t);
                }
            }

            //get the NPB data
            G1NpbPositions = G1BodPositions.FindAll(p => p.TradeCategory == "NPB");

            //Group NPB Data together by cusip
            G1NpbPositions = GroupNpb(G1NpbPositions);

            //load the DTC starting position
            DtcPositionViewParam dp = new DtcPositionViewParam();

            dp.DateofData_Date.AddParamValue(dtcDate);
            dp.ParticipantID.AddParamValue(Settings.Account.ToInt());

            dvFact.Load(DtcBodPositions, dp);

            //make sure there are some positions
            if (DtcBodPositions.Count < 1)
            {
                //                throw new Exception("Problem loading DTC positions. Only found " + DtcBodPositions.Count);
            }

            //load occ positions
            PositionParam occp = new PositionParam();
            occp.ActivityDate_Date.AddParamValue(dtcDate);
            pf.Load(OccBodPositions, occp);



            //now load all the starting positions from G1
            foreach (List<StartingPositionObject> g1 in G1NonNpbPositions.Values)
            {
                foreach (StartingPositionObject g in g1)
                {
                    if (!string.IsNullOrEmpty(g.Cusip) && !DtcBodPositions.ContainsKey(g.Cusip.ToUpper()))
                    {
                        //no dtc position for cusip, add it
                        DtcBodPositions.Add(g.Cusip, new DtcPositionViewObject(dtcDate, g.Cusip, 0, 0, 0, 0, Settings.Account.ToInt(), dtcDate, g.Ticker));
                    }
                }
            }

            // ------------------------------Alandias------------------------------
            //If account is 269, it accounts for overnight deliveries for our 269 account
            //if (Settings.Account == "269")
            {
                List<tblDTFPARTObject> DTFPO =new List<tblDTFPARTObject>();
                tblDTFPARTFactory DTFPFact = new tblDTFPARTFactory();
                tblDTFPARTParam DTFParam = new tblDTFPARTParam();
                DTFParam.DateofData.AddParamValue(DateTime.Today);
                DTFParam.TransOrigSource.AddParamValue("CFSD", "!=");   //Alandias 20130301
                //DTFParam.ParticipantNum.AddParamValue("0269");
                DTFParam.ParticipantNum.AddParamValue(Settings.Account.PadLeft(4, '0'));
                DTFParam.SubFunction.AddParamValue("DTFPDQ");
                DTFParam.StatusCode.AddParamValue("m");
                DTFPFact.Load(DTFPO,DTFParam);

                List<tblDTFPARTObject> ItemsInDTFPOButNotInDtcBodPositions = new List<tblDTFPARTObject>();

                foreach (tblDTFPARTObject t in DTFPO)
                {
                    if (DtcBodPositions.ContainsKey(t.CUSIP.ToUpper()) || DtcBodPositions.ContainsKey(t.CUSIP))
                    {
                        switch (t.TransTypeNew.Trim())
                        {
                            case "026": //026 is deliver
                                DtcBodPositions[t.CUSIP.ToUpper()].UnPledgedQuantity -= Convert.ToInt32(t.ShareQuantity);
                                break;
                            case "027": //027 is receive
                                DtcBodPositions[t.CUSIP.ToUpper()].UnPledgedQuantity += Convert.ToInt32(t.ShareQuantity);
                                break;
                        }
                    }
                    else
                    {
                        ItemsInDTFPOButNotInDtcBodPositions.Add(t);
                    }
                }


                foreach (tblDTFPARTObject t in ItemsInDTFPOButNotInDtcBodPositions)
                {   
                    string tempticker = "";
                    if (Stocks.ContainsKey(t.CUSIP.ToUpper()))
                    {
                        StockViewObject s = null;
                        Stocks.TryGetValue(t.CUSIP.ToUpper(), out s);

                        if (s != null)
                        {
                            tempticker = s.Ticker;
                        }

                        else
                        {
                            tempticker = "zzzzUNKNOWN";
                        }
                    }

                    Int32 qty = 0;
                    switch (t.TransTypeNew.Trim())
                    {
                        case "026": //026 is deliver
                            qty -= Convert.ToInt32(t.ShareQuantity);
                            break;
                        case "027": //027 is receive
                            qty += Convert.ToInt32(t.ShareQuantity);
                            break;
                    }

                    if (!DtcBodPositions.ContainsKey(t.CUSIP.ToUpper()))
                    {
                        DtcBodPositions.Add(t.CUSIP.ToUpper(), new DtcPositionViewObject(dtcDate, t.CUSIP.ToUpper(), 0, 0, 0, qty, Settings.Account.ToInt(), dtcDate, tempticker));
                    }
                    else 
                    {
                        DtcBodPositions[t.CUSIP.ToUpper()].UnPledgedQuantity += qty; 
                    }

                    Int32 t1 = DtcBodPositions[t.CUSIP.ToUpper()].UnPledgedQuantity;
                }

                foreach (var temp in DtcBodPositions)
                {
                    
                    temp.Value.ShareQuantity = temp.Value.UnPledgedQuantity;
                }
            }
            // -----------END-------------------Alandias---------------------------


            a = "1";
        }

        private void LoadG1Activity(DateTime dte)
        {
            //get the G1 Activity
            G1RealTimeActivityFactory g1f = new G1RealTimeActivityFactory();
            G1RealTimeActivityParam gp = new G1RealTimeActivityParam();
            gp.FileDate_Date.AddParamValue(dte);
            g1f.Load(AllG1Activity, gp);

            G1Activity = AllG1Activity.Where(w => w.BookName == Settings.BookName).ToList();

            //get the maxId
            if (AllG1Activity.Count > 0)
            {
                MaxG1Id = AllG1Activity.Max(item => item.G1ActivityId);
            }
            else
            {
                MaxG1Id = 0;
            }

        }


        private void LoadDtcActivity()
        {
            //get the DTC Activity
            IncomingDeliveryOrderParam doFromDtcParam = new IncomingDeliveryOrderParam();
            doFromDtcParam.DateAndTimeStamp_Date.AddParamValue(ProcessDate);
            idFact.Load(DtcActivity, doFromDtcParam);

            //Get Dtc Pending Activity
            DtcPendingActivity = DtcActivity.FindAll(delegate(IncomingDeliveryOrderObject d)
            { return d.DtcStatusIndicator != "X" && d.DtcStatusIndicator != " "; });

            //Remove pending
            DtcActivity.RemoveAll(delegate(IncomingDeliveryOrderObject d)
            { return d.DtcStatusIndicator != "X" && d.DtcStatusIndicator != " "; });

            /*
            //Get Dtc NPB Activity
            DtcNpbActivity = DtcActivity.FindAll(delegate(IncomingDeliveryOrderObject d)
            { return d.Deliverer == "00000238" && d.Receiver == "00005239" && d.ReasonCode == "010"; });
            */

            //Now store all the DTC Activity for use in the price control form
            //AllDtcActivity = DtcActivity.FindAll(delegate(IncomingDeliveryOrderObject d)
            //{ return d.Receiver == "00005239" || d.Deliverer == "00005239" || d.Receiver == "00000269" || d.Deliverer == "00000269"; });
            AllDtcActivity = new List<IncomingDeliveryOrderObject>(DtcActivity);

            //Remove non 5239 activity
            DtcActivity.RemoveAll(delegate(IncomingDeliveryOrderObject d)
            { return d.Receiver != Settings.Account.Padded() && d.Deliverer != Settings.Account.Padded(); });

            /* Dont care about NPB
            //Remove the NPB activity
            DtcActivity.RemoveAll(delegate(IncomingDeliveryOrderObject d)
            { return d.Deliverer == "00000238" && d.Receiver == "00005239" && d.ReasonCode == "010"; });

            DtcActivity.RemoveAll(delegate(IncomingDeliveryOrderObject d)
            { return d.Deliverer == "00005239" && d.Receiver == "00000238" && d.ReasonCode == "020"; });
            */

            //Remove records with an actioncode of 0
            DtcActivity.RemoveAll(delegate(IncomingDeliveryOrderObject d)
            { return d.ActionCode == "0"; });

            //Remove records that are receives but 5239 is not the receiver, same for deliver
            DtcActivity.RemoveAll(delegate(IncomingDeliveryOrderObject d)
            { return (d.Receiver != Settings.Account.Padded() && d.DelivererReceiverIndicator == "R") || (d.Deliverer != Settings.Account.Padded() && d.DelivererReceiverIndicator == "D"); });


            //get the maxId
            if (DtcActivity.Count > 0)
            {
                MaxDtcId = DtcActivity.Max(item => item.Id);
            }
            else
            {
                MaxDtcId = 0;
            }
            b = "1";
        }

        private List<StartingPositionObject> GroupNpb(List<StartingPositionObject> npb)
        {
            string last = "";
            StartingPositionObject lastPos = new StartingPositionObject();

            List<StartingPositionObject> groupedPositions = new List<StartingPositionObject>();

            npb.Sort(delegate(StartingPositionObject p1, StartingPositionObject p2)
            { return p1.Cusip.CompareTo(p2.Cusip); });

            npb.ForEach(delegate(StartingPositionObject p)
            {
                if (p.Cusip == last)
                {
                    lastPos.Quantity += p.Quantity;
                    lastPos.LoanValue += p.LoanValue;
                }
                else
                {
                    lastPos = new StartingPositionObject();
                    lastPos.Ticker = p.Ticker;
                    lastPos.Quantity = p.Quantity;
                    lastPos.CounterpartyCode = p.CounterpartyCode;
                    lastPos.Cusip = p.Cusip;
                    lastPos.LoanValue = p.LoanValue;

                    groupedPositions.Add(lastPos);
                    last = p.Cusip;
                }
            });

            return groupedPositions;
        }



        private void MarkRecall(RealTimePositionObject r, bool firstLoad)
        {
            if (firstLoad)
            {
                RecalledStockViewObject rs = new RecalledStockViewObject();
                rs.Cusip = r.Cusip;

                if (RecalledStocks.ContainsKey(rs))
                {
                    r.OnRecall = "On Recall";
                }
                else
                {
                    r.OnRecall = "";
                }
            }

            else
            {
                bool ret = rsf.Exists(r.Cusip);

                if (ret)
                {
                    r.OnRecall = "On Recall";
                }

                else
                {
                    r.OnRecall = "";
                }
            }
        }

        private void UpdateConduitInfo(RealTimePositionObject r)
        {
            avf.GetConduitQuantities(r);
        }

        public void UpdateConduitInfo(IList<RealTimePositionObject> rt)
        {
            avf.GetConduitQuantities(rt);
        }

        private void UpdateDtcInfo(RealTimePositionObject r)
        {
            r.DtcQuantityLoaned = 0;
            r.DtcQuantityReturned = 0;
            r.DtcQuantityBorrowed = 0;
            r.DtcQuantityReceived = 0;

            foreach (IncomingDeliveryOrderObject d in r.DtcActivity)
            {
                DeliveryOrderReasonCodeObject drc = null;

                DeliveryOrderLookups.ReasonCodes.TryGetValue(d.ReasonCode, out drc);

                //could not find reason code
                if (drc == null)
                {
                    //Trace.WriteLine("Could not find the reason code: " + d.ReasonCode, TraceEnum.LoggedError);
                }

                else
                {

                    if ((d.Receiver != Settings.Account.Padded() && d.DelivererReceiverIndicator == "R") || (d.Deliverer != Settings.Account.Padded() && d.DelivererReceiverIndicator == "D"))
                    {

                    }
                    //delivery
                    if (d.ActivityCode == "026")
                    {
                        if (drc.NewBorrowLoan)
                        {
                            //this is a new loan
                            r.DtcQuantityLoaned += d.ShareQuantity.Value;
                        }
                        else
                        {
                            //this was a return
                            r.DtcQuantityReturned += d.ShareQuantity.Value;
                        }
                    }

                    //receive
                    else if (d.ActivityCode == "027")
                    {
                        if (drc.NewBorrowLoan)
                        {
                            r.DtcQuantityBorrowed += d.ShareQuantity.Value;
                        }
                        else
                        {
                            r.DtcQuantityReceived += d.ShareQuantity.Value;
                        }
                    }

                    //could not find activity code
                    else
                    {
                        //WE HAVE A PROBLEM
                        //Trace.WriteLine("Could not find the activity code: " + d.ActivityCode, TraceEnum.LoggedError);
                    }
                }
            }
        }


        internal void UpdateNpbInfo(IList<RealTimePositionObject> rt)
        {
            List<CurrentPositionObject> npb = new List<CurrentPositionObject>();
            CurrentPositionFactory cpf = new CurrentPositionFactory();
            CurrentPositionParam cpp = new CurrentPositionParam();
            cpp.ClearingNo.AddParamValue(Settings.Account);
            cpp.BorrowLoanIndicator.AddParamValue("B");
            cpp.TradeCategory.AddParamValue("NPB");
            cpf.Load(npb, cpp);

            foreach (RealTimePositionObject r in rt)
            {
                r.G1NpbActivityQuantity = (int)npb.Where(w => w.Cusip == r.Cusip && w.ActivtyDate.Value.Date == DateTime.Today.Date).Sum(s => Math.Abs(s.Quantity.Value));
                r.G1BodNpbQuantity = (int)npb.Where(w => w.Cusip == r.Cusip && w.ActivtyDate.Value.Date != DateTime.Today.Date).Sum(s => Math.Abs(s.Quantity.Value));
            }
        }


        private void UpdateOccInfo(RealTimePositionObject r)
        {
            r.ContainsHedge = false;
            r.OccBodQuantity = 0;
            r.OccQuantityLoaned = 0;
            r.OccQuantityReturned = 0;
            r.OccQuantityBorrowed = 0;
            r.OccQuantityReceived = 0;

            //look up bod positions
            List<PositionObject> pos = OccBodPositions.FindAll(p => p.Cusip == r.Cusip);

            //calculate bod position
            foreach (PositionObject p in pos)
            {
                r.ContainsHedge = true;

                if (p.BorrowLoan == "B")
                {
                    r.OccBodQuantity += p.Quantity;
                }
                else if (p.BorrowLoan == "L")
                {
                    r.OccBodQuantity -= p.Quantity;
                }
            }

            //calculate occ info
            foreach (IncomingDeliveryOrderObject d in r.DtcActivity)
            {
                DeliveryOrderReasonCodeObject drc = null;

                DeliveryOrderLookups.ReasonCodes.TryGetValue(d.ReasonCode, out drc);

                //could not find reason code
                if (drc == null)
                {
                    //Trace.WriteLine("Could not find the reason code: " + d.ReasonCode, TraceEnum.LoggedError);
                }

                else
                {
                    //delivery
                    if (d.ActivityCode == "026" && IsHedge(d.ReasonCode))
                    {
                        if (drc.NewBorrowLoan)
                        {
                            //this is a new loan
                            r.OccQuantityLoaned += d.ShareQuantity.Value;
                        }
                        else
                        {
                            //this was a return
                            r.OccQuantityReturned += d.ShareQuantity.Value;
                        }
                        r.ContainsHedge = true;
                    }

                    //receive
                    else if (d.ActivityCode == "027" && IsHedge(d.ReasonCode))
                    {
                        if (drc.NewBorrowLoan)
                        {
                            r.OccQuantityBorrowed += d.ShareQuantity.Value;
                        }
                        else
                        {
                            r.OccQuantityReceived += d.ShareQuantity.Value;
                        }
                        r.ContainsHedge = true;
                    }

                    //could not find activity code
                    else
                    {
                    }


                }
            }
        }


        private void UpdateStockInfo(RealTimePositionObject r)
        {
            if (Stocks.ContainsKey(r.Cusip))
            {
                StockViewObject s = null;
                Stocks.TryGetValue(r.Cusip, out s);

                if (s != null)
                {
                    r.Ticker = s.Ticker;
                    r.Company = s.Name;
                }
                else
                {
                    r.Ticker = "zzzzUNKNOWN";
                }
            }
        }

        public string UpdateStockInfo(string cusip)
        {
            string ticker = "";

            if (Stocks.ContainsKey(cusip))
            {
                StockViewObject s = null;
                Stocks.TryGetValue(cusip, out s);

                if (s != null)
                {
                    ticker = s.Ticker;
                }
            }
            return ticker;
        }


        private void UpdateStockPrice(RealTimePositionObject r)
        {
            if (StockPrices.ContainsKey(r.Cusip))
            {
                StockPriceViewObject s = null;
                StockPrices.TryGetValue(r.Cusip, out s);

                if (s != null)
                {
                    r.Price = (double)s.StockloanPrice;
                }
                else
                {
                    r.Price = 0;
                }
            }
        }


        private bool IsHedge(string reasonCode)
        {
            bool ret;
            if (reasonCode == "260" || reasonCode == "261" ||
                reasonCode == "270" || reasonCode == "271")
            {
                ret = true;
            }
            else
            {
                ret = false;
            }
            return ret;
        }


        /*
        private void MarkRecalls(List<RealTimePositionObject> rt)
        {
            foreach (RealTimePositionObject r in rt)
            {
                MarkRecall(r);
            }
        }

        private void UpdateRateInfo(List<RealTimePositionObject> rt)
        {
            foreach (RealTimePositionObject r in rt)
            {
                UpdateRateInfo(r);
            }
        }

        private void UpdateStockInfo(List<RealTimePositionObject> rt)
        {
            foreach (RealTimePositionObject r in rt)
            {
                UpdateStockInfo(r);
            }
        }
          
         private void UpdateRateInfo(RealTimePositionObject r)
        {            
            r.BorrowRate = g1pFact.GetWeightedAvgRate(r.Cusip, "B");
            r.LoanRate = g1pFact.GetWeightedAvgRate(r.Cusip, "L");
        } 
          
          
          
         */





        internal void Clear()
        {
            OccBodPositions.Clear();
            G1BodPositions.Clear();
            G1NonNpbPositions.Clear();
            G1NpbPositions.Clear();
            DtcBodPositions.Clear();
            AllDtcActivity.Clear();
            DtcActivity.Clear();
            DtcPendingActivity.Clear();
            DtcNpbActivity.Clear();
            AllG1Activity.Clear();
            G1Activity.Clear();
            RealTimePositions.Clear();
        }
    }
}
