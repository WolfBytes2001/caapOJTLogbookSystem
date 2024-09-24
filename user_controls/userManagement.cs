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
    public partial class userManagement : UserControl
    {
        public userManagement()
        {
            InitializeComponent();
        }

        private void userManagement_Load(object sender, EventArgs e)
        {
            Database.loadDataGridView(adminTable, "SELECT * FROM admin");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (adminTable.Rows.Count > 0)
            {
                int selectedRowIndex = adminTable.CurrentRow.Index;
                string adminID = adminTable.Rows[selectedRowIndex].Cells[0].Value.ToString();

                try
                {
                    if (Database.Connect())
                    {
                        string query = $"SELECT * FROM admin WHERE id = {adminID}";
                        using (MySqlCommand cmd = new MySqlCommand(query, Database.conn))
                        {

                            MySqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {

                                usernameBox.Text = reader["username"].ToString();
                                PasswordBox.Text = reader["password"].ToString();

                            }
                        }
                        MessageBox.Show($"Admin:{adminID} loaded Sucessfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    Database.conn.Close();
                }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Database.Connect() && !string.IsNullOrEmpty(usernameBox.Text) && !string.IsNullOrEmpty(PasswordBox.Text))
            {
                if (adminTable.Rows.Count > 0)
                {
                    int selectedRowIndex = adminTable.CurrentRow.Index;
                    string vID = adminTable.Rows[selectedRowIndex].Cells[0].Value.ToString();

                    try
                    {
                        if (Database.Connect())
                        {
                            string query = $"UPDATE admin SET username = '{usernameBox.Text}', password = '{PasswordBox.Text}' WHERE id = {Convert.ToInt16(vID)}";
                            using (MySqlCommand command = new MySqlCommand(query, Database.conn))
                            {
                                command.ExecuteNonQuery();
                            }
                            MessageBox.Show("Admin Updated Successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            usernameBox.Clear();
                            PasswordBox.Clear();
                            Database.loadDataGridView(adminTable, "SELECT * FROM admin");
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            else
            {
                MessageBox.Show("Load data first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
                try
                {

                    if (Database.Connect() && !string.IsNullOrEmpty(usernameBox.Text ) && !string.IsNullOrEmpty(PasswordBox.Text))
                    {
                        string query = $"INSERT INTO admin(username,password)VALUES ('{usernameBox.Text}','{PasswordBox.Text}')";
                        using (MySqlCommand command = new MySqlCommand(query, Database.conn))
                        {
                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("Admin Added Successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        usernameBox.Clear();
                        PasswordBox.Clear();
                        Database.loadDataGridView(adminTable, "SELECT * FROM admin");
                    }
                    else
                    {
                    MessageBox.Show("Some fields are empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (adminTable.Rows.Count > 0)
            {
                int selectedRowIndex = adminTable.CurrentRow.Index;
                string vID = adminTable.Rows[selectedRowIndex].Cells[0].Value.ToString();

                try
                {
                    if (Database.Connect())
                    {
                        string query = $"DELETE FROM admin  WHERE id = {Convert.ToInt16(vID)}";
                        using (MySqlCommand command = new MySqlCommand(query, Database.conn))
                        {
                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("Admin deleted Successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        usernameBox.Clear();
                        PasswordBox.Clear();
                        Database.loadDataGridView(adminTable, "SELECT * FROM admin");
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Database.loadDataGridView(adminTable, $"SELECT * FROM admin WHERE username like '%{textBox1.Text}%'");
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                Database.loadDataGridView(adminTable, "SELECT * FROM admin");
            }
        }
    }
}
