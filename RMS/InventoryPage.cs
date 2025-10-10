using System;

using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace RMS
{
    public partial class InventoryPage : Form
    {
        String q = "";
        MySqlConnection c1;
       
        MySqlDataAdapter da;
        DataTable t;

        public InventoryPage()
        {
            InitializeComponent();
            c1 = DBConnection.GetConnection();
        }

        private void InventoryPage_Load(object sender, EventArgs e)
        {
            LoadData();
           
        }
        private void LoadData()
        {
            textSearch.Focus();
            c1.Open();
            try
            {
                q = "select * from inventory order by name ";
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
        private void PopulateDataGridView(DataTable table)
        {
            dataInventory.Rows.Clear(); // Clear existing rows

            foreach (DataRow row in table.Rows)
            {
                int rowIndex = dataInventory.Rows.Add();

                dataInventory.Rows[rowIndex].Cells["productName"].Value = row["name"];
                dataInventory.Rows[rowIndex].Cells["sellingRate"].Value = row["selling_rate"];
                dataInventory.Rows[rowIndex].Cells["quantity"].Value = row["qty"];
                dataInventory.Rows[rowIndex].Cells["purchaseRate"].Value = row["purchase_rate"];
             
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            string filterText = textSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(filterText))
            {
                PopulateDataGridView(t); // Reset to original data if search text is empty
            }
            else
            {
                // Create a new DataTable to hold filtered results
                DataTable filteredTable = t.Clone(); // Clone structure

                foreach (DataRow row in t.Rows)
                {
                    string name = row["name"].ToString().ToLower();
                    string qty = row["qty"].ToString().ToLower();
                    string rate = row["purchase_rate"].ToString().ToLower();

                    if (name.Contains(filterText) || qty.Contains(filterText) || rate.Contains(filterText))
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                PopulateDataGridView(filteredTable);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            // PDF Export
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "PDF Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create PDF document
            Document doc = new Document(PageSize.A4, 10, 10, 10, 10);
            try
            {
                // Get Downloads folder path
                string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\InventoryReport.pdf";
                PdfWriter.GetInstance(doc, new FileStream(downloadsPath, FileMode.Create));
                doc.Open();

                // Add title
                Paragraph title = new Paragraph("Inventory Report", FontFactory.GetFont("Arial", "16", Font.Bold));
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                doc.Add(new Paragraph("\n"));

                // Create table with column count
                PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);

                // Add headers
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    pdfTable.AddCell(cell);
                }

                // Add rows
                // Add rows with custom alignment
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            string cellText = row.Cells[i].Value != null ? row.Cells[i].Value.ToString() : "";

                            PdfPCell pdfCell = new PdfPCell(new Phrase(cellText));

                            // Apply alignment based on column index
                            if (i == 0 ) // Assuming column 0 = Item Name, column 1 = Quantity
                            {
                                pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            }
                          
                            else if (i == 2) // Assuming column 2 = Price
                            {
                                pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            }
                            else
                            {
                                pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            }

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

        private void button2_Click(object sender, EventArgs e)
        {
            //low stock
            c1.Open();
            try
            {
                q = "select * from inventory where qty<10";
                da = new MySqlDataAdapter(q, c1);
                t = new DataTable();

                da.Fill(t);
                if(t.Rows.Count > 0)
                {
                    dataGridView1.DataSource = t;
                }
                else
                {
                    MessageBox.Show("Not Low Stock", "Inventory Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null; // Optional: clear the grid
                }

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

        private void button1_Click(object sender, EventArgs e)
        {
            //Zero stock
            c1.Open();
            try
            {
                q = "select * from inventory where qty=0";
                da = new MySqlDataAdapter(q, c1);
                t = new DataTable();

                da.Fill(t);
                if (t.Rows.Count > 0)
                {
                    dataGridView1.DataSource = t;
                }
                else
                {
                    MessageBox.Show("Not Low Stock", "Inventory Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null; // Optional: clear the grid
                }

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
