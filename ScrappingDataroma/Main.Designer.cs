using System.Windows.Forms;

namespace ScrappingDataroma
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnMAGIC = new Button();
            listBoxMan = new ListBox();
            listBoxActivos = new ListBox();
            listBoxResult = new ListBox();
            txtBoxNumMANs = new TextBox();
            txtBoxPesoActivo = new TextBox();
            label1 = new Label();
            label2 = new Label();
            btnRefresh = new Button();
            lblTotal = new Label();
            listBoxFINAL = new ListBox();
            label3 = new Label();
            btnSave = new Button();
            lblSaved = new Label();
            listBoxSalen = new ListBox();
            listBoxEntran = new ListBox();
            label4 = new Label();
            label5 = new Label();
            btnDownload = new Button();
            lblLastDownload = new Label();
            btnSearch = new Button();
            txtSearch = new TextBox();
            lblSearch = new Label();
            SuspendLayout();
            // 
            // btnMAGIC
            // 
            btnMAGIC.Location = new Point(359, 9);
            btnMAGIC.Name = "btnMAGIC";
            btnMAGIC.Size = new Size(138, 24);
            btnMAGIC.TabIndex = 0;
            btnMAGIC.Text = "MAGIC!!";
            btnMAGIC.UseVisualStyleBackColor = true;
            btnMAGIC.Click += btnMAGIC_Click;
            // 
            // listBoxMan
            // 
            listBoxMan.Enabled = false;
            listBoxMan.FormattingEnabled = true;
            listBoxMan.ItemHeight = 14;
            listBoxMan.Location = new Point(12, 38);
            listBoxMan.Name = "listBoxMan";
            listBoxMan.Size = new Size(338, 662);
            listBoxMan.TabIndex = 1;
            // 
            // listBoxActivos
            // 
            listBoxActivos.Enabled = false;
            listBoxActivos.FormattingEnabled = true;
            listBoxActivos.ItemHeight = 14;
            listBoxActivos.Location = new Point(359, 39);
            listBoxActivos.Name = "listBoxActivos";
            listBoxActivos.Size = new Size(138, 158);
            listBoxActivos.TabIndex = 2;
            // 
            // listBoxResult
            // 
            listBoxResult.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxResult.Enabled = false;
            listBoxResult.FormattingEnabled = true;
            listBoxResult.ItemHeight = 14;
            listBoxResult.Location = new Point(697, 108);
            listBoxResult.Name = "listBoxResult";
            listBoxResult.Size = new Size(373, 592);
            listBoxResult.TabIndex = 3;
            listBoxResult.DrawItem += ListBox1_DrawItem;
            listBoxResult.MouseDoubleClick += listBoxResult_MouseDoubleClick;
            // 
            // txtBoxNumMANs
            // 
            txtBoxNumMANs.Location = new Point(697, 52);
            txtBoxNumMANs.Name = "txtBoxNumMANs";
            txtBoxNumMANs.Size = new Size(45, 22);
            txtBoxNumMANs.TabIndex = 4;
            txtBoxNumMANs.Text = "0";
            txtBoxNumMANs.TextAlign = HorizontalAlignment.Center;
            // 
            // txtBoxPesoActivo
            // 
            txtBoxPesoActivo.Location = new Point(697, 80);
            txtBoxPesoActivo.Name = "txtBoxPesoActivo";
            txtBoxPesoActivo.Size = new Size(45, 22);
            txtBoxPesoActivo.TabIndex = 5;
            txtBoxPesoActivo.Text = "3";
            txtBoxPesoActivo.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(751, 58);
            label1.Name = "label1";
            label1.Size = new Size(252, 14);
            label1.TabIndex = 6;
            label1.Text = "Número de Portfoleos a contabilizar";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(748, 85);
            label2.Name = "label2";
            label2.Size = new Size(322, 14);
            label2.TabIndex = 7;
            label2.Text = "Peso mínimo la cartera para ser contabilizado";
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(697, 22);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(338, 24);
            btnRefresh.TabIndex = 8;
            btnRefresh.Text = "REFRESH / CALCULATE";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(503, 13);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(21, 14);
            lblTotal.TabIndex = 9;
            lblTotal.Text = "00";
            // 
            // listBoxFINAL
            // 
            listBoxFINAL.FormattingEnabled = true;
            listBoxFINAL.ItemHeight = 14;
            listBoxFINAL.Location = new Point(359, 232);
            listBoxFINAL.Name = "listBoxFINAL";
            listBoxFINAL.Size = new Size(239, 438);
            listBoxFINAL.TabIndex = 10;
            listBoxFINAL.MouseDoubleClick += listBoxFINAL_MouseDoubleClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Consolas", 9F, FontStyle.Bold);
            label3.Location = new Point(436, 209);
            label3.Name = "label3";
            label3.Size = new Size(84, 14);
            label3.TabIndex = 11;
            label3.Text = "LISTA FINAL";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(359, 676);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(239, 24);
            btnSave.TabIndex = 12;
            btnSave.Text = "SAVE";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lblSaved
            // 
            lblSaved.AutoSize = true;
            lblSaved.Location = new Point(697, 5);
            lblSaved.Name = "lblSaved";
            lblSaved.Size = new Size(14, 14);
            lblSaved.TabIndex = 13;
            lblSaved.Text = ".";
            // 
            // listBoxSalen
            // 
            listBoxSalen.ForeColor = Color.Red;
            listBoxSalen.ItemHeight = 14;
            listBoxSalen.Location = new Point(607, 232);
            listBoxSalen.Name = "listBoxSalen";
            listBoxSalen.Size = new Size(81, 186);
            listBoxSalen.TabIndex = 14;
            // 
            // listBoxEntran
            // 
            listBoxEntran.ForeColor = Color.Green;
            listBoxEntran.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxEntran.FormattingEnabled = true;
            listBoxEntran.ItemHeight = 14;
            listBoxEntran.Location = new Point(607, 442);
            listBoxEntran.Name = "listBoxEntran";
            listBoxEntran.Size = new Size(81, 228);
            listBoxEntran.DrawItem += ListBoxEntran_DrawItem;
            listBoxEntran.TabIndex = 15;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Consolas", 9F, FontStyle.Bold);
            label4.ForeColor = Color.Red;
            label4.Location = new Point(624, 214);
            label4.Name = "label4";
            label4.Size = new Size(42, 14);
            label4.TabIndex = 16;
            label4.Text = "SALEN";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Consolas", 9F, FontStyle.Bold);
            label5.ForeColor = Color.Green;
            label5.Location = new Point(624, 422);
            label5.Name = "label5";
            label5.Size = new Size(49, 14);
            label5.TabIndex = 17;
            label5.Text = "ENTRAN";
            // 
            // btnDownload
            // 
            btnDownload.BackColor = Color.FromArgb(192, 255, 192);
            btnDownload.Location = new Point(12, 8);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(120, 24);
            btnDownload.TabIndex = 18;
            btnDownload.Text = "DOWNLOAD";
            btnDownload.UseVisualStyleBackColor = false;
            btnDownload.Click += btnDownload_Click;
            // 
            // lblLastDownload
            // 
            lblLastDownload.AutoSize = true;
            lblLastDownload.Location = new Point(138, 14);
            lblLastDownload.Name = "lblLastDownload";
            lblLastDownload.Size = new Size(63, 14);
            lblLastDownload.TabIndex = 19;
            lblLastDownload.Text = "YYYYMMdd";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(607, 108);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(81, 23);
            btnSearch.TabIndex = 20;
            btnSearch.Text = "Buscar";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(608, 137);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(80, 22);
            txtSearch.TabIndex = 21;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(634, 162);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(28, 14);
            lblSearch.TabIndex = 22;
            lblSearch.Text = "000";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1169, 715);
            Controls.Add(lblSearch);
            Controls.Add(txtSearch);
            Controls.Add(btnSearch);
            Controls.Add(lblLastDownload);
            Controls.Add(btnDownload);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(listBoxEntran);
            Controls.Add(listBoxSalen);
            Controls.Add(lblSaved);
            Controls.Add(btnSave);
            Controls.Add(label3);
            Controls.Add(listBoxFINAL);
            Controls.Add(lblTotal);
            Controls.Add(btnRefresh);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtBoxPesoActivo);
            Controls.Add(txtBoxNumMANs);
            Controls.Add(listBoxResult);
            Controls.Add(listBoxActivos);
            Controls.Add(listBoxMan);
            Controls.Add(btnMAGIC);
            Font = new Font("Consolas", 9F);
            Name = "Main";
            Text = "DataRomaData";
            Load += Main_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnMAGIC;
        private ListBox listBoxMan;
        private ListBox listBoxActivos;
        private ListBox listBoxResult;
        private TextBox txtBoxNumMANs;
        private TextBox txtBoxPesoActivo;
        private Label label1;
        private Label label2;
        private Button btnRefresh;
        private Label lblTotal;
        private ListBox listBoxFINAL;
        private Label label3;
        private Button btnSave;
        private Label lblSaved;
        private ListBox listBoxSalen;
        private ListBox listBoxEntran;
        private Label label4;
        private Label label5;
        private Button btnDownload;
        private Label lblLastDownload;
        private Button btnSearch;
        private TextBox txtSearch;
        private Label lblSearch;
    }
}
