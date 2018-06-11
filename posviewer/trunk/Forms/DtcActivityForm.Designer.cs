namespace Maple.Dtc.PositionClient
{
    partial class DtcActivityForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMadeBorrow = new System.Windows.Forms.Panel();
            this.txtPendingBorrow = new System.Windows.Forms.Panel();
            this.txtMadeLoan = new System.Windows.Forms.Panel();
            this.txtPendingLoan = new System.Windows.Forms.Panel();
            this.txtPendingReturnOut = new System.Windows.Forms.Panel();
            this.txtMadeReturnOut = new System.Windows.Forms.Panel();
            this.txtPendingReturnIn = new System.Windows.Forms.Panel();
            this.txtMadeRetunIn = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtReclaimedReturnsOut = new System.Windows.Forms.Panel();
            this.txtReclaimedLoans = new System.Windows.Forms.Panel();
            this.txtReclaimedReturnsIn = new System.Windows.Forms.Panel();
            this.txtReclaimedBorrows = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbPendingFilter = new System.Windows.Forms.CheckBox();
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
            this.dgvDeliveryOrders.Location = new System.Drawing.Point(1, 51);
            this.dgvDeliveryOrders.Name = "dgvDeliveryOrders";
            this.dgvDeliveryOrders.ReadOnly = true;
            this.dgvDeliveryOrders.Size = new System.Drawing.Size(768, 99);
            this.dgvDeliveryOrders.TabIndex = 0;
            this.dgvDeliveryOrders.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvDeliveryOrders_ColumnHeaderMouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Made Borrows";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Pending Borrows";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(181, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Made Loans";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Pending Loans";
            // 
            // txtMadeBorrow
            // 
            this.txtMadeBorrow.BackColor = System.Drawing.Color.RoyalBlue;
            this.txtMadeBorrow.Location = new System.Drawing.Point(12, 1);
            this.txtMadeBorrow.Name = "txtMadeBorrow";
            this.txtMadeBorrow.Size = new System.Drawing.Size(23, 13);
            this.txtMadeBorrow.TabIndex = 23;
            // 
            // txtPendingBorrow
            // 
            this.txtPendingBorrow.BackColor = System.Drawing.Color.LightSteelBlue;
            this.txtPendingBorrow.Location = new System.Drawing.Point(12, 16);
            this.txtPendingBorrow.Name = "txtPendingBorrow";
            this.txtPendingBorrow.Size = new System.Drawing.Size(23, 13);
            this.txtPendingBorrow.TabIndex = 24;
            // 
            // txtMadeLoan
            // 
            this.txtMadeLoan.BackColor = System.Drawing.Color.LimeGreen;
            this.txtMadeLoan.Location = new System.Drawing.Point(152, 1);
            this.txtMadeLoan.Name = "txtMadeLoan";
            this.txtMadeLoan.Size = new System.Drawing.Size(23, 13);
            this.txtMadeLoan.TabIndex = 24;
            // 
            // txtPendingLoan
            // 
            this.txtPendingLoan.BackColor = System.Drawing.Color.GreenYellow;
            this.txtPendingLoan.Location = new System.Drawing.Point(152, 16);
            this.txtPendingLoan.Name = "txtPendingLoan";
            this.txtPendingLoan.Size = new System.Drawing.Size(23, 13);
            this.txtPendingLoan.TabIndex = 24;
            // 
            // txtPendingReturnOut
            // 
            this.txtPendingReturnOut.BackColor = System.Drawing.Color.NavajoWhite;
            this.txtPendingReturnOut.Location = new System.Drawing.Point(424, 16);
            this.txtPendingReturnOut.Name = "txtPendingReturnOut";
            this.txtPendingReturnOut.Size = new System.Drawing.Size(23, 13);
            this.txtPendingReturnOut.TabIndex = 30;
            // 
            // txtMadeReturnOut
            // 
            this.txtMadeReturnOut.BackColor = System.Drawing.Color.Orange;
            this.txtMadeReturnOut.Location = new System.Drawing.Point(424, 1);
            this.txtMadeReturnOut.Name = "txtMadeReturnOut";
            this.txtMadeReturnOut.Size = new System.Drawing.Size(23, 13);
            this.txtMadeReturnOut.TabIndex = 31;
            // 
            // txtPendingReturnIn
            // 
            this.txtPendingReturnIn.BackColor = System.Drawing.Color.Plum;
            this.txtPendingReturnIn.Location = new System.Drawing.Point(274, 16);
            this.txtPendingReturnIn.Name = "txtPendingReturnIn";
            this.txtPendingReturnIn.Size = new System.Drawing.Size(23, 13);
            this.txtPendingReturnIn.TabIndex = 32;
            // 
            // txtMadeRetunIn
            // 
            this.txtMadeRetunIn.BackColor = System.Drawing.Color.MediumOrchid;
            this.txtMadeRetunIn.Location = new System.Drawing.Point(274, 1);
            this.txtMadeRetunIn.Name = "txtMadeRetunIn";
            this.txtMadeRetunIn.Size = new System.Drawing.Size(23, 13);
            this.txtMadeRetunIn.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(453, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Made Returns Out";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(453, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Pending Returns Out";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(303, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Made Returns In";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(303, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Pending Returns In";
            // 
            // txtReclaimedReturnsOut
            // 
            this.txtReclaimedReturnsOut.BackColor = System.Drawing.Color.Gold;
            this.txtReclaimedReturnsOut.Location = new System.Drawing.Point(424, 32);
            this.txtReclaimedReturnsOut.Name = "txtReclaimedReturnsOut";
            this.txtReclaimedReturnsOut.Size = new System.Drawing.Size(23, 13);
            this.txtReclaimedReturnsOut.TabIndex = 39;
            // 
            // txtReclaimedLoans
            // 
            this.txtReclaimedLoans.BackColor = System.Drawing.Color.SpringGreen;
            this.txtReclaimedLoans.Location = new System.Drawing.Point(152, 32);
            this.txtReclaimedLoans.Name = "txtReclaimedLoans";
            this.txtReclaimedLoans.Size = new System.Drawing.Size(23, 13);
            this.txtReclaimedLoans.TabIndex = 36;
            // 
            // txtReclaimedReturnsIn
            // 
            this.txtReclaimedReturnsIn.BackColor = System.Drawing.Color.LightPink;
            this.txtReclaimedReturnsIn.Location = new System.Drawing.Point(274, 32);
            this.txtReclaimedReturnsIn.Name = "txtReclaimedReturnsIn";
            this.txtReclaimedReturnsIn.Size = new System.Drawing.Size(23, 13);
            this.txtReclaimedReturnsIn.TabIndex = 40;
            // 
            // txtReclaimedBorrows
            // 
            this.txtReclaimedBorrows.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.txtReclaimedBorrows.Location = new System.Drawing.Point(12, 32);
            this.txtReclaimedBorrows.Name = "txtReclaimedBorrows";
            this.txtReclaimedBorrows.Size = new System.Drawing.Size(23, 13);
            this.txtReclaimedBorrows.TabIndex = 35;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(453, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 13);
            this.label9.TabIndex = 38;
            this.label9.Text = "Reclaimed Returns Out";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(181, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Reclaimed Loans";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(303, 32);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 13);
            this.label11.TabIndex = 37;
            this.label11.Text = "Reclaimed Returns In";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(41, 32);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 13);
            this.label12.TabIndex = 33;
            this.label12.Text = "Reclaimed Borrows";
            // 
            // cbPendingFilter
            // 
            this.cbPendingFilter.AutoSize = true;
            this.cbPendingFilter.Location = new System.Drawing.Point(599, 1);
            this.cbPendingFilter.Name = "cbPendingFilter";
            this.cbPendingFilter.Size = new System.Drawing.Size(99, 30);
            this.cbPendingFilter.TabIndex = 41;
            this.cbPendingFilter.Text = "Filter Pendings \r\nthat were made";
            this.cbPendingFilter.UseVisualStyleBackColor = true;
            this.cbPendingFilter.CheckedChanged += new System.EventHandler(this.cbPendingFilter_CheckedChanged);
            // 
            // DtcActivityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 153);
            this.Controls.Add(this.cbPendingFilter);
            this.Controls.Add(this.txtReclaimedReturnsOut);
            this.Controls.Add(this.txtPendingReturnOut);
            this.Controls.Add(this.txtReclaimedLoans);
            this.Controls.Add(this.txtPendingLoan);
            this.Controls.Add(this.txtReclaimedReturnsIn);
            this.Controls.Add(this.txtReclaimedBorrows);
            this.Controls.Add(this.txtMadeReturnOut);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtMadeLoan);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtPendingReturnIn);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtMadeRetunIn);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtPendingBorrow);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMadeBorrow);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvDeliveryOrders);
            this.Name = "DtcActivityForm";
            this.Text = "Cusip DTC Activity";
            this.Load += new System.EventHandler(this.DeliveryOrderForms_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DtcActivityForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveryOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDeliveryOrders;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel txtMadeBorrow;
        private System.Windows.Forms.Panel txtPendingBorrow;
        private System.Windows.Forms.Panel txtMadeLoan;
        private System.Windows.Forms.Panel txtPendingLoan;
        private System.Windows.Forms.Panel txtPendingReturnOut;
        private System.Windows.Forms.Panel txtMadeReturnOut;
        private System.Windows.Forms.Panel txtPendingReturnIn;
        private System.Windows.Forms.Panel txtMadeRetunIn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel txtReclaimedReturnsOut;
        private System.Windows.Forms.Panel txtReclaimedLoans;
        private System.Windows.Forms.Panel txtReclaimedReturnsIn;
        private System.Windows.Forms.Panel txtReclaimedBorrows;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cbPendingFilter;
    }
}