using OxyPlot.WindowsForms;

namespace MTT01_winforms.UControls.Calculo
{
    partial class UCAnalisisDeDatos
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
            btnLoadCSV = new Button();
            txtPathCSV = new TextBox();
            openFileDialog1 = new OpenFileDialog();
            tabIntegracion = new TabPage();
            plotViewIntegracion = new PlotView();
            groupBox1 = new GroupBox();
            plotViewHLOC = new PlotView();
            tabData = new TabPage();
            dataGridView = new DataGridView();
            tabControlBase = new TabControl();
            lblPercentAlcista = new Label();
            lblPercentBajista = new Label();
            lblMsgBajista = new Label();
            lblMsgAlcista = new Label();
            tabIntegracion.SuspendLayout();
            groupBox1.SuspendLayout();
            tabData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            tabControlBase.SuspendLayout();
            SuspendLayout();
            // 
            // btnLoadCSV
            // 
            btnLoadCSV.Location = new Point(493, 32);
            btnLoadCSV.Name = "btnLoadCSV";
            btnLoadCSV.Size = new Size(399, 23);
            btnLoadCSV.TabIndex = 3;
            btnLoadCSV.Text = "Cargar csv de datos (mt4, mt5)";
            btnLoadCSV.UseVisualStyleBackColor = true;
            btnLoadCSV.Click += btnLoadCSV_Click;
            // 
            // txtPathCSV
            // 
            txtPathCSV.Location = new Point(15, 32);
            txtPathCSV.Name = "txtPathCSV";
            txtPathCSV.Size = new Size(472, 23);
            txtPathCSV.TabIndex = 4;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabIntegracion
            // 
            tabIntegracion.Controls.Add(plotViewIntegracion);
            tabIntegracion.Location = new Point(4, 24);
            tabIntegracion.Name = "tabIntegracion";
            tabIntegracion.Size = new Size(1187, 748);
            tabIntegracion.TabIndex = 2;
            tabIntegracion.Text = "Integracion";
            tabIntegracion.UseVisualStyleBackColor = true;
            // 
            // plotViewIntegracion
            // 
            plotViewIntegracion.Dock = DockStyle.Fill;
            plotViewIntegracion.Location = new Point(0, 0);
            plotViewIntegracion.Margin = new Padding(0);
            plotViewIntegracion.Name = "plotViewIntegracion";
            plotViewIntegracion.PanCursor = Cursors.Hand;
            plotViewIntegracion.Size = new Size(1187, 748);
            plotViewIntegracion.TabIndex = 1;
            plotViewIntegracion.ZoomHorizontalCursor = Cursors.SizeWE;
            plotViewIntegracion.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotViewIntegracion.ZoomVerticalCursor = Cursors.SizeNS;
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
            // plotViewHLOC
            // 
            plotViewHLOC.Dock = DockStyle.Fill;
            plotViewHLOC.Location = new Point(3, 19);
            plotViewHLOC.Margin = new Padding(0);
            plotViewHLOC.Name = "plotViewHLOC";
            plotViewHLOC.PanCursor = Cursors.Hand;
            plotViewHLOC.Size = new Size(1175, 656);
            plotViewHLOC.TabIndex = 0;
            plotViewHLOC.ZoomHorizontalCursor = Cursors.SizeWE;
            plotViewHLOC.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotViewHLOC.ZoomVerticalCursor = Cursors.SizeNS;
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
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(3, 3);
            dataGridView.Name = "dataGridView";
            dataGridView.Size = new Size(1181, 742);
            dataGridView.TabIndex = 2;
            // 
            // tabControlBase
            // 
            tabControlBase.Controls.Add(tabData);
            tabControlBase.Controls.Add(tabIntegracion);
            tabControlBase.Location = new Point(0, 61);
            tabControlBase.Name = "tabControlBase";
            tabControlBase.SelectedIndex = 0;
            tabControlBase.Size = new Size(1195, 776);
            tabControlBase.TabIndex = 5;
            // 
            // lblPercentAlcista
            // 
            lblPercentAlcista.AutoSize = true;
            lblPercentAlcista.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPercentAlcista.ForeColor = Color.FromArgb(0, 192, 0);
            lblPercentAlcista.Location = new Point(1123, 36);
            lblPercentAlcista.Name = "lblPercentAlcista";
            lblPercentAlcista.Size = new Size(48, 15);
            lblPercentAlcista.TabIndex = 14;
            lblPercentAlcista.Text = "00.00%";
            lblPercentAlcista.Visible = false;
            // 
            // lblPercentBajista
            // 
            lblPercentBajista.AutoSize = true;
            lblPercentBajista.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPercentBajista.ForeColor = Color.Red;
            lblPercentBajista.Location = new Point(1123, 36);
            lblPercentBajista.Name = "lblPercentBajista";
            lblPercentBajista.Size = new Size(48, 15);
            lblPercentBajista.TabIndex = 13;
            lblPercentBajista.Text = "00.00%";
            lblPercentBajista.Visible = false;
            // 
            // lblMsgBajista
            // 
            lblMsgBajista.AutoSize = true;
            lblMsgBajista.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMsgBajista.ForeColor = Color.Red;
            lblMsgBajista.Location = new Point(898, 36);
            lblMsgBajista.Name = "lblMsgBajista";
            lblMsgBajista.Size = new Size(219, 15);
            lblMsgBajista.TabIndex = 12;
            lblMsgBajista.Text = "Posibilidades de siguiente vela Bajista :";
            lblMsgBajista.Visible = false;
            // 
            // lblMsgAlcista
            // 
            lblMsgAlcista.AutoSize = true;
            lblMsgAlcista.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMsgAlcista.ForeColor = Color.FromArgb(0, 192, 0);
            lblMsgAlcista.Location = new Point(898, 36);
            lblMsgAlcista.Name = "lblMsgAlcista";
            lblMsgAlcista.Size = new Size(219, 15);
            lblMsgAlcista.TabIndex = 11;
            lblMsgAlcista.Text = "Posibilidades de siguiente vela Alcista: ";
            lblMsgAlcista.Visible = false;
            // 
            // UCAnalisisDeDatos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblPercentAlcista);
            Controls.Add(lblPercentBajista);
            Controls.Add(lblMsgBajista);
            Controls.Add(lblMsgAlcista);
            Controls.Add(tabControlBase);
            Controls.Add(txtPathCSV);
            Controls.Add(btnLoadCSV);
            Name = "UCAnalisisDeDatos";
            Size = new Size(1195, 860);
            tabIntegracion.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tabData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            tabControlBase.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnLoadCSV;
        private TextBox txtPathCSV;
        private OpenFileDialog openFileDialog1;
        private TabPage tabIntegracion;
        private PlotView plotViewIntegracion;
        private GroupBox groupBox1;
        private PlotView plotViewHLOC;
        private TabPage tabData;
        private DataGridView dataGridView;
        private TabControl tabControlBase;
        private Label lblPercentAlcista;
        private Label lblPercentBajista;
        private Label lblMsgBajista;
        private Label lblMsgAlcista;
    }
}
