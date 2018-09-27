﻿namespace Reports
{
    partial class CustomizedForDSE_ReviewCustbalance
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
            this.rdoPossitiveBal = new System.Windows.Forms.RadioButton();
            this.rdoNegetive = new System.Windows.Forms.RadioButton();
            this.rdoNegetiveSpecificer = new System.Windows.Forms.RadioButton();
            this.rdoPositiveSpecific = new System.Windows.Forms.RadioButton();
            this.rdoNegetiveSpecificerTillSpecificDate = new System.Windows.Forms.RadioButton();
            this.rdoPositiveSpecificerTillSpecificDate = new System.Windows.Forms.RadioButton();
            this.rdoNegetiveDateRange = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.gpPeriod = new System.Windows.Forms.GroupBox();
            this.chbOrder = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.gpPeriod.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdoPossitiveBal
            // 
            this.rdoPossitiveBal.AutoSize = true;
            this.rdoPossitiveBal.Checked = true;
            this.rdoPossitiveBal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoPossitiveBal.ForeColor = System.Drawing.Color.Black;
            this.rdoPossitiveBal.Location = new System.Drawing.Point(14, 21);
            this.rdoPossitiveBal.Name = "rdoPossitiveBal";
            this.rdoPossitiveBal.Size = new System.Drawing.Size(193, 17);
            this.rdoPossitiveBal.TabIndex = 0;
            this.rdoPossitiveBal.TabStop = true;
            this.rdoPossitiveBal.Text = "Customer with Possitive (+) Balance";
            this.rdoPossitiveBal.UseVisualStyleBackColor = true;
            this.rdoPossitiveBal.CheckedChanged += new System.EventHandler(this.rdoPossitiveBal_CheckedChanged);
            // 
            // rdoNegetive
            // 
            this.rdoNegetive.AutoSize = true;
            this.rdoNegetive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoNegetive.ForeColor = System.Drawing.Color.Black;
            this.rdoNegetive.Location = new System.Drawing.Point(14, 44);
            this.rdoNegetive.Name = "rdoNegetive";
            this.rdoNegetive.Size = new System.Drawing.Size(191, 17);
            this.rdoNegetive.TabIndex = 1;
            this.rdoNegetive.TabStop = true;
            this.rdoNegetive.Text = "Customer with Negetive (-) Balance";
            this.rdoNegetive.UseVisualStyleBackColor = true;
            this.rdoNegetive.CheckedChanged += new System.EventHandler(this.rdoPossitiveBal_CheckedChanged);
            // 
            // rdoNegetiveSpecificer
            // 
            this.rdoNegetiveSpecificer.AutoSize = true;
            this.rdoNegetiveSpecificer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoNegetiveSpecificer.ForeColor = System.Drawing.Color.Black;
            this.rdoNegetiveSpecificer.Location = new System.Drawing.Point(14, 90);
            this.rdoNegetiveSpecificer.Name = "rdoNegetiveSpecificer";
            this.rdoNegetiveSpecificer.Size = new System.Drawing.Size(245, 17);
            this.rdoNegetiveSpecificer.TabIndex = 12;
            this.rdoNegetiveSpecificer.TabStop = true;
            this.rdoNegetiveSpecificer.Text = "Customer Negetive(-) Balance of Specific Date";
            this.rdoNegetiveSpecificer.UseVisualStyleBackColor = true;
            this.rdoNegetiveSpecificer.CheckedChanged += new System.EventHandler(this.rdoPossitiveBal_CheckedChanged);
            // 
            // rdoPositiveSpecific
            // 
            this.rdoPositiveSpecific.AutoSize = true;
            this.rdoPositiveSpecific.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoPositiveSpecific.ForeColor = System.Drawing.Color.Black;
            this.rdoPositiveSpecific.Location = new System.Drawing.Point(14, 67);
            this.rdoPositiveSpecific.Name = "rdoPositiveSpecific";
            this.rdoPositiveSpecific.Size = new System.Drawing.Size(242, 17);
            this.rdoPositiveSpecific.TabIndex = 13;
            this.rdoPositiveSpecific.TabStop = true;
            this.rdoPositiveSpecific.Text = "Customer Positive(+) Balance of Specific Date";
            this.rdoPositiveSpecific.UseVisualStyleBackColor = true;
            this.rdoPositiveSpecific.CheckedChanged += new System.EventHandler(this.rdoPossitiveBal_CheckedChanged);
            // 
            // rdoNegetiveSpecificerTillSpecificDate
            // 
            this.rdoNegetiveSpecificerTillSpecificDate.AutoSize = true;
            this.rdoNegetiveSpecificerTillSpecificDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoNegetiveSpecificerTillSpecificDate.ForeColor = System.Drawing.Color.Black;
            this.rdoNegetiveSpecificerTillSpecificDate.Location = new System.Drawing.Point(14, 136);
            this.rdoNegetiveSpecificerTillSpecificDate.Name = "rdoNegetiveSpecificerTillSpecificDate";
            this.rdoNegetiveSpecificerTillSpecificDate.Size = new System.Drawing.Size(249, 17);
            this.rdoNegetiveSpecificerTillSpecificDate.TabIndex = 14;
            this.rdoNegetiveSpecificerTillSpecificDate.TabStop = true;
            this.rdoNegetiveSpecificerTillSpecificDate.Text = "Customer Negetive(-) Balance Till Specific Date";
            this.rdoNegetiveSpecificerTillSpecificDate.UseVisualStyleBackColor = true;
            this.rdoNegetiveSpecificerTillSpecificDate.CheckedChanged += new System.EventHandler(this.rdoNegetiveSpecificerTillSpecificDate_CheckedChanged);
            // 
            // rdoPositiveSpecificerTillSpecificDate
            // 
            this.rdoPositiveSpecificerTillSpecificDate.AutoSize = true;
            this.rdoPositiveSpecificerTillSpecificDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoPositiveSpecificerTillSpecificDate.ForeColor = System.Drawing.Color.Black;
            this.rdoPositiveSpecificerTillSpecificDate.Location = new System.Drawing.Point(14, 113);
            this.rdoPositiveSpecificerTillSpecificDate.Name = "rdoPositiveSpecificerTillSpecificDate";
            this.rdoPositiveSpecificerTillSpecificDate.Size = new System.Drawing.Size(246, 17);
            this.rdoPositiveSpecificerTillSpecificDate.TabIndex = 15;
            this.rdoPositiveSpecificerTillSpecificDate.TabStop = true;
            this.rdoPositiveSpecificerTillSpecificDate.Text = "Customer Positive(+) Balance Till Specific Date";
            this.rdoPositiveSpecificerTillSpecificDate.UseVisualStyleBackColor = true;
            this.rdoPositiveSpecificerTillSpecificDate.CheckedChanged += new System.EventHandler(this.rdoPositiveSpecificerTillSpecificDate_CheckedChanged);
            // 
            // rdoNegetiveDateRange
            // 
            this.rdoNegetiveDateRange.AutoSize = true;
            this.rdoNegetiveDateRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoNegetiveDateRange.Location = new System.Drawing.Point(14, 159);
            this.rdoNegetiveDateRange.Name = "rdoNegetiveDateRange";
            this.rdoNegetiveDateRange.Size = new System.Drawing.Size(272, 17);
            this.rdoNegetiveDateRange.TabIndex = 16;
            this.rdoNegetiveDateRange.TabStop = true;
            this.rdoNegetiveDateRange.Text = "Customer Negetive(-) Balance Between Date Range";
            this.rdoNegetiveDateRange.UseVisualStyleBackColor = true;
            this.rdoNegetiveDateRange.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoNegetiveDateRange);
            this.groupBox1.Controls.Add(this.rdoPositiveSpecificerTillSpecificDate);
            this.groupBox1.Controls.Add(this.rdoNegetiveSpecificerTillSpecificDate);
            this.groupBox1.Controls.Add(this.rdoPositiveSpecific);
            this.groupBox1.Controls.Add(this.rdoNegetiveSpecificer);
            this.groupBox1.Controls.Add(this.rdoNegetive);
            this.groupBox1.Controls.Add(this.rdoPossitiveBal);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(2, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 184);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Catagory";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnReport
            // 
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnReport.Location = new System.Drawing.Point(12, 310);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(286, 37);
            this.btnReport.TabIndex = 1;
            this.btnReport.Text = "View Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(64, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "To Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpToDate.Enabled = false;
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(125, 45);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(101, 20);
            this.dtpToDate.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(64, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "From Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(126, 19);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(100, 20);
            this.dtpFromDate.TabIndex = 6;
            // 
            // gpPeriod
            // 
            this.gpPeriod.Controls.Add(this.dtpFromDate);
            this.gpPeriod.Controls.Add(this.label2);
            this.gpPeriod.Controls.Add(this.dtpToDate);
            this.gpPeriod.Controls.Add(this.label1);
            this.gpPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpPeriod.ForeColor = System.Drawing.Color.Black;
            this.gpPeriod.Location = new System.Drawing.Point(2, 196);
            this.gpPeriod.Name = "gpPeriod";
            this.gpPeriod.Size = new System.Drawing.Size(320, 72);
            this.gpPeriod.TabIndex = 4;
            this.gpPeriod.TabStop = false;
            this.gpPeriod.Text = "Report Duration";
            this.gpPeriod.Enter += new System.EventHandler(this.gpPeriod_Enter);
            // 
            // chbOrder
            // 
            this.chbOrder.AutoSize = true;
            this.chbOrder.Checked = true;
            this.chbOrder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbOrder.ForeColor = System.Drawing.Color.MidnightBlue;
            this.chbOrder.Location = new System.Drawing.Point(24, 287);
            this.chbOrder.Name = "chbOrder";
            this.chbOrder.Size = new System.Drawing.Size(257, 17);
            this.chbOrder.TabIndex = 5;
            this.chbOrder.Text = "Display Customer Code Oder BY Balance";
            this.chbOrder.UseVisualStyleBackColor = true;
            this.chbOrder.CheckedChanged += new System.EventHandler(this.chbOrder_CheckedChanged);
            // 
            // CustomizedForDSE_ReviewCustbalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 349);
            this.Controls.Add(this.chbOrder);
            this.Controls.Add(this.gpPeriod);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "CustomizedForDSE_ReviewCustbalance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Review Customer Balance";
            this.Load += new System.EventHandler(this.ReviewCustbalance_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gpPeriod.ResumeLayout(false);
            this.gpPeriod.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoPossitiveBal;
        private System.Windows.Forms.RadioButton rdoNegetive;
        private System.Windows.Forms.RadioButton rdoNegetiveSpecificer;
        private System.Windows.Forms.RadioButton rdoPositiveSpecific;
        private System.Windows.Forms.RadioButton rdoNegetiveSpecificerTillSpecificDate;
        private System.Windows.Forms.RadioButton rdoPositiveSpecificerTillSpecificDate;
        private System.Windows.Forms.RadioButton rdoNegetiveDateRange;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.GroupBox gpPeriod;
        private System.Windows.Forms.CheckBox chbOrder;

    }
}