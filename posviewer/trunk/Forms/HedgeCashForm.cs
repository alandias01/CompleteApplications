using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Maple.Dtc;
using Maple.Dtc.Views;
using Maple.Nscc.NsccBizObjects;
using Maple.Occ.OccBizObjects;
using Maple.Occ.OccBizObjects.Views;
using Maple.Utilities;
using Maple.Utilities.DgvHelper;
using Outlook = Microsoft.Office.Interop.Outlook;
using Maple.Slate.BizObjects.Views;

namespace Maple.Dtc.PositionClient
{
    public partial class HedgeCashForm : Form, IDgvForm
    {

        private class ExpectedHedgeObject
        {

            private string _cusip;
            private string _symbol;
            private string _clearingNo;
            private int _borrowQty;
            private int _loanQty;
            private double _borrowValue;
            private double _loanValue;

            
            private int _expectedBorrowQty;
            private int _expectedLoanQty;
            private double _expectedBorrowValue;
            private double _expectedLoanValue;


            public ExpectedHedgeObject(string cusip, string symbol, string clearingNo, int borrowQty, int loanQty, double borrowValue, double loanValue)
            {
                Cusip = cusip;
                Symbol = symbol;
                ClearingNo = clearingNo;
                BorrowQty = borrowQty;
                LoanQty = loanQty;
                BorrowValue = borrowValue;
                LoanValue = loanValue;
            }

            public string Cusip
            {
                get { return _cusip; }
                set { _cusip = value; }
            }

            public string Symbol
            {
                get { return _symbol; }
                set { _symbol = value; }
            }

            public string ClearingNo
            {
                get { return _clearingNo; }
                set { _clearingNo = value; }
            }

            public int BorrowQty
            {
                get { return _borrowQty; }
                set { _borrowQty = value; }
            }

            public int LoanQty
            {
                get { return _loanQty; }
                set { _loanQty = value; }
            }

            public double BorrowValue
            {
                get { return _borrowValue; }
                set { _borrowValue = value; }
            }

            public double LoanValue
            {
                get { return _loanValue; }
                set { _loanValue = value; }
            }



            public int ExpectedBorrowQty
            {
                get { return _expectedBorrowQty; }
                set { _expectedBorrowQty = value; }
            }

            public int ExpectedLoanQty
            {
                get { return _expectedLoanQty; }
                set { _expectedLoanQty = value; }
            }

            public double ExpectedBorrowValue
            {
                get { return _expectedBorrowValue; }
                set { _expectedBorrowValue = value; }
            }

            public double ExpectedLoanValue
            {
                get { return _expectedLoanValue; }
                set { _expectedLoanValue = value; }
            }
        }

        private class PositionSummaryObject
        {
            private string _cusip;
            private string _symbol;
            private string _clearingNo;
            private int _borrowQty;
            private int _loanQty;
            private double _borrowValue;
            private double _loanValue;


            private int _startingBorrowQty;
            private int _startingLoanQty;
            private double _startingBorrowValue;
            private double _startingLoanValue;


            
            private int _expectedBorrowQty;
            private int _expectedLoanQty;
            private double _expectedBorrowValue;
            private double _expectedLoanValue;


            public int StartingBorrowQty
            {
                get { return _startingBorrowQty; }
                set { _startingBorrowQty = value; }
            }

            public int StartingLoanQty
            {
                get { return _startingLoanQty; }
                set { _startingLoanQty = value; }
            }

            public double StartingBorrowValue
            {
                get { return _startingBorrowValue; }
                set { _startingBorrowValue = value; }
            }

            public double StartingLoanValue
            {
                get { return _startingLoanValue; }
                set { _startingLoanValue = value; }
            }

            public string Cusip
            {
                get { return _cusip; }
                set { _cusip = value; }
            }

            public string Symbol
            {
                get { return _symbol; }
                set { _symbol = value; }
            }

            public string ClearingNo
            {
                get { return _clearingNo; }
                set { _clearingNo = value; }
            }

            public int BorrowQty
            {
                get { return _borrowQty; }
                set { _borrowQty = value; }
            }

            public int LoanQty
            {
                get { return _loanQty; }
                set { _loanQty = value; }
            }

            public double BorrowValue
            {
                get { return _borrowValue; }
                set { _borrowValue = value; }
            }

            public double LoanValue
            {
                get { return _loanValue; }
                set { _loanValue = value; }
            }


            public int QuantityOver
            {
                get { return (StartingBorrowQty + BorrowQty) - (StartingLoanQty + LoanQty); }

            }

            public double MoneyDifference
            {
                get { return (StartingBorrowValue + BorrowValue) - (StartingLoanValue + LoanValue); }
            }

            public double ExpectedMoneyDifference
            {
                get { return (StartingBorrowValue + ExpectedBorrowValue) - (StartingLoanValue + ExpectedLoanValue); }
            }

            public double StartingMoneyDifference
            {
                get { return (StartingBorrowValue) - (StartingLoanValue); }
            }

            public string Status
            {
                get
                {
                    if (QuantityOver > 0)
                    {
                        return "OverBorrow";
                    }
                    if (QuantityOver < 0)
                    {
                        return "OverLoan";
                    }
                    else
                    {
                        return "Flat";
                    }
                }
            }

            public string SortField
            {
                get { return Math.Abs(MoneyDifference).ToString("000000000000000"); }
            }



            public int ExpectedBorrowQty
            {
                get { return _expectedBorrowQty; }
                set { _expectedBorrowQty = value; }
            }

            public int ExpectedLoanQty
            {
                get { return _expectedLoanQty; }
                set { _expectedLoanQty = value; }
            }

            public double ExpectedBorrowValue
            {
                get { return _expectedBorrowValue; }
                set { _expectedBorrowValue = value; }
            }

            public double ExpectedLoanValue
            {
                get { return _expectedLoanValue; }
                set { _expectedLoanValue = value; }
            }



            public PositionSummaryObject(HedgePositionSummaryViewObject p)
            {
                Cusip = p.Cusip;
                Symbol = p.Symbol;
                ClearingNo = p.ClearingNo;

                if (p.BorrowLoan == "B")
                {
                    StartingBorrowQty = p.Quantity.Value;
                    StartingBorrowValue = p.ContractValue.Value;
                }

                if (p.BorrowLoan == "L")
                {
                    StartingLoanQty = p.Quantity.Value;
                    StartingLoanValue = p.ContractValue.Value;
                }
                LoanQty = 0;
                BorrowQty = 0;
            }

            public PositionSummaryObject(IncomingDeliveryOrderObject d)
            {
                /*
                Cusip = p.Cusip;
                Symbol = p.Symbol;
                ClearingNo = p.ClearingNo;

                if (p.BorrowLoan == "B")
                {
                    StartingBorrowQty = p.Quantity.Value;
                    StartingBorrowValue = p.ContractValue.Value;
                }

                if (p.BorrowLoan == "L")
                {
                    StartingLoanQty = p.Quantity.Value;
                    StartingLoanValue = p.ContractValue.Value;
                }
                LoanQty = 0;
                BorrowQty = 0;
                */
            }


        }


        private IncomingDeliveryOrderFactory dof = new IncomingDeliveryOrderFactory();

        private DgvHelpers helper = new DgvHelpers();

        private RealTimePositionCalculator calc;
        private Color selectedColor = Color.Azure;
        private string selectedCusip = "";

        private HedgeCollateralViewFactory hcvf = new HedgeCollateralViewFactory();
        private HedgeMarkTotalViewFactory hmvf = new HedgeMarkTotalViewFactory();

        private List<HedgeCollateralViewObject> collateral = new List<HedgeCollateralViewObject>();
        private List<HedgeMarkTotalViewObject> marks = new List<HedgeMarkTotalViewObject>();

        private SortableBindingList<PositionSummaryObject> positions5239 = new SortableBindingList<PositionSummaryObject>();
        private SortableBindingList<PositionSummaryObject> filtered5239 = new SortableBindingList<PositionSummaryObject>();
        private SortableBindingList<PositionSummaryObject> positions269 = new SortableBindingList<PositionSummaryObject>();
        private SortableBindingList<PositionSummaryObject> filtered269 = new SortableBindingList<PositionSummaryObject>();
        private SortableBindingList<PositionSummaryObject> expected = new SortableBindingList<PositionSummaryObject>();
        private double hedgeMarkTotal = 0;


        private HedgeTradeViewFactory htvf = new HedgeTradeViewFactory();
        private List<HedgeTradeViewObject> hedgeTrades = new List<HedgeTradeViewObject>();


        public HedgeCashForm(RealTimePositionCalculator c)
        {
            InitializeComponent();

            calc = c;

            helper.handleCellSelected += new CellSelectEventHandler(h_handleCellSelected);

            DtcChannel.Instance.handleDtcMessageReceived += new IncomingDtcMessageEventHandler(Instance_handleDtcMessageReceived);
        }

        void Instance_handleDtcMessageReceived(object sender, IncomingDtcMessageEventArgs e)
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
                        CalcSummary(d, "00005239", positions5239);
                        CalcSummary(d, "00000269", positions269);
                                         
                        UpdateFilter(positions5239, filtered5239);
                        UpdateFilter(positions269, filtered269);
                        CalculateExpected();
                        CalcSpread();       

                        dgv5239.Refresh();
                        dgv269.Refresh();
                        txtRealTimeSpread.Refresh();
                        //filtered5239.Sort("SortField", ListSortDirection.Descending);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex, TraceEnum.LoggedError);
                    }
                });
            }).Start();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            dgv5239.DataSource = filtered5239;
            dgv269.DataSource = positions269;
            dgvExpected.DataSource = expected;

            helper.Add(new DgvHelper(dgv5239, this.Name, true));
            helper.Add(new DgvHelper(dgv269, this.Name, true));
            helper.Add(new DgvHelper(dgvExpected, this.Name, true));

            cbxFilter.Items.Add("All");
            cbxFilter.Items.Add("OverBorrows");
            cbxFilter.Items.Add("OverLoans");

            cbxFilter.SelectedIndex = 0;

            try
            {
                txtBorrowPosition269.Text = "$0";
                txtBorrowPosition5239.Text = "$0";
                txtDifference.Text = "$0";
                txtHedgeMark.Text = "$0";
                txtHedgeSpread.Text = "$0";
                txtLoanPosition269.Text = "$0";
                txtLoanPosition5239.Text = "$0";
                txtSpread269.Text = "$0";
                txtSpread5239.Text = "$0";

                //calc mark
                HedgeMarkTotalViewParam hmp = new HedgeMarkTotalViewParam();
                hmp.ActivityDate_Date.AddParamValue(Utils.GetNthBusinessDay(DateTime.Today, -1));

                hmvf.Load(marks, hmp);

                double markTotal = 0;

                marks.ForEach(m => markTotal += m.Total.Value);

                //load collateral values
                HedgeCollateralViewParam hcp = new HedgeCollateralViewParam();
                hcp.ActivityDate_Date.AddParamValue(Utils.GetNthBusinessDay(DateTime.Today, -1));

                hcvf.Load(collateral, hcp);

                double borrowPosition5239 = 0;
                double borrowPosition269 = 0;
                double loanPosition5239 = 0;
                double loanPosition269 = 0;

                foreach (HedgeCollateralViewObject h in collateral)
                {
                    if (h.BorrowLoan == "B")
                    {
                        if (h.ClearingNo == "05239")
                        {
                            borrowPosition5239 = h.total.Value;
                        }
                        if (h.ClearingNo == "00269")
                        {
                            borrowPosition269 = h.total.Value;
                        }
                    }
                    else if (h.BorrowLoan == "L")
                    {
                        if (h.ClearingNo == "05239")
                        {
                            loanPosition5239 = h.total.Value;
                        }
                        if (h.ClearingNo == "00269")
                        {
                            loanPosition269 = h.total.Value;
                        }
                    }
                }

                txtBorrowPosition5239.Text = borrowPosition5239.ToString("c0");
                txtBorrowPosition269.Text = borrowPosition269.ToString("c0");
                txtLoanPosition5239.Text = loanPosition5239.ToString("c0");
                txtLoanPosition269.Text = loanPosition269.ToString("c0");

                double spread269 = borrowPosition269 - loanPosition269;
                double spread5239 = borrowPosition5239 - loanPosition5239;
                double hedgeMark = Math.Abs(markTotal);
                double difference = (spread269 + spread5239);
                double hedgeSpread = difference - hedgeMark;

                txtSpread269.Text = spread269.ToString("c0");
                txtSpread5239.Text = spread5239.ToString("c0");
                txtHedgeMark.Text = hedgeMark.ToString("c0");

                txtDifference.Text = difference.ToString("c0");

                if (borrowPosition5239 > loanPosition5239)
                {
                    txtHedgeSpread.Text = hedgeSpread.ToString("c0") + "\r\n" + "OverBorrow";
                }
                else
                {
                    txtHedgeSpread.Text = hedgeSpread.ToString("c0") + "\r\n" + "OverLoan";
                }
                //calculate over borrows and loans
                HedgePositionSummaryViewFactory hpsf = new HedgePositionSummaryViewFactory();
                List<HedgePositionSummaryViewObject> hedgePos = new List<HedgePositionSummaryViewObject>();
                HedgePositionSummaryViewParam hpsp = new HedgePositionSummaryViewParam();
                List<PositionSummaryObject> temp = new List<PositionSummaryObject>();

                hpsp.ActivityDate.AddParamValue(Utils.GetNthBusinessDay(DateTime.Today, -1));

                hpsf.Load(hedgePos, hpsp);

                foreach (HedgePositionSummaryViewObject h in hedgePos)
                {
                    PositionSummaryObject p = temp.Find(t => t.Cusip == h.Cusip &&
                                                             t.ClearingNo == h.ClearingNo);

                    //does not exist
                    if (p == null)
                    {
                        PositionSummaryObject t = new PositionSummaryObject(h);
                        temp.Add(t);
                    }
                    else
                    {
                        if (h.BorrowLoan == "B")
                        {
                            p.StartingBorrowQty += h.Quantity.Value;
                            p.StartingBorrowValue += h.ContractValue.Value;
                        }

                        if (h.BorrowLoan == "L")
                        {
                            p.StartingLoanQty += h.Quantity.Value;
                            p.StartingLoanValue += h.ContractValue.Value;
                        }
                    }
                }

                foreach (PositionSummaryObject p in temp)
                {
                    if (p.ClearingNo == "05239")
                    {
                        positions5239.Add(p);
                    }
                    if (p.ClearingNo == "00269")
                    {
                        positions269.Add(p);
                    }
                }

                //now calculate what the current positions are
                foreach (IncomingDeliveryOrderObject ido in calc.AllDtcActivity)
                {
                    CalcSummary(ido, "00005239", positions5239);
                    CalcSummary(ido, "00000269", positions269);
                }
                
                UpdateFilter(positions5239, filtered5239);
                UpdateFilter(positions269, filtered269);

                CalculateExpected();
                CalcSpread();

                filtered5239.Sort("SortField", ListSortDirection.Descending);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
                MessageBox.Show("Error starting app: \r\n" + ex.ToString());
            }
        }

        private void CalculateExpected()
        {
            expected.Load(filtered5239);

            HedgeTradeViewParam htvp = new HedgeTradeViewParam();
            htvp.Book.AddParamValue("FMAI");            
            htvf.Load(hedgeTrades, htvp);

            foreach (PositionSummaryObject ps in expected)
            {
                HedgeTradeViewObject h = hedgeTrades.Find(h1 => h1.Cusip == ps.Cusip);

                if (h != null)
                {
                    if (h.BorrowLoan == "B")
                    {
                        ps.ExpectedBorrowQty = h.Quantity.Value;
                        ps.ExpectedBorrowValue = h.LoanValue.Value;
                    }

                    if (h.BorrowLoan == "L")
                    {
                        ps.ExpectedLoanQty = h.Quantity.Value;
                        ps.ExpectedLoanValue = h.LoanValue.Value;
                    }
                }
            }
        }


        private void UpdateFilter(SortableBindingList<PositionSummaryObject> pos, SortableBindingList<PositionSummaryObject> filt)
        {
            filt.Clear();
            if (cbxFilter.Text == "All")
            {
                foreach (PositionSummaryObject p in pos)
                {
                    filt.Add(p);
                }
            }
            else if (cbxFilter.Text == "OverBorrows")
            {
                foreach (PositionSummaryObject p in pos)
                {
                    if (p.Status == "OverBorrow")
                    {
                        filt.Add(p);
                    }

                }
            }
            else if (cbxFilter.Text == "OverLoans")
            {
                foreach (PositionSummaryObject p in pos)
                {
                    if (p.Status == "OverLoan")
                    {
                        filt.Add(p);
                    }
                }
            }

        }

        private void CalcSpread()
        {
            double spread = 0;
            double expectedSpread = 0;

            foreach (PositionSummaryObject p in positions5239)
            {
                spread += p.MoneyDifference;
                expectedSpread += p.ExpectedMoneyDifference;
            }

            if (spread > 0)
            {
                txtRealTimeSpread.Text = spread.ToString("c0") + "\r\nOverBorrow";                
            }
            else
            {
                txtRealTimeSpread.Text = spread.ToString("c0") + "\r\nOverLoan";                
            }

            if (expectedSpread > 0)
            {
                txtExpectedSpread.Text = expectedSpread.ToString("c0") + "\r\nOverBorrow";
            }
            else
            {
                txtExpectedSpread.Text = expectedSpread.ToString("c0") + "\r\nOverLoan";
            }
        }

        private void CalcSummary(IncomingDeliveryOrderObject ido, string account, SortableBindingList<PositionSummaryObject> pos)
        {
            //new loan
            if (ido.ReasonCode == "260")
            {
                //we borrowed stock
                if (ido.Receiver == account)
                {
                    //check to see if we have a position
                    int i = pos.Find("Cusip", ido.Cusip);

                    if (i != -1)
                    {
                        pos[i].BorrowQty += ido.ShareQuantity.Value;
                        pos[i].BorrowValue += ido.MoneyValue.Value;
                    }
                    //TODO: WE DONT HAVE A POSITION SO ADD IT
                    else
                    {
                        //                        PositionSummaryObject p = new PositionSummaryObject();
                        //                        p.Cu
                        //                        p.BorrowQty = ido.ShareQuantity.Value;

                        pos.Add(new PositionSummaryObject(ido));
                    }
                }
                //we delivered stock
                if (ido.Deliverer == account)
                {
                    //check to see if we have a position
                    int i = pos.Find("Cusip", ido.Cusip);

                    if (i != -1)
                    {
                        pos[i].LoanQty += ido.ShareQuantity.Value;
                        pos[i].LoanValue += ido.MoneyValue.Value;
                    }
                }
            }
            //loan reclaim
            if (ido.ReasonCode == "261")
            {
                if (ido.Receiver == account)
                {
                    //check to see if we have a position
                    int i = positions5239.Find("Cusip", ido.Cusip);

                    if (i != -1)
                    {
                        pos[i].LoanQty -= ido.ShareQuantity.Value;
                        pos[i].LoanValue -= ido.MoneyValue.Value;
                    }
                }
                //we delivered stock
                if (ido.Deliverer == account)
                {
                    //check to see if we have a position
                    int i = positions5239.Find("Cusip", ido.Cusip);

                    if (i != -1)
                    {
                        pos[i].BorrowQty -= ido.ShareQuantity.Value;
                        pos[i].BorrowValue -= ido.MoneyValue.Value;
                    }
                }

            }
            //new return
            if (ido.ReasonCode == "270")
            {
                if (ido.Receiver == account)
                {
                    //check to see if we have a position
                    int i = positions5239.Find("Cusip", ido.Cusip);

                    if (i != -1)
                    {
                        pos[i].LoanQty += ido.ShareQuantity.Value;
                        pos[i].LoanValue += ido.MoneyValue.Value;
                    }
                }
                //we delivered stock
                if (ido.Deliverer == account)
                {
                    //check to see if we have a position
                    int i = pos.Find("Cusip", ido.Cusip);

                    if (i != -1)
                    {
                        pos[i].BorrowQty += ido.ShareQuantity.Value;
                        pos[i].BorrowValue += ido.MoneyValue.Value;
                    }
                }

            }
            //new return dk
            if (ido.ReasonCode == "271")
            {
                //we borrowed stock
                if (ido.Receiver == account)
                {
                    //check to see if we have a position
                    int i = pos.Find("Cusip", ido.Cusip);

                    if (i != -1)
                    {
                        pos[i].BorrowQty -= ido.ShareQuantity.Value;
                        pos[i].BorrowValue -= ido.MoneyValue.Value;
                    }
                }
                //we delivered stock
                if (ido.Deliverer == account)
                {
                    //check to see if we have a position
                    int i = pos.Find("Cusip", ido.Cusip);

                    if (i != -1)
                    {
                        pos[i].LoanQty -= ido.ShareQuantity.Value;
                        pos[i].LoanValue -= ido.MoneyValue.Value;
                    }
                }
            }
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


        private void ViewPositionForm_Shown(object sender, EventArgs e)
        {
            helper.SetAllColumns();
        }

        private void ViewPositionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }


        private void dgv5239_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            PositionSummaryObject p = (PositionSummaryObject)dgv5239.CurrentRow.DataBoundItem;

            DtcChannel.Instance.CusipSelected(p.Cusip);
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

        private void cbxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFilter(positions5239, filtered5239);
            UpdateFilter(positions269, filtered269);
            filtered5239.Sort("SortField", ListSortDirection.Descending);
            filtered269.Sort("SortField", ListSortDirection.Descending);
        }
    }
}
