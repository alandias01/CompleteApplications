namespace Maple.Dtc.PositionClient
{
    partial class RecentDtcActivityForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvDeliveryOrders = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveryOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDeliveryOrders
            // 
            this.dgvDeliveryOrders.AllowUserToAddRows = false;
            this.dgvDeliveryOrders.AllowUserToDeleteRows = false;
            this.dgvDeliveryOrders.AllowUserToOrderColumns = true;
            this.dgvDeliveryOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDeliveryOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeliveryOrders.Location = new System.Drawing.Point(12, 12);
            this.dgvDeliveryOrders.Name = "dgvDeliveryOrders";
            this.dgvDeliveryOrders.ReadOnly = true;
            this.dgvDeliveryOrders.Size = new System.Drawing.Size(639, 211);
            this.dgvDeliveryOrders.TabIndex = 0;
            // 
            // RecentDtcActivityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 235);
            this.Controls.Add(this.dgvDeliveryOrders);
            this.Name = "RecentDtcActivityForm";
            this.Text = "Recent DTC Activity";
            this.Load += new System.EventHandler(this.DeliveryOrderForms_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RecentDtcActivityForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveryOrders)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDeliveryOrders;
    }
}