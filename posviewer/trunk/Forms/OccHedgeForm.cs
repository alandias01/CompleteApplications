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
    public partial class OccHedgeForm : Form, IDgvForm
    {
        private RealTimePositionCalculator calc;

        private SortableBindingList<IncomingDeliveryOrderObject> IncomingOrders = new SortableBindingList<IncomingDeliveryOrderObject>();
        private SortableBindingList<IncomingDeliveryOrderObject> OutgoingOrders = new SortableBindingList<IncomingDeliveryOrderObject>();

        private IncomingDeliveryOrderFactory dof = new IncomingDeliveryOrderFactory();

        private DgvHelpers helper = new DgvHelpers();

        private Color selectedColor = Color.Azure;
        private string selectedCusip = "";
        private Dictionary<string, string> Notes = new Dictionary<string, string>();

        public OccHedgeForm(RealTimePositionCalculator c)
        {
            InitializeComponent();

            calc = c;

            dgvIncoming.DataSource = IncomingOrders;
            dgvOutgoing.DataSource = OutgoingOrders;
            helper.Add(new DgvHelper(dgvIncoming, Name, true));
            helper.Add(new DgvHelper(dgvOutgoing, Name, true));


            helper.handleCellSelected += new CellSelectEventHandler(h_handleCellSelected);

            DtcChannel.Instance.handleDtcMessageReceived += new IncomingDtcMessageEventHandler(DtcMessageReceived);
        }

        private void OccHedgeForm_Load(object sender, EventArgs e)
        {
            try
            {
                //load the data from the table first                
                foreach (IncomingDeliveryOrderObject d in calc.AllDtcActivity)
                {
                    ProcessOrders(d);
                }
                ColorRows();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, TraceEnum.LoggedError);
            }

            foreach (DataGridViewColumn c in dgvIncoming.Columns)
            {
                c.ReadOnly = true;
            }

            //dgvIncoming.Columns["Notes"].ReadOnly = false;

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
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex, TraceEnum.LoggedError);
                    }
                });
            }).Start();
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
                foreach (DataGridViewRow row in dgvIncoming.Rows)
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
                dgvIncoming.Refresh();
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
                /*
                if (calc.StockPrices.ContainsKey(d.Cusip))
                {                    
                    StockPriceViewObject s = null;
                    calc.StockPrices.TryGetValue(d.Cusip, out s);

                    if (s != null)
                    {
                        d.Price = (double)s.Price;
                    }
                    
                }
            */
                if ( (d.DtcStatusIndicator == " " || d.DtcStatusIndicator == "X") &&
                     (d.ReasonCode == "260" || d.ReasonCode == "261" || d.ReasonCode == "270" || d.ReasonCode == "271")   
                   )
                {
                    if (d.MessageSide == DtcMessageSideEnum.Receiver)
                    {
                        IncomingOrders.Add(d);
                    }

                    if (d.MessageSide == DtcMessageSideEnum.Deliverer)
                    {
                        OutgoingOrders.Add(d);
                    }
                    
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

        private void OccHedgeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }

    }
}


