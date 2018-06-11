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
    public partial class RecallActivityForm : Form
    {
        private SortableBindingList<RecalledLotViewObject> RecalledLots = new SortableBindingList<RecalledLotViewObject>();
        private SortableBindingList<RecallViewObject> Recalls = new SortableBindingList<RecallViewObject>();

        private RecallViewFactory rf = new RecallViewFactory();
        private RecalledLotViewFactory rl = new RecalledLotViewFactory();

        private DgvHelpers helper = new DgvHelpers();

        CusipSelectEventHandler handleCusipSelected;

        public RecallActivityForm()
        {
            InitializeComponent();            

            dgvRecalledLots.DataSource = RecalledLots;
            dgvRecalls.DataSource = Recalls;

            helper.Add(new DgvHelper(dgvRecalls, Name, true));
            helper.Add(new DgvHelper(dgvRecalledLots, Name, true));

            handleCusipSelected = new CusipSelectEventHandler(OnCusipSelected);
            DtcChannel.Instance.handleCusipSelected += handleCusipSelected;
        }

        void OnCusipSelected(object sender, CusipSelectEventArgs e)
        {
            helper.SetAllColumns();

            RecalledLotViewParam rlp = new RecalledLotViewParam();
            RecallViewParam rp = new RecallViewParam();

            rlp.Cusip.AddParamValue(e.Cusip);
            rp.Cusip.AddParamValue(e.Cusip);

            rf.Load(Recalls, rp);
            rl.Load(RecalledLots, rlp);

            this.Text = "Recalls " + e.Cusip;
        }

        private void RecallActivityForm_Load(object sender, EventArgs e)
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

        private void RecallActivityForm_FormClosing(object sender, FormClosingEventArgs e)
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
