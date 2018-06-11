namespace Maple.Dtc.PositionClient
{
    partial class ResearchForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResearchForm));
            this.dgvDeliveryOrders = new System.Windows.Forms.DataGridView();
            this.txtCusip = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFilter = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtShares = new System.Windows.Forms.TextBox();
            this.txtMoneyValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxSharesCriteria = new System.Windows.Forms.ComboBox();
            this.cbxMoneyCriteria = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCtpy = new System.Windows.Forms.TextBox();
            this.cbxTimeCriteria = new System.Windows.Forms.ComboBox();
            this.txtHour = new System.Windows.Forms.TextBox();
            this.txtMinute = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtReasonCode = new System.Windows.Forms.TextBox();
            this.cbxDirection = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbxMapleCtpy = new System.Windows.Forms.ComboBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.clbStatus = new System.Windows.Forms.CheckedListBox();
            this.lstCusip = new System.Windows.Forms.ListBox();
            this.lstReasonCode = new System.Windows.Forms.ListBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDeliveryOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDeliveryOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDeliveryOrders.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDeliveryOrders.Location = new System.Drawing.Point(186, 25);
            this.dgvDeliveryOrders.Name = "dgvDeliveryOrders";
            this.dgvDeliveryOrders.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDeliveryOrders.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDeliveryOrders.Size = new System.Drawing.Size(592, 691);
            this.dgvDeliveryOrders.TabIndex = 20;
            // 
            // txtCusip
            // 
            this.txtCusip.Location = new System.Drawing.Point(13, 269);
            this.txtCusip.Name = "txtCusip";
            this.txtCusip.Size = new System.Drawing.Size(67, 20);
            this.txtCusip.TabIndex = 7;
            this.txtCusip.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCusip_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 253);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "CUSIP";
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(12, 708);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 17;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(12, 41);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(84, 20);
            this.dtpDate.TabIndex = 0;
            // 
            // txtShares
            // 
            this.txtShares.Location = new System.Drawing.Point(56, 411);
            this.txtShares.Name = "txtShares";
            this.txtShares.Size = new System.Drawing.Size(72, 20);
            this.txtShares.TabIndex = 12;
            this.txtShares.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // txtMoneyValue
            // 
            this.txtMoneyValue.Location = new System.Drawing.Point(56, 471);
            this.txtMoneyValue.Name = "txtMoneyValue";
            this.txtMoneyValue.Size = new System.Drawing.Size(72, 20);
            this.txtMoneyValue.TabIndex = 14;
            this.txtMoneyValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Maple CTPY";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 395);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Shares";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 455);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Money Value";
            // 
            // cbxSharesCriteria
            // 
            this.cbxSharesCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSharesCriteria.FormattingEnabled = true;
            this.cbxSharesCriteria.Location = new System.Drawing.Point(12, 411);
            this.cbxSharesCriteria.Name = "cbxSharesCriteria";
            this.cbxSharesCriteria.Size = new System.Drawing.Size(39, 21);
            this.cbxSharesCriteria.TabIndex = 11;
            // 
            // cbxMoneyCriteria
            // 
            this.cbxMoneyCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMoneyCriteria.FormattingEnabled = true;
            this.cbxMoneyCriteria.Location = new System.Drawing.Point(12, 470);
            this.cbxMoneyCriteria.Name = "cbxMoneyCriteria";
            this.cbxMoneyCriteria.Size = new System.Drawing.Size(39, 21);
            this.cbxMoneyCriteria.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "CTPY";
            // 
            // txtCtpy
            // 
            this.txtCtpy.Location = new System.Drawing.Point(12, 192);
            this.txtCtpy.Name = "txtCtpy";
            this.txtCtpy.Size = new System.Drawing.Size(120, 20);
            this.txtCtpy.TabIndex = 6;
            this.txtCtpy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // cbxTimeCriteria
            // 
            this.cbxTimeCriteria.FormattingEnabled = true;
            this.cbxTimeCriteria.Location = new System.Drawing.Point(12, 88);
            this.cbxTimeCriteria.Name = "cbxTimeCriteria";
            this.cbxTimeCriteria.Size = new System.Drawing.Size(39, 21);
            this.cbxTimeCriteria.TabIndex = 1;
            // 
            // txtHour
            // 
            this.txtHour.Location = new System.Drawing.Point(60, 89);
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(28, 20);
            this.txtHour.TabIndex = 2;
            this.txtHour.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // txtMinute
            // 
            this.txtMinute.Location = new System.Drawing.Point(98, 89);
            this.txtMinute.Name = "txtMinute";
            this.txtMinute.Size = new System.Drawing.Size(28, 20);
            this.txtMinute.TabIndex = 4;
            this.txtMinute.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(62, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "HH";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(88, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = ":";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(100, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "MM";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Time";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(91, 708);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 505);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Status";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 331);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Reason Code";
            // 
            // txtReasonCode
            // 
            this.txtReasonCode.Location = new System.Drawing.Point(12, 347);
            this.txtReasonCode.Name = "txtReasonCode";
            this.txtReasonCode.Size = new System.Drawing.Size(67, 20);
            this.txtReasonCode.TabIndex = 9;
            this.txtReasonCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReasonCode_KeyDown);
            // 
            // cbxDirection
            // 
            this.cbxDirection.FormattingEnabled = true;
            this.cbxDirection.Location = new System.Drawing.Point(12, 668);
            this.cbxDirection.Name = "cbxDirection";
            this.cbxDirection.Size = new System.Drawing.Size(120, 21);
            this.cbxDirection.TabIndex = 16;
            this.cbxDirection.SelectedIndexChanged += new System.EventHandler(this.cbxDirection_SelectedIndexChanged);
            this.cbxDirection.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 652);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "Direction";
            // 
            // cbxMapleCtpy
            // 
            this.cbxMapleCtpy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMapleCtpy.FormattingEnabled = true;
            this.cbxMapleCtpy.Location = new System.Drawing.Point(12, 139);
            this.cbxMapleCtpy.Name = "cbxMapleCtpy";
            this.cbxMapleCtpy.Size = new System.Drawing.Size(120, 21);
            this.cbxMapleCtpy.TabIndex = 5;
            this.cbxMapleCtpy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(183, 9);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(13, 13);
            this.lblCount.TabIndex = 32;
            this.lblCount.Text = "0";
            this.lblCount.Click += new System.EventHandler(this.lblCount_Click);
            // 
            // clbStatus
            // 
            this.clbStatus.FormattingEnabled = true;
            this.clbStatus.Location = new System.Drawing.Point(12, 525);
            this.clbStatus.Name = "clbStatus";
            this.clbStatus.Size = new System.Drawing.Size(154, 109);
            this.clbStatus.TabIndex = 15;
            this.clbStatus.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // lstCusip
            // 
            this.lstCusip.FormattingEnabled = true;
            this.lstCusip.Location = new System.Drawing.Point(85, 253);
            this.lstCusip.Name = "lstCusip";
            this.lstCusip.Size = new System.Drawing.Size(95, 56);
            this.lstCusip.TabIndex = 8;
            this.lstCusip.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstCusip_MouseDoubleClick);
            // 
            // lstReasonCode
            // 
            this.lstReasonCode.FormattingEnabled = true;
            this.lstReasonCode.Location = new System.Drawing.Point(85, 331);
            this.lstReasonCode.Name = "lstReasonCode";
            this.lstReasonCode.Size = new System.Drawing.Size(95, 56);
            this.lstReasonCode.TabIndex = 10;
            this.lstReasonCode.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstReasonCode_MouseDoubleClick);
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(186, 722);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            this.txtTotal.TabIndex = 33;
            // 
            // ResearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 749);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.lstReasonCode);
            this.Controls.Add(this.lstCusip);
            this.Controls.Add(this.clbStatus);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.cbxMapleCtpy);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cbxDirection);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtReasonCode);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMinute);
            this.Controls.Add(this.txtHour);
            this.Controls.Add(this.cbxTimeCriteria);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCtpy);
            this.Controls.Add(this.cbxMoneyCriteria);
            this.Controls.Add(this.cbxSharesCriteria);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMoneyValue);
            this.Controls.Add(this.txtShares);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCusip);
            this.Controls.Add(this.dgvDeliveryOrders);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ResearchForm";
            this.Text = "ResearchForm";
            this.Load += new System.EventHandler(this.ResearchForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveryOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDeliveryOrders;
        private System.Windows.Forms.TextBox txtCusip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtShares;
        private System.Windows.Forms.TextBox txtMoneyValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxSharesCriteria;
        private System.Windows.Forms.ComboBox cbxMoneyCriteria;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCtpy;
        private System.Windows.Forms.ComboBox cbxTimeCriteria;
        private System.Windows.Forms.TextBox txtHour;
        private System.Windows.Forms.TextBox txtMinute;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtReasonCode;
        private System.Windows.Forms.ComboBox cbxDirection;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbxMapleCtpy;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.CheckedListBox clbStatus;
        private System.Windows.Forms.ListBox lstCusip;
        private System.Windows.Forms.ListBox lstReasonCode;
        private System.Windows.Forms.TextBox txtTotal;
    }
}