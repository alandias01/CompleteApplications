using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Maple.Utilities;
using Maple.Utilities.DgvHelper;
using System.Threading;
using System.Diagnostics;

namespace Maple.Dtc.PositionClient
{
    public partial class DtcActivityForm : Form
    {
        private List<IncomingDeliveryOrderObject> Orders = new List<IncomingDeliveryOrderObject>();
        private SortableBindingList<IncomingDeliveryOrderObject> Display = new SortableBindingList<IncomingDeliveryOrderObject>();

        private IncomingDeliveryOrderFactory df = new IncomingDeliveryOrderFactory();

        private DgvHelpers helper = new DgvHelpers();

        IncomingDtcMessageEventHandler handleDtcMessageReceived;
        CusipSelectEventHandler handleCusipSelected;
        DgvHelper DGVDel;

        //ALANDIAS
        SortableBindingList<IncomingDeliveryOrderObject> DisplayWOUnecessaryPendings = new SortableBindingList<IncomingDeliveryOrderObject>();
        //------------


        public DtcActivityForm()
        {
            InitializeComponent();

            dgvDeliveryOrders.DataSource = Display;
            
            DGVDel = new DgvHelper(dgvDeliveryOrders, this.Name, true);

            helper.Add(DGVDel);

            helper.handleCellSelected += new CellSelectEventHandler(h_handleCellSelected);


            handleDtcMessageReceived = new IncomingDtcMessageEventHandler(Instance_handleDtcMessageReceived);
            handleCusipSelected = new CusipSelectEventHandler(OnCusipSelected);

            DtcChannel.Instance.handleDtcMessageReceived += handleDtcMessageReceived;
            DtcChannel.Instance.handleCusipSelected += handleCusipSelected;            
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

            if (d.Receiver == Settings.Account.Padded() || d.Deliverer == Settings.Account.Padded())
            {
                try
                {
                    new Thread((ThreadStart)delegate()
                    {
                        this.BeginInvoke((ThreadStart)delegate()
                        {
                            try
                            {
                                if (Orders.Count > 0)
                                {
                                    if (Orders[0].Cusip == d.Cusip)
                                    {
                                        //ALANDIAS
                                        RemoveCorrespondingPendingIfIncomingOrderIsAMadeAterPending(d);
                                        //--------


                                        Orders.Add(d);
                                        Display.Add(d);
                                        ColorRows();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Trace.WriteLine(ex, TraceEnum.LoggedError);
                            }
                        });
                    }).Start();

                }
                finally
                {

                }
            }
            
        }
        
        void OnCusipSelected(object sender, CusipSelectEventArgs e)
        {
            IncomingDeliveryOrderParam p = new IncomingDeliveryOrderParam();
            p.DateAndTimeStamp_Date.AddParamValue(DateTime.Today);
            p.Cusip.AddParamValue(e.Cusip);
            
            df.Load(Orders, p);

            //Remove records that are receives but 5239 is not the receiver, same for deliver
            Orders.RemoveAll(delegate(IncomingDeliveryOrderObject d)
            { return (d.Receiver != Settings.Account.Padded() && d.DelivererReceiverIndicator == "R") || (d.Deliverer != Settings.Account.Padded() && d.DelivererReceiverIndicator == "D"); });

            Display.Load(Orders);

            Display.Sort("DateAndTimeStamp");

            ColorRows();

            this.Text = "DTC Activity " + e.Cusip;

            //ALANDIAS
            cbPendingFilter_CheckedChanged(null, null);
            //--------
        }

        private void DeliveryOrderForms_Load(object sender, EventArgs e)
        {           
            
        }

        private void ColorRows()
        {
            foreach (DataGridViewRow row in dgvDeliveryOrders.Rows)
            {
                IncomingDeliveryOrderObject i = (IncomingDeliveryOrderObject)row.DataBoundItem;
                
                if (i.DeliveryOrderDirection == DeliveryOrderDirectionEnum.Borrow)
                {
                    if (i.Status == DtcMessageStatusEnum.Made || i.Status == DtcMessageStatusEnum.MadeAfterPend)
                    {
                        row.DefaultCellStyle.BackColor = txtMadeBorrow.BackColor;                        
                    }

                    else if (i.Status == DtcMessageStatusEnum.Pending)
                    {
                        row.DefaultCellStyle.BackColor = txtPendingBorrow.BackColor;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
                else if (i.DeliveryOrderDirection == DeliveryOrderDirectionEnum.BorrowReclaimed)
                {
                    row.DefaultCellStyle.BackColor = txtReclaimedBorrows.BackColor;
                }

                else if (i.DeliveryOrderDirection == DeliveryOrderDirectionEnum.Loan)
                {
                    if (i.Status == DtcMessageStatusEnum.Made || i.Status == DtcMessageStatusEnum.MadeAfterPend)
                    {
                        row.DefaultCellStyle.BackColor = txtMadeLoan.BackColor;                        
                    }

                    else if (i.Status == DtcMessageStatusEnum.Pending)
                    {
                        row.DefaultCellStyle.BackColor = txtPendingLoan.BackColor;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }

                else if (i.DeliveryOrderDirection == DeliveryOrderDirectionEnum.LoanReclaimed)
                {
                    row.DefaultCellStyle.BackColor = txtReclaimedLoans.BackColor;
                }

                else if (i.DeliveryOrderDirection == DeliveryOrderDirectionEnum.ReturnIn)
                {
                    if (i.Status == DtcMessageStatusEnum.Made || i.Status == DtcMessageStatusEnum.MadeAfterPend)
                    {
                        row.DefaultCellStyle.BackColor = txtMadeRetunIn.BackColor;                        
                    }

                    else if (i.Status == DtcMessageStatusEnum.Pending)
                    {
                        row.DefaultCellStyle.BackColor = txtPendingReturnIn.BackColor;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }

                else if (i.DeliveryOrderDirection == DeliveryOrderDirectionEnum.ReturnInReclaimed)
                {
                    row.DefaultCellStyle.BackColor = txtReclaimedReturnsIn.BackColor;
                }

                else if (i.DeliveryOrderDirection == DeliveryOrderDirectionEnum.ReturnOut)
                {
                    if (i.Status == DtcMessageStatusEnum.Made || i.Status == DtcMessageStatusEnum.MadeAfterPend)
                    {
                        row.DefaultCellStyle.BackColor = txtMadeReturnOut.BackColor;                        
                    }

                    else if (i.Status == DtcMessageStatusEnum.Pending)
                    {
                        row.DefaultCellStyle.BackColor = txtPendingReturnOut.BackColor;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }

                else if (i.DeliveryOrderDirection == DeliveryOrderDirectionEnum.ReturnOutReclaimed)
                {
                    row.DefaultCellStyle.BackColor = txtReclaimedReturnsOut.BackColor;
                }   
      
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
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

        private void dgvDeliveryOrders_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ColorRows();
        }

        private void DtcActivityForm_FormClosing(object sender, FormClosingEventArgs e)
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
        }





        //ALANDIAS-----------------------------------------------------------------------
        
        private void cbPendingFilter_CheckedChanged(object sender, EventArgs e)
        {            
            if (cbPendingFilter.Checked)
            {
                var Pendings = Display.Where(x => x.Status == DtcMessageStatusEnum.Pending).ToList();
                var MadeAfterPendings = Display.Where(x => x.Status == DtcMessageStatusEnum.MadeAfterPend).ToList();
                
                
                DisplayWOUnecessaryPendings.Load(Display);

                foreach (var P in Pendings)
                {
                    foreach (var MAP in MadeAfterPendings)
                    {
                        if (P.Cusip == MAP.Cusip && P.CounterParty == MAP.CounterParty && P.ShareQuantity == MAP.ShareQuantity && P.DelivererAccountNum == MAP.DelivererAccountNum)
                        {
                            DisplayWOUnecessaryPendings.Remove(P);
                            break;
                        }

                    }
                }
                dgvDeliveryOrders.DataSource = DisplayWOUnecessaryPendings;
                ColorRows();
                DGVDel.SetColumns();
            }
            else
            {
                dgvDeliveryOrders.DataSource = Display;
                ColorRows();
                DGVDel.SetColumns();
            }

        }

        private void RemoveCorrespondingPendingIfIncomingOrderIsAMadeAterPending(IncomingDeliveryOrderObject d)
        {
            if (cbPendingFilter.Checked)
            {
                DisplayWOUnecessaryPendings.Add(d);

                if (d.Status == DtcMessageStatusEnum.MadeAfterPend)
                {
                    var Pendings = DisplayWOUnecessaryPendings.Where(x => x.Status == DtcMessageStatusEnum.Pending).ToList();

                    foreach (var P in Pendings)
                    {
                        if (P.Cusip == d.Cusip && P.CounterParty == d.CounterParty && P.ShareQuantity == d.ShareQuantity && P.DelivererAccountNum == d.DelivererAccountNum)
                        {
                            DisplayWOUnecessaryPendings.Remove(P);
                            break;
                        }
                    }

                }
            }            
            
        }

        //ALANDIAS----END----------------------------------------------------------------------------
    
    }
}
