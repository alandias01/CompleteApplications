namespace Maple.Dtc.PositionClient
{
    partial class RecallActivityForm
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
            this.dgvRecalls = new System.Windows.Forms.DataGridView();
            this.dgvRecalledLots = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecalls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecalledLots)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvRecalls
            // 
            this.dgvRecalls.AllowUserToAddRows = false;
            this.dgvRecalls.AllowUserToDeleteRows = false;
            this.dgvRecalls.AllowUserToOrderColumns = true;
            this.dgvRecalls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecalls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecalls.Location = new System.Drawing.Point(3, 16);
            this.dgvRecalls.Name = "dgvRecalls";
            this.dgvRecalls.ReadOnly = true;
            this.dgvRecalls.Size = new System.Drawing.Size(547, 85);
            this.dgvRecalls.TabIndex = 0;
            // 
            // dgvRecalledLots
            // 
            this.dgvRecalledLots.AllowUserToAddRows = false;
            this.dgvRecalledLots.AllowUserToDeleteRows = false;
            this.dgvRecalledLots.AllowUserToOrderColumns = true;
            this.dgvRecalledLots.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecalledLots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecalledLots.Location = new System.Drawing.Point(3, 16);
            this.dgvRecalledLots.Name = "dgvRecalledLots";
            this.dgvRecalledLots.ReadOnly = true;
            this.dgvRecalledLots.Size = new System.Drawing.Size(547, 82);
            this.dgvRecalledLots.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(2, 6);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dgvRecalls);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.dgvRecalledLots);
            this.splitContainer1.Size = new System.Drawing.Size(553, 209);
            this.splitContainer1.SplitterDistance = 104;
            this.splitContainer1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ctpy Recalls";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Maple Recalls";
            // 
            // RecallActivityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 220);
            this.Controls.Add(this.splitContainer1);
            this.Name = "RecallActivityForm";
            this.Text = "Recalls";
            this.Load += new System.EventHandler(this.RecallActivityForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RecallActivityForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecalls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecalledLots)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRecalls;
        private System.Windows.Forms.DataGridView dgvRecalledLots;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}