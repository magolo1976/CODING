using MTT01_winforms.Properties;

namespace MTT01_winforms.UControls.EAs
{
    public partial class UCGeneraEA_mt4_TotalRules : UserControl
    {
        private string PathFileStrategies = string.Empty;

        string[] pares = { "EURUSD", "USDJPY", "GBPUSD", "USDCHF", "AUDUSD", "USDCAD", "NZDUSD",
                             "EURGBP", "EURJPY", "EURCHF", "EURAUD", "EURCAD", "EURNZD", "GBPJPY",
                             "GBPCHF", "GBPAUD", "GBPCAD", "GBPNZD", "AUDJPY", "AUDCHF", "AUDCAD",
                             "AUDNZD", "CADJPY", "CADCHF", "NZDJPY", "NZDCHF", "NZDCAD",
                           "USDCNY", "USDHKD", "USDSGD", "USDZAR", "USDTRY", "USDINR", "USDBRL",
                           "USDMXN", "USDRUB", "USDPLN", "EURTRY", "EURZAR", "EURHUF", "EURPLN",
                           "EURCZK", "GBPZAR", "GBPTRY", "AUDSGD", "CADSGD", "NZDSGD", "SGDJPY" };

        public enum EAReglas_Type
        {
            EA_Solo,
            EA_Probador
        }

        public EAReglas_Type EATipoReglas = EAReglas_Type.EA_Solo;

        public UCGeneraEA_mt4_TotalRules(EAReglas_Type eATipoReglas)
        {
            InitializeComponent();

            cmbEAType.SelectedIndex = 0;

            EATipoReglas = eATipoReglas;

            label1.Text = label1.Text + " >> " + eATipoReglas.ToString();

        }

        [STAThread]
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBoxFile.Text) && !string.IsNullOrEmpty(cmbEAType.SelectedItem.ToString()))
            {
                string fileOrigen;
                string file;

                string extraFlag = "compra = true;";

                if (EATipoReglas == EAReglas_Type.EA_Probador)
                {
                    switch (cmbEAType.SelectedIndex)
                    {
                        case 0: 
                            fileOrigen = Resources.PlantillaEA_ProbadorReglas_compras;
                            extraFlag = "compra = true;"; 
                            break;
                        case 1: 
                            fileOrigen = Resources.PlantillaEA_ProbadorReglas_ventas;
                            extraFlag = "ventas = true;"; 
                            break;
                        default: fileOrigen = Resources.PlantillaEA_ProbadorReglas_compras; break;
                    }
                }
                else
                {
                    switch (cmbEAType.SelectedIndex)
                    {
                        case 0: 
                            fileOrigen = Resources.PlantillaEA_Reglas_compras;
                            extraFlag = "compra = true;"; 
                            break;
                        case 1: 
                            fileOrigen = Resources.PlantillaEA_Reglas_ventas;
                            extraFlag = "ventas = true;"; 
                            break;
                        default: fileOrigen = Resources.PlantillaEA_Reglas_compras; break;
                    }
                }

                file = fileOrigen;

                string total_rows = "";
                int totalRules = 0;
                int file_version = 1;
                int TOTAL_STRATEGIES = 1024;

                List<string> repeated_rows = new List<string>();

                foreach (string row in richTextBoxFile.Lines)
                {
                    if (string.IsNullOrEmpty(row)) { break; }

                    try
                    {
                        string[] rules_splits = row.Split("=>");

                        if (rules_splits.Length > 1 &&
                            !string.IsNullOrEmpty(rules_splits[0].Trim()) &&
                            !repeated_rows.Contains(rules_splits[0].Trim()))
                        {
                            if (total_rows.Length > 0 && !total_rows.EndsWith("else if(\n"))
                                total_rows += " || \n";

                            repeated_rows.Add(rules_splits[0].Trim());

                            totalRules++;

                            string currentRule = (EATipoReglas == EAReglas_Type.EA_Probador) ? $"TotalRules == {totalRules} && " : string.Empty;

                            total_rows += GetTransformedRow(rules_splits[0].Trim(), currentRule);

                            if (totalRules%TOTAL_STRATEGIES == 0)
                            {
                                total_rows += "\n){" + extraFlag+ "}else if(\n";
                                //totalRules = 0;

                                /*SaveStrategiesToFile(totalRules, total_rows, file, file_version); 

                                totalRules = 0;
                                file_version++; 
                                total_rows = "";
                                file = fileOrigen;*/
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show($"Regla con error: '{row}'");
                        totalRules--;
                    }
                }

                if (totalRules >= 1)
                {
                    SaveStrategiesToFile(totalRules, total_rows, file, ((extraFlag.Contains("compra")?"_UP":"_DN")));
                }

                MessageBox.Show($"EA creado en el escritorio!!");

                lblFilesLoaded.Visible = false;
                richTextBoxFile.Text = string.Empty;
            }
        }

        /// <summary>
        /// GENERA FICHERO CON LAS ESTRATEGIAS
        /// </summary>
        /// <param name="totalRules"></param>
        /// <param name="total_rows"></param>
        /// <param name="file"></param>
        /// <param name="file_version"></param>
        private void SaveStrategiesToFile(int totalRules, string total_rows, string file, string file_version)
        {
            file = file.Replace("{NOMBRE}", txtEAName.Text);
            file = file.Replace("{NUM_RULES}", totalRules.ToString());
            file = file.Replace("{REGLAS}", total_rows);

            string rutaEscritorio = Path.Combine(PathFileStrategies, "BLOCKS"); // Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if(!Directory.Exists(rutaEscritorio))
                Directory.CreateDirectory(rutaEscritorio);

            string nombreArchivo = string.Concat(txtEAName.Text, $"{file_version}.mq4");
            string rutaArchivo = Path.Combine(rutaEscritorio, nombreArchivo);

            File.WriteAllText(rutaArchivo, file);
        }

        private void btnCargarReglas_Click(object sender, EventArgs e)
        {
            lblFilesLoaded.Visible = false;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (Directory.Exists(folderBrowserDialog1.SelectedPath))
                {
                    PathFileStrategies = folderBrowserDialog1.SelectedPath;

                    string[] files = Directory.GetFiles(PathFileStrategies, "*.txt");

                    string rules = string.Empty;
                    int count = 0;
                    foreach (string file in files)
                    {
                        foreach (string line in File.ReadAllLines(file))
                        {
                            if (!string.IsNullOrEmpty(line) &&
                                !line.StartsWith("==") &&
                                !line.StartsWith("JRIP") &&
                                !line.StartsWith("Number of Rules"))
                            {
                                rules += line + "\n";
                                count++;
                            }
                        }

                        // Máximo de reglas por EA
                        if (count == 2049)
                            break;
                    }

                    if (string.IsNullOrEmpty(rules))
                        rules = "NA";

                    richTextBoxFile.Text = rules;

                    lblFilesLoaded.Text = files.Length.ToString() + " ficheros";
                    lblFilesLoaded.Text = string.Concat(lblFilesLoaded.Text, " >> ", richTextBoxFile.Lines.Length, " strategies");

                    lblFilesLoaded.Visible = true;

                }
            }
        }

        private string GetTransformedRow(string row, string totalRule)
        {
            string new_row = string.Concat("(", totalRule, row.Replace("and", "&&"), ")");

            #region MERCADO ESFERICO

            string extra_row = new_row;
            foreach (string par in pares)
            {
                if (extra_row.Contains(par))
                    extra_row = extra_row.Replace(par, $"iRSI(\"{par}\", 0, 14, PRICE_MEDIAN, 1)");
            }

            new_row = extra_row;

            #endregion

            #region INDICATORS

            new_row = new_row.Replace("Hora", "(TimeHour(Time[1])*100)");

            new_row = new_row.Replace("Mom(7)", "(Close[1]/iMomentum(NULL, 0, 7, PRICE_CLOSE, 1))");
            new_row = new_row.Replace("Mom(14)", "(Close[1]/iMomentum(NULL, 0, 14, PRICE_CLOSE, 1))");
            new_row = new_row.Replace("Mom(28)", "(Close[1]/iMomentum(NULL, 0, 28, PRICE_CLOSE, 1))");

            new_row = new_row.Replace("ADX(7)", "iADX(NULL, 0, 7, PRICE_CLOSE, MODE_MAIN, 1)");
            new_row = new_row.Replace("ADX(14)", "iADX(NULL, 0, 14, PRICE_CLOSE, MODE_MAIN, 1)");
            new_row = new_row.Replace("ADX(21)", "iADX(NULL, 0, 21, PRICE_CLOSE, MODE_MAIN, 1)");

            new_row = new_row.Replace("CCI(7)", "iCCI(NULL, 0, 7, PRICE_CLOSE, 1)");
            new_row = new_row.Replace("CCI(14)", "iCCI(NULL, 0, 14, PRICE_CLOSE, 1)");
            new_row = new_row.Replace("CCI(21)", "iCCI(NULL, 0, 21, PRICE_CLOSE, 1)");

            new_row = new_row.Replace("BBANDS(7)", "(iBands(NULL, 0, 7, 2, 0, PRICE_CLOSE, MODE_HIGH, 1))-(iBands(NULL, 0, 7, 2, 0, PRICE_CLOSE, MODE_LOWER, 1))");
            new_row = new_row.Replace("BBANDS(14)", "(iBands(NULL, 0, 14, 2, 0, PRICE_CLOSE, MODE_HIGH, 1))-(iBands(NULL, 0, 14, 2, 0, PRICE_CLOSE, MODE_LOWER, 1))");
            new_row = new_row.Replace("BBANDS(20)", "(iBands(NULL, 0, 20, 2, 0, PRICE_CLOSE, MODE_HIGH, 1))-(iBands(NULL, 0, 20, 2, 0, PRICE_CLOSE, MODE_LOWER, 1))");

            new_row = new_row.Replace("STOC(5)", "iStochastic(NULL, 0, 5, 3, 3, MODE_SMA, 0, MODE_MAIN, 1)");
            new_row = new_row.Replace("STOC(20)", "iStochastic(NULL, 0, 20, 3, 3, MODE_SMA, 0, MODE_MAIN, 1)");

            new_row = new_row.Replace("MACD(12)", "iMACD(NULL, 0, 12, 26, 9, PRICE_CLOSE, MODE_MAIN, 1)");
            new_row = new_row.Replace("MACD(50)", "iMACD(NULL, 0, 50, 80, 6, PRICE_CLOSE, MODE_MAIN, 1)");


            //Close[1]/Close[2] 
            //Close[1]/Close[3] 
            //Close[1]/Close[4]

            //Close[1]/Close[15] 
            //Close[1]/Close[22] 
            //Close[1]/Close[29]

            //Low[1]/Low[2] 
            //Low[1]/Low[3] 
            //Low[1]/Low[4]

            //High[1]/High[2] 
            //High[1]/High[3] 
            //High[1]/High[4]

            new_row = new_row.Replace("Vol[1]/Vol[2]", "Volume[1]/Volume[2]");

            new_row = new_row.Replace("EaseOfMovement", "iCustom(NULL, 0, \"MT/MTCustom/EaseOfMovement\", 20, 0, 1)");

            new_row = new_row.Replace("Volatility(30)", "iCustom(NULL, 0, \"MT/MTCustom/Volatility\", 30, 0, 1)");

            new_row = new_row.Replace("Volatility(30)", "iCustom(NULL, 0, \"MT/MTCustom/Volatility\", 30, 0, 1)");

            new_row = new_row.Replace("VolStdDev(30)", "iCustom(NULL, 0, \"MT/MTCustom/VolStdDev\", 30, 0, 1)");

            new_row = new_row.Replace("PriceVolTrend(2)", "iCustom(NULL, 0, \"MT/MTCustom/PriceVolTrend\", 2, 1, 1)");
            new_row = new_row.Replace("PriceVolTrend(21)", "iCustom(NULL, 0, \"MT/MTCustom/PriceVolTrend\", 21, 1, 1)");

            new_row = new_row.Replace("Aroon(9)", "(iCustom(NULL, 0, \"MT/MTCustom/Aroon\", 9, 0, 1) / iCustom(NULL, 0, \"MT/MTCustom/Aroon\", 9, 1, 1))");
            new_row = new_row.Replace("Aroon(14)", "(iCustom(NULL, 0, \"MT/MTCustom/Aroon\", 14, 0, 1) / iCustom(NULL, 0, \"MT/MTCustom/Aroon\", 14, 1, 1))");
            new_row = new_row.Replace("Aroon(21)", "(iCustom(NULL, 0, \"MT/MTCustom/Aroon\", 21, 0, 1) / iCustom(NULL, 0, \"MT/MTCustom/Aroon\", 21, 1, 1))");
            new_row = new_row.Replace("Aroon(25)", "(iCustom(NULL, 0, \"MT/MTCustom/Aroon\", 25, 0, 1) / iCustom(NULL, 0, \"MT/MTCustom/Aroon\", 25, 1, 1))");

            new_row = new_row.Replace("ChaikinOsc(3-10)", "iCustom(NULL, 0, \"MT/MTCustom/ChaikinOsc\", 10, 3, 0, 1)");

            new_row = new_row.Replace("ChaikinMF(9)", "iCustom(NULL, 0, \"MT/MTCustom/ChaikinMF\", 9, 0, 1)");
            new_row = new_row.Replace("ChaikinMF(14)", "iCustom(NULL, 0, \"MT/MTCustom/ChaikinMF\",14, 0, 1)");

            new_row = new_row.Replace("TRIX(9)", "(iCustom(NULL, 0, \"MT/MTCustom/Trix\", 9, 0, 1) / iCustom(NULL, 0, \"MT/MTCustom/Trix\", 9, 1, 1))");
            new_row = new_row.Replace("TRIX(21)", "(iCustom(NULL, 0, \"MT/MTCustom/Trix\", 21, 0, 1) / iCustom(NULL, 0, \"MT/MTCustom/Trix\", 21, 1, 1))");

            new_row = new_row.Replace("C/L(14)", "(Close[1]/Close[iLowest(NULL, 0, MODE_CLOSE, 14, 1)])");
            new_row = new_row.Replace("C/H(14)", "(Close[1]/Close[iHighest(NULL, 0, MODE_CLOSE, 14, 1)])");

            new_row = new_row.Replace("DI+(7)", "iADX(NULL, 0, 7, PRICE_CLOSE, MODE_PLUSDI, 1)");
            new_row = new_row.Replace("DI-(7)", "iADX(NULL, 0, 7, PRICE_CLOSE, MODE_MINUSDI, 1)");
            new_row = new_row.Replace("DI+(14)", "iADX(NULL, 0, 14, PRICE_CLOSE, MODE_PLUSDI, 1)");
            new_row = new_row.Replace("DI-(14)", "iADX(NULL, 0, 14, PRICE_CLOSE, MODE_MINUSDI, 1)");
            new_row = new_row.Replace("DI+(21)", "iADX(NULL, 0, 21, PRICE_CLOSE, MODE_PLUSDI, 1)");
            new_row = new_row.Replace("DI-(21)", "iADX(NULL, 0, 21, PRICE_CLOSE, MODE_MINUSDI, 1)");

            new_row = new_row.Replace("Percentile(7)", "((High[1]+Low[1])/2) / CalculatePercentile(7, 1)");
            new_row = new_row.Replace("Percentile(14)", "((High[1]+Low[1])/2) / CalculatePercentile(14, 1)");
            new_row = new_row.Replace("Percentile(21)", "((High[1]+Low[1])/2) / CalculatePercentile(21, 1)");

            new_row = new_row.Replace("Kurtosis(7)", "CalculateKurtosis(7, 1)");
            new_row = new_row.Replace("Kurtosis(14)", "CalculateKurtosis(14, 1)");
            new_row = new_row.Replace("Kurtosis(21)", "CalculateKurtosis(21, 1)");

            new_row = new_row.Replace("LReg(7)", "CalculateLinearRegression(7, 1)");
            new_row = new_row.Replace("LReg(14)", "CalculateLinearRegression(14, 1)");
            new_row = new_row.Replace("LReg(21)", "CalculateLinearRegression(21, 1)");

            new_row = new_row.Replace("HeikenLine(1.7)", "(iCustom(NULL, 0, \"MT/MTCustom/Heiken_Ashi_Lines\", 1.7, 1, 0) - iCustom(NULL, 0, \"MT/MTCustom/Heiken_Ashi_Lines\", 1.7, 0, 0))");

            new_row = new_row.Replace("VWAP(day)", "iCustom(NULL, 0, \"MT/MTCustom/VWAP\", true, false, false, 0, 1)");
            new_row = new_row.Replace("VWAP(week)", "iCustom(NULL, 0, \"MT/MTCustom/VWAP\", false, true, false, 1, 1)");
            new_row = new_row.Replace("VWAP(month)", "iCustom(NULL, 0, \"MT/MTCustom/VWAP\", false, false, true, 2, 1)");
            new_row = new_row.Replace("WP()", "iCustom(NULL, 0, \"MT/MTCustom/VWAP\", false, false, false, 3, 1)");

            #endregion

            #region MA

            new_row = new_row.Replace("EMA(7)", "(Close[1]/iMA(NULL, 0, 7, 0, MODE_EMA, PRICE_CLOSE, 1))");
            new_row = new_row.Replace("EMA(14)", "(Close[1]/iMA(NULL, 0, 14, 0, MODE_EMA, PRICE_CLOSE, 1))");
            new_row = new_row.Replace("EMA(21)", "(Close[1]/iMA(NULL, 0, 21, 0, MODE_EMA, PRICE_CLOSE, 1))");

            new_row = new_row.Replace("LMA(7)", "(Close[1]/iMA(NULL, 0, 7, 0, MODE_LWMA, PRICE_CLOSE, 1))");
            new_row = new_row.Replace("LMA(14)", "(Close[1]/iMA(NULL, 0, 14, 0, MODE_LWMA, PRICE_CLOSE, 1))");
            new_row = new_row.Replace("LMA(21)", "(Close[1]/iMA(NULL, 0, 21, 0, MODE_LWMA, PRICE_CLOSE, 1))");

            new_row = new_row.Replace("SMA(7)", "(Close[1]/iMA(NULL, 0, 7, 0, MODE_SMMA, PRICE_CLOSE, 1))");
            new_row = new_row.Replace("SMA(14)", "(Close[1]/iMA(NULL, 0, 14, 0, MODE_SMMA, PRICE_CLOSE, 1))");
            new_row = new_row.Replace("SMA(21)", "(Close[1]/iMA(NULL, 0, 21, 0, MODE_SMMA, PRICE_CLOSE, 1))");

            new_row = new_row.Replace("MA(7)", "(Close[1]/iMA(NULL, 0, 7, 0, MODE_SMA, PRICE_CLOSE, 1))");
            new_row = new_row.Replace("MA(14)", "(Close[1]/iMA(NULL, 0, 14, 0, MODE_SMA, PRICE_CLOSE, 1))");
            new_row = new_row.Replace("MA(21)", "(Close[1]/iMA(NULL, 0, 21, 0, MODE_SMA, PRICE_CLOSE, 1))");

            #endregion

            #region ITCHIMOKU

            int ichi_a = 9;
            int ichi_b = 26;
            int ichi_c = 52;
            new_row = new_row.Replace($"Close_TENKANSEN[{ichi_a}]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_TENKANSEN,1))");
            new_row = new_row.Replace($"Close_KIJUNSEN[{ichi_a}]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_KIJUNSEN,1))");
            new_row = new_row.Replace($"Close_SENKOUSPANA[{ichi_a}]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_SENKOUSPANA,1))");
            new_row = new_row.Replace($"Close_SENKOUSPANB[{ichi_a}]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_SENKOUSPANB,1))");
            new_row = new_row.Replace($"Close_CHIKOUSPAN[{ichi_a}]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_CHIKOUSPAN,1))");
            new_row = new_row.Replace($"TENKANSEN[{ichi_a}]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_TENKANSEN,1)");
            new_row = new_row.Replace($"KIJUNSEN[{ichi_a}]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_KIJUNSEN,1)");
            new_row = new_row.Replace($"SENKOUSPANA[{ichi_a}]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_SENKOUSPANA,1)");
            new_row = new_row.Replace($"SENKOUSPANB[{ichi_a}]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_SENKOUSPANB,1)");
            new_row = new_row.Replace($"CHIKOUSPAN[{ichi_a}]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_CHIKOUSPAN,1)");
            ichi_a = 8;
            ichi_b = 34;
            ichi_c = 144;
            new_row = new_row.Replace($"Close_TENKANSEN[{ichi_a}]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_TENKANSEN,1))");
            new_row = new_row.Replace($"Close_KIJUNSEN[{ichi_a}]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_KIJUNSEN,1))");
            new_row = new_row.Replace($"Close_SENKOUSPANA[{ichi_a}]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_SENKOUSPANA,1))");
            new_row = new_row.Replace($"Close_SENKOUSPANB[{ichi_a}]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_SENKOUSPANB,1))");
            new_row = new_row.Replace($"Close_CHIKOUSPAN[{ichi_a}]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_CHIKOUSPAN,1))");
            new_row = new_row.Replace($"TENKANSEN[{ichi_a}]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_TENKANSEN,1)");
            new_row = new_row.Replace($"KIJUNSEN[{ichi_a}]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_KIJUNSEN,1)");
            new_row = new_row.Replace($"SENKOUSPANA[{ichi_a}]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_SENKOUSPANA,1)");
            new_row = new_row.Replace($"SENKOUSPANB[{ichi_a}]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_SENKOUSPANB,1)");
            new_row = new_row.Replace($"CHIKOUSPAN[{ichi_a}]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_CHIKOUSPAN,1)");
            ichi_a = 7;
            ichi_b = 22;
            ichi_c = 44;
            new_row = new_row.Replace($"Close_TENKANSEN[{ichi_a}]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_TENKANSEN,1))");
            new_row = new_row.Replace($"Close_KIJUNSEN[{ichi_a}]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_KIJUNSEN,1))");
            new_row = new_row.Replace($"Close_SENKOUSPANA[{ichi_a}]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_SENKOUSPANA,1))");
            new_row = new_row.Replace($"Close_SENKOUSPANB[{ichi_a}]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_SENKOUSPANB,1))");
            new_row = new_row.Replace($"Close_CHIKOUSPAN[{ichi_a}]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_CHIKOUSPAN,1))");
            new_row = new_row.Replace($"TENKANSEN[{ichi_a}]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_TENKANSEN,1)");
            new_row = new_row.Replace($"KIJUNSEN[{ichi_a}]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_KIJUNSEN,1)");
            new_row = new_row.Replace($"SENKOUSPANA[{ichi_a}]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_SENKOUSPANA,1)");
            new_row = new_row.Replace($"SENKOUSPANB[{ichi_a}]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_SENKOUSPANB,1)");
            new_row = new_row.Replace($"CHIKOUSPAN[{ichi_a}]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_CHIKOUSPAN,1)");
            ichi_a = 7;
            ichi_b = 28;
            ichi_c = 119;
            new_row = new_row.Replace($"Close_TENKANSEN[{ichi_a}.]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_TENKANSEN,1))");
            new_row = new_row.Replace($"Close_KIJUNSEN[{ichi_a}.]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_KIJUNSEN,1))");
            new_row = new_row.Replace($"Close_SENKOUSPANA[{ichi_a}.]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_SENKOUSPANA,1))");
            new_row = new_row.Replace($"Close_SENKOUSPANB[{ichi_a}.]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_SENKOUSPANB,1))");
            new_row = new_row.Replace($"Close_CHIKOUSPAN[{ichi_a}.]", $"(Close[1]/iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_CHIKOUSPAN,1))");
            new_row = new_row.Replace($"TENKANSEN[{ichi_a}.]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_TENKANSEN,1)");
            new_row = new_row.Replace($"KIJUNSEN[{ichi_a}.]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_KIJUNSEN,1)");
            new_row = new_row.Replace($"SENKOUSPANA[{ichi_a}.]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_SENKOUSPANA,1)");
            new_row = new_row.Replace($"SENKOUSPANB[{ichi_a}.]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_SENKOUSPANB,1)");
            new_row = new_row.Replace($"CHIKOUSPAN[{ichi_a}.]", $"iIchimoku(NULL,0,{ichi_a},{ichi_b},{ichi_c},MODE_CHIKOUSPAN,1)");
            #endregion

            #region RSI
            
            new_row = new_row.Replace("RSI[2]", "iRSI(NULL, 0, 2, PRICE_CLOSE, 1)");
            new_row = new_row.Replace("RSI[4]", "iRSI(NULL, 0, 4, PRICE_CLOSE, 1)");
            new_row = new_row.Replace("RSI[7]", "iRSI(NULL, 0, 7, PRICE_CLOSE, 1)");
            new_row = new_row.Replace("RSI[14]", "iRSI(NULL, 0, 14, PRICE_CLOSE, 1)");
            new_row = new_row.Replace("RSI[2vs4]", "(iRSI(NULL, 0, 2, PRICE_CLOSE, 1)-iRSI(NULL, 0, 4, PRICE_CLOSE, 1))");
            new_row = new_row.Replace("RSI[2vs7]", "(iRSI(NULL, 0, 2, PRICE_CLOSE, 1)-iRSI(NULL, 0, 7, PRICE_CLOSE, 1))");
            new_row = new_row.Replace("RSI[2vs14]", "(iRSI(NULL, 0, 2, PRICE_CLOSE, 1)-iRSI(NULL, 0, 14, PRICE_CLOSE, 1))");
            new_row = new_row.Replace("RSI[4vs7]", "(iRSI(NULL, 0, 4, PRICE_CLOSE, 1)-iRSI(NULL, 0, 7, PRICE_CLOSE, 1))");
            new_row = new_row.Replace("RSI[4vs14]", "(iRSI(NULL, 0, 4, PRICE_CLOSE, 1)-iRSI(NULL, 0, 14, PRICE_CLOSE, 1))");
            new_row = new_row.Replace("RSI[7vs14]", "(iRSI(NULL, 0, 7, PRICE_CLOSE, 1)-iRSI(NULL, 0, 14, PRICE_CLOSE, 1))");

            #endregion

            #region BEAR POWER

            new_row = new_row.Replace("BEARS(7)", "iBearsPower(NULL,0,7,PRICE_CLOSE,1)");
            new_row = new_row.Replace("BEARS(14)", "iBearsPower(NULL,0,14,PRICE_CLOSE,1)");
            new_row = new_row.Replace("BEARS(21)", "iBearsPower(NULL,0,21,PRICE_CLOSE,1)");

            #endregion

            #region IBS

            string ibs1 = "(100 * ((Close[1] - Low[1]) / (High[1] - Low[1])))";
            string ibs2 = "(100 * ((Close[2] - Low[2]) / (High[2] - Low[2])))";
            new_row = new_row.Replace("IBS", $"({ibs1}-{ibs2})");

            #endregion

            #region

            new_row = new_row.Replace("ATR(7)", "iATR(NULL, 0, 7, 1)");
            new_row = new_row.Replace("ATR(14)", "iATR(NULL, 0, 14, 1)");
            new_row = new_row.Replace("ATR(20)", "iATR(NULL, 0, 20, 1)");

            #endregion


            return new_row;

        }

    }
}
