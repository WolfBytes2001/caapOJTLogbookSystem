using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace caapOJTLogbookSystem.user_controls
{
    public partial class LoginAuth : Form
    {
        public LoginAuth()
        {
            InitializeComponent();
            // Set the LinkLabel text and add a link
            
            linkLabel1.Links.Add(0, linkLabel1.Text.Length, "https://www.facebook.com/renz.santiago.9803");

            // Attach the LinkClicked event handler
            linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Extract the URL from the LinkLabel
            string url = e.Link.LinkData as string;
            if (url != null)
            {
                try
                {
                    // Open the URL in the default web browser
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unable to open link: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (Database.Connect())
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM admin WHERE username = @username AND password = @password";
                    using (MySqlCommand cmd = new MySqlCommand(query, Database.conn))
                    {
                        // Use parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@username", usernameBox.Text);
                        cmd.Parameters.AddWithValue("@password", passwordBox.Text);

                        // Execute the query and get the result
                        int result = Convert.ToInt32(cmd.ExecuteScalar());

                        // Check if a matching record was found
                        if (result > 0)
                        {
                           
                                userManagement userManagement = new userManagement();
                                userManagement.Dock = DockStyle.Fill;
                                Program.dashboard.mainPanel.Controls.Clear();
                                Program.dashboard.mainPanel.Controls.Add(userManagement);
                                userManagement.Show();
                                Close();
                            
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.", "Invalid Credentials", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Unable to connect to the database.");
            }

        }

        private void LoginAuth_Load(object sender, EventArgs e)
        {

        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent the ding sound
                button1_Click(sender, e); // Call the login method
            }
        }
    }
}
