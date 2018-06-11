using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Maple.Dtc.Views;
using Maple.Utilities;
using Maple.Utilities.DgvHelper;
using Maple.Global1.G1BizObjects;
using System.Threading;
using Maple.Global1.G1BizObjects.Views;

namespace Maple.Dtc.PositionClient
{
    public partial class CtpySummaryForm : Form
    {
        private List<CtpySummaryViewObject> Positions = new List<CtpySummaryViewObject>();
        private List<IncomingDeliveryOrderObject> Orders = new List<IncomingDeliveryOrderObject>();
        private CtpySummaryViewFactory cpf = new CtpySummaryViewFactory();
        Dictionary<string, CounterpartySummaryObject> Summary = new Dictionary<string, CounterpartySummaryObject>();
        SortableBindingList<CounterpartySummaryObject> Display = new SortableBindingList<CounterpartySummaryObject>();


        IncomingDtcMessageEventHandler handleDtcMessageReceived;

        private DgvHelpers helper = new DgvHelpers();

        public CtpySummaryForm(IList<IncomingDeliveryOrderObject> orders)
        {
            InitializeComponent();

            dgvSummary.DataSource = Summary.Values;

            Orders = new List<IncomingDeliveryOrderObject>(orders);

            helper.Add(new DgvHelper(dgvSummary, this.Name, true));
            helper.handleCellSelected += new CellSelectEventHandler(h_handleCellSelected);

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
                            //Orders.Insert(0, d);
                            //Orders.Sort("DateAndTimeStamp", ListSortDirection.Descending);
                            
                        });
                    }).Start();
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void CtpySummaryForm_Load(object sender, EventArgs e)
        {
            LoadData();

            handleDtcMessageReceived = new IncomingDtcMessageEventHandler(Instance_handleDtcMessageReceived);
            DtcChannel.Instance.handleDtcMessageReceived += handleDtcMessageReceived;
        }

        private void LoadData()
        {
            Summary.Clear();

            CtpySummaryViewParam cp = new CtpySummaryViewParam();
            cp.Book.AddParamValue(Settings.BookName);
            cpf.Load(Positions, cp);

            foreach (CtpySummaryViewObject c in Positions)
            {
                if (!Summary.ContainsKey(c.CounterpartyCode))
                {
                    Summary.Add(c.CounterpartyCode, new CounterpartySummaryObject(c.CounterpartyCode, 0, 0, 0 , 0));
                }

                if (c.BorrowLoanIndicator == "B")
                {
                    Summary[c.CounterpartyCode].Borrowed = c.LoanValue.Value;
                   
                }
                else
                {
                    Summary[c.CounterpartyCode].Loaned = c.LoanValue.Value;
                }
            }

            /*
            foreach (IncomingDeliveryOrderObject d in Orders)
            {
                if (!Summary.ContainsKey(d.CounterParty))
                {
                    Summary.Add(d.CounterParty, new CounterpartySummaryObject(d.CounterParty, 0, 0,0,0));
                }

                if (d.DelivererReceiverIndicator == "R")
                {
                    Summary[d.CounterParty].RealTimeBorrowed += d.MoneyValue.Value;

                }
                else
                {
                    Summary[d.CounterParty].RealTimeLoaned += d.MoneyValue.Value;
                }
            }
            */
            Display = new SortableBindingList<CounterpartySummaryObject>(Summary.Values.ToList());
            dgvSummary.DataSource = Display;
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

        private void G1PositionsForm_FormClosing(object sender, FormClosingEventArgs e)
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

        public class CounterpartySummaryObject
        {
            public string Counterparty { get; set; }
            public double Borrowed { get; set; }
            public double Loaned { get; set; }
            public double Net { get { return Borrowed - Loaned; } }

            public CounterpartySummaryObject(string c, double b, double l, double rb, double rl)
            {
                Counterparty = c;
                Borrowed = b;
                Loaned = l;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
