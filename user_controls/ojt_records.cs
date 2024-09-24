using ComponentFactory.Krypton.Toolkit;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caapOJTLogbookSystem.user_controls
{
    public partial class ojt_records : UserControl
    {
        public ojt_records()
        {
            InitializeComponent();
        }

        private void ojt_records_Load(object sender, EventArgs e)
        {
            Database.loadDataGridView(ojtTable, "SELECT\r\n\tojt_profile.visitor_id, \r\n\tojt_profile.MAC, \r\n\tojt_profile.fullname, \r\n\tojt_profile.contact_number, \r\n\tojt_profile.street, \r\n\tojt_profile.organization, \r\n\tojt_profile.added_at\r\nFROM\r\n\tojt_profile\r\nORDER BY\r\n\tojt_profile.added_at DESC");
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {   string keyword = textBox1.Text;

            Database.loadDataGridView(ojtTable, $"SELECT\r\n\tojt_profile.visitor_id, \r\n\tojt_profile.MAC, \r\n\tojt_profile.fullname, \r\n\tojt_profile.contact_number, \r\n\tojt_profile.street, \r\n\tojt_profile.organization, \r\n\tojt_profile.added_at\r\nFROM\r\n\tojt_profile\r\nWHERE\r\n\tojt_profile.fullname LIKE '%{keyword}%'  OR\r\n\tojt_profile.visitor_id LIKE '%{keyword}%'   OR\r\n\tojt_profile.organization LIKE '%{keyword}%'  OR ojt_profile.mac LIKE '%{keyword}%' \r\n\t  \r\nORDER BY\r\n\tojt_profile.added_at DESC");

            if (String.IsNullOrEmpty(keyword))
            {
                Database.loadDataGridView(ojtTable, "SELECT\r\n\tojt_profile.visitor_id, \r\n\tojt_profile.MAC, \r\n\tojt_profile.fullname, \r\n\tojt_profile.contact_number, \r\n\tojt_profile.street, \r\n\tojt_profile.organization, \r\n\tojt_profile.added_at\r\nFROM\r\n\tojt_profile\r\nORDER BY\r\n\tojt_profile.added_at DESC");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = fullnameBox.Text;
            string contact = contactBox.Text;
            string org = schoolBox.Text;
            string email = emailBox.Text;
            string street = AddressBox.Text;

            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(contact) || string.IsNullOrEmpty(org) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(street))
            {
                MessageBox.Show("Load the data first before updating it", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            if (ojtTable.Rows.Count > 0 )
            {
                int selectedRowIndex = ojtTable.CurrentRow.Index;
                string vID = ojtTable.Rows[selectedRowIndex].Cells[0].Value.ToString();

                try
                {
                    if (Database.Connect())
                    {
                        string query = $"UPDATE ojt_profile SET fullname = '{name}', contact_number = '{contact}', organization = '{org}', email = '{email}', street = '{street}' WHERE visitor_id = {Convert.ToInt16(vID)}";
                        using (MySqlCommand command = new MySqlCommand(query, Database.conn))
                        {
                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("Trainee Updated Successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fullnameBox.Clear();
                        contactBox.Clear();
                        AddressBox.Clear();
                        macBox.Clear();
                        emailBox.Clear();
                 
                        schoolBox.Clear();

                        Database.loadDataGridView(ojtTable, "SELECT\r\n\tojt_profile.visitor_id, \r\n\tojt_profile.MAC, \r\n\tojt_profile.fullname, \r\n\tojt_profile.contact_number, \r\n\tojt_profile.street, \r\n\tojt_profile.organization, \r\n\tojt_profile.added_at\r\nFROM\r\n\tojt_profile\r\nORDER BY\r\n\tojt_profile.added_at DESC");
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            if (ojtTable.Rows.Count > 0 )
            {
                int selectedRowIndex = ojtTable.CurrentRow.Index;
                string ojtID = ojtTable.Rows[selectedRowIndex].Cells[0].Value.ToString();

                try
                {
                    if (Database.Connect())
                    {
                        string query = "SELECT * FROM ojt_profile WHERE visitor_id = @visitor_id";
                        using (MySqlCommand cmd = new MySqlCommand(query, Database.conn))
                        {
                            cmd.Parameters.AddWithValue("@visitor_id", ojtID);

                            MySqlDataReader reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    fullnameBox.Text = reader["fullname"].ToString();
                                    contactBox.Text = reader["contact_number"].ToString();
                                    schoolBox.Text = reader["organization"].ToString();
                                    macBox.Text = reader["mac"].ToString();
                                    emailBox.Text = reader["email"].ToString();
                                    AddressBox.Text = reader["street"].ToString();
                                }
                                MessageBox.Show($"Trainee: {ojtID} loaded successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No data found for the selected trainee.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to connect to the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Database.conn.Close();
                }
            }
            else
            {
                MessageBox.Show("No trainee data available to load.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
