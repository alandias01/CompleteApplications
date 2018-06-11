using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Maple.Utilities;
using Maple.Utilities.DgvHelper;
using System.Threading;
using Maple.Dtc.Views;


namespace Maple.Dtc.PositionClient
{
    public partial class PriceControlForm : Form, IDgvForm
    {
        private enum MisPriceStatusEnum
        {
            MapleOverPaidCounterParty,
            MapleUnderPaidCounterparty,
            CounterPartyOverPaidMaple,
            CounterPartyUnderPaidMaple,
            Unknown
        }

        private class PriceControlDeliveryOrder : IncomingDeliveryOrderObject
        {
            private double _price;
            private string _notes;

            public string SortField
            {
                get
                {
                    if (MisPriceStatus == MisPriceStatusEnum.CounterPartyUnderPaidMaple || MisPriceStatus == MisPriceStatusEnum.MapleOverPaidCounterParty)
                    {
                        return "Z" + Math.Abs(MisPriceValue).ToString("0000000000");
                    }
                    else
                    {
                        return "A" + Math.Abs(MisPriceValue).ToString("0000000000");
                    }
                }
            }

            public string Notes
            {
                get { return _notes; }
                set { _notes = value; }
            }


            public double DtcPrice
            {
                get
                {
                    if (base.ShareQuantity != 0)
                    {
                        return (double)(base.MoneyValue / base.ShareQuantity);
                    }
                    else
                    {
                        return 0;
                    }

                }

            }

            public double MaxPrice
            {
                get
                {
                    return Math.Ceiling(Price * Properties.Settings.Default.PriceControlMax);
                }
            }

            public double MinPrice
            {
                get
                {
                    return Math.Ceiling(Price * Properties.Settings.Default.PriceControlMin);
                }
            }

            public double CollateralExposure
            {
                get
                {
                    /*
                      On a stock borrow if we send > bbg * 1.05 

                        on a stock loan if we receive < bbg
                    */

                    double c = 0;

                    if (MessageSide == DtcMessageSideEnum.Receiver)
                    {
                        c = DtcPrice - Price;
                    }
                    else if (MessageSide == DtcMessageSideEnum.Deliverer)
                    {
                        c = (Price * 1.05) - DtcPrice;
                    }

                    if (c < 0)
                    {
                        c = 0;
                    }

                    return c * ShareQuantity.Value;
                }
            }

            //Market price
            public double Price
            {
                get { return _price; }
                set { _price = value; }
            }

            public double MisPrice
            {
                get
                {
                    return DtcPrice > MaxPrice ? DtcPrice - MaxPrice : DtcPrice - MinPrice;
                }

            }

            public double MisPriceValue
            {
                get
                {
                    return MisPrice * ShareQuantity.Value;
                }
            }

            public MisPriceStatusEnum MisPriceStatus
            {
                get
                {
                    MisPriceStatusEnum m = MisPriceStatusEnum.Unknown;

                    if (DelivererReceiverIndicator == "R")
                    {
                        if (MisPrice > 0)
                        {
                            m = MisPriceStatusEnum.MapleOverPaidCounterParty;
                        }
                        else
                        {
                            m = MisPriceStatusEnum.MapleUnderPaidCounterparty;
                        }
                    }
                    else if (DelivererReceiverIndicator == "D")
                    {
                        if (MisPrice > 0)
                        {
                            m = MisPriceStatusEnum.CounterPartyOverPaidMaple;
                        }
                        else
                        {
                            m = MisPriceStatusEnum.CounterPartyUnderPaidMaple;
                        }
                    }

                    return m;
                }
            }

            public double PriceDiff { get; set; }

            public PriceControlDeliveryOrder(IncomingDeliveryOrderObject i)
            {
                base.DateReceived = i.DateReceived;
                base.ProcessDate = i.ProcessDate;
                base.RawMessageId = i.RawMessageId;
                base.QueueName = i.QueueName;
                base.OriginCode = i.OriginCode;
                base.Deliverer = i.Deliverer;
                base.CopyIndicator = i.CopyIndicator;
                base.VersionControl = i.VersionControl;
                base.Cusip = i.Cusip;
                base.CmoFactor = i.CmoFactor;
                base.ReasonForPendingIndicator = i.ReasonForPendingIndicator;
                base.MdhTime_ = i.MdhTime_;
                base.MdhTime = i.MdhTime;
                base.AccountType = i.AccountType;
                base.ActionCode = i.ActionCode;
                base.ActivityCode = i.ActivityCode;
                base.Receiver = i.Receiver;
                base.PendingTranIndicator = i.PendingTranIndicator;
                base.ForeignSharesIndicator = i.ForeignSharesIndicator;
                base.OriginalInputSource = i.OriginalInputSource;
                base.MaturityDate = i.MaturityDate;
                base.SubActivityCode = i.SubActivityCode;
                base.MoneyValue_ = i.MoneyValue_;
                base.MoneyValue = i.MoneyValue;
                base.DelivererReceiverIndicator = i.DelivererReceiverIndicator;
                base.RadIndicator = i.RadIndicator;
                base.JournalCode = i.JournalCode;
                base.BatchVarCon = i.BatchVarCon;
                base.DelivererAccountNum = i.DelivererAccountNum;
                base.ReceiverAccountNum = i.ReceiverAccountNum;
                base.SubordinateBankNum = i.SubordinateBankNum;
                base.SettleDate_ = i.SettleDate_;
                base.SettleDate = i.SettleDate;
                base.ReasonCode = i.ReasonCode;
                base.ConditionalDoIdIndicator = i.ConditionalDoIdIndicator;
                base.ThirdPartyIdentifier = i.ThirdPartyIdentifier;
                base.DueBillIndicator = i.DueBillIndicator;
                base.DtcStatusIndicator = i.DtcStatusIndicator;
                base.DayNightIndicator = i.DayNightIndicator;
                base.DateStamp = i.DateStamp;
                base.TimeStamp = i.TimeStamp;
                base.DateAndTimeStamp = i.DateAndTimeStamp;
                base.CusipDescription = i.CusipDescription;
                base.Comments = i.Comments;
                base.MuniBond = i.MuniBond;
                base.FastIndicator = i.FastIndicator;
                base.SdfsIndicator = i.SdfsIndicator;
                base.TransactionSequenceNum = i.TransactionSequenceNum;
                base.CurrentRecordNum = i.CurrentRecordNum;
                base.RependedIndicator = i.RependedIndicator;
                base.ShareQuantity_ = i.ShareQuantity_;
                base.ShareQuantity = i.ShareQuantity;
                base.PendDropReason = i.PendDropReason;
                base.DropCode = i.DropCode;
                base.SubIssueType = i.SubIssueType;
                base.CnsSubAccount = i.CnsSubAccount;
                base.PendingPosition_ = i.PendingPosition_;
                base.PendingPosition = i.PendingPosition;
                base.DatedDate = i.DatedDate;
                base.IpoCustomerAccount = i.IpoCustomerAccount;
                base.IpoBank = i.IpoBank;
                base.IpoTradeDate = i.IpoTradeDate;
                base.IpoBroker = i.IpoBroker;
                base.IpoCorrespondent = i.IpoCorrespondent;
                base.IpoBuySell = i.IpoBuySell;
                base.Filler1 = i.Filler1;
                base.DtcOriginalRecordNum = i.DtcOriginalRecordNum;
                base.ReclaimDate = i.ReclaimDate;
                base.DepositoryTransNum = i.DepositoryTransNum;
                base.Filler2 = i.Filler2;

                base.SetFields();
            }
        }

        private RealTimePositionCalculator calc;

        private SortableBindingList<PriceControlDeliveryOrder> ProblemOrders = new SortableBindingList<PriceControlDeliveryOrder>();
        private SortableBindingList<PriceControlDeliveryOrder> DifferentPricedOrders = new SortableBindingList<PriceControlDeliveryOrder>();

        private IncomingDeliveryOrderFactory dof = new IncomingDeliveryOrderFactory();

        private DgvHelpers helper = new DgvHelpers();

        private Color selectedColor = Color.Azure;
        private string selectedCusip = "";
        private Dictionary<string, string> Notes = new Dictionary<string, string>();

        private Dictionary<string, List<PriceControlDeliveryOrder>> Orders = new Dictionary<string, List<PriceControlDeliveryOrder>>();

        IncomingDtcMessageEventHandler handleDtcMessageReceived;
        CusipSelectEventHandler handleCusipSelected;
        StockInfoUpdateEventHandler handleStockInfoUpdated;
        StockPriceUpdateEventHandler handleStockPriceUpdated;
        
        public PriceControlForm(RealTimePositionCalculator c)
        {
            InitializeComponent();

            calc = c;

            dgvControl.DataSource = ProblemOrders;
            dgvDifferentPrices.DataSource = DifferentPricedOrders;

            helper.Add(new DgvHelper(dgvControl, this.Name, true));
            helper.Add(new DgvHelper(dgvDifferentPrices, this.Name, true));
            helper.handleCellSelected += new CellSelectEventHandler(h_handleCellSelected);

            handleDtcMessageReceived = new IncomingDtcMessageEventHandler(DtcMessageReceived);
            handleCusipSelected = new CusipSelectEventHandler(Instance_handleCusipSelected);
            handleStockInfoUpdated = new StockInfoUpdateEventHandler(Instance_handleStockInfoUpdated);
            handleStockPriceUpdated = new StockPriceUpdateEventHandler(Instance_handleStockPriceUpdated);

            DtcChannel.Instance.handleDtcMessageReceived += handleDtcMessageReceived;
            DtcChannel.Instance.handleCusipSelected += handleCusipSelected;
            DtcChannel.Instance.handleStockInfoUpdated += handleStockInfoUpdated;
            DtcChannel.Instance.handleStockPriceUpdated += handleStockPriceUpdated;
        }

        private void PriceControlForm_Load(object sender, EventArgs e)
        {
            try
            {
                //load the data from the table first                
                foreach (IncomingDeliveryOrderObject d in calc.AllDtcActivity)
                {
                    ProcessOrders(d);
                }

                RemoveNonCashItemsFromProblemOrders(ProblemOrders); //ALANDIAS 

                ColorRows();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
            }

            foreach (DataGridViewColumn c in dgvControl.Columns)
            {
                c.ReadOnly = true;
            }

            foreach (DataGridViewColumn c in dgvDifferentPrices.Columns)
            {
                c.ReadOnly = true;
            }

            dgvControl.Columns["Notes"].ReadOnly = false;
            dgvDifferentPrices.Columns["Notes"].ReadOnly = false;

            ProblemOrders.Sort("SortField", ListSortDirection.Descending);

            //Use this for testing
            /*
            IncomingDeliveryOrderObject o = new IncomingDeliveryOrderObject();

            o.Id = 147293;
            o.DateAndTimeStamp = DateTime.Now;
            o.Cusip = "88023U101";

            o.DtcStatusIndicator = " ";

            o.Receiver = "00005239";
            o.Deliverer = "00000773";
            o.DelivererReceiverIndicator = "R";
            o.ActionCode = "1";
            o.ActivityCode = "027";
            o.ReasonCode = "020";
            o.MoneyValue = 2000;
            o.ShareQuantity = 100;
            ProcessOrders(o);

            o.Receiver = "00000774";
            o.Deliverer = "00005239";
            o.DelivererReceiverIndicator = "D";
            ProcessOrders(o);

            o.Receiver = "00000775";
            o.Cusip = "759351109";
            o.MoneyValue = 200;
            o.ShareQuantity = 100;
            ProcessOrders(o);

            o.Receiver = "00005239";
            o.Deliverer = "00000776";
            o.DelivererReceiverIndicator = "R";
            ProcessOrders(o);

            o.Cusip = "004329108";
            o.MoneyValue = 200;
            o.ShareQuantity = 100;
            o.Receiver = "00005239";
            o.Deliverer = "00000777";
            o.DelivererReceiverIndicator = "R";
            ProcessOrders(o);
            */
        }

        #region IDtcFeedCallback Members

        void Instance_handleCusipSelected(object sender, CusipSelectEventArgs e)
        {

        }

        void DtcMessageReceived(object sender, IncomingDtcMessageEventArgs e)
        {
            IncomingDeliveryOrderObject d;
            if (e.DtcMessage is IncomingDeliveryOrderObject)
            {
                d = (IncomingDeliveryOrderObject)e.DtcMessage;
            }
            else
            {
                return;
            }

            new Thread((ThreadStart)delegate()
            {
                this.BeginInvoke((ThreadStart)delegate()
                {
                    try
                    {
                        ProcessOrders(d);

                        RemoveNonCashItemsFromProblemOrders(ProblemOrders); //ALANDIAS

                        ProblemOrders.Sort("SortField", ListSortDirection.Descending);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex, TraceEnum.LoggedError);
                    }
                });
            }).Start();
        }

        void Instance_handleStockPriceUpdated(object sender, StockPriceUpdateEventArgs e)
        {
            try
            {
                new Thread((ThreadStart)delegate()
                {
                    this.BeginInvoke((ThreadStart)delegate()
                    {
                        try
                        {
                            /*
                            //find the stock price in the list
                            int i = AllRealTimePositions.Find("Cusip", e.StockPrice.Cusip);

                            if (i != -1)
                            {
                                //update the stock
                                AllRealTimePositions[i].Price = e.StockPrice.Price;
                            }
                            */
                            dgvControl.Refresh();
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine(ex, TraceEnum.LoggedError);
                        }
                    });
                }).Start();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
            }
        }

        void Instance_handleStockInfoUpdated(object sender, StockInfoUpdateEventArgs e)
        {
            try
            {
                new Thread((ThreadStart)delegate()
                {
                    this.BeginInvoke((ThreadStart)delegate()
                    {
                        try
                        {
                            /*
                            int i = AllRealTimePositions.Find("Cusip", e.StockInfo.Cusip);

                            if (i != -1)
                            {

                                //update the stock
                                AllRealTimePositions[i].Ticker = e.StockInfo.Ticker;
                                AllRealTimePositions[i].Company = e.StockInfo.Name;
                            }
                            */
                            dgvControl.Refresh();
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine(ex, TraceEnum.LoggedError);
                        }
                    });
                }).Start();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
            }
        }


        #endregion

        private void dgvRealTimePosition_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    /*
                    RealTimePositionObject d = (RealTimePositionObject)dgvControl.Rows[e.RowIndex].DataBoundItem;
                    DtcChannel.Instance.CusipSelected(d.Cusip);

                    selectedCusip = d.Cusip;
                    */
                    //color rows
                    ColorRows();
                }

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ColorRows()
        {
            try
            {
                foreach (DataGridViewRow row in dgvControl.Rows)
                {
                    /*
                    RealTimePositionObject r = (RealTimePositionObject)row.DataBoundItem;

                    if (r.Cusip == selectedCusip)
                    {

                        row.DefaultCellStyle.BackColor = selectedColor;
                    }

                    else
                    {

                    }
                    */
                }
                dgvControl.Refresh();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
            }
        }

        private void ProcessOrders(IncomingDeliveryOrderObject d)
        {
            try
            {
                /*if (d.Cusip == "493267108")
                {
                    int c = 2;
                }*/


                this.Text = "Price Control - " + DateTime.Now.ToString();

                PriceControlDeliveryOrder d2 = new PriceControlDeliveryOrder(d);

                if (calc.StockPrices.ContainsKey(d2.Cusip))
                {
                    StockPriceViewObject s = null;
                    calc.StockPrices.TryGetValue(d2.Cusip, out s);

                    if (s != null)
                    {
                        d2.Price = (double)s.Price;
                    }
                }

                double priceDiff = Math.Abs(d.MoneyValue.Value - (d2.Price * d.ShareQuantity.Value));
                                
                if ((d.DtcStatusIndicator == " " || d.DtcStatusIndicator == "X") &&
                     (d.OriginalInputSource != "CNS") &&
                     (d2.Price > 0) &&
                     ((d.MoneyValue / d.ShareQuantity) > d2.MaxPrice || (d.MoneyValue / d.ShareQuantity) < d2.MinPrice) &&
                     (d.DeliveryOrderDirection == DeliveryOrderDirectionEnum.Borrow || d.DeliveryOrderDirection == DeliveryOrderDirectionEnum.Loan ||
                      d.DeliveryOrderDirection == DeliveryOrderDirectionEnum.BorrowReclaimed || d.DeliveryOrderDirection == DeliveryOrderDirectionEnum.LoanReclaimed) &&
                     (d.Receiver == Settings.Account.Padded() || d.Deliverer == Settings.Account.Padded())
                   )
                {

                    d2.PriceDiff = priceDiff;
                    ProblemOrders.Add(d2);
                }

                if ((d.DtcStatusIndicator == " " || d.DtcStatusIndicator == "X") &&
                     (d.OriginalInputSource != "CNS") &&
                     (d2.Price > 0) &&
                     (d.DeliveryOrderDirection == DeliveryOrderDirectionEnum.Borrow || d.DeliveryOrderDirection == DeliveryOrderDirectionEnum.Loan ||
                      d.DeliveryOrderDirection == DeliveryOrderDirectionEnum.BorrowReclaimed || d.DeliveryOrderDirection == DeliveryOrderDirectionEnum.LoanReclaimed) &&
                     (d.Receiver == Settings.Account.Padded() || d.Deliverer == Settings.Account.Padded())
                   )
                {
                    if (!Orders.ContainsKey(d2.Cusip))
                    {
                        List<PriceControlDeliveryOrder> p = new List<PriceControlDeliveryOrder>();
                        p.Add(d2);
                        Orders.Add(d2.Cusip, p);
                    }
                    else
                    {
                        //check to see if an order for a different price had come in
                        List<PriceControlDeliveryOrder> p = Orders[d2.Cusip];

                        int i = p.Count(x =>
                            (x.MoneyValue / x.ShareQuantity) != (d2.MoneyValue / d2.ShareQuantity) &&
                            (x.Deliverer == d2.Deliverer || x.Deliverer == d2.Receiver));

                        if (i != 0)
                        {
                            DifferentPricedOrders.Add(d2);
                        }

                        //add to list of orders
                        Orders[d2.Cusip].Add(d2);
                    }
                }

                
            }
            catch (Exception)
            {
                throw;
            }
        }


        //ALANDIAS Run This everytime ProcessOrders() runs, not inside the method since this method runs through a loop
        private void RemoveNonCashItemsFromProblemOrders(SortableBindingList<PriceControlDeliveryOrder> POList)
        {
            List<NonCashItemsFromCurrentPositionObject> NCIList = new List<NonCashItemsFromCurrentPositionObject>();
            NonCashItemsFromCurrentPositionDa NCIDA = new NonCashItemsFromCurrentPositionDa();
            NCIDA.Load(NCIList);

            NCIList.ForEach(x => { if (x.Ctpydtcnumber != null) { x.Ctpydtcnumber = x.Ctpydtcnumber.PadLeft(4, '0'); } });

            List<PriceControlDeliveryOrder> ProblemOrdersItemsToRemove = new List<PriceControlDeliveryOrder>();
            foreach (var item in NCIList)
            {

                PriceControlDeliveryOrder ItemToRemove = POList.FirstOrDefault(x => x.ClearingNumber == item.ClearingNo && x.CounterParty == item.Ctpydtcnumber && x.Cusip == item.cusip && x.ShareQuantity == item.quantity);
                
                if (ItemToRemove != null)
                {
                    ProblemOrdersItemsToRemove.Add(ItemToRemove);
                }                
            }

            //Allow in production
            ProblemOrdersItemsToRemove.ForEach(x => POList.Remove(x));
        }


        #region IDgvForm Members

        public event CellSelectEventHandler handleCellSelected;

        protected virtual void OnCellSelected(CellSelectEventArgs e)
        {
            if (handleCellSelected != null)
            {
                handleCellSelected(this, e);
            }
        }

        void h_handleCellSelected(object sender, CellSelectEventArgs e)
        {
            Console.WriteLine(e.CellValue);
            OnCellSelected(e);
        }

        #endregion

        private void PriceControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DtcChannel.Instance.handleDtcMessageReceived -= handleDtcMessageReceived;
            DtcChannel.Instance.handleCusipSelected -= handleCusipSelected;
            DtcChannel.Instance.handleStockInfoUpdated -= handleStockInfoUpdated;
            DtcChannel.Instance.handleStockPriceUpdated -= handleStockPriceUpdated;


            /*
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
            */
        }

        private void dgvControl_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    PriceControlDeliveryOrder d = (PriceControlDeliveryOrder)dgvControl.Rows[e.RowIndex].DataBoundItem;
                    DtcChannel.Instance.CusipSelected(d.Cusip);
                }

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void dgvDifferentPrices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    PriceControlDeliveryOrder d = (PriceControlDeliveryOrder)dgvDifferentPrices.Rows[e.RowIndex].DataBoundItem;
                    DtcChannel.Instance.CusipSelected(d.Cusip);
                }

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void PriceControlForm_Shown(object sender, EventArgs e)
        {
            helper.SetAllColumns();
        }

    }
}


