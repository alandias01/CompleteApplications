namespace Maple.Dtc.PositionClient
{
    partial class ReturnByCtpyActivityForm
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
            this.dgvReturns = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblReturns = new System.Windows.Forms.Label();
            this.lblAllocations = new System.Windows.Forms.Label();
            this.dgvAllocations = new System.Windows.Forms.DataGridView();
            this.btnFilter = new System.Windows.Forms.Button();
            this.txtCtpy = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSymbol = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTradeCategory = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturns)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllocations)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvReturns
            // 
            this.dgvReturns.AllowUserToAddRows = false;
            this.dgvReturns.AllowUserToDeleteRows = false;
            this.dgvReturns.AllowUserToOrderColumns = true;
            this.dgvReturns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReturns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReturns.Location = new System.Drawing.Point(3, 16);
            this.dgvReturns.Name = "dgvReturns";
            this.dgvReturns.ReadOnly = true;
            this.dgvReturns.Size = new System.Drawing.Size(751, 125);
            this.dgvReturns.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(2, 45);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblReturns);
            this.splitContainer1.Panel1.Controls.Add(this.dgvReturns);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblAllocations);
            this.splitContainer1.Panel2.Controls.Add(this.dgvAllocations);
            this.splitContainer1.Size = new System.Drawing.Size(757, 295);
            this.splitContainer1.SplitterDistance = 144;
            this.splitContainer1.TabIndex = 2;
            // 
            // lblReturns
            // 
            this.lblReturns.AutoSize = true;
            this.lblReturns.Location = new System.Drawing.Point(3, 0);
            this.lblReturns.Name = "lblReturns";
            this.lblReturns.Size = new System.Drawing.Size(44, 13);
            this.lblReturns.TabIndex = 1;
            this.lblReturns.Text = "Returns";
            // 
            // lblAllocations
            // 
            this.lblAllocations.AutoSize = true;
            this.lblAllocations.Location = new System.Drawing.Point(3, 0);
            this.lblAllocations.Name = "lblAllocations";
            this.lblAllocations.Size = new System.Drawing.Size(58, 13);
            this.lblAllocations.TabIndex = 2;
            this.lblAllocations.Text = "Allocations";
            // 
            // dgvAllocations
            // 
            this.dgvAllocations.AllowUserToAddRows = false;
            this.dgvAllocations.AllowUserToDeleteRows = false;
            this.dgvAllocations.AllowUserToOrderColumns = true;
            this.dgvAllocations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAllocations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllocations.Location = new System.Drawing.Point(3, 16);
            this.dgvAllocations.Name = "dgvAllocations";
            this.dgvAllocations.ReadOnly = true;
            this.dgvAllocations.Size = new System.Drawing.Size(751, 128);
            this.dgvAllocations.TabIndex = 1;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(367, 17);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 3;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // txtCtpy
            // 
            this.txtCtpy.Location = new System.Drawing.Point(13, 18);
            this.txtCtpy.Name = "txtCtpy";
            this.txtCtpy.Size = new System.Drawing.Size(100, 20);
            this.txtCtpy.TabIndex = 4;
            this.txtCtpy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCtpy_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ctpy";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Ticker";
            // 
            // txtSymbol
            // 
            this.txtSymbol.Location = new System.Drawing.Point(131, 18);
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Size = new System.Drawing.Size(100, 20);
            this.txtSymbol.TabIndex = 7;
            this.txtSymbol.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSymbol_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Trade Category";
            // 
            // txtTradeCategory
            // 
            this.txtTradeCategory.Location = new System.Drawing.Point(249, 18);
            this.txtTradeCategory.Name = "txtTradeCategory";
            this.txtTradeCategory.Size = new System.Drawing.Size(100, 20);
            this.txtTradeCategory.TabIndex = 9;
            this.txtTradeCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTradeCategory_KeyDown);
            // 
            // ReturnByCtpyActivityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 343);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTradeCategory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSymbol);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCtpy);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ReturnByCtpyActivityForm";
            this.Text = "Returns By Counterparty";
            this.Load += new System.EventHandler(this.ReturnActivityForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReturnActivityForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturns)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllocations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReturns;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblReturns;
        private System.Windows.Forms.Label lblAllocations;
        private System.Windows.Forms.DataGridView dgvAllocations;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.TextBox txtCtpy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSymbol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTradeCategory;
    }
}