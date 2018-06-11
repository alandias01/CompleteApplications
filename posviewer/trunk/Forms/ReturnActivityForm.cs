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

namespace Maple.Dtc.PositionClient
{
    public partial class ReturnActivityForm : Form
    {
        private SortableBindingList<ReturnViewObject> Returns = new SortableBindingList<ReturnViewObject>();
        private SortableBindingList<AllocationViewObject> Allocations = new SortableBindingList<AllocationViewObject>();

        private ReturnViewFactory rf = new ReturnViewFactory();
        private AllocationViewFactory af = new AllocationViewFactory();

        private DgvHelpers helper = new DgvHelpers();

        CusipSelectEventHandler handleCusipSelected;

        public ReturnActivityForm()
        {
            InitializeComponent();
                        
            dgvAllocations.DataSource = Allocations ;
            dgvReturns.DataSource = Returns;

            helper.Add(new DgvHelper(dgvReturns, Name, true));
            helper.Add(new DgvHelper(dgvAllocations, Name, true));
            helper.handleCellSelected += new CellSelectEventHandler(h_handleCellSelected);

            handleCusipSelected = new CusipSelectEventHandler(OnCusipSelected);
            DtcChannel.Instance.handleCusipSelected += handleCusipSelected;
        }

        void OnCusipSelected(object sender, CusipSelectEventArgs e)
        {
            helper.SetAllColumns();

            ReturnViewParam rlp = new ReturnViewParam();
            AllocationViewParam rp = new AllocationViewParam();

            rlp.Cusip.AddParamValue(e.Cusip);
            rp.Cusip.AddParamValue(e.Cusip);

            rf.Load(Returns, rlp);
            af.Load(Allocations, rp);

            this.Text = "Returns " + e.Cusip;
        }

        private void ReturnActivityForm_Load(object sender, EventArgs e)
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

        private void ReturnActivityForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
            */

            DtcChannel.Instance.handleCusipSelected -= handleCusipSelected;
        }
    }
}
