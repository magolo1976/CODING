using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using MagoloRuleExtraction.Classes;
using OxyPlot;
using OxyPlot.WindowsForms;

namespace MagoloRuleExtraction.Sections.Data
{
    public partial class DataUC : UserControl
    {
        // Propiedades para almacenar los datos y máscaras
        private DataTable _dataTable;
        private List<bool> _maskTrain;
        private List<bool> _maskTest;
        private List<bool> _maskForward;
        private List<double> _trainReturns;
        private List<double> _testReturns;
        private List<double> _forwardReturns;
        private string _dateColumnName;
        private double _pValue;
        private double _totalReturns;

        public DataUC()
        {
            InitializeComponent();

            InitializeControls();
        }

        #region Initialize Controls

        private void InitializeControls()
        {
            // Configurar el layout principal
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.Dock = DockStyle.Fill;

            // Crear controles
            Button btnUpload = new Button
            {
                Text = "Seleccionar archivo CSV",
                BackColor = SystemColors.ActiveCaption,
                Width = 200,
                Height = 30
            };
            btnUpload.Click += BtnUpload_Click;

            //  Tabs para mostrar los datos
            TabPage tabData = new TabPage
            {
                Location = new Point(4, 24),
                Name = "tabData",
                Padding = new Padding(3),
                TabIndex = 0,
                Text = "Datos proporcionados",
                UseVisualStyleBackColor = true
            };

            TabPage tabGrafic = new TabPage
            {
                Location = new Point(4, 24),
                Name = "tabGrafic",
                Padding = new Padding(3),
                TabIndex = 0,
                Text = "Evolución del precio",
                UseVisualStyleBackColor = true
            };

            TabPage tabTraiTest = new TabPage
            {
                Location = new Point(4, 24),
                Name = "tabTraiTest",
                Padding = new Padding(3),
                TabIndex = 0,
                Text = "Comparación de Distribuciones Train-Test",
                UseVisualStyleBackColor = true
            };

            TabPage tabMagnReturnsTrainTest = new TabPage
            {
                Location = new Point(4, 24),
                Name = "tabMagnReturnsTrainTest",
                Padding = new Padding(3),
                TabIndex = 0,
                Text = "Análisis de Magnitud de Retornos Train-Test",
                UseVisualStyleBackColor = true
            };

            TabControl tabControlBase = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 1, true),
                Name = "tabControlBase"
            };

            // Crear RichTextBox para mostrar los datos
            RichTextBox rtbData = new RichTextBox
            {
                Name = "rtbData",
                Dock = DockStyle.Fill,
                ReadOnly = true,
                ScrollBars = RichTextBoxScrollBars.ForcedBoth,
                WordWrap = false
            };

            // PlotView para representar el grafico
            PlotView plotView = new PlotView
            {
                Dock = DockStyle.Fill,
                Location = new Point(3, 19),
                Margin = new Padding(0),
                Name = "plotView",
                PanCursor = Cursors.Hand,
                TabIndex = 0,
                ZoomHorizontalCursor = Cursors.SizeWE,
                ZoomRectangleCursor = Cursors.SizeNWSE,
                ZoomVerticalCursor = Cursors.SizeNS
            };

            PlotView plotTrainTest = new PlotView
            {
                Dock = DockStyle.Fill,
                Location = new Point(3, 19),
                Margin = new Padding(0),
                Name = "plotTrainTest",
                PanCursor = Cursors.Hand,
                TabIndex = 0,
                ZoomHorizontalCursor = Cursors.SizeWE,
                ZoomRectangleCursor = Cursors.SizeNWSE,
                ZoomVerticalCursor = Cursors.SizeNS
            };

            PlotView plotMagnReturnsTrainTest = new PlotView
            {
                Name = "plotMagnReturnsTrainTest",
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Size = new Size(600, 400),
                BackColor = Color.WhiteSmoke,
            };

            tabData.Controls.Add(rtbData);
            tabGrafic.Controls.Add(plotView);
            tabTraiTest.Controls.Add(plotTrainTest);
            tabMagnReturnsTrainTest.Controls.Add(plotMagnReturnsTrainTest);
            tabControlBase.Controls.Add(tabGrafic);
            tabControlBase.Controls.Add(tabTraiTest);
            tabControlBase.Controls.Add(tabMagnReturnsTrainTest);
            tabControlBase.Controls.Add(tabData);

            // Crear grupos de controles de fecha
            GroupBox gbTest = CreateDateGroupBox("Test", "test");
            GroupBox gbTrain = CreateDateGroupBox("Train", "train");
            GroupBox gbForward = CreateDateGroupBox("Forward", "forward");

            // Panel para los selectores de fecha
            TableLayoutPanel datePanel = new TableLayoutPanel
            {
                RowCount = 1,
                ColumnCount = 3,
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            datePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            datePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            datePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            datePanel.Controls.Add(gbTest, 0, 0);
            datePanel.Controls.Add(gbTrain, 1, 0);
            datePanel.Controls.Add(gbForward, 2, 0);

            // Botón para procesar datos después de seleccionar fechas
            Button btnProcess = new Button
            {
                Text = "Procesar datos",
                BackColor = SystemColors.ActiveCaption,
                Name = "btnProcess",
                Width = 200,
                Height = 30,
                Dock = DockStyle.Bottom
            };
            btnProcess.Click += BtnProcess_Click;

            // Etiqueta para mostrar información del archivo
            Label lblFileInfo = new Label
            {
                Name = "lblFileInfo",
                Text = "No se ha cargado ningún archivo",
                AutoSize = true,
                Dock = DockStyle.Top
            };

            // Panel principal
            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                RowCount = 5,
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Añadir controles al panel principal
            TableLayoutPanel uploadPanel = new TableLayoutPanel { ColumnCount = 2, AutoSize = true };
            uploadPanel.Controls.Add(btnUpload, 0, 0);
            uploadPanel.Controls.Add(lblFileInfo, 1, 0);
            mainPanel.Controls.Add(uploadPanel, 0, 1);
            mainPanel.Controls.Add(tabControlBase, 0, 2);
            mainPanel.Controls.Add(datePanel, 0, 3);
            mainPanel.Controls.Add(btnProcess, 0, 4);

            // Agregar el panel principal a este control
            this.Controls.Add(mainPanel);

            // Desactivar los selectores de fecha hasta que se cargue un archivo
            gbTest.Enabled = false;
            gbTrain.Enabled = false;
            gbForward.Enabled = false;
            btnProcess.Enabled = false;
        }

        private GroupBox CreateDateGroupBox(string title, string namePrefix)
        {
            GroupBox gb = new GroupBox
            {
                Text = title,
                Name = title,
                Dock = DockStyle.Fill,
                AutoSize = true
            };

            // Crear selectores de fecha
            DateTimePicker dtpStart = new DateTimePicker
            {
                Name = namePrefix + "Start",
                Format = DateTimePickerFormat.Short,
                Width = 100,
                Dock = DockStyle.Top
            };

            DateTimePicker dtpEnd = new DateTimePicker
            {
                Name = namePrefix + "End",
                Format = DateTimePickerFormat.Short,
                Width = 100,
                Dock = DockStyle.Top
            };

            // Etiquetas
            Label lblStart = new Label
            {
                Text = "Inicio " + title,
                AutoSize = true,
                Dock = DockStyle.Top
            };

            Label lblEnd = new Label
            {
                Text = "Fin " + title,
                AutoSize = true,
                Dock = DockStyle.Top
            };

            // Añadir controles al grupo
            TableLayoutPanel panel = new TableLayoutPanel
            {
                RowCount = 4,
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            panel.Controls.Add(lblStart, 0, 0);
            panel.Controls.Add(dtpStart, 0, 1);
            panel.Controls.Add(lblEnd, 0, 2);
            panel.Controls.Add(dtpEnd, 0, 3);
            gb.Controls.Add(panel);

            return gb;
        }

        #endregion

        #region Eventos

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            // Mostrar diálogo para seleccionar archivo
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos CSV (*.csv)|*.csv",
                Title = "Selecciona un archivo CSV"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                btnStatistic.Visible = false;
                btnConclusiones.Visible = false;

                Label lblFileInfo = Controls.Find("lblFileInfo", true).FirstOrDefault() as Label;
                if (lblFileInfo != null)
                    lblFileInfo.Text = "Procesando...";

                LoadAndProcessCsv(openFileDialog.FileName);

                BtnProcess_Click(null, null);
            }
        }

        private void BtnProcess_Click(object sender, EventArgs e)
        {
            ProcessDateRanges();

            PlotPriceEvolution("Date");

            PlotDistribucionesTrainTest();

            PlotMagnitudeOfReturnsTrainTest();

            btnStatistic.Visible = true;
            btnConclusiones.Visible = true;
        }

        private void btnStatistic_Click(object sender, EventArgs e)
        {
            StatisticDataForm form = new StatisticDataForm(_trainReturns, _testReturns, _forwardReturns);
            form.ShowDialog();

            DataTable StatsTable = form.StatsTable;
        }

        private void btnConclusiones_Click(object sender, EventArgs e)
        {
            new ConclusionesForm(_totalReturns, _pValue).ShowDialog();
        }

        #endregion

        #region Métodos para procesar datos

        private void LoadAndProcessCsv(string filePath)
        {
            try
            {
                string rybText = string.Empty;

                // Leer el archivo CSV
                _dataTable = ReadCsvFile(filePath, ref rybText);

                // Calcular el Target
                _dataTable = Calculate_Target.Calculate(_dataTable);

                // Mostrar las 100 primeras filas del DataTable en el DataGridView
                RichTextBox rtb = Controls.Find("rtbData", true).FirstOrDefault() as RichTextBox;
                if (rtb != null)
                {
                    DataTable dtPrimeras100 = _dataTable.AsEnumerable()
                                                        .Take(50)
                                                        .CopyToDataTable();
                    rtb.Text = MostrarDataTableEnRichTextBox(dtPrimeras100);
                }

                // Detectar columna de fecha
                _dateColumnName = DetectDateColumn(_dataTable);

                if (string.IsNullOrEmpty(_dateColumnName))
                {
                    MessageBox.Show("No se encontró una columna de fecha. Por favor, asegúrate de que tu CSV tiene una columna de fecha.");
                    return;
                }

                // Obtener el rango de fechas
                DateTime minDate = GetMinDate(_dataTable, _dateColumnName);
                DateTime maxDate = GetMaxDate(_dataTable, _dateColumnName);

                // Actualizar etiqueta con información del archivo
                Label lblFileInfo = Controls.Find("lblFileInfo", true).FirstOrDefault() as Label;
                if (lblFileInfo != null)
                {
                    lblFileInfo.Text = $"Archivo cargado: {Path.GetFileName(filePath)}\nRango de fechas: {minDate.ToString("yyyy-MM-dd")} a {maxDate.ToString("yyyy-MM-dd")}";
                }

                // Configurar las fechas predeterminadas en los selectores
                SetDefaultDates(minDate, maxDate);

                // Habilitar los selectores de fecha y el botón de procesar
                EnableDateControls(true);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string MostrarDataTableEnRichTextBox(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            // 1. Agregar encabezados de columnas
            foreach (DataColumn column in dt.Columns)
            {
                sb.Append($"{column.ColumnName,-20}"); // Alinea a la izquierda con 20 caracteres de ancho
            }
            sb.AppendLine(); // Nueva línea después de los encabezados

            // 2. Agregar separador
            sb.AppendLine(new string('-', dt.Columns.Count * 20));

            // 3. Agregar filas
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    sb.Append($"{item.ToString(),-20}"); // Alinea cada valor
                }
                sb.AppendLine(); // Nueva línea después de cada fila
            }

            // 4. Asignar el texto al RichTextBox
            return sb.ToString();
        }

        private DataTable ReadCsvFile(string filePath, ref string rybText)
        {
            DataTable dt = new DataTable();

            // Leer todos los datos
            if (File.Exists(filePath))
                rybText = File.ReadAllText(filePath);

            using (StreamReader sr = new StreamReader(filePath))
            {
                // Leer encabezados
                string headerLine = sr.ReadLine();
                string[] headers = headerLine.Split(',');
                if (headers.Length <= 1)
                    headers = headerLine.Split(';');

                foreach (string header in headers)
                {
                    dt.Columns.Add(header.Trim());
                }

                // Leer datos
                while (!sr.EndOfStream)
                {
                    string currentRow = sr.ReadLine();
                    string[] rows = currentRow.Split(',');
                    if (rows.Length <= 1)
                        rows = currentRow.Split(';');

                    if (rows.Length == headers.Length)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }

            // Convertir columnas a tipos adecuados
            ConvertColumnTypes(dt);

            return dt;
        }

        private void ConvertColumnTypes(DataTable dt)
        {
            // Intentar convertir cada columna al tipo adecuado
            for (int c = 0; c < dt.Columns.Count; c++)
            {
                DataColumn column = dt.Columns[c];

                // Identificar posibles columnas de fecha
                if (column.ColumnName.ToLower().Contains("date") || column.ColumnName.ToLower().Contains("fecha"))
                {
                    // Crear una nueva columna de fecha
                    DataColumn newColumn = new DataColumn(column.ColumnName + "_temp", typeof(DateTime));
                    dt.Columns.Add(newColumn);

                    // Convertir los valores
                    foreach (DataRow row in dt.Rows)
                    {
                        try
                        {
                            row[newColumn] = Convert.ToDateTime(row[column]);
                        }
                        catch
                        {
                            row[newColumn] = DBNull.Value;
                        }
                    }

                    // Reemplazar la columna original
                    dt.Columns.Remove(column);
                    newColumn.ColumnName = column.ColumnName;
                }
                else
                {
                    // Intentar detectar si es numérica
                    bool isNumeric = true;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[column] == DBNull.Value) continue;

                        string value = row[column].ToString();
                        if (!double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                        {
                            isNumeric = false;
                            break;
                        }
                    }

                    if (isNumeric)
                    {
                        // Crear una nueva columna numérica
                        DataColumn newColumn = new DataColumn(column.ColumnName + "_temp", typeof(double));
                        dt.Columns.Add(newColumn);

                        // Convertir los valores
                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                if (row[column] != DBNull.Value)
                                {
                                    row[newColumn] = double.Parse(row[column].ToString(), CultureInfo.InvariantCulture);
                                }
                                else
                                {
                                    row[newColumn] = DBNull.Value;
                                }
                            }
                            catch
                            {
                                row[newColumn] = DBNull.Value;
                            }
                        }

                        // Reemplazar la columna original
                        dt.Columns.Remove(column);
                        newColumn.ColumnName = column.ColumnName;
                    }
                }
            }
        }

        private string DetectDateColumn(DataTable dt)
        {
            // Buscar columnas de tipo DateTime
            foreach (DataColumn column in dt.Columns)
            {
                if (column.DataType == typeof(DateTime))
                {
                    return column.ColumnName;
                }
            }

            // Si no hay columnas DateTime, buscar por nombres comunes
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName.ToLower().Contains("date") || column.ColumnName.ToLower().Contains("fecha"))
                {
                    return column.ColumnName;
                }
            }

            return null;
        }

        private DateTime GetMinDate(DataTable dt, string dateColumnName)
        {
            DateTime minDate = DateTime.MaxValue;
            foreach (DataRow row in dt.Rows)
            {
                if (row[dateColumnName] != DBNull.Value)
                {
                    DateTime date = Convert.ToDateTime(row[dateColumnName]);
                    if (date < minDate)
                    {
                        minDate = date;
                    }
                }
            }
            return minDate;
        }

        private DateTime GetMaxDate(DataTable dt, string dateColumnName)
        {
            DateTime maxDate = DateTime.MinValue;
            foreach (DataRow row in dt.Rows)
            {
                if (row[dateColumnName] != DBNull.Value)
                {
                    DateTime date = Convert.ToDateTime(row[dateColumnName]);
                    if (date > maxDate)
                    {
                        maxDate = date;
                    }
                }
            }
            return maxDate;
        }

        private void EnableDateControls(bool enable)
        {
            // Habilitar o deshabilitar los grupos de fecha
            GroupBox gbTest = Controls.Find("Test", true).FirstOrDefault() as GroupBox;
            GroupBox gbTrain = Controls.Find("Train", true).FirstOrDefault() as GroupBox;
            GroupBox gbForward = Controls.Find("Forward", true).FirstOrDefault() as GroupBox;

            if (gbTest != null) gbTest.Enabled = enable;
            if (gbTrain != null) gbTrain.Enabled = enable;
            if (gbForward != null) gbForward.Enabled = enable;

            // Habilitar o deshabilitar el botón de procesar
            Button btnProcess = Controls.Find("btnProcess", true).FirstOrDefault() as Button;
            if (btnProcess != null) btnProcess.Enabled = enable;
        }

        private void SetDefaultDates(DateTime minDate, DateTime maxDate)
        {
            // Calcular fechas predeterminadas
            DateTime testStart = minDate;
            DateTime testEnd = new DateTime(2020, 1, 1);
            DateTime trainStart = new DateTime(2020, 1, 1);
            DateTime trainEnd = new DateTime(2023, 1, 1);
            DateTime forwardStart = new DateTime(2023, 1, 1);
            DateTime forwardEnd = maxDate;

            // Asegurarse de que todas las fechas estén dentro del rango
            if (testEnd > maxDate) testEnd = maxDate;
            if (trainStart < minDate) trainStart = minDate;
            if (trainEnd > maxDate) trainEnd = maxDate;
            if (forwardStart < minDate) forwardStart = minDate;

            // Configurar los selectores de fecha
            DateTimePicker dtpTestStart = Controls.Find("testStart", true).FirstOrDefault() as DateTimePicker;
            DateTimePicker dtpTestEnd = Controls.Find("testEnd", true).FirstOrDefault() as DateTimePicker;
            DateTimePicker dtpTrainStart = Controls.Find("trainStart", true).FirstOrDefault() as DateTimePicker;
            DateTimePicker dtpTrainEnd = Controls.Find("trainEnd", true).FirstOrDefault() as DateTimePicker;
            DateTimePicker dtpForwardStart = Controls.Find("forwardStart", true).FirstOrDefault() as DateTimePicker;
            DateTimePicker dtpForwardEnd = Controls.Find("forwardEnd", true).FirstOrDefault() as DateTimePicker;

            // Configurar rangos
            if (dtpTestStart != null)
            {
                dtpTestStart.MinDate = minDate;
                dtpTestStart.MaxDate = maxDate;
                dtpTestStart.Value = testStart;
            }

            if (dtpTestEnd != null)
            {
                dtpTestEnd.MinDate = minDate;
                dtpTestEnd.MaxDate = maxDate;
                dtpTestEnd.Value = testEnd;
            }

            if (dtpTrainStart != null)
            {
                dtpTrainStart.MinDate = minDate;
                dtpTrainStart.MaxDate = maxDate;
                dtpTrainStart.Value = trainStart;
            }

            if (dtpTrainEnd != null)
            {
                dtpTrainEnd.MinDate = minDate;
                dtpTrainEnd.MaxDate = maxDate;
                dtpTrainEnd.Value = trainEnd;
            }

            if (dtpForwardStart != null)
            {
                dtpForwardStart.MinDate = minDate;
                dtpForwardStart.MaxDate = maxDate;
                dtpForwardStart.Value = forwardStart;
            }

            if (dtpForwardEnd != null)
            {
                dtpForwardEnd.MinDate = minDate;
                dtpForwardEnd.MaxDate = maxDate;
                dtpForwardEnd.Value = forwardEnd;
            }
        }

        private void ProcessDateRanges()
        {
            if (_dataTable == null || string.IsNullOrEmpty(_dateColumnName))
            {
                MessageBox.Show("No hay datos para procesar.");
                return;
            }

            try
            {
                // Obtener las fechas seleccionadas
                DateTimePicker dtpTestStart = Controls.Find("testStart", true).FirstOrDefault() as DateTimePicker;
                DateTimePicker dtpTestEnd = Controls.Find("testEnd", true).FirstOrDefault() as DateTimePicker;
                DateTimePicker dtpTrainStart = Controls.Find("trainStart", true).FirstOrDefault() as DateTimePicker;
                DateTimePicker dtpTrainEnd = Controls.Find("trainEnd", true).FirstOrDefault() as DateTimePicker;
                DateTimePicker dtpForwardStart = Controls.Find("forwardStart", true).FirstOrDefault() as DateTimePicker;
                DateTimePicker dtpForwardEnd = Controls.Find("forwardEnd", true).FirstOrDefault() as DateTimePicker;

                DateTime testStart = dtpTestStart.Value;
                DateTime testEnd = dtpTestEnd.Value;
                DateTime trainStart = dtpTrainStart.Value;
                DateTime trainEnd = dtpTrainEnd.Value;
                DateTime forwardStart = dtpForwardStart.Value;
                DateTime forwardEnd = dtpForwardEnd.Value;

                // Crear máscaras
                _maskTrain = new List<bool>();
                _maskTest = new List<bool>();
                _maskForward = new List<bool>();

                // Inicializar listas para almacenar resultados
                _trainReturns = new List<double>();
                _testReturns = new List<double>();
                _forwardReturns = new List<double>();

                // Procesar cada fila para crear las máscaras
                foreach (DataRow row in _dataTable.Rows)
                {
                    if (row[_dateColumnName] != DBNull.Value)
                    {
                        DateTime rowDate = Convert.ToDateTime(row[_dateColumnName]);

                        // Crear máscaras
                        bool isInTrain = rowDate >= trainStart && rowDate <= trainEnd;
                        bool isInTest = rowDate >= testStart && rowDate <= testEnd;
                        bool isInForward = rowDate >= forwardStart && rowDate <= forwardEnd;

                        _maskTrain.Add(isInTrain);
                        _maskTest.Add(isInTest);
                        _maskForward.Add(isInForward);

                        // Recopilar retornos
                        if (isInTrain && row["Target"] != DBNull.Value)
                        {
                            _trainReturns.Add(Convert.ToDouble(row["Target"]));
                        }

                        if (isInTest && row["Target"] != DBNull.Value)
                        {
                            _testReturns.Add(Convert.ToDouble(row["Target"]));
                        }

                        if (isInForward && row["Target"] != DBNull.Value)
                        {
                            _forwardReturns.Add(Convert.ToDouble(row["Target"]));
                        }
                    }
                }

                // Mostrar resultados
                MessageBox.Show(
                    $"Procesamiento completado:\n" +
                    $"- Datos de Train: {_trainReturns.Count} registros\n" +
                    $"- Datos de Test: {_testReturns.Count} registros\n" +
                    $"- Datos de Forward: {_forwardReturns.Count} registros",
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Aquí se podrían realizar más acciones con los datos procesados
                // como guardar en variables de sesión, exportar, etc.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar los rangos de fecha: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Gráficación de datos

        public void PlotPriceEvolution(string dateColumnName)
        {
            PlotView plotView = Controls.Find("plotView", true).FirstOrDefault() as PlotView;
            if (plotView == null || _testReturns.Count == 0 || _trainReturns.Count == 0 || _forwardReturns.Count == 0)
                return;

            DateTimePicker dtpTestStart = Controls.Find("testStart", true).FirstOrDefault() as DateTimePicker;
            DateTimePicker dtpTestEnd = Controls.Find("testEnd", true).FirstOrDefault() as DateTimePicker;
            DateTimePicker dtpTrainStart = Controls.Find("trainStart", true).FirstOrDefault() as DateTimePicker;
            DateTimePicker dtpTrainEnd = Controls.Find("trainEnd", true).FirstOrDefault() as DateTimePicker;
            DateTimePicker dtpForwardStart = Controls.Find("forwardStart", true).FirstOrDefault() as DateTimePicker;
            DateTimePicker dtpForwardEnd = Controls.Find("forwardEnd", true).FirstOrDefault() as DateTimePicker;

            DateTime testStart = dtpTestStart.Value;
            DateTime testEnd = dtpTestEnd.Value;
            DateTime trainStart = dtpTrainStart.Value;
            DateTime trainEnd = dtpTrainEnd.Value;
            DateTime forwardStart = dtpForwardStart.Value;
            DateTime forwardEnd = dtpForwardEnd.Value;

            plotView.Model = Plot_Price_Evolution.GetPlot(
                            _dataTable,
                            dateColumnName,
                            trainStart,
                            trainEnd,
                            testStart,
                            testEnd,
                            forwardStart,
                            forwardEnd);
        }

        private void PlotDistribucionesTrainTest()
        {
            PlotView plotView = Controls.Find("plotTrainTest", true).FirstOrDefault() as PlotView;
            if (plotView == null || _testReturns.Count == 0 || _trainReturns.Count == 0 || _forwardReturns.Count == 0)
                return;

            (PlotModel plotModel, double ksStatistic, _pValue) = Plot_KS_Test.GetPlot(_trainReturns, _testReturns);

            plotView.Model = plotModel;

            if (_pValue < 0.05)
            {
                // Mostrar resultados
                MessageBox.Show(
                    "Resultado:\n" +
                    "⚠️ Las distribuciones de train y test \n" +
                    "son significativamente diferentes.\r\n\n" +
                    "Los resultados de la validación podrían\n" +
                    "NO ser representativos.",
                    "Importante",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }

        private void PlotMagnitudeOfReturnsTrainTest()
        {
            PlotView plotView = Controls.Find("plotMagnReturnsTrainTest", true).FirstOrDefault() as PlotView;
            if (plotView == null || _testReturns.Count == 0 || _trainReturns.Count == 0 || _forwardReturns.Count == 0)
                return;

            List<double> trainTestReturns = _trainReturns.Concat(_testReturns).ToList();

            (plotView.Model, _totalReturns) = Plot_Returns_Waterfall.PlotReturns(trainTestReturns);

            new ConclusionesForm(_totalReturns, _pValue).ShowDialog();
        }

        #endregion

        #region Métodos Públicos para acceder a los datos procesados

        /// <summary>
        /// Devuelve los datos de retorno para el periodo de entrenamiento
        /// </summary>
        public List<double> GetTrainReturns()
        {
            return _trainReturns ?? new List<double>();
        }

        /// <summary>
        /// Devuelve los datos de retorno para el periodo de prueba
        /// </summary>
        public List<double> GetTestReturns()
        {
            return _testReturns ?? new List<double>();
        }

        /// <summary>
        /// Devuelve los datos de retorno para el periodo de forward
        /// </summary>
        public List<double> GetForwardReturns()
        {
            return _forwardReturns ?? new List<double>();
        }

        /// <summary>
        /// Devuelve el DataTable procesado
        /// </summary>
        public DataTable GetProcessedData()
        {
            return _dataTable;
        }

        #endregion

    }
}
