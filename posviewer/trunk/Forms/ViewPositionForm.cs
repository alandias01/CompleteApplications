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
using Maple.Global1.G1BizObjects;

namespace Maple.Dtc.PositionClient
{
    public partial class ViewPositionForm : Form, IDgvForm
    {
        private RealTimePositionCalculator calc;
        private SortableBindingList<RealTimePositionObject> AllRealTimePositions = new SortableBindingList<RealTimePositionObject>();
        private SortableBindingList<RealTimePositionObject> HeldUpRealTimePositions = new SortableBindingList<RealTimePositionObject>();
        private SortableBindingList<RealTimePositionObject> HedgedRealTimePositions = new SortableBindingList<RealTimePositionObject>();

        private IncomingDeliveryOrderFactory dof = new IncomingDeliveryOrderFactory();
        private G1RealTimeActivityFactory g1f = new G1RealTimeActivityFactory();

        private DgvHelpers helper = new DgvHelpers();

        private Color selectedColor = Color.Azure;
        private string selectedCusip = "";
        private Dictionary<string, string> Notes = new Dictionary<string, string>();

        IncomingDtcMessageEventHandler handleDtcMessageReceived;
        CusipSelectEventHandler handleCusipSelected;
        StockInfoUpdateEventHandler handleStockInfoUpdated;
        StockPriceUpdateEventHandler handleStockPriceUpdated;

        
        

        //ALANDIAS------------------------------------------------------------------------
        int MaxPledgeId = 0;
        IncomingPledgeOrderFactory POF = new IncomingPledgeOrderFactory();
        //--------------------------------------------------------------------------------


        public ViewPositionForm(RealTimePositionCalculator c)
        {
            InitializeComponent();
            
            calc = c;

            helper.Add(new DgvHelper(dgvRealTimePosition, Name, true));
            dgvRealTimePosition.DataSource = AllRealTimePositions;

            helper.handleCellSelected += new CellSelectEventHandler(h_handleCellSelected);

            handleDtcMessageReceived = new IncomingDtcMessageEventHandler(DtcMessageReceived);
            handleCusipSelected = new CusipSelectEventHandler(Instance_handleCusipSelected);
            handleStockInfoUpdated = new StockInfoUpdateEventHandler(Instance_handleStockInfoUpdated);
            handleStockPriceUpdated = new StockPriceUpdateEventHandler(Instance_handleStockPriceUpdated);

            DtcChannel.Instance.handleDtcMessageReceived += handleDtcMessageReceived;
            DtcChannel.Instance.handleCusipSelected += handleCusipSelected;
            DtcChannel.Instance.handleStockInfoUpdated += handleStockInfoUpdated;
            DtcChannel.Instance.handleStockPriceUpdated += handleStockPriceUpdated;

            ToolTip ToolTipBtnPledge = new ToolTip();
            ToolTipBtnPledge.SetToolTip(btnPledge, "Pledge the highlighted rows");
            
            /*
            DtcChannel.Instance.handleDtcMessageReceived += new IncomingDtcMessageEventHandler(DtcMessageReceived);
            DtcChannel.Instance.handleCusipSelected += new CusipSelectEventHandler(Instance_handleCusipSelected);
            DtcChannel.Instance.handleStockInfoUpdated += new StockInfoUpdateEventHandler(Instance_handleStockInfoUpdated);
            DtcChannel.Instance.handleStockPriceUpdated += new StockPriceUpdateEventHandler(Instance_handleStockPriceUpdated);
            */
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {            
            chkAutoRefresh.Checked = Properties.Settings.Default.AutoRefresh;
            SetAutoRefresh(Properties.Settings.Default.AutoRefresh);

            try
            {
                //load the data from the table first                
                AllRealTimePositions.Load(calc.RealTimePositions.Values.OrderByDescending(o=>o.SortField));
                
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

            //bwt.RunWorkerAsync(d);

            
            new Thread((ThreadStart)delegate()
            {
                this.BeginInvoke((ThreadStart)delegate()
                {
                    try
                    {
                        RealTimePositionObject pos = calc.CalculatePosition(d);
                        UpdatePositions(pos);

                        //if they are looking at the held up returns, update those positions
                        if (rbHeldUp.Checked)
                        {
                            LoadHeldUp();
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex, TraceEnum.LoggedError);
                    }
                });
            }).Start();
            
        }

        private void bwt_DoWork(object sender, DoWorkEventArgs e)
        {
            IncomingDeliveryOrderObject d = (IncomingDeliveryOrderObject)e.Argument;
            
            new Thread((ThreadStart)delegate()
            {
                this.BeginInvoke((ThreadStart)delegate()
                {
                    try
                    {
                        RealTimePositionObject pos = calc.CalculatePosition(d);
                        UpdatePositions(pos);

                        //if they are looking at the held up returns, update those positions
                        if (rbHeldUp.Checked)
                        {
                            LoadHeldUp();
                        }
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
                            //find the stock price in the list
                            int i = AllRealTimePositions.Find("Cusip", e.StockPrice.Cusip);

                            if (i != -1)
                            {
                                //update the stock
                                AllRealTimePositions[i].Price = e.StockPrice.Price;
                            }

//                            dgvRealTimePosition.Refresh();
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
//                            dgvRealTimePosition.Refresh();
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

        private void btnConnect_Click(object sender, EventArgs e)
        {
            /*
            //subscribe to the types you are interested in
            DtcChannel.User.Subscriptions.Add(new SubscriptionType(DtcMessageDirectionEnum.FromDtc, DtcMessageSideEnum.Deliverer, DtcMessageTypeEnum.DeliveryOrder), 0);
            DtcChannel.User.Subscriptions.Add(new SubscriptionType(DtcMessageDirectionEnum.FromDtc, DtcMessageSideEnum.Receiver, DtcMessageTypeEnum.DeliveryOrder), 0);


            //subscribe to the feed
            DtcChannel.Instance.JoinFeed(calc.DtcActivity[calc.DtcActivity.Count - 1].Id);
            */


            IncomingDeliveryOrderObject d = new IncomingDeliveryOrderObject();

            d.Id = 147293;
            d.DateAndTimeStamp = DateTime.Parse("5/29/2008 13:03");
            d.Cusip = "05545E209";

            d.DtcStatusIndicator = " ";

            d.Receiver = "00005239";
            d.Deliverer = "00000773";
            d.DelivererReceiverIndicator = "R";

            d.ActionCode = "1";
            d.ActivityCode = "027";
            d.ReasonCode = "020";

            d.MoneyValue = 200;
            d.ShareQuantity = 100;

            DtcChannel.Instance.DtcMessageReceived(d);
                        
        }

        private void dgvRealTimePosition_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    RealTimePositionObject d = (RealTimePositionObject)dgvRealTimePosition.Rows[e.RowIndex].DataBoundItem;
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
                    RealTimePositionObject r = (RealTimePositionObject)row.DataBoundItem;

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
//                dgvRealTimePosition.Refresh();
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
            try
            {
                RealTimePositionObject rt = (RealTimePositionObject)dgvRealTimePosition.CurrentRow.DataBoundItem;

                UpdateForm uf = new UpdateForm(rt, "Price");
                uf.ShowDialog();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
            }
        }

        private void updateTickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RealTimePositionObject rt = (RealTimePositionObject)dgvRealTimePosition.CurrentRow.DataBoundItem;

                UpdateForm uf = new UpdateForm(rt, "Ticker");
                uf.ShowDialog();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
            }
        }

        private void investigateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RealTimePositionObject rt = (RealTimePositionObject)dgvRealTimePosition.CurrentRow.DataBoundItem;

                InvestigateForm i = new InvestigateForm(rt);
                i.ShowDialog();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
            }
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
                        AllRealTimePositions.Add(pos);
                    }
                    else
                    {
                        //new position. add it
                        AllRealTimePositions.Add(pos);
                    }
                    AllRealTimePositions.Sort("SortField", ListSortDirection.Descending);                    
                    ColorRows();
                    
//                    dgvRealTimePosition.Refresh();

                    DtcChannel.Instance.PositionUpdated(pos);
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

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                SearchForm();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void SearchForm()
        {
            try
            {
                int start = dgvRealTimePosition.CurrentRow.Index + 1;
                int end;
                bool looped = false;
                SortableBindingList<RealTimePositionObject> temp;

                if (rbHeldUp.Checked)
                {
                    temp = HeldUpRealTimePositions;
                }
                else if (rbHedged.Checked)
                {
                    temp = HedgedRealTimePositions;
                }
                else
                {
                    temp = AllRealTimePositions;
                }

                end = temp.Count;


                for (int i = start; i < end; i++)
                {
                    RealTimePositionObject r;

                    r = temp[i];

                    if (r.Ticker != null && r.Ticker.ToUpper().Contains(txtFind.Text.ToUpper()))
                    {
                        dgvRealTimePosition.CurrentCell = dgvRealTimePosition["Ticker", i];
                        return;
                    }
                    else if (r.Cusip != null && r.Cusip.ToUpper().Contains(txtFind.Text.ToUpper()))
                    {
                        dgvRealTimePosition.CurrentCell = dgvRealTimePosition["Cusip", i];
                        return;
                    }
                    else if (looped && i == end - 1)
                    {
                        MessageBox.Show("Could not find: " + txtFind.Text);
                        return;
                    }

                    if (i == end - 1 && !looped)
                    {
                        end = start;
                        i = -1;
                        looped = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    SearchForm();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex, TraceEnum.LoggedError);
                    MessageBox.Show(ex.ToString(), "Error");
                }
            }
        }

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

                /* commented out because g1 activity wasnt being used and it takes a long time
                List<G1RealTimeActivityObject> tmp = new List<G1RealTimeActivityObject>();
                g1f.LoadNewActivity(tmp, 5000, Settings.BookName, calc.MaxG1Id);

                foreach (G1RealTimeActivityObject g in tmp)
                {
                    DtcChannel.Instance.G1MessageReceived(g);
                }
                */

                calc.UpdateConduitInfo(AllRealTimePositions);

                //calc.UpdateNpbInfo(AllRealTimePositions);

                //ALANDIAS--------------------------------------------------------------
                //if (Settings.Account == "269")
                {
                    

                    List<IncomingPledgeOrderObject> ListPOF = new List<IncomingPledgeOrderObject>();
                    IncomingPledgeOrderParam IP = new IncomingPledgeOrderParam();
                    IP.Id.AddParamValue(MaxPledgeId, ">");
                    IP.StatusIndicator.AddParamValue("m");
                    IP.PledgorParticipantNum.AddParamValue(Settings.Account.PadLeft(8, '0'));
                    POF.Load(ListPOF, IP);
                    
                    if (ListPOF.Count != 0)
                    {
                        MaxPledgeId = ListPOF.Max(x => x.Id);

                        foreach (RealTimePositionObject RTPO in AllRealTimePositions)
                        {
                            foreach (IncomingPledgeOrderObject IPOO in ListPOF)
                            {
                                if (RTPO.Cusip == IPOO.Cusip)
                                {
                                    int qty = Convert.ToInt32(IPOO.ShareQuantityNew);
                                    if (IPOO.ActivityCode == "051")
                                    {
                                        RTPO.DtcNonNpbActivityQuantity -= qty;
                                    }

                                    if (IPOO.ActivityCode == "056")
                                    {
                                        RTPO.DtcNonNpbActivityQuantity += qty;
                                    }

                                }
                            }
                        }
                    }                    

                }
                //--------------------------------------------------------------


            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
                MessageBox.Show(ex.ToString(), "Error");
            }
            finally
            {
                this.TopLevelControl.Cursor = Cursors.Default;
                this.TopLevelControl.Text = "";
                
                for (int i = 0; i < 10; i++)
                {
                   this.TopLevelControl.Text += " " + Settings.Account + " ";
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Hour > 23 || DateTime.Now.Hour < 5)
            {
                this.ParentForm.Close();
            }
            else
            {
                UpdateData();
            }
        }

        private void chkAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            SetAutoRefresh(chkAutoRefresh.Checked);
        }

        private void SetAutoRefresh(bool ar)
        {
            if (ar)
            {
                btnRefresh.Visible = false;
                timer1.Enabled = true;
                timer1.Interval = Properties.Settings.Default.RefreshInterval;
            }
            else
            {
                btnRefresh.Visible = true;
                timer1.Enabled = false;
            }
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

        private void rbHedged_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHedged.Checked)
            {
                LoadHedged();



                dgvRealTimePosition.DataSource = HedgedRealTimePositions;
                dgvRealTimePosition.EnableHeadersVisualStyles = false;
                dgvRealTimePosition.RowHeadersDefaultCellStyle.BackColor = Color.Green;
                ColorRows();
                helper.SetAllColumns();

                dgvRealTimePosition.Columns["RealTimePosition"].Visible  = false;
                dgvRealTimePosition.Columns["Exposure"].Visible = false;

                dgvRealTimePosition.Columns["OccBodQuantity"].DisplayIndex = 2;
                dgvRealTimePosition.Columns["RealTimeHedgedPosition"].DisplayIndex = 3;                
                dgvRealTimePosition.Columns["OccQuantityLoaned"].DisplayIndex = 4;
                dgvRealTimePosition.Columns["OccQuantityBorrowed"].DisplayIndex = 5;
                dgvRealTimePosition.Columns["OccQuantityReturned"].DisplayIndex = 6;                
                dgvRealTimePosition.Columns["OccQuantityReceived"].DisplayIndex = 7;
                dgvRealTimePosition.Columns["OccQuantityCalledIn"].DisplayIndex = 8;
                dgvRealTimePosition.Columns["OccQuantityCalledOut"].DisplayIndex = 9;
                
                dgvRealTimePosition.Refresh();              
            }
        }

        private void LoadHeldUp()
        {
            SortableBindingList<RealTimePositionObject> tmp = new SortableBindingList<RealTimePositionObject>();

            AllocationViewFactory avf = new AllocationViewFactory();
            List<AllocationViewObject> heldUpReturns = new List<AllocationViewObject>();
            AllocationViewParam avp = new AllocationViewParam();

            tmp.Load(AllRealTimePositions);
            HeldUpRealTimePositions.Clear();

            avp.HeldUp.AddParamValue(1);
            avf.Load(heldUpReturns, avp);

            //now remove the non Held Up positions
            foreach (RealTimePositionObject rt in tmp)
            {
                if (heldUpReturns.Exists(av => av.Cusip == rt.Cusip))
                {
                    HeldUpRealTimePositions.Add(rt);
                }
            }

            HeldUpRealTimePositions.Sort("TradeCategory", ListSortDirection.Ascending);

        }

        private void LoadHedged()
        {
            SortableBindingList<RealTimePositionObject> tmp = new SortableBindingList<RealTimePositionObject>();
            
            tmp.Load(AllRealTimePositions);
            HedgedRealTimePositions.Clear();
                        
            //now remove the non Hedge
            foreach (RealTimePositionObject rt in tmp)
            {
                if (rt.ContainsHedge)
                {
                    HedgedRealTimePositions.Add(rt);
                }
            }

            HedgedRealTimePositions.Sort("HedgedSortField", ListSortDirection.Descending);
        }

        private void ViewPositionForm_Shown(object sender, EventArgs e)
        {
            helper.SetAllColumns();
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            string body = "";
            List<RealTimePositionObject> AdrPositions = new List<RealTimePositionObject>();
            List<RealTimePositionObject> HrdPositions = new List<RealTimePositionObject>();
            List<RealTimePositionObject> OtherPositions = new List<RealTimePositionObject>();

            //generate the text
            LoadHeldUp();

            foreach (RealTimePositionObject r in HeldUpRealTimePositions)
            {
                //find positions with hedge data
                if (r.DtcActivity.Exists(d => d.ReasonCode == "260") || r.DtcActivity.Exists(d => d.ReasonCode == "261") ||
                    r.DtcActivity.Exists(d => d.ReasonCode == "270") || r.DtcActivity.Exists(d => d.ReasonCode == "271"))
                {
                    r.ContainsHedge = true;
                }

                if (r.TradeCategory.Contains("ADR"))
                {
                    AdrPositions.Add(r);
                }
                if (r.TradeCategory.Contains("HRD"))
                {
                    HrdPositions.Add(r);
                }
                if (!r.TradeCategory.Contains("HRD") && !r.TradeCategory.Contains("ADR"))
                {
                    OtherPositions.Add(r);
                }
            }

            AdrPositions.Sort((a1, a2) => a1.Ticker.CompareTo(a2.Ticker));
            HrdPositions.Sort((a1, a2) => a1.Ticker.CompareTo(a2.Ticker));
            OtherPositions.Sort((a1, a2) => a1.Ticker.CompareTo(a2.Ticker));

            body += "<br>ADR<br>";
            body += GenerateHtmlTable(AdrPositions);

            body += "<br>HRD<br>";
            body += GenerateHtmlTable(HrdPositions);

            body += "<br>OTHER<br>";
            body += GenerateHtmlTable(OtherPositions);

            Outlook.Application oApp = new Outlook.Application();
            Outlook._MailItem oMailItem = (Outlook._MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);

            oMailItem.To = "";
            oMailItem.Subject = "Held Up Positions";
            oMailItem.BodyFormat = Outlook.OlBodyFormat.olFormatHTML;
            oMailItem.HTMLBody = body;

            oMailItem.Display(false);
        }

        private string GenerateHtmlTable(List<RealTimePositionObject> data)
        {
            string body = "";

            body += "<table  border='1'>\r\n";
            body += "<TR> ";
            body += "<TD> Ticker </TD>\r\n";
            body += "<TD> Cusip </TD>\r\n";
            body += "<TD> RealTime Position </TD>\r\n";
            body += "<TD> Qty Held Up </TD>\r\n";
            body += "<TD> Qty Called In </TD>\r\n";
            body += "<TD> Trade Category </TD>\r\n";
            body += "<TD> Hedged </TD>\r\n";
            body += "</TR> \r\n";

            foreach (RealTimePositionObject r in data)
            {
                body += "<TR> ";
                body += "<TD>" + r.Ticker + "</TD>\r\n";
                body += "<TD>" + r.Cusip + "</TD>\r\n";
                body += "<TD align = 'right'>" + r.RealTimePosition.ToString("n0") + "</TD>\r\n";
                body += "<TD align = 'right'>" + r.QuantityHeldUp.ToString("n0") + "</TD>\r\n";
                body += "<TD align = 'right'>" + r.QuantityCalledIn.ToString("n0") + "</TD>\r\n";
                body += "<TD>" + r.TradeCategory + "</TD>\r\n";

                if (r.ContainsHedge)
                {
                    body += "<TD> H </TD>\r\n";
                }
                else
                {
                    body += "<TD> </TD>\r\n";
                }

                
                body += "</TR> \r\n";
            }

            body += "</table>\r\n";

            return body;
        }

        private void ViewPositionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
            */


            DtcChannel.Instance.handleDtcMessageReceived -= handleDtcMessageReceived;
            DtcChannel.Instance.handleCusipSelected -= handleCusipSelected;
            DtcChannel.Instance.handleStockInfoUpdated -= handleStockInfoUpdated;
            DtcChannel.Instance.handleStockPriceUpdated -= handleStockPriceUpdated;


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

        

        private void btnPledge_Click(object sender, EventArgs e)
        {
            List<RealTimePositionObject> RTObjToPledge = new List<RealTimePositionObject>();
            foreach (DataGridViewRow d in dgvRealTimePosition.SelectedRows)
            {
                RealTimePositionObject r = d.DataBoundItem as RealTimePositionObject;
                if (r!=null)
                {
                    RTObjToPledge.Add(r);                    
                }
                
            }
        }

        

    }
}
