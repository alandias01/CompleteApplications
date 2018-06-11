using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Maple.Utilities.DgvHelper;
using System.Threading;
using System.Diagnostics;
using Maple.Utilities;

namespace Maple.Dtc.PositionClient
{
    public partial class RecentDtcActivityForm : Form
    {
        private SortableBindingList<IncomingDeliveryOrderObject> Orders = new SortableBindingList<IncomingDeliveryOrderObject>();

        private IncomingDeliveryOrderFactory df = new IncomingDeliveryOrderFactory();

        private DgvHelpers helper = new DgvHelpers();

        IncomingDtcMessageEventHandler handleDtcMessageReceived;


        public RecentDtcActivityForm(IList<IncomingDeliveryOrderObject> orders)
        {
            InitializeComponent();

            Orders.Load(orders);

            Orders.Sort("DateAndTimeStamp", ListSortDirection.Descending);

            dgvDeliveryOrders.DataSource = Orders;

            helper.Add(new DgvHelper(dgvDeliveryOrders, this.Name, true));
            helper.handleCellSelected += new CellSelectEventHandler(h_handleCellSelected);


            handleDtcMessageReceived = new IncomingDtcMessageEventHandler(Instance_handleDtcMessageReceived);
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

            if (d.Receiver == Settings.Account.Padded() || d.Deliverer == Settings.Account.Padded())
            {
                try
                {
                    new Thread((ThreadStart)delegate()
                    {
                        this.BeginInvoke((ThreadStart)delegate()
                        {
                            Orders.Insert(0, d);
                            //Orders.Sort("DateAndTimeStamp", ListSortDirection.Descending);
                            dgvDeliveryOrders.Refresh();
                        });
                    }).Start();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex, TraceEnum.LoggedError);
                }
            }
        }

        private void DeliveryOrderForms_Load(object sender, EventArgs e)
        {
            helper.SetAllColumns();            
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

        private void RecentDtcActivityForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
            */

            DtcChannel.Instance.handleDtcMessageReceived -= handleDtcMessageReceived;
        }
    }
}
