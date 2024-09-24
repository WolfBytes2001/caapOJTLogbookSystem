using caapOJTLogbookSystem.forms;
using caapOJTLogbookSystem.user_controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caapOJTLogbookSystem
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void viewLogbookRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewLogbook viewLogbook = new viewLogbook();
            viewLogbook.Dock = DockStyle.Fill;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(viewLogbook);
            viewLogbook.Show();
        }

        private void viewOJTTraineesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ojt_records ojt_Records = new ojt_records();
            ojt_Records.Dock = DockStyle.Fill;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(ojt_Records);
            ojt_Records.Show();
        }

        private void logInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginAuth loginAuth = new LoginAuth(); 
            loginAuth.Show();
        }

        private void getReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reports reports = new reports();
            reports.Dock = DockStyle.Fill;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(reports);
            reports.Show();
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
