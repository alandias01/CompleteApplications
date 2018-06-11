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
    public partial class ReturnByCtpyActivityForm : Form
    {
        private SortableBindingList<ReturnViewObject> Returns = new SortableBindingList<ReturnViewObject>();
        private SortableBindingList<AllocationViewObject> Allocations = new SortableBindingList<AllocationViewObject>();

        private ReturnViewFactory rf = new ReturnViewFactory();
        private AllocationViewFactory af = new AllocationViewFactory();

        private DgvHelpers helper = new DgvHelpers();

        CusipSelectEventHandler handleCusipSelected;

        public ReturnByCtpyActivityForm()
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

        private void btnFilter_Click(object sender, EventArgs e)
        {
            helper.SetAllColumns();

            ReturnViewParam rlp = new ReturnViewParam();
            AllocationViewParam rp = new AllocationViewParam();

            if (!string.IsNullOrEmpty(txtCtpy.Text))
            {
                rlp.CtpyCode.AddParamValue(txtCtpy.Text);
                rp.CtpyCode.AddParamValue(txtCtpy.Text);
            }

            if (!string.IsNullOrEmpty(txtSymbol.Text))
            {
                rlp.Ticker.AddParamValue(txtSymbol.Text);
                rp.Ticker.AddParamValue(txtSymbol.Text);
            }

            //Alan Dias
            if (!string.IsNullOrEmpty(txtTradeCategory.Text))
            {
                rlp.TradeCategory.AddParamValue(txtTradeCategory.Text);
                rp.TradeCategory.AddParamValue(txtTradeCategory.Text);
            }



            rf.Load(Returns, rlp);
            af.Load(Allocations, rp);

            lblReturns.Text = "Returns: " + Returns.Count.ToString();
            lblAllocations.Text = "Allocations: " + Allocations.Count.ToString();
        }

        private void txtCtpy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void txtSymbol_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void txtTradeCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }
    }
}
