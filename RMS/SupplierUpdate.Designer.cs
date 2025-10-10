namespace RMS
{
    partial class SupplierUpdate
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textName = new System.Windows.Forms.TextBox();
            this.textMobile = new System.Windows.Forms.TextBox();
            this.textEmail = new System.Windows.Forms.TextBox();
            this.textBank = new System.Windows.Forms.TextBox();
            this.textIfsc = new System.Windows.Forms.TextBox();
            this.textPan = new System.Windows.Forms.TextBox();
            this.textGstin = new System.Windows.Forms.TextBox();
            this.textAddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelSupplierEmail = new System.Windows.Forms.Label();
            this.labelSupplierAddress = new System.Windows.Forms.Label();
            this.labelSupplierMobile = new System.Windows.Forms.Label();
            this.labelSupplierName = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(690, 40);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "पुरवठादार माहिती (Supplier Info)";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 353);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(690, 37);
            this.panel2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(455, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(554, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Save / जतन करा";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(234, 72);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(403, 22);
            this.textName.TabIndex = 0;
            // 
            // textMobile
            // 
            this.textMobile.Location = new System.Drawing.Point(234, 100);
            this.textMobile.Name = "textMobile";
            this.textMobile.Size = new System.Drawing.Size(403, 22);
            this.textMobile.TabIndex = 2;
            // 
            // textEmail
            // 
            this.textEmail.Location = new System.Drawing.Point(234, 128);
            this.textEmail.Name = "textEmail";
            this.textEmail.ReadOnly = true;
            this.textEmail.Size = new System.Drawing.Size(403, 22);
            this.textEmail.TabIndex = 3;
            this.textEmail.TabStop = false;
            // 
            // textBank
            // 
            this.textBank.Location = new System.Drawing.Point(234, 156);
            this.textBank.Name = "textBank";
            this.textBank.Size = new System.Drawing.Size(403, 22);
            this.textBank.TabIndex = 3;
            // 
            // textIfsc
            // 
            this.textIfsc.Location = new System.Drawing.Point(234, 184);
            this.textIfsc.Name = "textIfsc";
            this.textIfsc.Size = new System.Drawing.Size(403, 22);
            this.textIfsc.TabIndex = 4;
            // 
            // textPan
            // 
            this.textPan.Location = new System.Drawing.Point(234, 212);
            this.textPan.Name = "textPan";
            this.textPan.Size = new System.Drawing.Size(403, 22);
            this.textPan.TabIndex = 5;
            // 
            // textGstin
            // 
            this.textGstin.Location = new System.Drawing.Point(234, 239);
            this.textGstin.Name = "textGstin";
            this.textGstin.Size = new System.Drawing.Size(403, 22);
            this.textGstin.TabIndex = 6;
            // 
            // textAddress
            // 
            this.textAddress.Location = new System.Drawing.Point(234, 267);
            this.textAddress.Multiline = true;
            this.textAddress.Name = "textAddress";
            this.textAddress.Size = new System.Drawing.Size(403, 51);
            this.textAddress.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(53, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "GSTIN";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(53, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "PAN / पॅन क्रमांक";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(53, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "IFSC / आयएफएससी कोड";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(53, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Bank Account / बँक खाते क्रमांक";
            // 
            // labelSupplierEmail
            // 
            this.labelSupplierEmail.AutoSize = true;
            this.labelSupplierEmail.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSupplierEmail.Location = new System.Drawing.Point(53, 130);
            this.labelSupplierEmail.Name = "labelSupplierEmail";
            this.labelSupplierEmail.Size = new System.Drawing.Size(117, 15);
            this.labelSupplierEmail.TabIndex = 5;
            this.labelSupplierEmail.Text = "Supplier Email / ईमेल";
            // 
            // labelSupplierAddress
            // 
            this.labelSupplierAddress.AutoSize = true;
            this.labelSupplierAddress.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSupplierAddress.Location = new System.Drawing.Point(53, 269);
            this.labelSupplierAddress.Name = "labelSupplierAddress";
            this.labelSupplierAddress.Size = new System.Drawing.Size(127, 15);
            this.labelSupplierAddress.TabIndex = 4;
            this.labelSupplierAddress.Text = "Supplier Address / पत्ता ";
            // 
            // labelSupplierMobile
            // 
            this.labelSupplierMobile.AutoSize = true;
            this.labelSupplierMobile.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSupplierMobile.Location = new System.Drawing.Point(53, 102);
            this.labelSupplierMobile.Name = "labelSupplierMobile";
            this.labelSupplierMobile.Size = new System.Drawing.Size(175, 15);
            this.labelSupplierMobile.TabIndex = 7;
            this.labelSupplierMobile.Text = "Supplier Mobile / मोबाईल क्रमांक ";
            // 
            // labelSupplierName
            // 
            this.labelSupplierName.AutoSize = true;
            this.labelSupplierName.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSupplierName.Location = new System.Drawing.Point(53, 74);
            this.labelSupplierName.Name = "labelSupplierName";
            this.labelSupplierName.Size = new System.Drawing.Size(177, 15);
            this.labelSupplierName.TabIndex = 6;
            this.labelSupplierName.Text = "Supplier Name / पुरवठादाराचे नाव ";
            // 
            // SupplierUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 390);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelSupplierEmail);
            this.Controls.Add(this.labelSupplierAddress);
            this.Controls.Add(this.labelSupplierMobile);
            this.Controls.Add(this.labelSupplierName);
            this.Controls.Add(this.textAddress);
            this.Controls.Add(this.textGstin);
            this.Controls.Add(this.textPan);
            this.Controls.Add(this.textIfsc);
            this.Controls.Add(this.textBank);
            this.Controls.Add(this.textEmail);
            this.Controls.Add(this.textMobile);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SupplierUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SupplierUpdate";
            this.Load += new System.EventHandler(this.SupplierUpdate_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.TextBox textMobile;
        private System.Windows.Forms.TextBox textEmail;
        private System.Windows.Forms.TextBox textBank;
        private System.Windows.Forms.TextBox textIfsc;
        private System.Windows.Forms.TextBox textPan;
        private System.Windows.Forms.TextBox textGstin;
        private System.Windows.Forms.TextBox textAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelSupplierEmail;
        private System.Windows.Forms.Label labelSupplierAddress;
        private System.Windows.Forms.Label labelSupplierMobile;
        private System.Windows.Forms.Label labelSupplierName;
    }
}