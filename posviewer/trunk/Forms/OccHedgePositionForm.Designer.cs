namespace Maple.Dtc.PositionClient
{
    partial class OccHedgePositionForm
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
            this.cmsRT = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updatePriceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateTickerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.investigateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblRetrieved = new System.Windows.Forms.Label();
            this.txtOnRecall = new System.Windows.Forms.TextBox();
            this.txtMissingPrice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMissingBorrows = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gbxView = new System.Windows.Forms.GroupBox();
            this.rbHeldUp = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRealTimePosition)).BeginInit();
            this.cmsRT.SuspendLayout();
            this.gbxView.SuspendLayout();
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
            this.dgvRealTimePosition.Location = new System.Drawing.Point(12, 47);
            this.dgvRealTimePosition.Name = "dgvRealTimePosition";
            this.dgvRealTimePosition.Size = new System.Drawing.Size(639, 538);
            this.dgvRealTimePosition.TabIndex = 2;
            this.dgvRealTimePosition.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvRealTimePosition_MouseDown);
            this.dgvRealTimePosition.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRealTimePosition_CellDoubleClick);
            this.dgvRealTimePosition.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRealTimePosition_ColumnHeaderMouseClick);
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
            this.updatePriceToolStripMenuItem.Click += new System.EventHandler(this.updatePriceToolStripMenuItem_Click);
            // 
            // updateTickerToolStripMenuItem
            // 
            this.updateTickerToolStripMenuItem.Name = "updateTickerToolStripMenuItem";
            this.updateTickerToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.updateTickerToolStripMenuItem.Text = "Update Ticker";
            this.updateTickerToolStripMenuItem.Click += new System.EventHandler(this.updateTickerToolStripMenuItem_Click);
            // 
            // investigateToolStripMenuItem
            // 
            this.investigateToolStripMenuItem.Name = "investigateToolStripMenuItem";
            this.investigateToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.investigateToolStripMenuItem.Text = "Investigate";
            this.investigateToolStripMenuItem.Click += new System.EventHandler(this.investigateToolStripMenuItem_Click);
            // 
            // lblRetrieved
            // 
            this.lblRetrieved.AutoSize = true;
            this.lblRetrieved.Location = new System.Drawing.Point(12, 28);
            this.lblRetrieved.Name = "lblRetrieved";
            this.lblRetrieved.Size = new System.Drawing.Size(53, 13);
            this.lblRetrieved.TabIndex = 6;
            this.lblRetrieved.Text = "Retrieved";
            this.lblRetrieved.Visible = false;
            // 
            // txtOnRecall
            // 
            this.txtOnRecall.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.txtOnRecall.Location = new System.Drawing.Point(516, 1);
            this.txtOnRecall.Name = "txtOnRecall";
            this.txtOnRecall.Size = new System.Drawing.Size(23, 20);
            this.txtOnRecall.TabIndex = 7;
            // 
            // txtMissingPrice
            // 
            this.txtMissingPrice.BackColor = System.Drawing.Color.Gold;
            this.txtMissingPrice.Location = new System.Drawing.Point(386, 24);
            this.txtMissingPrice.Name = "txtMissingPrice";
            this.txtMissingPrice.Size = new System.Drawing.Size(23, 20);
            this.txtMissingPrice.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(545, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "On Recall";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(415, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Missing Price";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(545, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Missing Borrows";
            // 
            // txtMissingBorrows
            // 
            this.txtMissingBorrows.BackColor = System.Drawing.Color.NavajoWhite;
            this.txtMissingBorrows.Location = new System.Drawing.Point(516, 24);
            this.txtMissingBorrows.Name = "txtMissingBorrows";
            this.txtMissingBorrows.Size = new System.Drawing.Size(23, 20);
            this.txtMissingBorrows.TabIndex = 13;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 15000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // gbxView
            // 
            this.gbxView.Controls.Add(this.rbHeldUp);
            this.gbxView.Controls.Add(this.rbAll);
            this.gbxView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbxView.Location = new System.Drawing.Point(194, 21);
            this.gbxView.Name = "gbxView";
            this.gbxView.Size = new System.Drawing.Size(103, 24);
            this.gbxView.TabIndex = 19;
            this.gbxView.TabStop = false;
            // 
            // rbHeldUp
            // 
            this.rbHeldUp.AutoSize = true;
            this.rbHeldUp.Location = new System.Drawing.Point(40, 6);
            this.rbHeldUp.Name = "rbHeldUp";
            this.rbHeldUp.Size = new System.Drawing.Size(64, 17);
            this.rbHeldUp.TabIndex = 1;
            this.rbHeldUp.TabStop = true;
            this.rbHeldUp.Text = "Held Up";
            this.rbHeldUp.UseVisualStyleBackColor = true;
            this.rbHeldUp.CheckedChanged += new System.EventHandler(this.rbHeldUp_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(0, 6);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(36, 17);
            this.rbAll.TabIndex = 0;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // OccHedgePositionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 597);
            this.Controls.Add(this.gbxView);
            this.Controls.Add(this.dgvRealTimePosition);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMissingBorrows);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMissingPrice);
            this.Controls.Add(this.txtOnRecall);
            this.Controls.Add(this.lblRetrieved);
            this.Name = "OccHedgePositionForm";
            this.Text = "Position Viewer";
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.Shown += new System.EventHandler(this.ViewPositionForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewPositionForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRealTimePosition)).EndInit();
            this.cmsRT.ResumeLayout(false);
            this.gbxView.ResumeLayout(false);
            this.gbxView.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRealTimePosition;
        private System.Windows.Forms.ContextMenuStrip cmsRT;
        private System.Windows.Forms.ToolStripMenuItem updatePriceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateTickerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem investigateToolStripMenuItem;
        private System.Windows.Forms.Label lblRetrieved;
        private System.Windows.Forms.TextBox txtOnRecall;
        private System.Windows.Forms.TextBox txtMissingPrice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMissingBorrows;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox gbxView;
        private System.Windows.Forms.RadioButton rbHeldUp;
        private System.Windows.Forms.RadioButton rbAll;
    }
}

