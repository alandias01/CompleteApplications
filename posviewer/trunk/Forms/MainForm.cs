using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Outlook = Microsoft.Office.Interop.Outlook;

using Maple.Utilities;
using Maple.Utilities.FormHelper;
using Maple.Utilities.VersioningSystem;

namespace Maple.Dtc.PositionClient
{
    public partial class MainForm : Form
    {
        private RealTimePositionCalculator calc = new RealTimePositionCalculator();
        private FormHelper helper = new FormHelper();

        private bool m_bLayoutCalled = false;

        public MainForm(string account)
        {
            InitializeComponent();


            SetAccount(account);
            SetTitle();
        }

        private void SetAccount(string account)
        {
            switch (account)
            {
                case "269":
                    Settings.Account = "269";
                    Settings.BookName = "MP269";
                    Settings.DatabaseName = "0269";
                    break;

                case "5239":
                    Settings.Account = "5239";
                    Settings.BookName = "FMAI";
                    Settings.DatabaseName = "5239";
                    break;

                case "5289":
                    Settings.Account = "5289";
                    Settings.BookName = "MPUSA.FGN";
                    Settings.DatabaseName = "5289";
                    break;

                case "514":
                    Settings.Account = "514";
                    Settings.BookName = "FMUK";
                    Settings.DatabaseName = "0514";
                    break;

                case "516":
                    Settings.Account = "516";
                    Settings.BookName = "MPFP";
                    Settings.DatabaseName = "0516";
                    break;

                case "518":
                    Settings.Account = "518";
                    Settings.BookName = "NONE";
                    Settings.DatabaseName = "0518";
                    break;

                case "434":
                    Settings.Account = "434";
                    Settings.BookName = "NONE";
                    Settings.DatabaseName = "0434";
                    break;

                case "5072":
                    Settings.Account = "5072";
                    Settings.BookName = "NONE";
                    Settings.DatabaseName = "5072";
                    break;

                default:
                    break;
            }
        }
        
        public void SetTitle()
        {
            this.Text = "";

            for (int i = 0; i < 10; i++)
            {
                this.Text += " " + Settings.Account + " ";
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            /*
            if (Environment.UserName.ToUpper() != "DAVIDJ")
            {
                //check the version of the app
                string msg = "";
                bool vers = VersioningSystem.CheckVersion(out msg);

                if (!vers)
                {
                    SplashScreen.CloseForm();
                    MessageBox.Show(msg + "\r\n\r\nPlease contact IT IMMEDIATELY", "Version Check", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    Trace.WriteLine(Environment.UserName + " is using an old version of the Real Time Position Viewer", TraceEnum.LoggedError);
                }
            }
            */
            InitializeScreens();
        }

        private void InitializeScreens()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                foreach (Form f in this.MdiChildren)
                {
                    f.Close();
                }

                bool loadedLayout = helper.LoadLayout(Settings.Account);
                FormHelperObject fh = null;

                calc.Clear();

                calc.CalculateStartingPosition(DateTime.Today);

                ViewPositionForm v = new ViewPositionForm(calc);
                v.MdiParent = this;
                v.StartPosition = FormStartPosition.Manual;

                fh = helper.FirstOrDefault(c => c.FormName == "ViewPositionForm");
                 if (fh != null && loadedLayout)
                 {
                     v.Location = new Point(fh.X, fh.Y);
                     v.Size = new Size(fh.Width, fh.Height);
                 }
                 else
                 {
                     
                     v.Location = new Point(0, 0);
                 }
                
                v.Show();
                helper.Add(new FormHelperObject(v));
                v.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);

                fh = helper.FirstOrDefault(c=> c.FormName == "RecentDtcActivityForm");
                if (fh != null && loadedLayout) 
                {
                    RecentDtcActivityForm re = new RecentDtcActivityForm(calc.DtcActivity);
                    re.MdiParent = this;
                    re.StartPosition = FormStartPosition.Manual;
                    re.Location = new Point(fh.X, fh.Y);
                    re.Size = new Size(fh.Width, fh.Height);
                    re.Show();
                    helper.Add(new FormHelperObject(re));
                    re.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);    
                }

                fh = helper.FirstOrDefault(c => c.FormName == "DtcActivityForm");
                if (fh != null && loadedLayout) 
                 {
                     DtcActivityForm d = new DtcActivityForm();
                     d.MdiParent = this;
                     d.StartPosition = FormStartPosition.Manual;
                     d.Location = new Point(fh.X, fh.Y);
                     d.Size = new Size(fh.Width, fh.Height);
                     d.Show();
                     helper.Add(new FormHelperObject(d));
                     d.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
                 }

                 fh = helper.FirstOrDefault(c => c.FormName == "ReturnActivityForm");
                 if (fh != null && loadedLayout) 
                 {
                     ReturnActivityForm r = new ReturnActivityForm();
                     r.MdiParent = this;
                     r.StartPosition = FormStartPosition.Manual;
                     r.Location = new Point(fh.X, fh.Y);
                     r.Size = new Size(fh.Width, fh.Height);
                     r.Show();
                     helper.Add(new FormHelperObject(r));
                     r.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
                 }

                 fh = helper.FirstOrDefault(c => c.FormName == "RecallActivityForm");
                 if (fh != null && loadedLayout) 
                 {
                     RecallActivityForm ra = new RecallActivityForm();
                     ra.MdiParent = this;
                     ra.StartPosition = FormStartPosition.Manual;
                     ra.Location = new Point(fh.X, fh.Y);
                     ra.Size = new Size(fh.Width, fh.Height);
                     ra.Show();
                     helper.Add(new FormHelperObject(ra));
                     ra.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
                 }


                 fh = helper.FirstOrDefault(c => c.FormName == "G1PositionsForm");
                 if (fh != null && loadedLayout) 
                 {
                     G1PositionsForm g = new G1PositionsForm();
                     g.MdiParent = this;
                     g.StartPosition = FormStartPosition.Manual;
                     g.Location = new Point(fh.X, fh.Y);
                     g.Size = new Size(fh.Width, fh.Height);
                     g.Show();
                     helper.Add(new FormHelperObject(g));
                     g.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
                 }

                 fh = helper.FirstOrDefault(c => c.FormName == "NsccProjectedNeedsForm");
                 if (fh != null && loadedLayout) 
                 {
                     NsccProjectedNeedsForm ns = new NsccProjectedNeedsForm(calc);
                     ns.MdiParent = this;
                     ns.StartPosition = FormStartPosition.Manual;
                     ns.Location = new Point(fh.X, fh.Y);
                     ns.Size = new Size(fh.Width, fh.Height); ns.Show();
                     helper.Add(new FormHelperObject(ns));
                     ns.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
                 }

                 fh = helper.FirstOrDefault(c => c.FormName == "PriceControlForm");
                 if (fh != null && loadedLayout) 
                 {
                     PriceControlForm pc = new PriceControlForm(calc);
                     pc.MdiParent = this;
                     pc.StartPosition = FormStartPosition.Manual;
                     pc.Location = new Point(fh.X, fh.Y);
                     pc.Size = new Size(fh.Width, fh.Height);
                     pc.Show();
                     helper.Add(new FormHelperObject(pc));
                     pc.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
                 }

                 fh = helper.FirstOrDefault(c => c.FormName == "CtpySummaryForm");
                 if (fh != null && loadedLayout) 
                 {
                     CtpySummaryForm csf = new CtpySummaryForm(calc.DtcActivity);
                     csf.MdiParent = this;
                     csf.StartPosition = FormStartPosition.Manual;
                     csf.Location = new Point(fh.X, fh.Y);
                     csf.Size = new Size(fh.Width, fh.Height);
                     csf.Show();
                     helper.Add(new FormHelperObject(csf));
                     csf.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
                 }

                 fh = helper.FirstOrDefault(c => c.FormName == "G1HistoricalForm");
                 if (fh != null && loadedLayout) 
                 {
                     G1HistoricalForm g1f = new G1HistoricalForm();
                     g1f.MdiParent = this;
                     g1f.StartPosition = FormStartPosition.Manual;
                     g1f.Location = new Point(fh.X, fh.Y);
                     g1f.Size = new Size(fh.Width, fh.Height);
                     g1f.Show();
                     helper.Add(new FormHelperObject(g1f));
                     g1f.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
                 }


                foreach (Form f in this.MdiChildren)
                {
                    if (helper.Count(c=> c.FormName == f.Name) == 0)
                    {
                        f.Close();
                    }
                }

                /*
                if (helper.LoadLayout(Settings.Account))
                {
                 
                    foreach (FormHelperObject f in helper)
                    {
                        Type t = Type.GetType("PositionClient." + f.FormName);
                        object r = Activator.CreateInstance(t);

                        ((Form)r).MdiParent = this;
                        ((Form)r).StartPosition = FormStartPosition.Manual;
                        ((Form)r).Location = new Point(f.X, f.Y);
                        ((Form)r).Width = f.Width;
                        ((Form)r).Height = f.Height;

                        ((Form)r).Show();
                    }
                }
                */

                /* 
                */

                /*
                HedgeCashForm hcf = new HedgeCashForm(calc);
                hcf.MdiParent = this;
                hcf.StartPosition = FormStartPosition.Manual;
                hcf.Location = new Point(0, ra.Location.Y + ra.Height + 2);
                hcf.Show();
                helper.Add(new FormHelperObject(hcf));
                hcf.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
                */

                //                helper.LoadLayout();
            }
            catch (Exception ex)
            {
                SplashScreen.CloseForm();
                Trace.WriteLine(ex, TraceEnum.LoggedError);
                MessageBox.Show(ex.ToString(), "Error");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        void v_handleCellSelected(object sender, Maple.Utilities.DgvHelper.CellSelectEventArgs e)
        {
            double d = 0;
            try
            {
                d = double.Parse(e.CellValue.ToString());
            }
            catch (Exception ex)
            {
            }

            if (d < Int64.MaxValue && (Int64)d != d)
            {
                txtTotal.Text = d.ToString("n2");
            }
            else
            {
                txtTotal.Text = d.ToString("n0");
            }
        }

        private bool CheckIfFormOpen(Type form)
        {
            bool ret = false;

            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == form)
                {
                    try
                    {
                        f.Focus();
                        if (f.WindowState == FormWindowState.Minimized)
                        {
                            f.WindowState = FormWindowState.Normal;
                        }
                    }
                    catch (Exception)
                    {
                    }
                    ret = true;
                    break;
                }
            }
            return ret;
        }

        private void tsmPosition_Click(object sender, EventArgs e)
        {
            if (!CheckIfFormOpen(typeof(ViewPositionForm)))
            {
                ViewPositionForm v = new ViewPositionForm(calc);
                v.MdiParent = this;
                v.StartPosition = FormStartPosition.Manual;
                v.Location = new Point(0, 0);
                v.Show();

                v.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
            }
        }

        private void tsmResearch_Click(object sender, EventArgs e)
        {
            if (!CheckIfFormOpen(typeof(G1HistoricalForm)))
            {
                G1HistoricalForm re = new G1HistoricalForm();
                re.MdiParent = this;
                re.StartPosition = FormStartPosition.Manual;
                re.Show();

                re.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
            }
        }

        private void tsmDtcActivity_Click(object sender, EventArgs e)
        {
            if (!CheckIfFormOpen(typeof(RecentDtcActivityForm)))
            {
                RecentDtcActivityForm re = new RecentDtcActivityForm(calc.DtcActivity);
                re.MdiParent = this;
                re.StartPosition = FormStartPosition.Manual;
                re.Show();

                re.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
            }
        }

        private void tsmRecalls_Click(object sender, EventArgs e)
        {
            if (!CheckIfFormOpen(typeof(RecallActivityForm)))
            {
                RecallActivityForm ra = new RecallActivityForm();
                ra.MdiParent = this;
                ra.StartPosition = FormStartPosition.Manual;
                ra.Show();

                ra.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
            }
        }

        private void tsmCusipDtcActivity_Click(object sender, EventArgs e)
        {
            if (!CheckIfFormOpen(typeof(DtcActivityForm)))
            {
                DtcActivityForm d = new DtcActivityForm();
                d.MdiParent = this;
                d.StartPosition = FormStartPosition.Manual;
                d.Show();

                d.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
            }
        }

        private void tsmReturns_Click(object sender, EventArgs e)
        {
            if (!CheckIfFormOpen(typeof(ReturnActivityForm)))
            {
                ReturnActivityForm r = new ReturnActivityForm();
                r.MdiParent = this;
                r.StartPosition = FormStartPosition.Manual;
                r.Show();

                r.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
            }

            if (!CheckIfFormOpen(typeof(ReturnByCtpyActivityForm)))
            {
                ReturnByCtpyActivityForm r = new ReturnByCtpyActivityForm();
                r.MdiParent = this;
                r.StartPosition = FormStartPosition.CenterScreen;                
                r.Show();

                r.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
            }
        }

        private void tsmPositions_Click(object sender, EventArgs e)
        {
            if (!CheckIfFormOpen(typeof(G1PositionsForm)))
            {
                G1PositionsForm g = new G1PositionsForm();
                g.MdiParent = this;
                g.StartPosition = FormStartPosition.Manual;
                g.Show();

                g.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (m_bLayoutCalled == false)
            {
                m_bLayoutCalled = true;


                //                if( SplashScreen.SplashForm != null )
                //					SplashScreen.SplashForm.Owner = this;

                this.Activate();
                SplashScreen.CloseForm();
            }
        }

        private void tsmiSaveLayout_Click(object sender, EventArgs e)
        {
            helper.Clear();

            this.MdiChildren.ToList().ForEach(f=> helper.Add(new FormHelperObject(f)));
            
            helper.SaveLayout(Settings.Account);
        }

        private void tsmiLoadLayout_Click(object sender, EventArgs e)
        {
            //            helper.LoadLayout();
        }

        private void tsmPriceControl_Click(object sender, EventArgs e)
        {
            if (!CheckIfFormOpen(typeof(PriceControlForm)))
            {
                PriceControlForm r = new PriceControlForm(calc);
                r.MdiParent = this;
                r.StartPosition = FormStartPosition.Manual;
                r.Show();

                r.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
            }
        }

        private void tsmOccHedge_Click(object sender, EventArgs e)
        {
            if (!CheckIfFormOpen(typeof(OccHedgeForm)))
            {
                OccHedgePositionForm r = new OccHedgePositionForm(calc);
                r.MdiParent = this;
                r.StartPosition = FormStartPosition.Manual;
                r.Show();

                r.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
            }
        }

        private void tsmProjectedNeeds_Click(object sender, EventArgs e)
        {
            if (!CheckIfFormOpen(typeof(NsccProjectedNeedsForm)))
            {
                NsccProjectedNeedsForm r = new NsccProjectedNeedsForm(calc);
                r.MdiParent = this;
                r.StartPosition = FormStartPosition.Manual;
                r.Show();

                r.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
            }
        }

        private void tsmi269_Click(object sender, EventArgs e)
        {
            SetAccount("269");
            InitializeScreens();
            SetTitle();
        }

        private void tsmi5239_Click(object sender, EventArgs e)
        {
            SetAccount("5239");
            InitializeScreens();
            SetTitle();
        }

        private void tsmi5289_Click(object sender, EventArgs e)
        {
            SetAccount("5289");
            InitializeScreens();
            SetTitle();
        }

        private void tsmi514_Click(object sender, EventArgs e)
        {
            SetAccount("514");
            InitializeScreens();
            SetTitle();
        }

        private void tsmi516_Click(object sender, EventArgs e)
        {
            SetAccount("516");
            InitializeScreens();
            SetTitle();
        }

        private void tsmi518_Click(object sender, EventArgs e)
        {
            SetAccount("518");
            InitializeScreens();
            SetTitle();
        }

        private void tsmi434_Click(object sender, EventArgs e)
        {
            SetAccount("434");

            InitializeScreens();
            SetTitle();
        }

        private void tsmCtpySummary_Click(object sender, EventArgs e)
        {
            if (!CheckIfFormOpen(typeof(CtpySummaryForm)))
            {
                CtpySummaryForm v = new CtpySummaryForm(calc.DtcActivity);
                v.MdiParent = this;
                v.StartPosition = FormStartPosition.Manual;
                v.Location = new Point(0, 0);
                v.Show();

                v.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
            }

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SetAccount("5072");

            InitializeScreens();
            SetTitle();
        }

        private void tsmPnLSummary_Click(object sender, EventArgs e)
        {
            if (!CheckIfFormOpen(typeof(PnlSummaryForm)))
            {
                PnlSummaryForm v = new PnlSummaryForm();
                v.MdiParent = this;
                v.StartPosition = FormStartPosition.Manual;
                v.Location = new Point(0, 0);
                v.Show();

                v.handleCellSelected += new Maple.Utilities.DgvHelper.CellSelectEventHandler(v_handleCellSelected);
            }
        }
    }
}
