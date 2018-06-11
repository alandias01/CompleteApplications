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

namespace Maple.Dtc.PositionClient
{
    public partial class PnlSummaryForm : Form
    {
        private DgvHelpers helper = new DgvHelpers();

        SortableBindingList<PnlObject> pnl = new SortableBindingList<PnlObject>();


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


        public PnlSummaryForm()
        {
            InitializeComponent();

            helper.Add(new DgvHelper(dgSummary, Name, true));
            helper.handleCellSelected += new CellSelectEventHandler(h_handleCellSelected);
        }

        private void PnlSummaryForm_Load(object sender, EventArgs e)
        {
            SP.GetPnLSummaryByTicker(pnl);

            dgSummary.DataSource = pnl;

            //ColorRows();
        }

        private void ColorRows()
        {
            foreach (DataGridViewRow r in dgSummary.Rows)
            {
                r.Cells["Profit"].Style.BackColor = Color.LightGreen;
            }
        }

        private void FormatColumns()
        {
            try
            {
                dgSummary.Columns["Profit"].DefaultCellStyle.Format = "$###,###,###";
                dgSummary.Columns["BorrowQty"].DefaultCellStyle.Format = "###,###,###";
                dgSummary.Columns["LoanQty"].DefaultCellStyle.Format = "###,###,###";
                dgSummary.Columns["BorrowCash"].DefaultCellStyle.Format = "###,###,###";
                dgSummary.Columns["LoanCash"].DefaultCellStyle.Format = "###,###,###";

            }
            catch (Exception ex)
            {

            }
        }

        private void dgSummary_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ColorRows();
            FormatColumns();
        }

    }
}
