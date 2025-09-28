namespace RMS
{
    partial class BackupData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupData));
            this.lblMain = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtBackupPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBackFile = new System.Windows.Forms.Button();
            this.btnRestFile = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.txtRestorePath = new System.Windows.Forms.TextBox();
            this.btnRestore = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.cmbOption = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMain
            // 
            this.lblMain.AutoSize = true;
            this.lblMain.Font = new System.Drawing.Font("Bahnschrift", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMain.Location = new System.Drawing.Point(19, 20);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(445, 40);
            this.lblMain.TabIndex = 2;
            this.lblMain.Text = "Backup or Restore Your Data";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(42, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 54);
            this.label1.TabIndex = 3;
            this.label1.Text = "Upload Your Backup\r\nFile to Google Drive\r\n ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(35, 72);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // txtBackupPath
            // 
            this.txtBackupPath.Enabled = false;
            this.txtBackupPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBackupPath.Location = new System.Drawing.Point(33, 139);
            this.txtBackupPath.Name = "txtBackupPath";
            this.txtBackupPath.ReadOnly = true;
            this.txtBackupPath.Size = new System.Drawing.Size(480, 41);
            this.txtBackupPath.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(29, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Save File to Location";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(29, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(265, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Choose Backup File to restore";
            // 
            // btnBackFile
            // 
            this.btnBackFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBackFile.BackgroundImage")));
            this.btnBackFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBackFile.Enabled = false;
            this.btnBackFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackFile.Location = new System.Drawing.Point(519, 139);
            this.btnBackFile.Name = "btnBackFile";
            this.btnBackFile.Padding = new System.Windows.Forms.Padding(5);
            this.btnBackFile.Size = new System.Drawing.Size(58, 41);
            this.btnBackFile.TabIndex = 8;
            this.btnBackFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBackFile.UseVisualStyleBackColor = true;
            this.btnBackFile.Click += new System.EventHandler(this.btnBackFile_Click);
            // 
            // btnRestFile
            // 
            this.btnRestFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRestFile.BackgroundImage")));
            this.btnRestFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRestFile.Enabled = false;
            this.btnRestFile.Location = new System.Drawing.Point(519, 215);
            this.btnRestFile.Name = "btnRestFile";
            this.btnRestFile.Padding = new System.Windows.Forms.Padding(5);
            this.btnRestFile.Size = new System.Drawing.Size(58, 41);
            this.btnRestFile.TabIndex = 8;
            this.btnRestFile.UseVisualStyleBackColor = true;
            this.btnRestFile.Click += new System.EventHandler(this.btnRestFile_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBackup.Enabled = false;
            this.btnBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackup.Image = ((System.Drawing.Image)(resources.GetObject("btnBackup.Image")));
            this.btnBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackup.Location = new System.Drawing.Point(417, 298);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnBackup.Size = new System.Drawing.Size(160, 50);
            this.btnBackup.TabIndex = 8;
            this.btnBackup.Text = "  Backup";
            this.btnBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // txtRestorePath
            // 
            this.txtRestorePath.Enabled = false;
            this.txtRestorePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRestorePath.Location = new System.Drawing.Point(33, 215);
            this.txtRestorePath.Name = "txtRestorePath";
            this.txtRestorePath.ReadOnly = true;
            this.txtRestorePath.Size = new System.Drawing.Size(480, 41);
            this.txtRestorePath.TabIndex = 9;
            // 
            // btnRestore
            // 
            this.btnRestore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRestore.Enabled = false;
            this.btnRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestore.Image = ((System.Drawing.Image)(resources.GetObject("btnRestore.Image")));
            this.btnRestore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestore.Location = new System.Drawing.Point(251, 298);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnRestore.Size = new System.Drawing.Size(160, 50);
            this.btnRestore.TabIndex = 8;
            this.btnRestore.Text = "  Restore";
            this.btnRestore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRestore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTime.Location = new System.Drawing.Point(63, 74);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(128, 18);
            this.lblTime.TabIndex = 3;
            this.lblTime.Text = "9 september 2025";
            // 
            // cmbOption
            // 
            this.cmbOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOption.FormattingEnabled = true;
            this.cmbOption.Items.AddRange(new object[] {
            "Backup",
            "Restore"});
            this.cmbOption.Location = new System.Drawing.Point(316, 74);
            this.cmbOption.Name = "cmbOption";
            this.cmbOption.Size = new System.Drawing.Size(261, 37);
            this.cmbOption.TabIndex = 10;
            this.cmbOption.SelectedIndexChanged += new System.EventHandler(this.cmbOption_SelectedIndexChanged);
            // 
            // BackupData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 384);
            this.Controls.Add(this.cmbOption);
            this.Controls.Add(this.txtRestorePath);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.btnRestFile);
            this.Controls.Add(this.btnBackFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBackupPath);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(630, 431);
            this.Name = "BackupData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OmniMart360 - Backup Restore";
            this.Load += new System.EventHandler(this.BackupData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtBackupPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBackFile;
        private System.Windows.Forms.Button btnRestFile;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.TextBox txtRestorePath;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.ComboBox cmbOption;
    }
}