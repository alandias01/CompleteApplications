namespace Maple.Dtc.PositionClient
{
    partial class OccHedgeForm
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
            this.dgvIncoming = new System.Windows.Forms.DataGridView();
            this.dgvOutgoing = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncoming)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutgoing)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvIncoming
            // 
            this.dgvIncoming.AllowUserToAddRows = false;
            this.dgvIncoming.AllowUserToDeleteRows = false;
            this.dgvIncoming.AllowUserToOrderColumns = true;
            this.dgvIncoming.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvIncoming.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIncoming.Location = new System.Drawing.Point(3, 16);
            this.dgvIncoming.Name = "dgvIncoming";
            this.dgvIncoming.ReadOnly = true;
            this.dgvIncoming.Size = new System.Drawing.Size(325, 195);
            this.dgvIncoming.TabIndex = 0;
            // 
            // dgvOutgoing
            // 
            this.dgvOutgoing.AllowUserToAddRows = false;
            this.dgvOutgoing.AllowUserToDeleteRows = false;
            this.dgvOutgoing.AllowUserToOrderColumns = true;
            this.dgvOutgoing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOutgoing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutgoing.Location = new System.Drawing.Point(3, 16);
            this.dgvOutgoing.Name = "dgvOutgoing";
            this.dgvOutgoing.ReadOnly = true;
            this.dgvOutgoing.Size = new System.Drawing.Size(328, 195);
            this.dgvOutgoing.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dgvIncoming);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.dgvOutgoing);
            this.splitContainer1.Size = new System.Drawing.Size(669, 214);
            this.splitContainer1.SplitterDistance = 331;
            this.splitContainer1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Incoming";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Outgoing";
            // 
            // OccHedgeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 494);
            this.Controls.Add(this.splitContainer1);
            this.Name = "OccHedgeForm";
            this.Text = "OccHedgeForm";
            this.Load += new System.EventHandler(this.OccHedgeForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OccHedgeForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncoming)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutgoing)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvIncoming;
        private System.Windows.Forms.DataGridView dgvOutgoing;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}