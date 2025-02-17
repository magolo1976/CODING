using System.Data;
using OxyPlot;

namespace MTT01_winforms.UControls.RuleExtraction
{
    public partial class UCRuleExtraction_Copia: UserControl
    {
        public partial class MainControl : UserControl
        {
            private BacktestAnalyzer _analyzer = new BacktestAnalyzer();
            private BacktestVisualizer _visualizer = new BacktestVisualizer();
            private Dictionary<string, DataTable> _dataTables = new Dictionary<string, DataTable>();
            private Dictionary<string, object> _sessionState = new Dictionary<string, object>();

            public MainControl()
            {
                InitializeComponent();
            }

            private async void BtnLoadCSV_Click(object sender, RoutedEventArgs e)
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    Title = "Select a CSV File"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        // Load and process the data
                        var dt = LoadCsvFile(openFileDialog.FileName);
                        dt = CalculateTarget(dt);

                        // Show preview of the data
                        dataPreviewGrid.ItemsSource = dt.DefaultView;

                        // Detect date column
                        string dateColumn = DetectDateColumn(dt);
                        if (string.IsNullOrEmpty(dateColumn))
                        {
                            MessageBox.Show("No date column found. Please make sure your CSV has a date column.");
                            return;
                        }

                        // Get date range
                        DateTime minDate = GetMinDate(dt, dateColumn);
                        DateTime maxDate = GetMaxDate(dt, dateColumn);
                        lblDateRange.Content = $"Available date range: {minDate:yyyy-MM-dd} to {maxDate:yyyy-MM-dd}";

                        // Set default dates
                        dtpTestStart.SelectedDate = minDate;
                        dtpTestEnd.SelectedDate = new DateTime(2020, 1, 1);
                        dtpTrainStart.SelectedDate = new DateTime(2020, 1, 1);
                        dtpTrainEnd.SelectedDate = new DateTime(2023, 1, 1);
                        dtpForwardStart.SelectedDate = new DateTime(2023, 1, 1);
                        dtpForwardEnd.SelectedDate = maxDate;

                        // Store data in session state
                        _sessionState["df"] = dt;
                        _sessionState["date_column"] = dateColumn;

                        // Update masks when dates are selected
                        UpdateDateMasks();

                        // Enable other tabs
                        tabFeatures.IsEnabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error processing file: {ex.Message}");
                    }
                }
            }

            private void UpdateDateMasks()
            {
                if (!_sessionState.ContainsKey("df") || !_sessionState.ContainsKey("date_column"))
                    return;

                var dt = (DataTable)_sessionState["df"];
                string dateColumn = (string)_sessionState["date_column"];

                DateTime testStart = dtpTestStart.SelectedDate ?? DateTime.MinValue;
                DateTime testEnd = dtpTestEnd.SelectedDate ?? DateTime.MaxValue;
                DateTime trainStart = dtpTrainStart.SelectedDate ?? DateTime.MinValue;
                DateTime trainEnd = dtpTrainEnd.SelectedDate ?? DateTime.MaxValue;
                DateTime forwardStart = dtpForwardStart.SelectedDate ?? DateTime.MinValue;
                DateTime forwardEnd = dtpForwardEnd.SelectedDate ?? DateTime.MaxValue;

                // Create masks and store in session state
                _sessionState["mask_train"] = CreateDateMask(dt, dateColumn, trainStart, trainEnd);
                _sessionState["mask_test"] = CreateDateMask(dt, dateColumn, testStart, testEnd);
                _sessionState["mask_forward"] = CreateDateMask(dt, dateColumn, forwardStart, forwardEnd);

                // Extract returns for each period
                _sessionState["train_returns"] = ExtractReturns(dt, (bool[])_sessionState["mask_train"]);
                _sessionState["test_returns"] = ExtractReturns(dt, (bool[])_sessionState["mask_test"]);
                _sessionState["forward_returns"] = ExtractReturns(dt, (bool[])_sessionState["mask_forward"]);

                // Update charts
                UpdatePriceEvolutionChart();
                UpdateReturnsStats();
                UpdateKSTest();
                UpdateWaterfallChart();
            }

            private async void BtnAnalyzeFeatures_Click(object sender, RoutedEventArgs e)
            {
                if (!_sessionState.ContainsKey("df") || !_sessionState.ContainsKey("mask_train") ||
                    !_sessionState.ContainsKey("date_column"))
                {
                    MessageBox.Show("Please load and configure data first.");
                    return;
                }

                string side = cmbSide.SelectedItem as string ?? "long";
                double correlationThreshold = sldCorrelation.Value;

                progressFeatures.Visibility = Visibility.Visible;
                await Task.Run(() =>
                {
                    // Filter data for train period
                    var dt = (DataTable)_sessionState["df"];
                    bool[] mask = (bool[])_sessionState["mask_train"];
                    string dateColumn = (string)_sessionState["date_column"];

                    DataTable trainData = FilterDataByMask(dt, mask);

                    // Analyze features
                    Tuple<DataTable, Dictionary<string, object>> result = AnalyzeAllFeatures(
                        trainData,
                        "Target",
                        dateColumn,
                        side,
                        correlationThreshold
                    );

                    // Store results in session state
                    _sessionState["primary_rules"] = result.Item2;
                    _sessionState["side"] = side;

                    // Update UI on UI thread
                    Dispatcher.Invoke(() =>
                    {
                        if (result.Item1.Rows.Count > 0)
                        {
                            featuresGrid.ItemsSource = result.Item1.DefaultView;
                        }
                        else
                        {
                            MessageBox.Show("No significant features found");
                        }
                        progressFeatures.Visibility = Visibility.Collapsed;
                        tabRules.IsEnabled = true;
                    });
                });
            }

            private async void BtnFindRules_Click(object sender, RoutedEventArgs e)
            {
                if (!_sessionState.ContainsKey("primary_rules") || !_sessionState.ContainsKey("df") ||
                    !_sessionState.ContainsKey("mask_train"))
                {
                    MessageBox.Show("Please complete feature analysis first.");
                    return;
                }

                // Calculate monkey distribution if not already done
                if (!_sessionState.ContainsKey("compound_threshold"))
                {
                    progressRules.Visibility = Visibility.Visible;
                    lblRuleProgress.Content = "Calculating monkey distribution...";

                    await Task.Run(() =>
                    {
                        double[] trainReturns = (double[])_sessionState["train_returns"];
                        string side = (string)_sessionState["side"];

                        double[] randomMetrics = CalculateRandomMetricsCompound(trainReturns, side);
                        double threshold = Quantile(randomMetrics, 0.99);

                        _sessionState["compound_threshold"] = threshold;
                        _sessionState["random_metrics"] = randomMetrics;

                        Dispatcher.Invoke(() =>
                        {
                            UpdateMonkeyDistributionChart(randomMetrics, threshold);
                        });
                    });
                }

                // Get base feature and find compound rules
                if (cmbBaseFeature.SelectedItem != null)
                {
                    string selectedFeature = cmbBaseFeature.SelectedItem.ToString();

                    progressRules.Visibility = Visibility.Visible;
                    lblRuleProgress.Content = "Finding compound rules...";

                    await Task.Run(() =>
                    {
                        var dt = (DataTable)_sessionState["df"];
                        bool[] mask = (bool[])_sessionState["mask_train"];
                        DataTable trainData = FilterDataByMask(dt, mask);

                        var primaryRules = (Dictionary<string, object>)_sessionState["primary_rules"];
                        string baseRule = (string)((Dictionary<string, object>)primaryRules[selectedFeature])["rule"];

                        // Get filtered features
                        List<string> filteredFeatures = GetFilteredFeatures(primaryRules);

                        DataTable compoundRules = FindSecondRule(
                            trainData,
                            selectedFeature,
                            baseRule,
                            "Target",
                            (string)_sessionState["date_column"],
                            (string)_sessionState["side"],
                            (double)_sessionState["compound_threshold"],
                            filteredFeatures
                        );

                        _sessionState["compound_rules_df"] = compoundRules;

                        Dispatcher.Invoke(() =>
                        {
                            rulesGrid.ItemsSource = compoundRules?.DefaultView;
                            progressRules.Visibility = Visibility.Collapsed;
                            tabValidation.IsEnabled = true;
                        });
                    });
                }
            }

            private async void TabValidation_Selected(object sender, RoutedEventArgs e)
            {
                // Check if all required data is present
                if (!_sessionState.ContainsKey("df") || !_sessionState.ContainsKey("compound_rules_df") ||
                    !_sessionState.ContainsKey("mask_test") || !_sessionState.ContainsKey("test_returns") ||
                    !_sessionState.ContainsKey("side"))
                {
                    MessageBox.Show("Please complete previous steps first.");
                    return;
                }

                // Calculate monkey test for test data if not already done
                if (!_sessionState.ContainsKey("test_random_metrics"))
                {
                    progressValidation.Visibility = Visibility.Visible;

                    await Task.Run(() =>
                    {
                        double[] testReturns = (double[])_sessionState["test_returns"];
                        string side = (string)_sessionState["side"];

                        double[] randomMetrics = CalculateRandomMetricsCompound(testReturns, side);
                        _sessionState["test_random_metrics"] = randomMetrics;
                    });
                }

                // Validate rules
                DataTable compoundRules = (DataTable)_sessionState["compound_rules_df"];
                if (compoundRules != null && compoundRules.Rows.Count > 0)
                {
                    progressValidation.Visibility = Visibility.Visible;

                    await Task.Run(() =>
                    {
                        var dt = (DataTable)_sessionState["df"];
                        bool[] mask = (bool[])_sessionState["mask_test"];
                        DataTable testData = FilterDataByMask(dt, mask);

                        DataTable validationResults = ValidateRules(
                            testData,
                            compoundRules,
                            "Target",
                            (string)_sessionState["side"],
                            (double[])_sessionState["test_random_metrics"]
                        );

                        // Prepare display data
                        DataTable displayDf = PrepareValidationDisplayTable(validationResults);
                        _sessionState["validation_df"] = displayDf;

                        Dispatcher.Invoke(() =>
                        {
                            validationGrid.ItemsSource = displayDf.DefaultView;
                            progressValidation.Visibility = Visibility.Collapsed;
                            tabForward.IsEnabled = true;
                        });
                    });
                }
            }

            private void SldValidation_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
            {
                if (!_sessionState.ContainsKey("validation_df"))
                    return;

                double threshold = sldValidation.Value;
                _sessionState["validation_threshold"] = threshold;

                DataTable validationDf = (DataTable)_sessionState["validation_df"];
                DataTable filteredRules = FilterRulesByValidation(validationDf, threshold);

                filteredRulesGrid.ItemsSource = filteredRules.DefaultView;
                tabBacktest.IsEnabled = filteredRules.Rows.Count > 0;
            }

            private void CmbRuleForBacktest_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (cmbRuleForBacktest.SelectedItem == null ||
                    !_sessionState.ContainsKey("df") ||
                    !_sessionState.ContainsKey("mask_train") ||
                    !_sessionState.ContainsKey("mask_test") ||
                    !_sessionState.ContainsKey("mask_forward") ||
                    !_sessionState.ContainsKey("side"))
                    return;

                string selectedRule = cmbRuleForBacktest.SelectedItem.ToString();

                // Get all data
                var dt = (DataTable)_sessionState["df"];
                bool[] maskTrain = (bool[])_sessionState["mask_train"];
                bool[] maskTest = (bool[])_sessionState["mask_test"];
                bool[] maskForward = (bool[])_sessionState["mask_forward"];

                // Combine masks
                bool[] maskAll = CombineMasks(maskTrain, maskTest, maskForward);
                DataTable dfAll = FilterDataByMask(dt, maskAll);

                // Calculate backtest metrics
                var result = _analyzer.CalculateBacktestMetrics(
                    dfAll,
                    selectedRule,
                    (string)_sessionState["side"]
                );

                if (result != null)
                {
                    UpdateBacktestMetricsDisplay(result.Metrics);

                    // Update charts
                    equityCurveChart.Model = _visualizer.PlotBacktestEquityCurve(
                        result.Trades,
                        (string)_sessionState["date_column"]
                    ).ToOxyPlotModel();

                    returnsDistChart.Model = _visualizer.PlotTradesDistribution(
                        result.Trades
                    ).ToOxyPlotModel();
                }
                else
                {
                    MessageBox.Show("Not enough trades to calculate metrics");
                }
            }

            // Helper methods (would be implemented in full version)
            private DataTable LoadCsvFile(string filename) { /* Implementation */ return null; }
            private DataTable CalculateTarget(DataTable dt) { /* Implementation */ return null; }
            private string DetectDateColumn(DataTable dt) { /* Implementation */ return null; }
            private DateTime GetMinDate(DataTable dt, string dateColumn) { /* Implementation */ return DateTime.MinValue; }
            private DateTime GetMaxDate(DataTable dt, string dateColumn) { /* Implementation */ return DateTime.MaxValue; }
            private bool[] CreateDateMask(DataTable dt, string dateColumn, DateTime start, DateTime end) { /* Implementation */ return null; }
            private double[] ExtractReturns(DataTable dt, bool[] mask) { /* Implementation */ return null; }
            private void UpdatePriceEvolutionChart() { /* Implementation */ }
            private void UpdateReturnsStats() { /* Implementation */ }
            private void UpdateKSTest() { /* Implementation */ }
            private void UpdateWaterfallChart() { /* Implementation */ }
            private DataTable FilterDataByMask(DataTable dt, bool[] mask) { /* Implementation */ return null; }
            private Tuple<DataTable, Dictionary<string, object>> AnalyzeAllFeatures(DataTable dt, string targetCol, string dateCol, string side, double threshold) { /* Implementation */ return null; }
            private double[] CalculateRandomMetricsCompound(double[] returns, string side) { /* Implementation */ return null; }
            private double Quantile(double[] values, double q) { /* Implementation */ return 0; }
            private void UpdateMonkeyDistributionChart(double[] metrics, double threshold) { /* Implementation */ }
            private List<string> GetFilteredFeatures(Dictionary<string, object> primaryRules) { /* Implementation */ return null; }
            private DataTable FindSecondRule(DataTable dt, string baseFeature, string baseRule, string targetCol, string dateCol, string side, double threshold, List<string> filteredFeatures) { /* Implementation */ return null; }
            private DataTable ValidateRules(DataTable dt, DataTable rules, string targetCol, string side, double[] randomMetrics) { /* Implementation */ return null; }
            private DataTable PrepareValidationDisplayTable(DataTable validationResults) { /* Implementation */ return null; }
            private DataTable FilterRulesByValidation(DataTable validationDf, double threshold) { /* Implementation */ return null; }
            private bool[] CombineMasks(bool[] mask1, bool[] mask2, bool[] mask3) { /* Implementation */ return null; }
            private void UpdateBacktestMetricsDisplay(BacktestAnalyzer.BacktestMetrics metrics) { /* Implementation */ }
        }

        // Extension methods for visualization
        public static class VisualizationExtensions
        {
            public static PlotModel ToOxyPlotModel(this BacktestVisualizer.EquityCurveData data)
            {
                // Convert EquityCurveData to OxyPlot.PlotModel
                PlotModel model = new PlotModel { Title = data.Title };
                /* Implementation */
                return model;
            }

            public static PlotModel ToOxyPlotModel(this BacktestVisualizer.HistogramData data)
            {
                // Convert HistogramData to OxyPlot.PlotModel
                PlotModel model = new PlotModel { Title = data.Title };
                /* Implementation */
                return model;
            }
        }
    }
}
