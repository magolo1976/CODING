using MagoloRuleExtraction.Classes;
using System.Data;

namespace MagoloRuleExtraction.Sections.Data
{
    public partial class StatisticDataForm : Form
    {
        public DataTable StatsTable;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbolName"></param>
        /// <param name="trainReturns"></param>
        /// <param name="testReturns"></param>
        /// <param name="forwardReturns"></param>
        public StatisticDataForm(List<double> trainReturns, List<double> testReturns, List<double> forwardReturns)
        {
            InitializeComponent();

            UpdateReturnsStatsTable(trainReturns, testReturns, forwardReturns);
        }

        /// <summary>
        /// Actualiza la tabla con las estadísticas de retornos
        /// </summary>
        /// <param name="trainReturns">Colección de retornos de entrenamiento</param>
        /// <param name="testReturns">Colección de retornos de prueba</param>
        /// <param name="forwardReturns">Colección de retornos de previsión</param>
        public void UpdateReturnsStatsTable(List<double> trainReturns, List<double> testReturns, List<double> forwardReturns)
        {
            // Crear la tabla
            StatsTable = Create_Returns_Stats_Table.DoWork(trainReturns, testReturns, forwardReturns);

            // Asignar al DataGridView
            dgvReturnsStats.DataSource = StatsTable;

            // Dar formato a las columnas
            FormatDataGridView();
        }

        /// <summary>
        /// Da formato al DataGridView
        /// </summary>
        private void FormatDataGridView()
        {
            // Colores para las columnas
            Color testColor = Color.FromArgb(255, 215, 0);  // Amarillo
            Color trainColor = Color.FromArgb(173, 216, 230);  // Azul claro
            Color forwardColor = Color.FromArgb(255, 192, 203);  // Rojo claro

            // Establecer colores de fondo para los encabezados de columna
            dgvReturnsStats.Columns["Test"].HeaderCell.Style.BackColor = testColor;
            dgvReturnsStats.Columns["Train"].HeaderCell.Style.BackColor = trainColor;
            dgvReturnsStats.Columns["Forward"].HeaderCell.Style.BackColor = forwardColor;

            // Establecer la columna de estadística como primera columna
            dgvReturnsStats.Columns["Estadística"].DisplayIndex = 0;

            // Ajustar estilo de celdas
            dgvReturnsStats.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvReturnsStats.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Establecer colores alternos para las filas
            dgvReturnsStats.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            // Otros ajustes estéticos
            dgvReturnsStats.BorderStyle = BorderStyle.None;
            dgvReturnsStats.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvReturnsStats.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        }

        /// <summary>
        /// Exporta las estadísticas a un archivo CSV
        /// </summary>
        private void btnSaveToCSV_Click(object sender, EventArgs e)
        {
            // Mostrar diálogo para salvar archivo
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Archivos CSV (*.csv)|*.csv",
                Title = "Selecciona un archivo CSV"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToCsv(saveFileDialog.FileName);
            }
        }

        public void ExportToCsv(string filePath)
        {
            if (dgvReturnsStats.DataSource == null)
                return;

            DataTable dt = (DataTable)dgvReturnsStats.DataSource;
            using (var writer = new StreamWriter(filePath))
            {
                // Escribir encabezados
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    writer.Write(dt.Columns[i].ColumnName);
                    if (i < dt.Columns.Count - 1)
                        writer.Write(",");
                }
                writer.WriteLine();

                // Escribir filas
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        writer.Write(row[i].ToString());
                        if (i < dt.Columns.Count - 1)
                            writer.Write(",");
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}
