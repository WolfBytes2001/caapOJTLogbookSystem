using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caapOJTLogbookSystem.forms
{
    public partial class viewLogbook : UserControl
    {
        public viewLogbook()
        {
            InitializeComponent();
        }

        private void viewLogbook_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Database.loadDataGridView(kryptonDataGridView1, "SELECT\r\n\tojt_profile.fullname, \r\n\tojt_profile.organization, \r\n\tlogbook_admin.visitor_entry, \r\n\tlogbook_admin.purpose, \r\n\tlogbook_admin.recorded_at\r\nFROM\r\n\tojt_profile\r\n\tINNER JOIN\r\n\tlogbook_admin\r\n\tON \r\n\t\tojt_profile.MAC = logbook_admin.MAC\r\nORDER BY\r\n\tlogbook_admin.recorded_at DESC");
        }

        private void kryptonCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }


        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

   
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void ExportDataGridViewToCSV(DataGridView dataGridView, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the header row
                    for (int i = 0; i < dataGridView.Columns.Count; i++)
                    {
                        writer.Write(dataGridView.Columns[i].HeaderText);
                        if (i < dataGridView.Columns.Count - 1)
                        {
                            writer.Write(",");
                        }
                    }
                    writer.WriteLine();

                    // Write the data rows
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            for (int i = 0; i < dataGridView.Columns.Count; i++)
                            {
                                writer.Write(row.Cells[i].Value?.ToString());
                                if (i < dataGridView.Columns.Count - 1)
                                {
                                    writer.Write(",");
                                }
                            }
                            writer.WriteLine();
                        }
                    }
                }

                MessageBox.Show("Data exported successfully.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while exporting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Database.loadDataGridView(kryptonDataGridView1, "SELECT\r\n\tojt_profile.fullname, \r\n\tojt_profile.organization, \r\n\tlogbook_admin.visitor_entry, \r\n\tlogbook_admin.purpose, \r\n\tlogbook_admin.recorded_at\r\nFROM\r\n\tojt_profile\r\n\tINNER JOIN\r\n\tlogbook_admin\r\n\tON \r\n\t\tojt_profile.MAC = logbook_admin.MAC\r\nORDER BY\r\n\tlogbook_admin.recorded_at DESC");
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void kryptonCheckBox1_CheckedChanged_1(object sender, EventArgs e)
        {
          
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            string selectedDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string query = $@"
        SELECT
	        ojt_profile.visitor_id, 
	        ojt_profile.fullname, 
	        logbook_admin.visitor_entry, 
	        logbook_admin.purpose, 
	        logbook_admin.recorded_at
        FROM
	        ojt_profile
	        INNER JOIN
	        logbook_admin
	        ON 
		        ojt_profile.MAC = logbook_admin.MAC
       
        WHERE
            DATE(logbook_admin.recorded_at) = '{selectedDate}'
        ORDER BY
            logbook_admin.recorded_at DESC";
            Database.loadDataGridView(kryptonDataGridView1, query);
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            string keyword = textBox1.Text;
            string query = $"SELECT\r\n\tojt_profile.fullname, \r\n\tojt_profile.organization, \r\n\tlogbook_admin.visitor_entry, \r\n\tlogbook_admin.purpose, \r\n\tlogbook_admin.recorded_at\r\nFROM\r\n\tojt_profile\r\n\tINNER JOIN\r\n\tlogbook_admin\r\n\tON \r\n\t\tojt_profile.MAC = logbook_admin.MAC\r\n\r\nWHERE\r\n\t ojt_profile.fullname LIKE '%{keyword}%' OR\r\n\tlogbook_admin.logbook_id LIKE '%{keyword}%'\r\n\t\r\nORDER BY\r\n\tlogbook_admin.recorded_at DESC";
            Database.loadDataGridView(kryptonDataGridView1, query);
            if (String.IsNullOrEmpty(keyword))
            {
                Database.loadDataGridView(kryptonDataGridView1, "SELECT\r\n\tojt_profile.fullname, \r\n\tojt_profile.organization, \r\n\tlogbook_admin.visitor_entry, \r\n\tlogbook_admin.purpose, \r\n\tlogbook_admin.recorded_at\r\nFROM\r\n\tojt_profile\r\n\tINNER JOIN\r\n\tlogbook_admin\r\n\tON \r\n\t\tojt_profile.MAC = logbook_admin.MAC\r\nORDER BY\r\n\tlogbook_admin.recorded_at DESC");
               
            }
        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            // Show a SaveFileDialog to let the user specify where to save the CSV file
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog.Title = "Save as CSV file";
                saveFileDialog.DefaultExt = "csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Call the export method with the chosen file path
                    ExportDataGridViewToCSV(kryptonDataGridView1, saveFileDialog.FileName);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
        }

        private void textBox1_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    }
}

