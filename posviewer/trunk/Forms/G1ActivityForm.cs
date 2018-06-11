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
    public partial class G1ActivityForm : Form
    {
        private List<IncomingDeliveryOrderObject> Orders = new List<IncomingDeliveryOrderObject>();
        private SortableBindingList<IncomingDeliveryOrderObject> Display = new SortableBindingList<IncomingDeliveryOrderObject>();

        private IncomingDeliveryOrderFactory df = new IncomingDeliveryOrderFactory();

        private DgvHelpers helper = new DgvHelpers();


        public G1ActivityForm()
        {
            InitializeComponent();

            dgvDeliveryOrders.DataSource = Display;

            helper.Add(new DgvHelper(dgvDeliveryOrders, this.Name, true));

            helper.handleCellSelected += new CellSelectEventHandler(h_handleCellSelected);

            DtcChannel.Instance.handleDtcMessageReceived += new IncomingDtcMessageEventHandler(Instance_handleDtcMessageReceived);
            DtcChannel.Instance.handleCusipSelected += new CusipSelectEventHandler(OnCusipSelected);
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
                        if (Orders.Count > 0)
                        {
                            if (Orders[0].Cusip == d.Cusip)
                            {
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
        }

        private void DeliveryOrderForms_Load(object sender, EventArgs e)
        {           
            
        }

        private void ColorRows()
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

        private void dgvDeliveryOrders_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ColorRows();
        }

        private void DtcActivityForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }
    
    }
}
