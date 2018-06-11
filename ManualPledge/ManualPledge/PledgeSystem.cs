using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApexFileCreator;
using Maple.Slate.BizObjects;
using Maple.Dtc;


namespace ManualPledge
{
    public class PledgeSystem
    {

        TradeCreator tc = new TradeCreator();
        PledgeFactory Pfac = new PledgeFactory();

        StockFactory sf = new StockFactory();
        StockParam sp = new StockParam();
        public List<StockObject> StockList = new List<StockObject>();

        OutgoingPledgeOrderFactory PledgeFac = new OutgoingPledgeOrderFactory();

        string Error;

        public PledgeSystem()
        {
            sp.DateOfData.AddParamValue(DateTime.Today);
            sf.Load(StockList, sp);
        }

        public string PledgeStocks(Dictionary<string, int> CusipQtyList, string source, string clearingNo, bool sendToApex, bool sendToDTC)
        {
            if (!sendToApex && !sendToDTC)
            {
                //Add message if we're not sending to any
                Error += "We must at least send to Apex or Send to DTC (OCC)" + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(clearingNo) || clearingNo.Length != 4)
            {
                //Add message for criteria not met
                Error += "Source and\\or clearingNo must have a value. Also, ClearingNo should also be 4 char" + Environment.NewLine;
            }

            foreach (var item in CusipQtyList)
            {
                //Add message for items you couldn't send
                if (string.IsNullOrEmpty(item.Key) || item.Value < 1) { Error += "Cusip cannot be empty" + Environment.NewLine; }
            }

            if (!string.IsNullOrEmpty(Error))
            {
                return Error;
            }

            int ctr = 0;
            string CurrTime = DateTime.Now.ToString("HHmmss");

            foreach (var item in CusipQtyList)
            {
                string cusip = item.Key;
                int qty = item.Value;

                PledgeObject p = PopulateSlatePledgeObject(source, cusip, clearingNo, qty, "P", sendToApex, sendToDTC);

                //add Loanvalue and Ticker to PledgeObject
                PriceTicker(p);

                //Set defaults
                int internalid = -1;
                string UniqueIdForEID = "";
                int UserReferenceNumber = 0;

                if (sendToDTC && sendToApex)
                {
                    Pfac.Insert(p);

                    internalid = p.PledgeId;
                    UniqueIdForEID = p.PledgeId.ToString();
                    UserReferenceNumber = p.PledgeId;
                }

                //When not sending to both and only 1 of them, we need to set the ID's since we were getting them from blob.slate.pledge id
                if (!sendToDTC || !sendToApex)
                {
                    internalid = -1;
                    UniqueIdForEID = CurrTime + ctr.ToString();
                    ctr++;
                    UserReferenceNumber = 199390;       //Current implementation allows only 6 digit number or will crash
                }

                int ReturnedDTCID = 0;

                if (sendToDTC)
                {
                    ReturnedDTCID = PledgeStockToOCC(UserReferenceNumber, cusip, (int)qty, clearingNo);
                }

                int ReturnedG1ID = 0;

                string legalEntity;
                string custodianAccountShortCode;

                switch (clearingNo)
                {
                    case "0269": legalEntity = "MS0269";
                        break;
                    case "5239": legalEntity = "MS5239";
                        break;
                    case "5289": legalEntity = "MS5289";
                        break;
                    default: legalEntity = "NA";
                        break;
                }

                custodianAccountShortCode = "DTC" + clearingNo;
                string ctpyExternalId2 = "OCC-" + legalEntity;

                if (sendToApex)
                {
                    //ExternalId2 has to be unique to Apex
                    string EID = "plg_" + UniqueIdForEID + "_" + DateTime.Today.ToString("yyyyMMdd");

                    ReturnedG1ID =
                    tc.CreateTrade(source, internalid, legalEntity,
                        EID, "CP", "GC", "OCC", ctpyExternalId2, cusip,
                        DateTime.Today, null, qty, 0, 0, .0000001, Environment.UserName, DateTime.Today, 100, 0,
                        null, custodianAccountShortCode, null, "OCC", "USD", true, true);
                }

                if (sendToApex && sendToDTC)
                {
                    p.G1Id = ReturnedG1ID;
                    p.DtcId = ReturnedDTCID;
                    Pfac.Update(p);
                }


            }

            return Error;
        }

        private void PriceTicker(PledgeObject p)
        {
            StockObject FoundStock = StockList.FirstOrDefault(x => x.Cusip.ToUpper() == p.Cusip.ToUpper());
            if (FoundStock != null)
            {
                p.Ticker = FoundStock.Ticker;
                p.LoanValue = p.Quantity * (double)FoundStock.Price;
            }
        }

        private int PledgeStockToOCC(int PledgeIdFromLocalTable, string CUSIP, int SharesToPledge, string clearingNo)
        {
            OutgoingPledgeOrderObject OPO = CreateBasePledge(PledgeIdFromLocalTable, "21", CUSIP, SharesToPledge, clearingNo);

            OPO.PledgePurpose = "1";
            OPO.HypothecationCode = "1";

            PledgeFac.Insert(OPO);

            return OPO.Id;
        }

        private OutgoingPledgeOrderObject CreateBasePledge(int PledgeIdFromLocalTable, string RecordType, string CUSIP, int SharesToUnpledge, string clearingNo)
        {
            OutgoingPledgeOrderObject OPO = new OutgoingPledgeOrderObject();

            OPO.DateEntered = DateTime.Now;
            OPO.EnteredBy = Environment.UserName;
            OPO.FeedbackIndicator = "";
            OPO.ProductionIndicator = "P";
            OPO.RecType = "COLC02";
            OPO.RecordSuffix = "01";
            OPO.VersionNumber = "01";
            OPO.UserReferenceNumber = PledgeIdFromLocalTable.ToString().Substring(0, 6);    //Reason we do this is because the table only allows up to 6 characters
            OPO.Adressee = "G0000346";
            OPO.RecordType = RecordType;
            OPO.Pledgor = clearingNo.PadLeft(8, '0');
            OPO.Pledgee = "00000981";
            OPO.Cusip = CUSIP;
            OPO.ShareQuantity = SharesToUnpledge.ToString().PadLeft(9, '0');
            OPO.LoanDate = "19730320";
            OPO.LoanValueWhole = "";
            OPO.LoanValueDecimal = "";
            OPO.PledgePurpose = "";
            OPO.ReleaseType = "";
            OPO.HypothecationCode = "";
            OPO.PreventPendIndicator = "";
            OPO.CnsIndicator = "";
            OPO.IpoIndicator = "";
            OPO.PtaIndicator = "";
            OPO.FederalReservePurpose = "";
            OPO.OccParticipantNumber = "";
            OPO.BankAbaNumber = "";
            OPO.OccNumber = "";
            OPO.OccClearingGroupId = "";
            OPO.OccClearingMemberNumber = clearingNo.PadLeft(5, '0');
            OPO.OccAccountType = "F";
            OPO.OccAccountId = "";
            OPO.OccCollateralType = "VS";
            OPO.OccOptionSymbol = "";
            OPO.OccExpirationYear = "";
            OPO.OccExpirationMonth = "";
            OPO.OccExpirationDay = "";
            OPO.OccOptionType = "";
            OPO.OccOptionStrike = "";
            OPO.OccOptionStrikeDecimal = "";
            OPO.Blank = "";
            OPO.CrossReferenceNumber = PledgeIdFromLocalTable.ToString();
            OPO.CustomerAccountNumber = "";
            OPO.OccFiller = "";
            OPO.DtccFiller = "";

            return OPO;
        }

        private PledgeObject PopulateSlatePledgeObject(string source, string cusip, string ClearingNo, int qty, string pledgeType, bool sendToG1, bool sendToDTC)
        {
            PledgeObject p = new PledgeObject();
            p.PledgeType = pledgeType;
            p.Source = source;
            p.DateEntered = DateTime.Now;
            p.EnteredBy = Environment.UserName;
            p.Processed = false;
            p.SendToG1 = sendToG1;
            p.SendToDtc = sendToDTC;
            p.ClearingNo = ClearingNo;
            p.Cusip = cusip;
            //p.Ticker = Ticker ?? "";
            p.Quantity = qty;
            //p.LoanValue = LoanValue;
            p.Pledgor = ClearingNo.TrimStart("0".ToCharArray());
            p.Pledgee = "981";
            p.DtcStatusCode = "";

            return p;
        }

        
    }

    public class PledgeStatus 
    {
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
