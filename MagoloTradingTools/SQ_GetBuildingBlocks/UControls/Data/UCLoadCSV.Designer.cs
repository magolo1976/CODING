using OxyPlot.WindowsForms;

namespace MTT01_winforms.UControls.Data
{
    partial class UCLoadCSV
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            btnSave = new Button();
            dataGridView = new DataGridView();
            btnLoadCSV = new Button();
            txtPathCSV = new TextBox();
            openFileDialog1 = new OpenFileDialog();
            tabControlBase = new TabControl();
            tabData = new TabPage();
            tabGrafic = new TabPage();
            groupBox2 = new GroupBox();
            checkClose = new CheckBox();
            checkLow = new CheckBox();
            checkHight = new CheckBox();
            checkOpen = new CheckBox();
            groupBox1 = new GroupBox();
            plotViewHLOC = new PlotView();
            tabGauss = new TabPage();
            plotViewGauss = new PlotView();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            tabControlBase.SuspendLayout();
            tabData.SuspendLayout();
            tabGrafic.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabGauss.SuspendLayout();
            SuspendLayout();
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Consolas", 8.25F);
            btnSave.Location = new Point(609, 32);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 1;
            btnSave.Text = "Salvar";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(3, 3);
            dataGridView.Name = "dataGridView";
            dataGridView.Size = new Size(1181, 742);
            dataGridView.TabIndex = 2;
            // 
            // btnLoadCSV
            // 
            btnLoadCSV.Location = new Point(357, 32);
            btnLoadCSV.Name = "btnLoadCSV";
            btnLoadCSV.Size = new Size(75, 23);
            btnLoadCSV.TabIndex = 3;
            btnLoadCSV.Text = "Cargar csv";
            btnLoadCSV.UseVisualStyleBackColor = true;
            btnLoadCSV.Click += btnLoadCSV_Click;
            // 
            // txtPathCSV
            // 
            txtPathCSV.Location = new Point(15, 32);
            txtPathCSV.Name = "txtPathCSV";
            txtPathCSV.Size = new Size(336, 23);
            txtPathCSV.TabIndex = 4;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabControlBase
            // 
            tabControlBase.Controls.Add(tabData);
            tabControlBase.Controls.Add(tabGrafic);
            tabControlBase.Controls.Add(tabGauss);
            tabControlBase.Location = new Point(0, 61);
            tabControlBase.Name = "tabControlBase";
            tabControlBase.SelectedIndex = 0;
            tabControlBase.Size = new Size(1195, 776);
            tabControlBase.TabIndex = 5;
            // 
            // tabData
            // 
            tabData.Controls.Add(dataGridView);
            tabData.Location = new Point(4, 24);
            tabData.Name = "tabData";
            tabData.Padding = new Padding(3);
            tabData.Size = new Size(1187, 748);
            tabData.TabIndex = 0;
            tabData.Text = "Datos";
            tabData.UseVisualStyleBackColor = true;
            // 
            // tabGrafic
            // 
            tabGrafic.Controls.Add(groupBox2);
            tabGrafic.Controls.Add(groupBox1);
            tabGrafic.Location = new Point(4, 24);
            tabGrafic.Name = "tabGrafic";
            tabGrafic.Padding = new Padding(3);
            tabGrafic.Size = new Size(1187, 748);
            tabGrafic.TabIndex = 1;
            tabGrafic.Text = "Gráfica";
            tabGrafic.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(checkClose);
            groupBox2.Controls.Add(checkLow);
            groupBox2.Controls.Add(checkHight);
            groupBox2.Controls.Add(checkOpen);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(3, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1181, 64);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Opciones";
            // 
            // checkClose
            // 
            checkClose.AutoSize = true;
            checkClose.Checked = true;
            checkClose.CheckState = CheckState.Checked;
            checkClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            checkClose.ForeColor = Color.FromArgb(192, 192, 0);
            checkClose.Location = new Point(185, 29);
            checkClose.Name = "checkClose";
            checkClose.Size = new Size(55, 19);
            checkClose.TabIndex = 3;
            checkClose.Text = "Close";
            checkClose.UseVisualStyleBackColor = true;
            checkClose.CheckedChanged += checkClose_CheckedChanged;
            // 
            // checkLow
            // 
            checkLow.AutoSize = true;
            checkLow.Checked = true;
            checkLow.CheckState = CheckState.Checked;
            checkLow.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            checkLow.ForeColor = Color.FromArgb(192, 0, 0);
            checkLow.Location = new Point(131, 29);
            checkLow.Name = "checkLow";
            checkLow.Size = new Size(49, 19);
            checkLow.TabIndex = 2;
            checkLow.Text = "Low";
            checkLow.UseVisualStyleBackColor = true;
            checkLow.CheckedChanged += checkLow_CheckedChanged;
            // 
            // checkHight
            // 
            checkHight.AutoSize = true;
            checkHight.Checked = true;
            checkHight.CheckState = CheckState.Checked;
            checkHight.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            checkHight.ForeColor = Color.RoyalBlue;
            checkHight.Location = new Point(69, 29);
            checkHight.Name = "checkHight";
            checkHight.Size = new Size(57, 19);
            checkHight.TabIndex = 1;
            checkHight.Text = "Hight";
            checkHight.UseVisualStyleBackColor = true;
            checkHight.CheckedChanged += checkHight_CheckedChanged;
            // 
            // checkOpen
            // 
            checkOpen.AutoSize = true;
            checkOpen.Checked = true;
            checkOpen.CheckState = CheckState.Checked;
            checkOpen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            checkOpen.ForeColor = Color.FromArgb(0, 192, 0);
            checkOpen.Location = new Point(8, 29);
            checkOpen.Name = "checkOpen";
            checkOpen.Size = new Size(56, 19);
            checkOpen.TabIndex = 0;
            checkOpen.Text = "Open";
            checkOpen.UseVisualStyleBackColor = true;
            checkOpen.CheckedChanged += checkOpen_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(plotViewHLOC);
            groupBox1.Dock = DockStyle.Bottom;
            groupBox1.Location = new Point(3, 67);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1181, 678);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Gráfica";
            // 
            // plotView
            // 
            plotViewHLOC.Dock = DockStyle.Fill;
            plotViewHLOC.Location = new Point(3, 19);
            plotViewHLOC.Margin = new Padding(0);
            plotViewHLOC.Name = "plotView";
            plotViewHLOC.PanCursor = Cursors.Hand;
            plotViewHLOC.Size = new Size(1175, 656);
            plotViewHLOC.TabIndex = 0;
            plotViewHLOC.ZoomHorizontalCursor = Cursors.SizeWE;
            plotViewHLOC.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotViewHLOC.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // tabGauss
            // 
            tabGauss.Controls.Add(plotViewGauss);
            tabGauss.Location = new Point(4, 24);
            tabGauss.Name = "tabGauss";
            tabGauss.Size = new Size(1187, 748);
            tabGauss.TabIndex = 2;
            tabGauss.Text = "Gauss";
            tabGauss.UseVisualStyleBackColor = true;
            // 
            // plotView1
            // 
            plotViewGauss.Dock = DockStyle.Fill;
            plotViewGauss.Location = new Point(0, 0);
            plotViewGauss.Margin = new Padding(0);
            plotViewGauss.Name = "plotView1";
            plotViewGauss.PanCursor = Cursors.Hand;
            plotViewGauss.Size = new Size(1187, 748);
            plotViewGauss.TabIndex = 1;
            plotViewGauss.ZoomHorizontalCursor = Cursors.SizeWE;
            plotViewGauss.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotViewGauss.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // UCLoadCSV
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControlBase);
            Controls.Add(txtPathCSV);
            Controls.Add(btnLoadCSV);
            Controls.Add(btnSave);
            Name = "UCLoadCSV";
            Size = new Size(1195, 860);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            tabControlBase.ResumeLayout(false);
            tabData.ResumeLayout(false);
            tabGrafic.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            tabGauss.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnSave;
        private DataGridView dataGridView;
        private Button btnLoadCSV;
        private TextBox txtPathCSV;
        private OpenFileDialog openFileDialog1;
        private TabControl tabControlBase;
        private TabPage tabData;
        private TabPage tabGrafic;
        private PlotView plotViewHLOC;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private CheckBox checkClose;
        private CheckBox checkLow;
        private CheckBox checkHight;
        private CheckBox checkOpen;
        private TabPage tabGauss;
        private PlotView plotViewGauss;
    }
}
