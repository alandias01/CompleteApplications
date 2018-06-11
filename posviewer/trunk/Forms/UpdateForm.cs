using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Maple.Dtc.PositionClient
{
    public partial class UpdateForm : Form
    {
        private string Mode;
        RealTimePositionObject rt;

        public UpdateForm(RealTimePositionObject p, string mode)
        {
            InitializeComponent();

            rt = p;

            txtCusip.Text = p.Cusip;
            txtTicker.Text = p.Ticker;
            txtDescription.Text = p.Company;
            txtPrice.Text = p.Price.ToString();

            Mode = mode;

            if (mode == "Price")
            {
                txtCusip.Enabled = false;
                txtTicker.Enabled = false;
                txtDescription.Enabled = false;

                txtPrice.Focus();
                
            }
            else if (mode == "Ticker")
            {
                txtCusip.Enabled = false;
                txtPrice.Enabled = false;

                txtTicker.Focus();
            }
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateStock();
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateStock();
            }
        }

        private void UpdateStock()
        {
            if (Mode == "Price")
            {
                double price = -1;

                double.TryParse(txtPrice.Text, out price);

                if (price < 0)
                {
                    MessageBox.Show(price.ToString() + " is an invalid price");
                    return;
                }

                rt.Price = price;

                StockPriceFactory spf = new StockPriceFactory();
                StockPriceObject spo = new StockPriceObject();

                spo.Cusip = txtCusip.Text;
                spo.Price = price;
                spo.PriceDate = DateTime.Today;
                
                try
                {
                    spf.Delete(spo);
                    spf.Insert(spo);                    
                }
                catch (Exception e)
                {
                    MessageBox.Show("Problem Updating Price: \r\n" + e.ToString());
                }

                try
                {
                    DtcChannel.Instance.StockPriceUpdated(spo);
                }
                catch (Exception)
                {
                }

            }
            else if (Mode == "Ticker")
            {

                rt.Cusip = txtCusip.Text;
                rt.Company = txtDescription.Text;
                rt.Ticker = txtTicker.Text;

                StockInfoFactory sif = new StockInfoFactory();
                StockInfoObject sio = new StockInfoObject();

                sio.Cusip = txtCusip.Text;
                sio.Name = txtDescription.Text.ToUpper();
                sio.Ticker = txtTicker.Text.ToUpper();

                try
                {
                    sif.Delete(sio);
                    sif.Insert(sio);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Problem Updating Stock: \r\n" + e.ToString());
                }

                try
                {
                    DtcChannel.Instance.StockInfoUpdated(sio);
                }
                catch (Exception)
                {
                }

            }
            this.Close();
        }
    }
}
