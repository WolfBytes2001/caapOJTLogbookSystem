using ComponentFactory.Krypton.Toolkit;
using MySql.Data.MySqlClient;
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

namespace caapOJTLogbookSystem.user_controls
{
    public partial class reports : UserControl
    {
        public reports()
        {
            InitializeComponent();
        }

        private void reports_Load(object sender, EventArgs e)
        {
            Database.loadDataGridView(reportsTable, GetBaseQuery());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
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
                    ExportDataGridViewToCSV(reportsTable, saveFileDialog.FileName);
                }
            }
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
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim();

            // Start with the base query
            string query = GetBaseQuery();

            // Add a filtering condition if searchText is not empty
            if (!string.IsNullOrEmpty(searchText))
            {
                query += $" WHERE fullname LIKE '%{searchText}%'";
            }
            
            // Load the data into the DataGridView
            Database.loadDataGridView(reportsTable, query);

        }
        private string GetBaseQuery()
        {
            return @"WITH Attendance AS (
        SELECT
            ojt_profile.fullname,
            logbook_admin.visitor_entry,
            logbook_admin.recorded_at,
            LEAD(logbook_admin.recorded_at) OVER (PARTITION BY ojt_profile.fullname ORDER BY logbook_admin.recorded_at) AS next_recorded_at
        FROM
            ojt_profile
        INNER JOIN
            logbook_admin
        ON 
            ojt_profile.MAC = logbook_admin.MAC
        WHERE
            logbook_admin.visitor_entry LIKE '%IN%' OR logbook_admin.visitor_entry LIKE '%OUT%'
        ORDER BY 
            ojt_profile.fullname, 
            logbook_admin.recorded_at
    ),
    FilteredAttendance AS (
        SELECT
            fullname,
            visitor_entry,
            CASE 
                WHEN HOUR(recorded_at) BETWEEN 8 AND 11 THEN recorded_at
                WHEN HOUR(recorded_at) BETWEEN 13 AND 16 THEN recorded_at
                ELSE NULL 
            END AS filtered_recorded_at,
            CASE 
                WHEN HOUR(next_recorded_at) BETWEEN 8 AND 11 THEN next_recorded_at
                WHEN HOUR(next_recorded_at) BETWEEN 13 AND 16 THEN next_recorded_at
                ELSE NULL 
            END AS filtered_next_recorded_at
        FROM
            Attendance
    ),
    VisitStats AS (
        SELECT 
            fullname,
            MIN(filtered_recorded_at) AS start_date,
            SUM(
                CASE 
                    WHEN HOUR(filtered_recorded_at) BETWEEN 8 AND 11 AND HOUR(filtered_next_recorded_at) BETWEEN 8 AND 11 THEN TIMESTAMPDIFF(MINUTE, filtered_recorded_at, filtered_next_recorded_at)
                    WHEN HOUR(filtered_recorded_at) BETWEEN 13 AND 16 AND HOUR(filtered_next_recorded_at) BETWEEN 13 AND 16 THEN TIMESTAMPDIFF(MINUTE, filtered_recorded_at, filtered_next_recorded_at)
                    ELSE 0 
                END
            ) / 60 AS total_duty_hours
        FROM
            FilteredAttendance
        WHERE
            visitor_entry LIKE '%IN%' AND filtered_next_recorded_at IS NOT NULL
        GROUP BY 
            fullname
    )
    SELECT 
        fullname,
        start_date,
        total_duty_hours
    FROM
        VisitStats";
        }



        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    }
}
