using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace RMS
{
    public partial class Receivable : Form
    {
        String q = "";
        MySqlConnection c1;
        MySqlDataAdapter da;
        DataTable t;

        public Receivable()
        {
            InitializeComponent();
            c1 = DBConnection.GetConnection();
        }

        private void Receivable_Load(object sender, EventArgs e)
        {

            LoadData();
        }
        private void LoadData()
        {
            txtSearch.Focus();
            c1.Open();
            try
            {
                q = "SELECT c.cname, c.cmobile,c.caddress, SUM(r.remaining) AS total_remaining FROM customers c JOIN receivable r ON c.cmobile = r.cmobile GROUP BY c.cmobile, c.cname having total_remaining>0 order by total_remaining desc;";
                da = new MySqlDataAdapter(q, c1);
                t = new DataTable();

                da.Fill(t);
                PopulateDataGridView(t);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (c1.State == ConnectionState.Open)
                    c1.Close();
            }
        }
        /*
         * Fetch and populate customer details
         */
        private void PopulateDataGridView(DataTable table)
        {
            dataGridView1.Rows.Clear(); // Clear existing rows

            foreach (DataRow row in table.Rows)
            {
                int rowIndex = dataGridView1.Rows.Add();

                dataGridView1.Rows[rowIndex].Cells["cname"].Value = row["cname"];
                dataGridView1.Rows[rowIndex].Cells["cmobile"].Value = row["cmobile"];
                dataGridView1.Rows[rowIndex].Cells["caddress"].Value = row["caddress"];
                dataGridView1.Rows[rowIndex].Cells["remAmount"].Value = row["total_remaining"];
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try 
            {

                if (e.ColumnIndex == 4 && e.RowIndex >= 0)
                {
                    string cname = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    long mobile = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    string add = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    decimal remaining = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());

                    ReceivableDetailscs obj = new ReceivableDetailscs(cname,mobile,add,remaining);
                    obj.ShowDialog();
                    //MessageBox.Show("" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(""+ex);
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No data found to Export...", "Omnimart360", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Document doc = new Document(PageSize.A4, 10, 10, 10, 10);
            try
            {
                string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\ReceivableList.pdf";
                PdfWriter.GetInstance(doc, new FileStream(downloadsPath, FileMode.Create));
                doc.Open();

                // Add title
                Paragraph title = new Paragraph("Receivable List", FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD));
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string filterText = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(filterText))
            {
                PopulateDataGridView(t); // Reset to original data if search text is empty
            }
            else
            {
                try
                {
                    DataView dv = new DataView(t);
                    dv.RowFilter = string.Format(
                        "CONVERT(cname, System.String) LIKE '%{0}%' OR " +
                        "CONVERT(cmobile, System.String) LIKE '%{0}%' OR " +
                        "CONVERT(caddress, System.String) LIKE '%{0}%' OR " +
                        "CONVERT(total_remaining, System.String) LIKE '%{0}%'",
                        filterText);
                    PopulateDataGridView(dv.ToTable());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during search: " + ex.Message);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
