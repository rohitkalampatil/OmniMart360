namespace RMS
{
    partial class MainPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panKpis = new System.Windows.Forms.Panel();
            this.lblOwner = new System.Windows.Forms.Label();
            this.tblKpi = new System.Windows.Forms.TableLayoutPanel();
            this.panKpiProfit = new System.Windows.Forms.Panel();
            this.lblKpiProfit = new System.Windows.Forms.Label();
            this.kpisales = new System.Windows.Forms.Label();
            this.pickpiProfit = new System.Windows.Forms.PictureBox();
            this.panKpiSales = new System.Windows.Forms.Panel();
            this.lblKpiSales = new System.Windows.Forms.Label();
            this.kpiPurchase = new System.Windows.Forms.Label();
            this.picKpiSales = new System.Windows.Forms.PictureBox();
            this.panKpiMargin = new System.Windows.Forms.Panel();
            this.lblKpiMargin = new System.Windows.Forms.Label();
            this.kpiProfit = new System.Windows.Forms.Label();
            this.picKpiMargin = new System.Windows.Forms.PictureBox();
            this.panKpiItems = new System.Windows.Forms.Panel();
            this.lblKpiItems = new System.Windows.Forms.Label();
            this.kpiIReceivable = new System.Windows.Forms.Label();
            this.picKpiItems = new System.Windows.Forms.PictureBox();
            this.panMain = new System.Windows.Forms.Panel();
            this.chartMonthlySales = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panKpis.SuspendLayout();
            this.tblKpi.SuspendLayout();
            this.panKpiProfit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pickpiProfit)).BeginInit();
            this.panKpiSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picKpiSales)).BeginInit();
            this.panKpiMargin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picKpiMargin)).BeginInit();
            this.panKpiItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picKpiItems)).BeginInit();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMonthlySales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // panKpis
            // 
            this.panKpis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.panKpis.Controls.Add(this.lblOwner);
            this.panKpis.Controls.Add(this.tblKpi);
            this.panKpis.Dock = System.Windows.Forms.DockStyle.Top;
            this.panKpis.Location = new System.Drawing.Point(0, 0);
            this.panKpis.Name = "panKpis";
            this.panKpis.Size = new System.Drawing.Size(1361, 193);
            this.panKpis.TabIndex = 3;
            // 
            // lblOwner
            // 
            this.lblOwner.AutoSize = true;
            this.lblOwner.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOwner.Location = new System.Drawing.Point(11, 16);
            this.lblOwner.Name = "lblOwner";
            this.lblOwner.Size = new System.Drawing.Size(368, 24);
            this.lblOwner.TabIndex = 0;
            this.lblOwner.Text = "Omnimar360 - Manage store Effectively";
            // 
            // tblKpi
            // 
            this.tblKpi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
            this.tblKpi.ColumnCount = 4;
            this.tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblKpi.Controls.Add(this.panKpiProfit, 1, 0);
            this.tblKpi.Controls.Add(this.panKpiSales, 0, 0);
            this.tblKpi.Controls.Add(this.panKpiMargin, 2, 0);
            this.tblKpi.Controls.Add(this.panKpiItems, 3, 0);
            this.tblKpi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblKpi.Location = new System.Drawing.Point(0, 54);
            this.tblKpi.Margin = new System.Windows.Forms.Padding(30);
            this.tblKpi.Name = "tblKpi";
            this.tblKpi.Padding = new System.Windows.Forms.Padding(25, 0, 25, 0);
            this.tblKpi.RowCount = 1;
            this.tblKpi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.42857F));
            this.tblKpi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.57143F));
            this.tblKpi.Size = new System.Drawing.Size(1361, 139);
            this.tblKpi.TabIndex = 5;
            // 
            // panKpiProfit
            // 
            this.panKpiProfit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panKpiProfit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(120)))), ((int)(((byte)(153)))));
            this.panKpiProfit.Controls.Add(this.lblKpiProfit);
            this.panKpiProfit.Controls.Add(this.kpisales);
            this.panKpiProfit.Controls.Add(this.pickpiProfit);
            this.panKpiProfit.Location = new System.Drawing.Point(355, 3);
            this.panKpiProfit.Name = "panKpiProfit";
            this.panKpiProfit.Size = new System.Drawing.Size(321, 133);
            this.panKpiProfit.TabIndex = 0;
            // 
            // lblKpiProfit
            // 
            this.lblKpiProfit.AutoSize = true;
            this.lblKpiProfit.Font = new System.Drawing.Font("Bahnschrift SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKpiProfit.ForeColor = System.Drawing.Color.White;
            this.lblKpiProfit.Location = new System.Drawing.Point(28, 89);
            this.lblKpiProfit.Name = "lblKpiProfit";
            this.lblKpiProfit.Size = new System.Drawing.Size(60, 24);
            this.lblKpiProfit.TabIndex = 2;
            this.lblKpiProfit.Text = "Sales";
            // 
            // kpisales
            // 
            this.kpisales.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kpisales.AutoSize = true;
            this.kpisales.BackColor = System.Drawing.Color.Transparent;
            this.kpisales.Font = new System.Drawing.Font("Bahnschrift SemiBold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kpisales.ForeColor = System.Drawing.Color.White;
            this.kpisales.Location = new System.Drawing.Point(9, 17);
            this.kpisales.Name = "kpisales";
            this.kpisales.Size = new System.Drawing.Size(200, 72);
            this.kpisales.TabIndex = 1;
            this.kpisales.Text = "$234K";
            // 
            // pickpiProfit
            // 
            this.pickpiProfit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pickpiProfit.Image = ((System.Drawing.Image)(resources.GetObject("pickpiProfit.Image")));
            this.pickpiProfit.Location = new System.Drawing.Point(0, 0);
            this.pickpiProfit.Name = "pickpiProfit";
            this.pickpiProfit.Size = new System.Drawing.Size(321, 133);
            this.pickpiProfit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pickpiProfit.TabIndex = 0;
            this.pickpiProfit.TabStop = false;
            // 
            // panKpiSales
            // 
            this.panKpiSales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panKpiSales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(189)))), ((int)(((byte)(193)))));
            this.panKpiSales.Controls.Add(this.lblKpiSales);
            this.panKpiSales.Controls.Add(this.kpiPurchase);
            this.panKpiSales.Controls.Add(this.picKpiSales);
            this.panKpiSales.Location = new System.Drawing.Point(28, 3);
            this.panKpiSales.Name = "panKpiSales";
            this.panKpiSales.Size = new System.Drawing.Size(321, 133);
            this.panKpiSales.TabIndex = 0;
            // 
            // lblKpiSales
            // 
            this.lblKpiSales.AutoSize = true;
            this.lblKpiSales.Font = new System.Drawing.Font("Bahnschrift SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKpiSales.ForeColor = System.Drawing.Color.White;
            this.lblKpiSales.Location = new System.Drawing.Point(27, 90);
            this.lblKpiSales.Name = "lblKpiSales";
            this.lblKpiSales.Size = new System.Drawing.Size(95, 24);
            this.lblKpiSales.TabIndex = 2;
            this.lblKpiSales.Text = "Purchase";
            // 
            // kpiPurchase
            // 
            this.kpiPurchase.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kpiPurchase.AutoSize = true;
            this.kpiPurchase.BackColor = System.Drawing.Color.Transparent;
            this.kpiPurchase.Font = new System.Drawing.Font("Bahnschrift SemiBold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kpiPurchase.ForeColor = System.Drawing.Color.White;
            this.kpiPurchase.Location = new System.Drawing.Point(9, 17);
            this.kpiPurchase.Name = "kpiPurchase";
            this.kpiPurchase.Size = new System.Drawing.Size(200, 72);
            this.kpiPurchase.TabIndex = 1;
            this.kpiPurchase.Text = "$234K";
            // 
            // picKpiSales
            // 
            this.picKpiSales.BackColor = System.Drawing.Color.Transparent;
            this.picKpiSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picKpiSales.Image = ((System.Drawing.Image)(resources.GetObject("picKpiSales.Image")));
            this.picKpiSales.Location = new System.Drawing.Point(0, 0);
            this.picKpiSales.Name = "picKpiSales";
            this.picKpiSales.Size = new System.Drawing.Size(321, 133);
            this.picKpiSales.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picKpiSales.TabIndex = 0;
            this.picKpiSales.TabStop = false;
            // 
            // panKpiMargin
            // 
            this.panKpiMargin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panKpiMargin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(142)))), ((int)(((byte)(119)))));
            this.panKpiMargin.Controls.Add(this.lblKpiMargin);
            this.panKpiMargin.Controls.Add(this.kpiProfit);
            this.panKpiMargin.Controls.Add(this.picKpiMargin);
            this.panKpiMargin.Location = new System.Drawing.Point(682, 3);
            this.panKpiMargin.Name = "panKpiMargin";
            this.panKpiMargin.Size = new System.Drawing.Size(321, 133);
            this.panKpiMargin.TabIndex = 0;
            // 
            // lblKpiMargin
            // 
            this.lblKpiMargin.AutoSize = true;
            this.lblKpiMargin.Font = new System.Drawing.Font("Bahnschrift SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKpiMargin.ForeColor = System.Drawing.Color.White;
            this.lblKpiMargin.Location = new System.Drawing.Point(30, 89);
            this.lblKpiMargin.Name = "lblKpiMargin";
            this.lblKpiMargin.Size = new System.Drawing.Size(60, 24);
            this.lblKpiMargin.TabIndex = 2;
            this.lblKpiMargin.Text = "Profit";
            // 
            // kpiProfit
            // 
            this.kpiProfit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kpiProfit.AutoSize = true;
            this.kpiProfit.BackColor = System.Drawing.Color.Transparent;
            this.kpiProfit.Font = new System.Drawing.Font("Bahnschrift SemiBold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kpiProfit.ForeColor = System.Drawing.Color.White;
            this.kpiProfit.Location = new System.Drawing.Point(9, 17);
            this.kpiProfit.Name = "kpiProfit";
            this.kpiProfit.Size = new System.Drawing.Size(200, 72);
            this.kpiProfit.TabIndex = 1;
            this.kpiProfit.Text = "$234K";
            // 
            // picKpiMargin
            // 
            this.picKpiMargin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picKpiMargin.Image = ((System.Drawing.Image)(resources.GetObject("picKpiMargin.Image")));
            this.picKpiMargin.Location = new System.Drawing.Point(0, 0);
            this.picKpiMargin.Name = "picKpiMargin";
            this.picKpiMargin.Size = new System.Drawing.Size(321, 133);
            this.picKpiMargin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picKpiMargin.TabIndex = 0;
            this.picKpiMargin.TabStop = false;
            // 
            // panKpiItems
            // 
            this.panKpiItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panKpiItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(122)))), ((int)(((byte)(125)))));
            this.panKpiItems.Controls.Add(this.lblKpiItems);
            this.panKpiItems.Controls.Add(this.kpiIReceivable);
            this.panKpiItems.Controls.Add(this.picKpiItems);
            this.panKpiItems.Location = new System.Drawing.Point(1009, 3);
            this.panKpiItems.Name = "panKpiItems";
            this.panKpiItems.Size = new System.Drawing.Size(324, 133);
            this.panKpiItems.TabIndex = 0;
            // 
            // lblKpiItems
            // 
            this.lblKpiItems.AutoSize = true;
            this.lblKpiItems.Font = new System.Drawing.Font("Bahnschrift SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKpiItems.ForeColor = System.Drawing.Color.White;
            this.lblKpiItems.Location = new System.Drawing.Point(28, 89);
            this.lblKpiItems.Name = "lblKpiItems";
            this.lblKpiItems.Size = new System.Drawing.Size(109, 24);
            this.lblKpiItems.TabIndex = 2;
            this.lblKpiItems.Text = "Receivable";
            // 
            // kpiIReceivable
            // 
            this.kpiIReceivable.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kpiIReceivable.AutoSize = true;
            this.kpiIReceivable.BackColor = System.Drawing.Color.Transparent;
            this.kpiIReceivable.Font = new System.Drawing.Font("Bahnschrift SemiBold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kpiIReceivable.ForeColor = System.Drawing.Color.White;
            this.kpiIReceivable.Location = new System.Drawing.Point(9, 17);
            this.kpiIReceivable.Name = "kpiIReceivable";
            this.kpiIReceivable.Size = new System.Drawing.Size(200, 72);
            this.kpiIReceivable.TabIndex = 1;
            this.kpiIReceivable.Text = "$234K";
            // 
            // picKpiItems
            // 
            this.picKpiItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picKpiItems.Image = ((System.Drawing.Image)(resources.GetObject("picKpiItems.Image")));
            this.picKpiItems.Location = new System.Drawing.Point(0, 0);
            this.picKpiItems.Name = "picKpiItems";
            this.picKpiItems.Size = new System.Drawing.Size(324, 133);
            this.picKpiItems.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picKpiItems.TabIndex = 0;
            this.picKpiItems.TabStop = false;
            // 
            // panMain
            // 
            this.panMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.panMain.Controls.Add(this.chartMonthlySales);
            this.panMain.Controls.Add(this.chart1);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 193);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(1361, 530);
            this.panMain.TabIndex = 4;
            // 
            // chartMonthlySales
            // 
            this.chartMonthlySales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chartMonthlySales.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartMonthlySales.Legends.Add(legend1);
            this.chartMonthlySales.Location = new System.Drawing.Point(694, 6);
            this.chartMonthlySales.Name = "chartMonthlySales";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "MonthlySales";
            series1.XValueMember = "month";
            series1.YValueMembers = "monthly_sales";
            this.chartMonthlySales.Series.Add(series1);
            this.chartMonthlySales.Size = new System.Drawing.Size(655, 512);
            this.chartMonthlySales.TabIndex = 1;
            this.chartMonthlySales.Text = "chart2";
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(28, 6);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series2.Legend = "Legend1";
            series2.Name = "sales";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(660, 512);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1361, 723);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.panKpis);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OmniMart360";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.panKpis.ResumeLayout(false);
            this.panKpis.PerformLayout();
            this.tblKpi.ResumeLayout(false);
            this.panKpiProfit.ResumeLayout(false);
            this.panKpiProfit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pickpiProfit)).EndInit();
            this.panKpiSales.ResumeLayout(false);
            this.panKpiSales.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picKpiSales)).EndInit();
            this.panKpiMargin.ResumeLayout(false);
            this.panKpiMargin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picKpiMargin)).EndInit();
            this.panKpiItems.ResumeLayout(false);
            this.panKpiItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picKpiItems)).EndInit();
            this.panMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMonthlySales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panKpis;
        private System.Windows.Forms.TableLayoutPanel tblKpi;
        private System.Windows.Forms.Panel panKpiProfit;
        private System.Windows.Forms.Panel panKpiSales;
        private System.Windows.Forms.Panel panKpiMargin;
        private System.Windows.Forms.Panel panKpiItems;
        private System.Windows.Forms.Label lblOwner;
        private System.Windows.Forms.PictureBox pickpiProfit;
        private System.Windows.Forms.PictureBox picKpiSales;
        private System.Windows.Forms.PictureBox picKpiMargin;
        private System.Windows.Forms.PictureBox picKpiItems;
        private System.Windows.Forms.Label kpiPurchase;
        private System.Windows.Forms.Label kpisales;
        private System.Windows.Forms.Label kpiProfit;
        private System.Windows.Forms.Label kpiIReceivable;
        private System.Windows.Forms.Label lblKpiProfit;
        private System.Windows.Forms.Label lblKpiSales;
        private System.Windows.Forms.Label lblKpiMargin;
        private System.Windows.Forms.Label lblKpiItems;
        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMonthlySales;
    }
}