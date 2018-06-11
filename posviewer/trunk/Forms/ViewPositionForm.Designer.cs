namespace Maple.Dtc.PositionClient
{
    partial class ViewPositionForm
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.cmsRT = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updatePriceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateTickerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.investigateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblRetrieved = new System.Windows.Forms.Label();
            this.txtOnRecall = new System.Windows.Forms.TextBox();
            this.txtMissingPrice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMissingBorrows = new System.Windows.Forms.TextBox();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chkAutoRefresh = new System.Windows.Forms.CheckBox();
            this.gbxView = new System.Windows.Forms.GroupBox();
            this.rbHeldUp = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbHedged = new System.Windows.Forms.RadioButton();
            this.btnEmail = new System.Windows.Forms.Button();
            this.bwt = new System.ComponentModel.BackgroundWorker();
            this.btnPledge = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
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
            this.dgvRealTimePosition.Size = new System.Drawing.Size(704, 538);
            this.dgvRealTimePosition.TabIndex = 2;
            this.dgvRealTimePosition.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvRealTimePosition_MouseDown);
            this.dgvRealTimePosition.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRealTimePosition_CellDoubleClick);
            this.dgvRealTimePosition.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRealTimePosition_ColumnHeaderMouseClick);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(678, -1);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(25, 23);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect To Server";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Visible = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
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
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(77, 23);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
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
            this.txtOnRecall.Location = new System.Drawing.Point(568, 1);
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
            this.label1.Location = new System.Drawing.Point(597, 4);
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
            this.label4.Location = new System.Drawing.Point(597, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Missing Borrows";
            // 
            // txtMissingBorrows
            // 
            this.txtMissingBorrows.BackColor = System.Drawing.Color.NavajoWhite;
            this.txtMissingBorrows.Location = new System.Drawing.Point(568, 24);
            this.txtMissingBorrows.Name = "txtMissingBorrows";
            this.txtMissingBorrows.Size = new System.Drawing.Size(23, 20);
            this.txtMissingBorrows.TabIndex = 13;
            // 
            // txtFind
            // 
            this.txtFind.Location = new System.Drawing.Point(196, 2);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(100, 20);
            this.txtFind.TabIndex = 15;
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(302, 0);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(65, 23);
            this.btnFind.TabIndex = 16;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 15000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chkAutoRefresh
            // 
            this.chkAutoRefresh.AutoSize = true;
            this.chkAutoRefresh.Location = new System.Drawing.Point(95, 6);
            this.chkAutoRefresh.Name = "chkAutoRefresh";
            this.chkAutoRefresh.Size = new System.Drawing.Size(85, 17);
            this.chkAutoRefresh.TabIndex = 18;
            this.chkAutoRefresh.Text = "AutoRefresh";
            this.chkAutoRefresh.UseVisualStyleBackColor = true;
            this.chkAutoRefresh.CheckedChanged += new System.EventHandler(this.chkAutoRefresh_CheckedChanged);
            // 
            // gbxView
            // 
            this.gbxView.Controls.Add(this.rbHeldUp);
            this.gbxView.Controls.Add(this.rbAll);
            this.gbxView.Controls.Add(this.rbHedged);
            this.gbxView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbxView.Location = new System.Drawing.Point(194, 21);
            this.gbxView.Name = "gbxView";
            this.gbxView.Size = new System.Drawing.Size(173, 23);
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
            // rbHedged
            // 
            this.rbHedged.AutoSize = true;
            this.rbHedged.Location = new System.Drawing.Point(110, 8);
            this.rbHedged.Name = "rbHedged";
            this.rbHedged.Size = new System.Drawing.Size(63, 17);
            this.rbHedged.TabIndex = 2;
            this.rbHedged.TabStop = true;
            this.rbHedged.Text = "Hedged";
            this.rbHedged.UseVisualStyleBackColor = true;
            this.rbHedged.CheckedChanged += new System.EventHandler(this.rbHedged_CheckedChanged);
            // 
            // btnEmail
            // 
            this.btnEmail.Location = new System.Drawing.Point(373, 0);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(65, 23);
            this.btnEmail.TabIndex = 20;
            this.btnEmail.Text = "Email";
            this.btnEmail.UseVisualStyleBackColor = true;
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            // 
            // bwt
            // 
            this.bwt.WorkerReportsProgress = true;
            this.bwt.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwt_DoWork);
            // 
            // btnPledge
            // 
            this.btnPledge.Location = new System.Drawing.Point(444, 0);
            this.btnPledge.Name = "btnPledge";
            this.btnPledge.Size = new System.Drawing.Size(75, 23);
            this.btnPledge.TabIndex = 21;
            this.btnPledge.Text = "Pledge";
            this.btnPledge.UseVisualStyleBackColor = true;
            this.btnPledge.Click += new System.EventHandler(this.btnPledge_Click);
            // 
            // toolTip1
            // 
            
            // 
            // ViewPositionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 597);
            this.Controls.Add(this.btnPledge);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.txtFind);
            this.Controls.Add(this.btnEmail);
            this.Controls.Add(this.gbxView);
            this.Controls.Add(this.chkAutoRefresh);
            this.Controls.Add(this.dgvRealTimePosition);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMissingBorrows);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMissingPrice);
            this.Controls.Add(this.txtOnRecall);
            this.Controls.Add(this.lblRetrieved);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnConnect);
            this.Name = "ViewPositionForm";
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
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ContextMenuStrip cmsRT;
        private System.Windows.Forms.ToolStripMenuItem updatePriceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateTickerToolStripMenuItem;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ToolStripMenuItem investigateToolStripMenuItem;
        private System.Windows.Forms.Label lblRetrieved;
        private System.Windows.Forms.TextBox txtOnRecall;
        private System.Windows.Forms.TextBox txtMissingPrice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMissingBorrows;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chkAutoRefresh;
        private System.Windows.Forms.GroupBox gbxView;
        private System.Windows.Forms.RadioButton rbHeldUp;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.Button btnEmail;
        private System.Windows.Forms.RadioButton rbHedged;
        private System.ComponentModel.BackgroundWorker bwt;
        private System.Windows.Forms.Button btnPledge;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

