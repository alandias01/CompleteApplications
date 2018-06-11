namespace Maple.Dtc.PositionClient
{
    partial class PriceControlForm
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
            this.components = new System.ComponentModel.Container();
            this.dgvControl = new System.Windows.Forms.DataGridView();
            this.cmsRT = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updatePriceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateTickerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.investigateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvDifferentPrices = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dgvControl)).BeginInit();
            this.cmsRT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDifferentPrices)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvControl
            // 
            this.dgvControl.AllowUserToAddRows = false;
            this.dgvControl.AllowUserToDeleteRows = false;
            this.dgvControl.AllowUserToOrderColumns = true;
            this.dgvControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvControl.Location = new System.Drawing.Point(3, 15);
            this.dgvControl.Name = "dgvControl";
            this.dgvControl.Size = new System.Drawing.Size(781, 111);
            this.dgvControl.TabIndex = 0;
            this.dgvControl.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvControl_CellDoubleClick);
            // 
            // cmsRT
            // 
            this.cmsRT.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updatePriceToolStripMenuItem,
            this.updateTickerToolStripMenuItem,
            this.investigateToolStripMenuItem});
            this.cmsRT.Name = "cmsRT";
            this.cmsRT.Size = new System.Drawing.Size(152, 70);
            // 
            // updatePriceToolStripMenuItem
            // 
            this.updatePriceToolStripMenuItem.Name = "updatePriceToolStripMenuItem";
            this.updatePriceToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.updatePriceToolStripMenuItem.Text = "Update Price";
            // 
            // updateTickerToolStripMenuItem
            // 
            this.updateTickerToolStripMenuItem.Name = "updateTickerToolStripMenuItem";
            this.updateTickerToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.updateTickerToolStripMenuItem.Text = "Update Ticker";
            // 
            // investigateToolStripMenuItem
            // 
            this.investigateToolStripMenuItem.Name = "investigateToolStripMenuItem";
            this.investigateToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.investigateToolStripMenuItem.Text = "Investigate";
            // 
            // dgvDifferentPrices
            // 
            this.dgvDifferentPrices.AllowUserToAddRows = false;
            this.dgvDifferentPrices.AllowUserToDeleteRows = false;
            this.dgvDifferentPrices.AllowUserToOrderColumns = true;
            this.dgvDifferentPrices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDifferentPrices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDifferentPrices.Location = new System.Drawing.Point(3, 15);
            this.dgvDifferentPrices.Name = "dgvDifferentPrices";
            this.dgvDifferentPrices.Size = new System.Drawing.Size(781, 113);
            this.dgvDifferentPrices.TabIndex = 1;
            this.dgvDifferentPrices.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDifferentPrices_CellDoubleClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvDifferentPrices);
            this.splitContainer1.Size = new System.Drawing.Size(787, 266);
            this.splitContainer1.SplitterDistance = 129;
            this.splitContainer1.TabIndex = 2;
            // 
            // PriceControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 290);
            this.Controls.Add(this.splitContainer1);
            this.Name = "PriceControlForm";
            this.Text = "Price Control";
            this.Load += new System.EventHandler(this.PriceControlForm_Load);
            this.Shown += new System.EventHandler(this.PriceControlForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PriceControlForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvControl)).EndInit();
            this.cmsRT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDifferentPrices)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvControl;
        private System.Windows.Forms.ContextMenuStrip cmsRT;
        private System.Windows.Forms.ToolStripMenuItem updatePriceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateTickerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem investigateToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvDifferentPrices;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}