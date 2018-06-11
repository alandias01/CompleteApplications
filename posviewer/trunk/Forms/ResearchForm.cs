using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Maple.Dtc;
using Maple.Utilities;
using Maple.Utilities.DgvHelper;

namespace Maple.Dtc.PositionClient
{
    /*
    cusip
    Reason code 
    direction (payor / payee) (receive / deliver)
    time
    shares
    value
    status (made / pend / etc...)
    ctpy
    */
    public partial class ResearchForm : Form
    {
        private IncomingDeliveryOrderFactory doFact = new IncomingDeliveryOrderFactory();
        private SortableBindingList<IncomingDeliveryOrderObject> orders = new SortableBindingList<IncomingDeliveryOrderObject>();

        private DgvHelpers helper = new DgvHelpers();

        public ResearchForm()
        {
            InitializeComponent();
            dgvDeliveryOrders.DataSource = orders;

            helper.Add(new DgvHelper(dgvDeliveryOrders, Name, true, txtTotal));

            cbxMapleCtpy.Items.Add("269");
            cbxMapleCtpy.Items.Add("5239");

            cbxMapleCtpy.SelectedIndex = 1;
            
            cbxMoneyCriteria.Items.Add("=");
            cbxMoneyCriteria.Items.Add("!=");
            cbxMoneyCriteria.Items.Add(">=");
            cbxMoneyCriteria.Items.Add("<=");
            cbxMoneyCriteria.SelectedIndex = 0;

            cbxSharesCriteria.Items.Add("=");
            cbxSharesCriteria.Items.Add("!=");            
            cbxSharesCriteria.Items.Add(">=");
            cbxSharesCriteria.Items.Add("<=");
            cbxSharesCriteria.SelectedIndex = 0;

            cbxTimeCriteria.Items.Add("=");
            cbxTimeCriteria.Items.Add("!=");
            cbxTimeCriteria.Items.Add(">=");
            cbxTimeCriteria.Items.Add("<=");
            cbxTimeCriteria.SelectedIndex = 0;


            clbStatus.Items.Add("Made");
            clbStatus.Items.Add("Pending");
            clbStatus.Items.Add("Made after Pending");
            clbStatus.Items.Add("CNS Deliver DO Drop");
            clbStatus.Items.Add("SDFS Prevent-Pend Drop");
            clbStatus.Items.Add("Killed");
            clbStatus.Items.Add("Repended");
            

            cbxDirection.Items.Add("Deliverer");
            cbxDirection.Items.Add("Receiver");            
        }

        private void ResearchForm_Load(object sender, EventArgs e)
        {
            helper.SetAllColumns();

            IncomingDeliveryOrderParam p = new IncomingDeliveryOrderParam();

            p.DateAndTimeStamp_Date.AddParamValue(DateTime.Today);

            p.ReasonCode.AddParamValue("010", "<>", ConditionalEnum.AND);
            p.ReasonCode.AddParamValue("020", "<>", ConditionalEnum.AND);

                        
            doFact.Load(orders, p, "00005239", null);
            lblCount.Text = orders.Count.ToString();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            helper.SaveAllLayouts();
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13 )
            {
                Filter();
            }
        }

        private void Filter()
        {
            IncomingDeliveryOrderParam p = new IncomingDeliveryOrderParam();
            string maple = null;
            string ctpy = null;

            p.DateAndTimeStamp_Date.AddParamValue(dtpDate.Value.Date);

            if (!string.IsNullOrEmpty(cbxMapleCtpy.Text))
            {
                maple = cbxMapleCtpy.Text.ToString();
                maple = maple.PadLeft(8, '0');
            }

            if (!string.IsNullOrEmpty(txtCtpy.Text))
            {
                ctpy = txtCtpy.Text.ToString();
                ctpy = ctpy.PadLeft(8, '0');
            }
            
            if (!string.IsNullOrEmpty(txtHour.Text) && !string.IsNullOrEmpty(txtMinute.Text))
            {
                DateTime t = dtpDate.Value.Date.AddHours(double.Parse(txtHour.Text)).AddMinutes(double.Parse(txtMinute.Text));
                p.DateAndTimeStamp.AddParamValue(t);
                p.DateAndTimeStamp.Comparer = cbxTimeCriteria.SelectedItem.ToString();
            }

            foreach (object o in lstCusip.Items)
            {
                p.Cusip.AddParamValue(o);
            }

            foreach (object o in lstReasonCode.Items)
            {
                p.ReasonCode.AddParamValue(o);
            }
            

            if (!string.IsNullOrEmpty(txtShares.Text))
            {
                p.ShareQuantity.AddParamValue(int.Parse(txtShares.Text));
                p.ShareQuantity.Comparer = cbxSharesCriteria.SelectedItem.ToString();
            }

            if (!string.IsNullOrEmpty(txtMoneyValue.Text))
            {
                p.MoneyValue.AddParamValue(float.Parse(txtMoneyValue.Text));
                p.MoneyValue.Comparer = cbxMoneyCriteria.SelectedItem.ToString();
            }

            if (!string.IsNullOrEmpty(cbxDirection.Text))
            {
                if (cbxDirection.Text == "Deliverer")
                {
                    p.DelivererReceiverIndicator.AddParamValue("D");
                }
                if (cbxDirection.Text == "Receiver")
                {
                    p.DelivererReceiverIndicator.AddParamValue("R");
                }
            }

            foreach (object i in clbStatus.CheckedItems)
            {
                switch (i.ToString())
                {
                    case "Made":
                        p.DtcStatusIndicator.AddParamValue("");
                        break;

                    case "Pending":
                        p.DtcStatusIndicator.AddParamValue("P");
                        break;

                    case "Made after Pending":
                        p.DtcStatusIndicator.AddParamValue("X");
                        break;

                    case "CNS Deliver DO Drop":
                        p.DtcStatusIndicator.AddParamValue("C");
                        break;

                    case "SDFS Prevent-Pend Drop":
                        p.DtcStatusIndicator.AddParamValue("D");
                        break;

                    case "Killed":
                        p.DtcStatusIndicator.AddParamValue("K");
                        break;

                    case "Repended":
                        p.DtcStatusIndicator.AddParamValue("R");
                        break;

                    default:
                        break;
                }
            }


            orders.Clear();
            doFact.Load(orders, p, maple, ctpy);

            lblCount.Text = orders.Count.ToString();
        }

        private void txtCusip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txtCusip.Text))
            {
                lstCusip.Items.Add(txtCusip.Text);
                txtCusip.Text = "";
            }
            else if (e.KeyCode == Keys.Enter && string.IsNullOrEmpty(txtCusip.Text))
            {
                Filter();
            }
        }

        private void txtReasonCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txtReasonCode.Text))
            {
                lstReasonCode.Items.Add(txtReasonCode.Text);
                txtReasonCode.Text = "";
            }
            else if (e.KeyCode == Keys.Enter && string.IsNullOrEmpty(txtReasonCode.Text))
            {
                Filter();
            }
        }

        private void cbxDirection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstCusip_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lstCusip.Items.Remove(lstCusip.SelectedItem);
        }

        private void lstReasonCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lstReasonCode.Items.Remove(lstReasonCode.SelectedItem);
        }

        private void lblCount_Click(object sender, EventArgs e)
        {

        }
    }
}
