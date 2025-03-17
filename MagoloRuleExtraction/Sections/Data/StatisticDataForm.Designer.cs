namespace MagoloRuleExtraction.Sections.Data
{
    partial class StatisticDataForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            lblTitle = new Label();
            dgvReturnsStats = new DataGridView();
            button1 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReturnsStats).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(lblTitle);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(684, 50);
            panel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(684, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Estadísticas de Retornos";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvReturnsStats
            // 
            dgvReturnsStats.AllowUserToAddRows = false;
            dgvReturnsStats.AllowUserToDeleteRows = false;
            dgvReturnsStats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReturnsStats.BackgroundColor = SystemColors.Control;
            dgvReturnsStats.BorderStyle = BorderStyle.None;
            dgvReturnsStats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReturnsStats.Dock = DockStyle.Fill;
            dgvReturnsStats.Location = new Point(0, 50);
            dgvReturnsStats.Name = "dgvReturnsStats";
            dgvReturnsStats.ReadOnly = true;
            dgvReturnsStats.RowHeadersVisible = false;
            dgvReturnsStats.Size = new Size(684, 211);
            dgvReturnsStats.TabIndex = 1;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaption;
            button1.Location = new Point(580, 233);
            button1.Name = "button1";
            button1.Size = new Size(104, 28);
            button1.TabIndex = 2;
            button1.Text = "Exportar a CSV";
            button1.UseVisualStyleBackColor = false;
            button1.Click += btnSaveToCSV_Click;
            // 
            // StatisticDataForm
            // 
            ClientSize = new Size(684, 261);
            Controls.Add(button1);
            Controls.Add(dgvReturnsStats);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "StatisticDataForm";
            StartPosition = FormStartPosition.CenterParent;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReturnsStats).EndInit();
            ResumeLayout(false);

        }

        private DataGridView dgvReturnsStats;
        private Panel panel1;
        private Label lblTitle;
        private Button button1;
    }

        #endregion
    
}