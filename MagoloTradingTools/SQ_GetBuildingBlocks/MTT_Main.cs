using System.Diagnostics;
using MTT01_winforms.UControls.EAs;
using MTT01_winforms.UControls.Data;
using MTT01_winforms.UControls.Visualization;
using MTT01_winforms.UControls.StrategyQuant;
using MTT01_winforms.UControls.Calculo;
using MTT01_winforms.UControls.WebScrapping;
using MTT_IA;
using MTT_Algorithms;
using MTT_Calculo;

namespace MTT01_winforms
{
    public partial class MTT_Main : Form
    {
        private int CurrentIndex = -1;

        public MTT_Main()
        {
            InitializeComponent();

            cmbModels.Items.Add("llama3.2");
            cmbModels.Items.Add("qwen2.5:32b");
            cmbModels.SelectedIndex = 0;
        }

        #region Private Methods

        // Cargar de los controles en el formulario raíz
        private void LoadControlIntoMain(UserControl control)
        {
            //  ClearForm
            if (CurrentIndex > -1)
                Controls.RemoveAt(CurrentIndex);

            Width = control.Width + 20;
            Height = control.Height + 20;

            Controls.Add(control);

            CurrentIndex = Controls.GetChildIndex(control);
        }

        #endregion

        #region Main Menu

        #region Data

        private void loadCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadControlIntoMain(new UCLoadCSV());

        }

        #endregion

        #region Strategy Quant

        private void buildingBlocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadControlIntoMain(new UCSqxBblockAnalizer());

        }

        #endregion

        #region Visualization

        private void viewFIleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadControlIntoMain(new UCVisualizeFile(openFileDialog1.FileName));

            }
        }

        private void viewFileGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadControlIntoMain(new UCVisualizeAllFiles(folderBrowserDialog1.SelectedPath));
            }
        }

        private void compararImágenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadControlIntoMain(new UCCompareImages());
        }

        #endregion

        #region EAs

        private void extractorDeReglasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string rutaBaseProyectoB = AppDomain.CurrentDomain.BaseDirectory;
            string rutaRelativa = @"Java_Jar\FilePathFrame_weka.jar";
            string jarPath = Path.GetFullPath(Path.Combine(rutaBaseProyectoB, rutaRelativa));

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "java";
                startInfo.Arguments = $"-jar \"{jarPath}\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;

                using (Process process = Process.Start(startInfo))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string output = reader.ReadToEnd();
                        Console.WriteLine(output);
                    }

                    using (StreamReader reader = process.StandardError)
                    {
                        string error = reader.ReadToEnd();
                        if (!string.IsNullOrEmpty(error))
                        {
                            Console.WriteLine("Error: " + error);
                        }
                    }

                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al ejecutar el archivo JAR: " + ex.Message);
            }

        }

        #region EAs - MT4

        private void unEAConReglasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadControlIntoMain(new UCGeneraEA_mt4_TotalRules(UCGeneraEA_mt4_TotalRules.EAReglas_Type.EA_Solo));

        }

        private void eAProbadorDeReglasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadControlIntoMain(new UCGeneraEA_mt4_TotalRules(UCGeneraEA_mt4_TotalRules.EAReglas_Type.EA_Probador));

        }

        private void eAReglasEspecificasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadControlIntoMain(new UCGeneraEA_mt4_RRR());
        }

        #endregion

        #endregion

        #region Cálculo

        private void distribuciónNormalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void kolmogorovSmirnovToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tradingDeParesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadControlIntoMain(new UCAnalisisDeDatos());// new UCTradingDePares());
        }

        #endregion

        #region Web Scrapping

        private void alphavantageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadControlIntoMain(new UCTimeSeriesStockDataAPIs());
        }

        #endregion

        #endregion

        #region IA

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string question = txtOllamaQuestion.Text.Trim();
                string model = cmbModels.SelectedItem.ToString();

                txtOllamaAnswer.Text = "******************************** PENSANDO !!!";

                string summary = await OllamaApiResponse.GetApiResponseAsync(question, model);

                txtOllamaAnswer.Text = summary;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error principal: {ex.Message}");
            }
        }

        private async void btnOpenAIConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string systemPrompt = txtOpenAIPrompt.Text;
                string question = txtOpenAIQuestion.Text;

                txtOpenAIAnswer.Text = "******************************** PENSANDO !!!";

                string response = await new OpenAIApiResponse().GetOpenAIResponseAsync(systemPrompt, question);

                txtOpenAIAnswer.Text = response;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error principal: {ex.Message}");
            }
        }

        #endregion


        private void entrenamientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EntrenoNeurona simulation = new EntrenoNeurona(0.5, 0.7, 0.2); // Valores iniciales para F2, G2 y N2
            var (fVal, gVal, iterations) = simulation.Simulate();

            Console.WriteLine($"F: {fVal}, G: {gVal}");
            Console.WriteLine($"Número de iteraciones necesarias: {iterations}");
        }

        private void cargaDeFicheroToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void posiciónCarverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Generar datos de ejemplo: retornos diarios (%)
            List<double> dailyReturns = new List<double>
            {
                0.01, -0.02, 0.03, -0.01, 0.02, 0.01, -0.01, 0.02, -0.03, 0.01,
                0.02, -0.01, 0.01, -0.02, 0.03, -0.01, 0.02, 0.01, -0.01, 0.02,
                -0.03, 0.01, 0.02, -0.01, 0.01, -0.02, 0.03, -0.01, 0.02, 0.01,
                -0.01, 0.02, -0.03, 0.01, 0.02, -0.01, 0.01, -0.02, 0.03, -0.01
            };

            // Parámetros
            double targetVolatility = 0.10; // 10% anual
            int lookbackPeriod = 30; // Usar los últimos 30 días

            // Calcular el tamaño de posición ajustado por volatilidad
            double positionSize = RobertCarverPositionSize.Calculate(dailyReturns, targetVolatility, lookbackPeriod, 2);

        }
    }
}
