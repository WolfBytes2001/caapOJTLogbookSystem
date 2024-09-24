namespace caapOJTLogbookSystem.user_controls
{
    partial class reports
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.reportsTable = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.reportsTable)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportsTable
            // 
            this.reportsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.reportsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reportsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportsTable.Location = new System.Drawing.Point(0, 147);
            this.reportsTable.Name = "reportsTable";
            this.reportsTable.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Black;
            this.reportsTable.RowHeadersWidth = 51;
            this.reportsTable.RowTemplate.Height = 24;
            this.reportsTable.Size = new System.Drawing.Size(1082, 435);
            this.reportsTable.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Navy;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.kryptonButton1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1082, 147);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(446, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "REPORTS";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonButton1.Location = new System.Drawing.Point(989, 101);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.kryptonButton1.Size = new System.Drawing.Size(79, 34);
            this.kryptonButton1.TabIndex = 2;
            this.kryptonButton1.Values.Text = "Export";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(141, 53);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(800, 30);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Search";
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 400;
            // 
            // reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.reportsTable);
            this.Controls.Add(this.panel1);
            this.Name = "reports";
            this.Size = new System.Drawing.Size(1082, 582);
            this.Load += new System.EventHandler(this.reports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.reportsTable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView reportsTable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
