using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;

using Outlook = Microsoft.Office.Interop.Outlook;

using Maple.Dtc;
using Maple.Dtc.Views;
using Maple.Utilities;
using Maple.Utilities.DgvHelper;
using System.Threading;
using System.Diagnostics;

namespace Maple.Dtc.PositionClient
{
    public partial class OccHedgePositionForm : Form, IDgvForm
    {
        private class RealTimeOccPositionObject : RealTimePositionObject
        {

            private double _hedgeQuantity;
            private double _hedgeLoanValue;


            public double HedgeQuantity
            {
                get { return _hedgeQuantity; }
                set { _hedgeQuantity = value; }
            }

            public double HedgeLoanValue
            {
                get { return _hedgeLoanValue; }
                set { _hedgeLoanValue = value; }
            }
            
            #region Constructors

            public RealTimeOccPositionObject()
            {
                TradeCategory = "";
            }

            public RealTimeOccPositionObject(RealTimePositionObject rtPos)
                : this()
            {
                DateOfData = rtPos.DateOfData;
                ParticipantId = rtPos.ParticipantId;
                Ticker = rtPos.Ticker;
                Cusip = rtPos.Cusip;
                Company = rtPos.Company;
                BodShareQuantity = rtPos.BodShareQuantity;
                LoanValue = rtPos.LoanValue;
                LastActivityDate = rtPos.LastActivityDate;
                Price = rtPos.Price;
                HistoricalBorrowRate = rtPos.HistoricalBorrowRate;
                HistoricalLoanRate = rtPos.HistoricalLoanRate;
                DtcNpbActivityQuantity = rtPos.DtcNpbActivityQuantity;
                DtcNpbActivityLoanValue = rtPos.DtcNpbActivityLoanValue;
                DtcNonNpbActivityQuantity = rtPos.DtcNonNpbActivityQuantity;
                DtcNonNpbActivityLoanValue = rtPos.DtcNonNpbActivityLoanValue;
                G1BodNpbQuantity = rtPos.G1BodNpbQuantity;
                G1BodNpbLoanValue = rtPos.G1BodNpbLoanValue;
                G1BodNonNpbQuantity = rtPos.G1BodNonNpbQuantity;
                G1BodNonNpbLoanValue = rtPos.G1BodNonNpbLoanValue;
                OnRecall = rtPos.OnRecall;
                Notes = rtPos.Notes;
            }

            #endregion
        }


        private RealTimePositionCalculator calc;
        private SortableBindingList<RealTimeOccPositionObject> AllRealTimePositions = new SortableBindingList<RealTimeOccPositionObject>();
        private SortableBindingList<RealTimeOccPositionObject> HeldUpRealTimePositions = new SortableBindingList<RealTimeOccPositionObject>();

        private IncomingDeliveryOrderFactory dof = new IncomingDeliveryOrderFactory();

        private DgvHelpers helper = new DgvHelpers();

        private Color selectedColor = Color.Azure;
        private string selectedCusip = "";
        private Dictionary<string, string> Notes = new Dictionary<string, string>();

        public OccHedgePositionForm(RealTimePositionCalculator c)
        {
            InitializeComponent();

            calc = c;

            helper.Add(new DgvHelper(dgvRealTimePosition, Name, true));
            dgvRealTimePosition.DataSource = AllRealTimePositions;

            helper.handleCellSelected += new CellSelectEventHandler(h_handleCellSelected);

            DtcChannel.Instance.handleCusipSelected += new CusipSelectEventHandler(Instance_handleCusipSelected);
            DtcChannel.Instance.handleStockInfoUpdated += new StockInfoUpdateEventHandler(Instance_handleStockInfoUpdated);
            DtcChannel.Instance.handleStockPriceUpdated += new StockPriceUpdateEventHandler(Instance_handleStockPriceUpdated);

            DtcChannel.Instance.handlePositionUpdated += new PositionUpdateEventHandler(Instance_handlePositionUpdated);

        }

        void Instance_handlePositionUpdated(object sender, PositionUpdateEventArgs e)
        {
            //TODO: Check to see if position has OCC
            if (e.Position.DtcActivity.Exists(d => IsHedge(d.ReasonCode)))
            {
                UpdatePositions(e.Position);
            }

        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            try
            {
                //load the data from the table first    
                foreach (RealTimePositionObject pos in calc.RealTimePositions.Values)
                {
                    if (pos.DtcActivity.Exists(d => IsHedge(d.ReasonCode)))
                    {
                        RealTimeOccPositionObject r = new RealTimeOccPositionObject(pos);
                        AllRealTimePositions.Add(r);
                    }
                }

                AllRealTimePositions.Sort("SortField", ListSortDirection.Descending);

                ColorRows();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
                MessageBox.Show("Error starting app: \r\n" + ex.ToString());
            }

            foreach (DataGridViewColumn c in dgvRealTimePosition.Columns)
            {
                c.ReadOnly = true;
            }
            dgvRealTimePosition.Columns["Notes"].ReadOnly = false;
        }

        #region IDtcFeedCallback Members

        void Instance_handleCusipSelected(object sender, CusipSelectEventArgs e)
        {

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
                            //find the stock price in the list
                            int i = AllRealTimePositions.Find("Cusip", e.StockPrice.Cusip);

                            if (i != -1)
                            {
                                //update the stock
                                AllRealTimePositions[i].Price = e.StockPrice.Price;
                            }

                            dgvRealTimePosition.Refresh();
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
                            int i = AllRealTimePositions.Find("Cusip", e.StockInfo.Cusip);

                            if (i != -1)
                            {

                                //update the stock
                                AllRealTimePositions[i].Ticker = e.StockInfo.Ticker;
                                AllRealTimePositions[i].Company = e.StockInfo.Name;
                            }
                            dgvRealTimePosition.Refresh();
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
                    RealTimeOccPositionObject d = (RealTimeOccPositionObject)dgvRealTimePosition.Rows[e.RowIndex].DataBoundItem;
                    DtcChannel.Instance.CusipSelected(d.Cusip);

                    selectedCusip = d.Cusip;

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
                foreach (DataGridViewRow row in dgvRealTimePosition.Rows)
                {
                    RealTimeOccPositionObject r = (RealTimeOccPositionObject)row.DataBoundItem;

                    if (r.Cusip == selectedCusip)
                    {

                        row.DefaultCellStyle.BackColor = selectedColor;
                    }

                    else
                    {
                        if (r.OnRecall == "On Recall")
                        {
                            row.DefaultCellStyle.BackColor = txtOnRecall.BackColor;
                        }

                        else
                        {
                            row.DefaultCellStyle.BackColor = Color.White;
                        }


                        if (r.Price == 0)
                        {
                            row.Cells["Exposure"].Style.BackColor = txtMissingPrice.BackColor;
                        }

                        else if (r.NumBorrows == 0)
                        {
                            row.Cells["Exposure"].Style.BackColor = txtMissingBorrows.BackColor;
                        }

                        else
                        {
                            row.Cells["Exposure"].Style.BackColor = row.Cells["RealTimePosition"].Style.BackColor;
                        }
                    }
                }
                dgvRealTimePosition.Refresh();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
            }
        }

        private void dgvRealTimePosition_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ColorRows();
        }

        private void updatePriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                RealTimeOccPositionObject rt = (RealTimeOccPositionObject)dgvRealTimePosition.CurrentRow.DataBoundItem;

                UpdateForm uf = new UpdateForm(rt, "Price");
                uf.ShowDialog();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
            }
            */
        }

        private void updateTickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                RealTimeOccPositionObject rt = (RealTimeOccPositionObject)dgvRealTimePosition.CurrentRow.DataBoundItem;

                UpdateForm uf = new UpdateForm(rt, "Ticker");
                uf.ShowDialog();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
            }
            */
        }

        private void investigateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                RealTimeOccPositionObject rt = (RealTimeOccPositionObject)dgvRealTimePosition.CurrentRow.DataBoundItem;

                InvestigateForm i = new InvestigateForm(rt);
                i.ShowDialog();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
            }
            */
        }

        private void dgvRealTimePosition_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {

                    System.Drawing.Point pt = dgvRealTimePosition.PointToClient(Cursor.Position);

                    DataGridView.HitTestInfo hti = dgvRealTimePosition.HitTest(pt.X, pt.Y);

                    if (hti.Type == DataGridViewHitTestType.Cell)
                    {
                        dgvRealTimePosition.CurrentCell = dgvRealTimePosition[hti.ColumnIndex, hti.RowIndex];
                        dgvRealTimePosition.ContextMenuStrip = cmsRT;
                    }

                    else
                    {
                        if (dgvRealTimePosition.ContextMenuStrip == cmsRT)
                        {
                            dgvRealTimePosition.ContextMenuStrip = null;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void UpdatePositions(RealTimePositionObject pos)
        {
            try
            {
                if (pos != null)
                {
                    //find the position
                    int i = AllRealTimePositions.Find("Cusip", pos.Cusip);

                    if (i != -1)
                    {
                        //position already exists, remove it and add new one
                        pos.Notes = AllRealTimePositions[i].Notes;

                        AllRealTimePositions.RemoveAt(i);
                        RealTimeOccPositionObject r = new RealTimeOccPositionObject(pos);
                        AllRealTimePositions.Add(r);
                    }
                    else
                    {
                        //new position. add it
                        RealTimeOccPositionObject r = new RealTimeOccPositionObject(pos);
                        AllRealTimePositions.Add(r);
                    }
                    AllRealTimePositions.Sort("SortField", ListSortDirection.Descending);
                    ColorRows();

                    dgvRealTimePosition.Refresh();
                }
            }
            catch (Exception)
            {
                throw;
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


        private void UpdateData()
        {
            try
            {
                this.TopLevelControl.Cursor = Cursors.WaitCursor;
                this.TopLevelControl.Text = "Updating...";
                lblRetrieved.Text = DateTime.Now.ToString("HH:mm:ss") + " Retrieving Positions...";
                Application.DoEvents();

                List<IncomingDeliveryOrderObject> newActivity = new List<IncomingDeliveryOrderObject>();

                dof.Load(newActivity, calc.MaxDtcId);

                lblRetrieved.Visible = true;
                lblRetrieved.Text = DateTime.Now.ToString("HH:mm:ss") + " Retrieved " + newActivity.Count + " Positions";

                foreach (IncomingDeliveryOrderObject d in newActivity)
                {
                    DtcChannel.Instance.DtcMessageReceived(d);
                }

                //                calc.UpdateConduitInfo(AllRealTimePositions);

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
                MessageBox.Show(ex.ToString(), "Error");
            }
            finally
            {
                this.TopLevelControl.Cursor = Cursors.Default;
                this.TopLevelControl.Text = "PositionViewer";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAll.Checked)
            {
                dgvRealTimePosition.DataSource = AllRealTimePositions;
                helper.SetAllColumns();
                dgvRealTimePosition.EnableHeadersVisualStyles = false;
                dgvRealTimePosition.RowHeadersDefaultCellStyle.BackColor = dgvRealTimePosition.ColumnHeadersDefaultCellStyle.BackColor;
            }
        }

        private void rbHeldUp_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHeldUp.Checked)
            {
                LoadHeldUp();
                dgvRealTimePosition.DataSource = HeldUpRealTimePositions;
                dgvRealTimePosition.EnableHeadersVisualStyles = false;
                dgvRealTimePosition.RowHeadersDefaultCellStyle.BackColor = Color.Red;
                ColorRows();
                helper.SetAllColumns();
            }
        }

        private void LoadHeldUp()
        {
            SortableBindingList<RealTimeOccPositionObject> tmp = new SortableBindingList<RealTimeOccPositionObject>();

            AllocationViewFactory avf = new AllocationViewFactory();
            List<AllocationViewObject> heldUpReturns = new List<AllocationViewObject>();
            AllocationViewParam avp = new AllocationViewParam();

            tmp.Load(AllRealTimePositions);
            HeldUpRealTimePositions.Clear();

            avp.HeldUp.AddParamValue(1);
            avf.Load(heldUpReturns, avp);

            //now remove the non Held Up positions
            foreach (RealTimeOccPositionObject rt in tmp)
            {
                if (heldUpReturns.Exists(av => av.Cusip == rt.Cusip))
                {
                    HeldUpRealTimePositions.Add(rt);
                }
            }

            HeldUpRealTimePositions.Sort("TradeCategory", ListSortDirection.Ascending);

        }

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

        private void CalculateHedge(RealTimeOccPositionObject pos)
        {
            pos.HedgeLoanValue = 0;
            pos.HedgeQuantity = 0;

            foreach (IncomingDeliveryOrderObject d in pos.DtcActivity)
            {
                if (IsHedge(d.ReasonCode))
                {
                    //TODO: Differentiate pending and made

                }
            }
        }
    }
}
