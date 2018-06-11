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

namespace Maple.Dtc.PositionClient
{
    public partial class G1PositionsForm : Form
    {
        private SortableBindingList<G1PositionObject> Borrows = new SortableBindingList<G1PositionObject>();
        private SortableBindingList<G1PositionObject> Loans = new SortableBindingList<G1PositionObject>();

        private G1PositionFactory af = new G1PositionFactory();

        private DgvHelpers helper = new DgvHelpers();

        CusipSelectEventHandler handleCusipSelected;

        public G1PositionsForm()
        {
            InitializeComponent();

            dgvBorrows.DataSource = Borrows;
            dgvLoans.DataSource = Loans;

            helper.Add(new DgvHelper(dgvBorrows, this.Name, true));
            helper.Add(new DgvHelper(dgvLoans, this.Name, true));

            helper.handleCellSelected += new CellSelectEventHandler(h_handleCellSelected);

            handleCusipSelected = new CusipSelectEventHandler(OnCusipSelected);
            DtcChannel.Instance.handleCusipSelected += handleCusipSelected;
        
        }

        void OnCusipSelected(object sender, CusipSelectEventArgs e)
        {
            G1PositionParam pb = new G1PositionParam();
            G1PositionParam pl = new G1PositionParam();

            pb.Cusip.AddParamValue(e.Cusip);
            pb.ClearingNo.AddParamValue(Settings.Account);
            pb.BorrowLoan.AddParamValue("B");

            pl.Cusip.AddParamValue(e.Cusip);
            pl.ClearingNo.AddParamValue(Settings.Account);
            pl.BorrowLoan.AddParamValue("L");

            af.Load(Borrows, pb);
            af.Load(Loans, pl);

            this.Text = "G1 Positions " + e.Cusip;
        }

        private void G1PositionsForm_Load(object sender, EventArgs e)
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

        private void G1PositionsForm_FormClosing(object sender, FormClosingEventArgs e)
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
