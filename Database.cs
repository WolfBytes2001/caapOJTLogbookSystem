using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace caapOJTLogbookSystem
{
    internal class Database
    {

        private static string server = "localhost";
        private static string database = "db_caap";
        private static string username = "root";
        private static string password = "root";
        public static MySqlConnection conn;

        public static Boolean Connect()
        {
            try
            {

                string connectionString = $"Server={server};Database={database};User ID={username};Password={password};";
                conn = new MySqlConnection(connectionString);
                conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to the database: " + ex.Message);
                return false;
                throw;
            }
        }

        public static void LoadComboBox(ComboBox cb, string columnName, string tableName)
        {
            try
            {
                if (Connect())
                {
                    string query = $"SELECT {columnName} FROM {tableName}";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    cb.Items.Clear();

                    while (reader.Read())
                    {
                        cb.Items.Add(reader[$"{columnName}"].ToString());
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading combobox: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void LoadListView(ListView lv)
        {
            try
            {
                if (Connect())
                {
                    string query = "SELECT patientID, fullname FROM patient_record";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    lv.Items.Clear();

                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["patientID"].ToString());
                        item.SubItems.Add(reader["fullname"].ToString());
                        lv.Items.Add(item);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading list view: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void loadDataGridView(DataGridView dgv, string query)
        {
            try
            {
                if (Connect())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dgv.DataSource = table;
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public static void appendNewEntry(DataGridView dgv, string query)
        {
            try
            {
                if (Connect())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable newEntries = new DataTable();
                    adapter.Fill(newEntries);

                    DataTable currentData = dgv.DataSource as DataTable;

                    if (currentData != null)
                    {
                        foreach (DataRow row in newEntries.Rows)
                        {
                            currentData.ImportRow(row);
                        }
                    }
                    else
                    {
                        dgv.DataSource = newEntries;
                        MessageBox.Show("new entry");

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error appending new entries: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
