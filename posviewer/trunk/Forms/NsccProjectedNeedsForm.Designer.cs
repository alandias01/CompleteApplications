namespace Maple.Dtc.PositionClient
{
    partial class NsccProjectedNeedsForm
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
            this.dgvRealTimePosition = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cmsRT = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updateTickerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.cmsExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRealTimePosition)).BeginInit();
            this.cmsRT.SuspendLayout();
            this.cmsExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvRealTimePosition
            // 
            this.dgvRealTimePosition.AllowUserToAddRows = false;
            this.dgvRealTimePosition.AllowUserToDeleteRows = false;
            this.dgvRealTimePosition.AllowUserToOrderColumns = true;
            this.dgvRealTimePosition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRealTimePosition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRealTimePosition.Location = new System.Drawing.Point(12, 28);
            this.dgvRealTimePosition.Name = "dgvRealTimePosition";
            this.dgvRealTimePosition.Size = new System.Drawing.Size(639, 184);
            this.dgvRealTimePosition.TabIndex = 2;
            this.dgvRealTimePosition.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvRealTimePosition_MouseDown);
            this.dgvRealTimePosition.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRealTimePosition_CellDoubleClick);
            this.dgvRealTimePosition.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRealTimePosition_ColumnHeaderMouseClick);
            this.dgvRealTimePosition.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvRealTimePosition_CellPainting);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 15000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cmsRT
            // 
            this.cmsRT.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateTickerToolStripMenuItem});
            this.cmsRT.Name = "cmsRT";
            this.cmsRT.Size = new System.Drawing.Size(152, 26);
            // 
            // updateTickerToolStripMenuItem
            // 
            this.updateTickerToolStripMenuItem.Name = "updateTickerToolStripMenuItem";
            this.updateTickerToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.updateTickerToolStripMenuItem.Text = "Update Ticker";
            this.updateTickerToolStripMenuItem.Click += new System.EventHandler(this.updateTickerToolStripMenuItem_Click_1);
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(12, 2);
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.ReadOnly = true;
            this.txtSummary.Size = new System.Drawing.Size(327, 20);
            this.txtSummary.TabIndex = 4;
            // 
            // cmsExport
            // 
            this.cmsExport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExport});
            this.cmsExport.Name = "cmsExport";
            this.cmsExport.Size = new System.Drawing.Size(118, 26);
            // 
            // tsmiExport
            // 
            this.tsmiExport.Name = "tsmiExport";
            this.tsmiExport.Size = new System.Drawing.Size(117, 22);
            this.tsmiExport.Text = "Export";
            this.tsmiExport.Click += new System.EventHandler(this.tsmiExport_Click_1);
            // 
            // NsccProjectedNeedsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 224);
            this.Controls.Add(this.dgvRealTimePosition);
            this.Controls.Add(this.txtSummary);
            this.Name = "NsccProjectedNeedsForm";
            this.Text = "CNS Needs";
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.Shown += new System.EventHandler(this.ViewPositionForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewPositionForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRealTimePosition)).EndInit();
            this.cmsRT.ResumeLayout(false);
            this.cmsExport.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRealTimePosition;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip cmsRT;
        private System.Windows.Forms.ToolStripMenuItem updateTickerToolStripMenuItem;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.ContextMenuStrip cmsExport;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
    }
}

