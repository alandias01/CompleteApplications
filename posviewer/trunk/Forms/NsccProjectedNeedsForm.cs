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
using Maple.Nscc.NsccBizObjects;
using System.Reflection;
using Maple.Nscc.NsccBizObjects.Views;
using Maple.NewtonDTCBizObjects;

namespace Maple.Dtc.PositionClient
{
    public partial class NsccProjectedNeedsForm : Form, IDgvForm
    {

        private class ProjectedNeedsSummaryObject
        {
            private string _ticker;
            private string _cusip;
            private int _beginningNeed;
            private int _tomorrowsNetSettling;
            private int _deliveredToCns;
            private int _received;

            private bool _etb;
            private bool _etbT3;


            private string _needType;


            public string NeedType
            {
                get { return _needType; }
                set { _needType = value; }
            }



            public int OpenNeed
            {
                get { return BeginningNeed - DeliveredToCns; }
            }

            public double OpenNeedRounded
            {
                get
                {
                    if (OpenNeed % 100 != 0)
                    {
                        return OpenNeed + 100 - (OpenNeed % 100);
                    }
                    else
                    {
                        return OpenNeed;
                    }

                }
            }

            public int PenaltyBoxAmount
            {
                get
                {
                    if (OpenNeed <= 0)
                    {
                        return 0;
                    }
                    else if (OpenNeed > 0 && TomorrowsNetSettling <= 0)
                    {
                        return OpenNeed;
                    }
                    else if (OpenNeed > 0 && TomorrowsNetSettling >= OpenNeed)
                    {
                        return 0;
                    }
                    else
                    {
                        return OpenNeed - TomorrowsNetSettling;
                    }
                }
            }

            public bool PenaltyBox
            {
                get
                {
                    if (PenaltyBoxAmount > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }


            public string Ticker
            {
                get { return _ticker; }
                set { _ticker = value; }
            }

            public string Cusip
            {
                get { return _cusip; }
                set { _cusip = value; }
            }

            public int BeginningNeed
            {
                get { return _beginningNeed; }
                set { _beginningNeed = value; }
            }

            public int TomorrowsNetSettling
            {
                get { return _tomorrowsNetSettling; }
                set { _tomorrowsNetSettling = value; }
            }

            public int DeliveredToCns
            {
                get { return _deliveredToCns; }
                set { _deliveredToCns = value; }
            }

            public int Received
            {
                get { return _received; }
                set { _received = value; }
            }

            public bool CurrentEtb
            {
                get { return _etb; }
                set { _etb = value; }
            }

            public bool EtbOnTradeDate3
            {
                get { return _etbT3; }
                set { _etbT3 = value; }
            }

            public ProjectedNeedsSummaryObject(string cusip, int beginningNeed, int tomorrowsNetSettling, int delivered, int received)
            {
                Cusip = cusip;
                BeginningNeed = beginningNeed;
                TomorrowsNetSettling = tomorrowsNetSettling;
                DeliveredToCns = delivered;
                Received = received;
            }

            public ProjectedNeedsSummaryObject(ProjectedNeedObject p)
            {
                Cusip = p.CUSIP;
                BeginningNeed = Math.Abs(p.Todays_Current_Net_Position.Value);
                TomorrowsNetSettling = p.Tomorrows_Net_Settling_Trades.Value;
                DeliveredToCns = 0;
                Received = 0;
                NeedType = "ProjectedNeed";
            }

            public ProjectedNeedsSummaryObject(BalanceOrderViewObject b)
            {
                Cusip = b.CUSIP;
                BeginningNeed = Math.Abs(b.Quantity.Value);
                //                TomorrowsNetSettling = p.Tomorrows_Net_Settling_Trades.Value;
                DeliveredToCns = 0;
                Received = 0;
                NeedType = "BalanceOrder";
            }

            //ALANDIAS Used for NSCC MISCELLANEOUS Data
            public ProjectedNeedsSummaryObject(vNSCCMiscellaneousActivityObject A)
            {
                Cusip = A.CUSIP;
                BeginningNeed = Convert.ToInt32(A.QuantityReceived);
                DeliveredToCns = 0;
                Received = 0;
                NeedType = "MiscellaneousFile";
            }

        }

        private SortableBindingList<ProjectedNeedsSummaryObject> ProjectedNeeds = new SortableBindingList<ProjectedNeedsSummaryObject>();

        private IncomingDeliveryOrderFactory dof = new IncomingDeliveryOrderFactory();

        private DgvHelpers helper = new DgvHelpers();

        private RealTimePositionCalculator calc;
        private Color selectedColor = Color.Azure;
        private string selectedCusip = "";
        private Dictionary<string, string> Notes = new Dictionary<string, string>();
        private Dictionary<string, EtbStockViewObject> etbList = new Dictionary<string, EtbStockViewObject>();
        private Dictionary<string, EtbStockViewObject> etbT3List = new Dictionary<string, EtbStockViewObject>();

        StockInfoUpdateEventHandler handleStockInfoUpdated;
        IncomingDtcMessageEventHandler handleDtcMessageReceived;


        public NsccProjectedNeedsForm(RealTimePositionCalculator c)
        {
            InitializeComponent();

            calc = c;

            helper.Add(new DgvHelper(dgvRealTimePosition, Name, true));
            dgvRealTimePosition.DataSource = ProjectedNeeds;

            handleStockInfoUpdated = new StockInfoUpdateEventHandler(Instance_handleStockInfoUpdated);
            handleDtcMessageReceived = new IncomingDtcMessageEventHandler(Instance_handleDtcMessageReceived);

            DtcChannel.Instance.handleStockInfoUpdated += handleStockInfoUpdated;
            DtcChannel.Instance.handleDtcMessageReceived += handleDtcMessageReceived;

            
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

            /*AlanDias New way to multithread
            new Thread(() => {
                this.BeginInvoke(new Action(() => { }));
            }).Start();
            */

            
            new Thread((ThreadStart)delegate()
            {
                this.BeginInvoke((ThreadStart)delegate()
                {                    
                    try
                    {
                        if (d.Receiver == "00000888" && d.Deliverer == Settings.Account.Padded())
                        {
                            int i = ProjectedNeeds.Find("Cusip", d.Cusip);

                            if (i != -1 && ProjectedNeeds[i].NeedType == "ProjectedNeed")
                            {
                                ProjectedNeeds[i].DeliveredToCns += d.ShareQuantity.Value;
                                CalcSummary();
                                ProjectedNeeds.Sort("OpenNeed", ListSortDirection.Descending);
                            }
                        }

                        if (d.Deliverer == "00000888" && d.Receiver == Settings.Account.Padded())
                        {
                            int i = ProjectedNeeds.Find("Cusip", d.Cusip);

                            if (i != -1 && ProjectedNeeds[i].NeedType == "ProjectedNeed")
                            {
                                ProjectedNeeds[i].Received += d.ShareQuantity.Value;
                                CalcSummary();
                                ProjectedNeeds.Sort("OpenNeed", ListSortDirection.Descending);
                            }
                        }

                        
                        //Reason Code 620 is for Balance Orders
                        if (d.ReasonCode == "620" && d.Deliverer == Settings.Account.Padded())
                        {
                            int i = ProjectedNeeds.Find("Cusip", d.Cusip);

                            if (i != -1 && (ProjectedNeeds[i].NeedType == "BalanceOrder" || ProjectedNeeds[i].NeedType == "ProjectedNeed"))
                            {
                                ProjectedNeeds[i].DeliveredToCns += d.ShareQuantity.Value;
                                CalcSummary();
                                ProjectedNeeds.Sort("OpenNeed", ListSortDirection.Descending);
                            }
                        }

                        if (d.ReasonCode == "620" && d.Receiver == Settings.Account.Padded())
                        {
                            int i = ProjectedNeeds.Find("Cusip", d.Cusip);

                            if (i != -1 && (ProjectedNeeds[i].NeedType == "BalanceOrder" || ProjectedNeeds[i].NeedType == "ProjectedNeed"))
                            {
                                ProjectedNeeds[i].Received += d.ShareQuantity.Value;
                                CalcSummary();
                                ProjectedNeeds.Sort("OpenNeed", ListSortDirection.Descending);
                            }
                        }
                        
                        /*
                        //ALANDIAS added for NSCCMISC  
                        if (d.ReasonCode == "620" && d.Deliverer == Settings.Account.Padded())
                        {
                            int i = ProjectedNeeds.Find("Cusip", d.Cusip);

                            if (i != -1 && ProjectedNeeds[i].NeedType == "MiscellaneousFile")
                            {
                                ProjectedNeeds[i].DeliveredToCns += d.ShareQuantity.Value;
                                CalcSummary();
                                ProjectedNeeds.Sort("OpenNeed", ListSortDirection.Descending);
                            }
                        }
                        */


                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex, TraceEnum.LoggedError);
                    }
                });
            }).Start();
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
                            int i = ProjectedNeeds.Find("Cusip", e.StockInfo.Cusip);

                            if (i != -1)
                            {

                                //update the stock
                                ProjectedNeeds[i].Ticker = e.StockInfo.Ticker;
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

        private void ServerForm_Load(object sender, EventArgs e)
        {
            try
            {
                //load NSCC projected needs
                ProjectedNeedFactory pnf = new ProjectedNeedFactory();
                ProjectedNeedParam pnp = new ProjectedNeedParam();
                List<ProjectedNeedObject> pn = new List<ProjectedNeedObject>();
                
                pnp.DateOfData_Date.AddParamValue(DateTime.Today);
                pnp.Todays_Current_Net_Position.AddParamValue(0, "<");
                pnp.Participant_Clearing_Number.AddParamValue(Settings.DatabaseName);

                pnf.Load(pn, pnp);

                //load up todays projected needs
                foreach (ProjectedNeedObject p in pn)
                {
                    ProjectedNeedsSummaryObject ps = new ProjectedNeedsSummaryObject(p);
                    ps.Ticker = calc.UpdateStockInfo(p.CUSIP);
                    ProjectedNeeds.Add(ps);
                }

                

                //load up todays balance orders
                BalanceOrderViewFactory bof = new BalanceOrderViewFactory();
                BalanceOrderViewParam bop = new BalanceOrderViewParam();
                List<BalanceOrderViewObject> bol = new List<BalanceOrderViewObject>();
                List<BalanceOrderViewObject> combined = new List<BalanceOrderViewObject>();
                                
                bop.DateOfData_Date.AddParamValue(Utils.GetNthBusinessDay(DateTime.Today, -1));                
                bop.Participant_Clearing_Number.AddParamValue(Settings.Account.PadLeft(4, '0'));

                bof.Load(bol, bop);

                foreach (BalanceOrderViewObject b in bol)
                {
                    BalanceOrderViewObject f = combined.Find(c => c.CUSIP == b.CUSIP);

                    if (f == null)
                    {
                        combined.Add(b);
                    }
                    else
                    {
                        f.Quantity += b.Quantity;
                    }
                }

                
                foreach (BalanceOrderViewObject b in combined)
                {
                    if (ProjectedNeeds.Any(x => x.Cusip != null && b.CUSIP != null && x.Cusip.ToLower() == b.CUSIP.ToLower()))
                    {
                        var item = ProjectedNeeds.SingleOrDefault(x => x.Cusip.ToLower() == b.CUSIP.ToLower());
                        item.BeginningNeed += Math.Abs(b.Quantity.Value);
                    }

                    else
                    {
                        ProjectedNeedsSummaryObject ps = new ProjectedNeedsSummaryObject(b);
                        ps.Ticker = calc.UpdateStockInfo(b.CUSIP);
                        ProjectedNeeds.Add(ps);
                    }
                }

                
                //ALANDIAS--------Balance orders weren't updating for overnight deliveries------------------------------
                if (combined.Count > 0)
                {
                    List<tblDTFPARTObject> DTFPO = new List<tblDTFPARTObject>();
                    tblDTFPARTFactory DTFPFact = new tblDTFPARTFactory();
                    tblDTFPARTParam DTFParam = new tblDTFPARTParam();                                        
                    DTFParam.DateofData.AddParamValue(DateTime.Today);                    
                    DTFParam.TransOrigSource.AddParamValue("CFSD", "!=");
                    DTFParam.ParticipantNum.AddParamValue(Settings.Account.PadLeft(4, '0'));
                    DTFParam.SubFunction.AddParamValue("DTFPDQ");
                    DTFParam.StatusCode.AddParamValue("m");
                    DTFParam.TransTypeNew.AddParamValue("026");
                    DTFParam.ContraParticipantNum.AddParamValue("0888");
                    DTFPFact.Load(DTFPO, DTFParam);
                                        
                    var BOinPN=ProjectedNeeds.Where(x=>x.NeedType!=null &&x.NeedType.ToLower()=="balanceorder");
                    foreach (var b in BOinPN)
                    {
                        var overNightDelivery = DTFPO.SingleOrDefault(x =>x.CUSIP.ToLower() == b.Cusip.ToLower());
                        if (overNightDelivery != null)
                        {
                            b.DeliveredToCns += Convert.ToInt32(overNightDelivery.ShareQuantity);
                        }
                    }

                }

                //------------------------------------------------------------------------------------------------------


                //ALANDIAS NSCCMISCFILE----------------------------------------
                //Please Note
                //Misc import was done incorrectly.  QuantityReceived is actually QuantityDelivered and vice versa
                //Activity '15' is cancel and '02' is new deliver
                //quantitydelivered with activity '15' means we received and it reduces the balance order we have to deliver
                //QuantityReceived with activity '02' means we have to deliver

                string NSCCMISCNewDeliver = "02";
                string NSCCMISCCancel = "15";

                vNSCCMiscellaneousActivityFactory MAF = new vNSCCMiscellaneousActivityFactory();
                vNSCCMiscellaneousActivityParam MAP = new vNSCCMiscellaneousActivityParam();
                List<vNSCCMiscellaneousActivityObject> MAList = new List<vNSCCMiscellaneousActivityObject>();

                string Acct = Settings.Account.PadLeft(4, '0');
                                
                MAP.DateOfData.AddParamValue(DateTime.Today);
                
                MAP.ParticipantClearingNumber.AddParamValue(Acct);



                MAP.Activity.AddParamValue(NSCCMISCNewDeliver);
                MAP.Activity.AddParamValue(NSCCMISCCancel);

                MAF.Load(MAList, MAP);

                
                var WhatWeHaveToDeliver = MAList.Where(x => (x.Activity == NSCCMISCNewDeliver) && (x.QuantityReceived != 0));
                var ReduceBalanceOrder = MAList.Where(x => (x.Activity == NSCCMISCCancel) && (x.QuantityDelivered != 0));

                
                foreach (vNSCCMiscellaneousActivityObject MAItem in WhatWeHaveToDeliver)
                {
                    ProjectedNeedsSummaryObject ps = new ProjectedNeedsSummaryObject(MAItem);
                    ps.Ticker = calc.UpdateStockInfo(MAItem.CUSIP);
                    ProjectedNeeds.Add(ps);
                }
                

                foreach (vNSCCMiscellaneousActivityObject rbo in ReduceBalanceOrder)
                {
                    if (ProjectedNeeds.Any(x => x.Cusip != null && rbo.CUSIP != null && x.Cusip.ToLower() == rbo.CUSIP.ToLower()))
                    {
                        var item = ProjectedNeeds.FirstOrDefault(x => x.Cusip.ToLower() == rbo.CUSIP.ToLower());
                        item.BeginningNeed -= Math.Abs(Convert.ToInt32(rbo.QuantityDelivered.Value));
                    }
                    
                    /*
                    else
                    {
                        ProjectedNeedsSummaryObject ps = new ProjectedNeedsSummaryObject(b);
                        ps.Ticker = calc.UpdateStockInfo(b.CUSIP);
                        ProjectedNeeds.Add(ps);
                    }
                    */ 
                }
                
                
                //--------------------------------------------------------------------------------






                /*IncomingDeliveryOrderObject o = calc.AllDtcActivity.Find(p => p.ReasonCode == "620");

                int a = 5;*/

                //load the data from the table first    
                foreach (IncomingDeliveryOrderObject ido in calc.AllDtcActivity)
                {                    
                    if (ido.Receiver == "00000888" && ido.Deliverer == Settings.Account.Padded())
                    {
                        int i = ProjectedNeeds.Find("Cusip", ido.Cusip);

                        if (i != -1)
                        {
                            ProjectedNeeds[i].DeliveredToCns += ido.ShareQuantity.Value;
                        }
                    }

                    if (ido.Deliverer == "00000888" && ido.Receiver == Settings.Account.Padded())
                    {
                        int i = ProjectedNeeds.Find("Cusip", ido.Cusip);

                        if (i != -1)
                        {
                            ProjectedNeeds[i].Received += ido.ShareQuantity.Value;
                        }
                    }

                    if (ido.ReasonCode == "620"/*"030"*/ && ido.Deliverer == Settings.Account.Padded())
                    {
                        int i = ProjectedNeeds.Find("Cusip", ido.Cusip);

                        if (i != -1 && ProjectedNeeds[i].NeedType == "BalanceOrder")
                        {
                            ProjectedNeeds[i].DeliveredToCns += ido.ShareQuantity.Value;
                            CalcSummary();
                            ProjectedNeeds.Sort("OpenNeed", ListSortDirection.Descending);
                        }
                    }
                    
                    //ALANDIAS added for NSCCMISC
                    if (ido.ReasonCode == "620"/*"030"*/ && ido.Deliverer == Settings.Account.Padded())
                    {
                        int i = ProjectedNeeds.Find("Cusip", ido.Cusip);

                        if (i != -1 && ProjectedNeeds[i].NeedType == "MiscellaneousFile")
                        {
                            ProjectedNeeds[i].DeliveredToCns += ido.ShareQuantity.Value;
                            CalcSummary();
                            ProjectedNeeds.Sort("OpenNeed", ListSortDirection.Descending);
                        }
                    }


                }

                ProjectedNeeds.Sort("OpenNeed", ListSortDirection.Descending);

                foreach (DataGridViewColumn c in dgvRealTimePosition.Columns)
                {
                    c.ReadOnly = true;
                }
                //dgvRealTimePosition.Columns["Ticker"].ReadOnly = false;

                ColorRows();

                //ETB stuff
                EtbStockViewFactory ef = new EtbStockViewFactory();
                EtbStockViewParam ep = new EtbStockViewParam();
                List<EtbStockViewObject> temp = new List<EtbStockViewObject>();

                ep.DateOfData_Date.AddParamValue(DateTime.Today);
                ef.Load(temp, ep);

                etbList = temp.ToDictionary(t => t.Symbol, t => t);

                ep = new EtbStockViewParam();
                temp = new List<EtbStockViewObject>();

                ep.DateOfData_Date.AddParamValue(Utils.GetNthBusinessDay(DateTime.Today, -3));
                ef.Load(temp, ep);

                etbT3List = temp.ToDictionary(t => t.Symbol, t => t);

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

            CalcSummary();
        }

        private void MarkETB(ProjectedNeedsSummaryObject p)
        {
            if (etbList.ContainsKey(p.Ticker))
            {
                p.CurrentEtb = true;
            }
            else
            {
                p.CurrentEtb = false;
            }

            if (etbT3List.ContainsKey(p.Ticker))
            {
                p.EtbOnTradeDate3 = true;
            }
            else
            {
                p.EtbOnTradeDate3 = false;
            }
        }

        private void CalcSummary()
        {
            int c = 0;
            int t = 0;
            foreach (ProjectedNeedsSummaryObject p in ProjectedNeeds)
            {
                if (p.OpenNeed > 0)
                {
                    c++;
                    t += (int)p.OpenNeedRounded;
                }
                MarkETB(p);
            }

            txtSummary.Text = "Open Cusips: " + c.ToString("n0") + "   Total Shares Needed: " + t.ToString("n0");
        }

        private void dgvRealTimePosition_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    ProjectedNeedsSummaryObject d = (ProjectedNeedsSummaryObject)dgvRealTimePosition.Rows[e.RowIndex].DataBoundItem;
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

        }

        private void dgvRealTimePosition_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ColorRows();
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void UpdatePositions(RealTimePositionObject pos)
        {

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

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void ViewPositionForm_Shown(object sender, EventArgs e)
        {
            helper.SetAllColumns();
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

            DtcChannel.Instance.handleStockInfoUpdated -= handleStockInfoUpdated;
            DtcChannel.Instance.handleDtcMessageReceived -= handleDtcMessageReceived;
        }

        private void updatePriceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void updateTickerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void investigateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void updateTickerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                ProjectedNeedsSummaryObject ps = (ProjectedNeedsSummaryObject)dgvRealTimePosition.CurrentRow.DataBoundItem;

                RealTimePositionObject rt = new RealTimePositionObject();
                rt.Cusip = ps.Cusip;
                rt.Ticker = ps.Ticker;
                rt.Company = "";
                rt.Price = 0;

                UpdateForm uf = new UpdateForm(rt, "Ticker");
                uf.ShowDialog();
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

                    else if (hti.Type == DataGridViewHitTestType.TopLeftHeader)
                    {
                        dgvRealTimePosition.ContextMenuStrip = cmsExport;
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

        private void tsmiExport_Click_1(object sender, EventArgs e)
        {
            SortableBindingList<ProjectedNeedsSummaryObject> pns = new SortableBindingList<ProjectedNeedsSummaryObject>();

            foreach (DataGridViewRow row in dgvRealTimePosition.Rows)
            {
                ProjectedNeedsSummaryObject p = (ProjectedNeedsSummaryObject)row.DataBoundItem;

                if (p.OpenNeedRounded > 0)
                {
                    pns.Add(p);
                }
            }

            pns.Sort("CUSIP");

            string s = ToHtml(pns);
            ExportToExcel(s);

        }

        private string ToHtml(IList<ProjectedNeedsSummaryObject> data)
        {

            #region microsoft office formats:

            /*
                mso-number-format:"0"     NO Decimals
                mso-number-format:"0\.000"      3 Decimals
                mso-number-format:"\#\,\#\#0\.000"    Comma with 3 dec
                mso-number-format:"mm\/dd\/yy" Date7
                mso-number-format:"mmmm\ d\,\ yyyy"   Date9
                mso-number-format:"m\/d\/yy\ h\:mm\ AM\/PM"       D -T AMPM
                mso-number-format:"Short Date" 01/03/1998
                mso-number-format:"Medium Date"       01-mar-98
                mso-number-format:"d\-mmm\-yyyy"      01-mar-1998
                mso-number-format:"Short Time" 5:16
                mso-number-format:"Medium Time"       5:16 am
                mso-number-format:"Long Time"   5:16:21:00
                mso-number-format:"Percent"     Percent - two decimals
                mso-number-format:"0%"    Percent - no decimals
                mso-number-format:"0\.E+00"     Scientific Notation
                mso-number-format:"\@"    Text
                mso-number-format:"\#\ ???\/???"      Fractions - up to 3 digits (312/943)
                mso-number-format:"\0022£\0022\#\,\#\#0\.00"      £12.76
                mso-number-format:"\#\,\#\#0\.00_ \;\[Red\]\-\#\,\#\#0\.00\ "
                2 decimals, negative numbers in red and signed (1.56   -1.56)
             */

            #endregion

            StringBuilder h = new StringBuilder();

            h.Append(@"<HTML><HEAD><TITLE></TITLE></HEAD><BODY>");
            h.Append(@"<H2 Align=Center></H2>");
            h.Append(@"<H3 Align=Left></H3>");

            h.Append(@"<TABLE  BORDER=1 WIDTH=100% CELLSPACING=1 CELLPADDING=1>");

            //header
            h.Append("<TR>");

            h.Append(@"<TH> Symbol </TH>");
            h.Append(@"<TH> CUSIP </TH>");
            h.Append(@"<TH> NEED </TH>");

            h.Append("</TR>");

            foreach (ProjectedNeedsSummaryObject r in data)
            {
                string row_bgcolor = "";

                row_bgcolor = "bgcolor=#" + ColorTranslator.ToHtml(Color.White);

                h.Append("<TR " + row_bgcolor + ">");

                h.Append(@"<TD style=mso-number-format:\@;>" + r.Ticker + @"</TD>");
                h.Append(@"<TD style=mso-number-format:\@;>" + r.Cusip + @"</TD>");
                h.Append(@"<TD style=mso-number-format:0;>" + r.OpenNeedRounded + @"</TD>");

                h.Append("</TR>");
            }

            h.Append(@"</TABLE>");

            return h.ToString();
        }

        private void ExportToExcel(string txt)
        {
            object objApp_Late;
            object objBook_Late;
            object objBooks_Late;
            object objSheets_Late;
            object objSheet_Late;
            object[] Parameters;

            Type objClassType;
            objClassType = Type.GetTypeFromProgID("Excel.Application");
            objApp_Late = Activator.CreateInstance(objClassType);

            //Get the workbooks collection.
            objBooks_Late = objApp_Late.GetType().InvokeMember("Workbooks",
            BindingFlags.GetProperty, null, objApp_Late, null);

            //Add a new workbook.
            objBook_Late = objBooks_Late.GetType().InvokeMember("Add",
                BindingFlags.InvokeMethod, null, objBooks_Late, null);

            //Get the worksheets collection.
            objSheets_Late = objBook_Late.GetType().InvokeMember("Worksheets",
                BindingFlags.GetProperty, null, objBook_Late, null);

            //Get the first worksheet.
            Parameters = new Object[1];
            Parameters[0] = 1;
            objSheet_Late = objSheets_Late.GetType().InvokeMember("Item",
                BindingFlags.GetProperty, null, objSheets_Late, Parameters);

            //store what was already on the clipboard
            IDataObject t = Clipboard.GetDataObject();

            //Set the clipboard
            Clipboard.SetText(txt);
            Parameters = new Object[2];
            Parameters[0] = Missing.Value;
            Parameters[1] = Missing.Value;
            objSheet_Late.GetType().InvokeMember("Paste",
                BindingFlags.InvokeMethod, null, objSheet_Late, Parameters);


            Parameters = new Object[1];
            Parameters[0] = true;
            objApp_Late.GetType().InvokeMember("Visible", BindingFlags.SetProperty,
                null, objApp_Late, Parameters);

        }

        private void dgvRealTimePosition_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            if (e.ColumnIndex < 0 && e.RowIndex >= 0 && e.RowIndex < dgvRealTimePosition.Rows.Count)
            {
                e.PaintBackground(e.ClipBounds, true);
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), this.Font, Brushes.Black, e.CellBounds, sf);
                e.Handled = true;
            }
        }
    }
}
