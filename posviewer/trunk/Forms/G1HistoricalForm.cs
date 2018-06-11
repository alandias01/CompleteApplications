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
using Maple.Global1.G1BizObjects.Views;

namespace Maple.Dtc.PositionClient
{
    public partial class G1HistoricalForm : Form
    {
        private SortableBindingList<HistoricalPositionViewObject> Positions = new SortableBindingList<HistoricalPositionViewObject>();
        private HistoricalPositionViewFactory cpf = new HistoricalPositionViewFactory();
        private DgvHelpers helper = new DgvHelpers();

        public G1HistoricalForm()
        {
            InitializeComponent();

            dgvSummary.DataSource = Positions;

            helper.Add(new DgvHelper(dgvSummary, this.Name, true));
            helper.handleCellSelected += new CellSelectEventHandler(h_handleCellSelected);
        }

        private void CtpySummaryForm_Load(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;
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
        }
        
        private void btnFind_Click(object sender, EventArgs e)
        {
//            if (string.IsNullOrEmpty(txtCusip.Text) && string.IsNullOrEmpty(txtTicker.Text) && string.IsNullOrEmpty(txtCtpy.Text))
//            {
//                MessageBox.Show("Please enter a cusip, ticker, or ctpy");
//                return;
//            }
            try
            {
                Cursor = Cursors.WaitCursor;

                HistoricalPositionViewParam cp = new HistoricalPositionViewParam();
                cp.Book.AddParamValue(Settings.BookName);

                if (!string.IsNullOrEmpty(txtCusip.Text))
                {
                    cp.Cusip.AddParamValue(txtCusip.Text);
                }
                if (!string.IsNullOrEmpty(txtTicker.Text))
                {
                    cp.Ticker.AddParamValue(txtTicker.Text);
                }

                if (!string.IsNullOrEmpty(txtCtpy.Text))
                {
                    cp.CounterpartyCode.AddParamValue(txtCtpy.Text);
                }

                cp.FileDate_DateStart.AddParamValue(dtpStartDate.Value);
                cp.FileDate_DateEnd.AddParamValue(dtpEndDate.Value);
                cpf.Load(Positions, cp);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void txtTicker_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }
    }
}
