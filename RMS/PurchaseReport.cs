using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace RMS
{
    public partial class PurchaseReport : Form
    {
        MySqlConnection c1=null;
        MySqlDataAdapter da = null;
        DataTable t = null;

        public PurchaseReport()
        {
            InitializeComponent();
        }


        private void Home_Load(object sender, EventArgs e)
        {
            c1 = DBConnection.GetConnection();
            loadTable();
            PopulateYearMonthFilters();
        }
        private void PopulateYearMonthFilters()
        {
            // Populate years from the data
            var years = t.AsEnumerable()
                         .Select(row => Convert.ToDateTime(row["date"]).Year)
                         .Distinct()
                         .OrderByDescending(y => y)
                         .ToList();

            comboYear.Items.Clear();
            comboYear.Items.Add("All");
            foreach (var year in years)
                comboYear.Items.Add(year.ToString());
            comboYear.SelectedIndex = 0;

            // Populate months
            comboMonth.Items.Clear();
            comboMonth.Items.Add("All");
            for (int i = 1; i <= 12; i++)
                comboMonth.Items.Add(new DateTime(2000, i, 1).ToString("MMMM"));
            comboMonth.SelectedIndex = 0;
        }
        private void loadTable()
        {
            c1.Open();
            try
            {
                string query = "select * from purchase order by date desc";
                da = new MySqlDataAdapter(query, c1);
                t = new DataTable();

                da.Fill(t);
                PopulateDataGridView(t);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "RMS - Retail Management System 1.0", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                c1.Close();
            }  
        }
        private void PopulateDataGridView(DataTable table) 
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow row in table.Rows)
            {
                int rowIndex = dataGridView1.Rows.Add();

                dataGridView1.Rows[rowIndex].Cells[0].Value = row["invoiceno"];
                dataGridView1.Rows[rowIndex].Cells[1].Value = Convert.ToDateTime(row["date"]).ToString("yyyy-MM-dd");
                dataGridView1.Rows[rowIndex].Cells[2].Value = row["supplier_name"].ToString().ToUpper();
                dataGridView1.Rows[rowIndex].Cells[3].Value = row["total_bill"];
                dataGridView1.Rows[rowIndex].Cells[4].Value = row["paid_amount"];
                dataGridView1.Rows[rowIndex].Cells[5].Value = row["remaining_amount"];
            }
        }

        //Search buttonn
        private void button1_Click(object sender, EventArgs e)
        {
            string filterText = textSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(filterText))
            {
                PopulateDataGridView(t); // Reset to original data if search text is empty
            }
            else
            {
                // Apply filter to the DataTable
                DataView dv = new DataView(t);
                dv.RowFilter = string.Format("CONVERT(invoiceno, System.String) LIKE '%{0}%' OR CONVERT(date, System.String) LIKE '%{0}%' OR CONVERT(supplier_name, System.String) LIKE '%{0}%' OR CONVERT(total_bill, System.String) LIKE '%{0}%' OR CONVERT(paid_amount, System.String) LIKE '%{0}%' OR CONVERT(remaining_amount, System.String) LIKE '%{0}%'", filterText);
                PopulateDataGridView(dv.ToTable());
            }
        }
        //view button
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex== 6 && e.RowIndex >= 0)
            {
                int inv = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                PurchaseUpdate obj = new PurchaseUpdate(inv);
                obj.ShowDialog();
                //MessageBox.Show("" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No data found to Export...", "Omnimart360", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Document doc = new Document(PageSize.A4, 10, 10, 10, 10);
            try
            {
                string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\PurchaseReport.pdf";
                PdfWriter.GetInstance(doc, new FileStream(downloadsPath, FileMode.Create));
                doc.Open();

                // Add title
                Paragraph title = new Paragraph("Purchase Report", FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD));
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                doc.Add(new Paragraph("\n"));

                // Create table with one less column
                int columnCount = dataGridView1.Columns.Count - 1;
                PdfPTable pdfTable = new PdfPTable(columnCount);
                pdfTable.WidthPercentage = 100;
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                // Define font with size 9
                iTextSharp.text.Font cellFont = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL);

                // Add headers (excluding last column)
                for (int i = 0; i < columnCount; i++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText, cellFont));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cell.NoWrap = true;
                    pdfTable.AddCell(cell);
                }

                // Add rows (excluding last column)
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        for (int i = 0; i < columnCount; i++)
                        {
                            string cellText = row.Cells[i].Value != null ? row.Cells[i].Value.ToString() : "";
                            PdfPCell pdfCell = new PdfPCell(new Phrase(cellText, cellFont));
                            pdfCell.NoWrap = true;
                            pdfTable.AddCell(pdfCell);
                        }
                    }
                }

                doc.Add(pdfTable);
                MessageBox.Show("PDF exported successfully to Downloads folder!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "PDF Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                doc.Close();
            }
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            FilterByYearMonth();
        }

        private void FilterByYearMonth()
        {
            string selectedYear = comboYear.SelectedItem.ToString();
            string selectedMonth = comboMonth.SelectedItem.ToString();

            int year = selectedYear != "All" ? Convert.ToInt32(selectedYear) : -1;
            int month = selectedMonth != "All" ? DateTime.ParseExact(selectedMonth, "MMMM", null).Month : -1;

            DataTable filteredTable = t.Clone(); // Clone structure

            foreach (DataRow row in t.Rows)
            {
                DateTime date = Convert.ToDateTime(row["date"]);
                bool matchYear = (year == -1 || date.Year == year);
                bool matchMonth = (month == -1 || date.Month == month);

                if (matchYear && matchMonth)
                {
                    filteredTable.ImportRow(row);
                }
            }

            PopulateDataGridView(filteredTable);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            loadTable();
            PopulateYearMonthFilters();
        }


    }
}
