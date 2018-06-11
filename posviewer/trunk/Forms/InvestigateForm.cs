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
    public partial class InvestigateForm : Form
    {
        private RealTimePositionObject rtPos;
        private SortableBindingList<RealTimePositionObject> position = new SortableBindingList<RealTimePositionObject>();

        private DgvHelpers helper = new DgvHelpers();

        public InvestigateForm(RealTimePositionObject rt)
        {
            InitializeComponent();

            rtPos = rt;

            position.Add(rtPos);

            dgvActivity.DataSource = rtPos.DtcActivity;
            dgvPosition.DataSource = position;

            helper.Add(new DgvHelper(dgvActivity, this.Name, true));
            helper.Add(new DgvHelper(dgvPosition, this.Name, true));

            helper.handleCellSelected += new CellSelectEventHandler(h_handleCellSelected);
        }

        private void InvestigateForm_Load(object sender, EventArgs e)
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
    }
}
