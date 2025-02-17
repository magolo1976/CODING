namespace MTT01_winforms.UControls.RuleExtraction
{
    // RuleExtractorControl.cs - Design portion
    using System;
    using System.Windows.Forms;
    using System.Drawing;
    using OxyPlot;
    using OxyPlot.WindowsForms;

    namespace RuleExtractor
    {
        public partial class RuleExtractorControl : UserControl
        {
            // Controls declaration
            private TabControl mainTabControl;
            private TabPage tabData, tabFeatures, tabRules, tabValidation, tabForward, tabBacktest;

            // Data Tab Controls
            private Button btnLoadCSV;
            private DataGridView dataPreviewGrid;
            private Label lblDateRange;
            private DateTimePicker dtpTestStart, dtpTestEnd, dtpTrainStart, dtpTrainEnd, dtpForwardStart, dtpForwardEnd;
            private PlotView priceEvolutionChart, ksTestChart, waterfallChart;
            private DataGridView returnsStatsGrid;

            // Feature Selection Tab Controls
            private ComboBox cmbSide;
            private TrackBar sldCorrelation;
            private Button btnAnalyzeFeatures;
            private ProgressBar progressFeatures;
            private DataGridView featuresGrid;

            // Rule Extraction Tab Controls
            private PlotView monkeyDistributionChart;
            private ComboBox cmbBaseFeature;
            private Button btnFindRules;
            private ProgressBar progressRules;
            private Label lblRuleProgress;
            private DataGridView rulesGrid;
            private PlotView ruleEvolutionChart;

            // Validation Tab Controls
            private ProgressBar progressValidation;
            private DataGridView validationGrid;
            private PlotView validationRuleChart;

            // Forward Tab Controls
            private TrackBar sldValidation;
            private Label lblValidationValue;
            private DataGridView filteredRulesGrid;
            private PlotView forwardRuleChart;

            // Backtest Tab Controls
            private ComboBox cmbRuleForBacktest;
            private Label lblTotalTrades, lblTotalReturn, lblAvgReturn, lblWinRate;
            private Label lblSharpe, lblMaxDD, lblBestTrade, lblWorstTrade;
            private PlotView equityCurveChart, returnsDistChart;

            public RuleExtractorControl()
            {
                InitializeComponent();
            }

            private void InitializeComponent()
            {
                this.Size = new Size(1024, 768);

                // Create main layout
                mainTabControl = new TabControl();
                mainTabControl.Dock = DockStyle.Fill;

                // Create tabs
                tabData = new TabPage("Data");
                tabFeatures = new TabPage("Feature Selection");
                tabRules = new TabPage("Rule Extraction");
                tabValidation = new TabPage("Validation");
                tabForward = new TabPage("Forward");
                tabBacktest = new TabPage("Backtest");

                // Disable tabs initially
                tabFeatures.Enabled = false;
                tabRules.Enabled = false;
                tabValidation.Enabled = false;
                tabForward.Enabled = false;
                tabBacktest.Enabled = false;

                // Setup Data Tab
                SetupDataTab();

                // Setup Feature Selection Tab
                SetupFeatureSelectionTab();

                // Setup Rule Extraction Tab
                SetupRuleExtractionTab();

                // Setup Validation Tab
                SetupValidationTab();

                // Setup Forward Tab
                SetupForwardTab();

                // Setup Backtest Tab
                SetupBacktestTab();

                // Add tabs to control
                mainTabControl.Controls.Add(tabData);
                mainTabControl.Controls.Add(tabFeatures);
                mainTabControl.Controls.Add(tabRules);
                mainTabControl.Controls.Add(tabValidation);
                mainTabControl.Controls.Add(tabForward);
                mainTabControl.Controls.Add(tabBacktest);

                // Add header
                Label headerLabel = new Label();
                headerLabel.Text = "🤖 RULE EXTRACTOR";
                headerLabel.Font = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold);
                headerLabel.AutoSize = true;
                headerLabel.Location = new Point(10, 10);

                Panel separatorPanel = new Panel();
                separatorPanel.Height = 2;
                separatorPanel.BackColor = Color.LightGray;
                separatorPanel.Dock = DockStyle.Top;
                separatorPanel.Location = new Point(10, 50);

                // Create layout
                TableLayoutPanel mainLayout = new TableLayoutPanel();
                mainLayout.Dock = DockStyle.Fill;
                mainLayout.RowCount = 3;
                mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 2));
                mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

                mainLayout.Controls.Add(headerLabel, 0, 0);
                mainLayout.Controls.Add(separatorPanel, 0, 1);
                mainLayout.Controls.Add(mainTabControl, 0, 2);

                this.Controls.Add(mainLayout);
            }

            private void SetupDataTab()
            {
                // Load CSV Button
                btnLoadCSV = new Button();
                btnLoadCSV.Text = "Load CSV File";
                btnLoadCSV.Size = new Size(150, 30);
                btnLoadCSV.Location = new Point(10, 10);
                btnLoadCSV.Click += BtnLoadCSV_Click;

                // Data Preview Grid
                Label dataPreviewLabel = new Label();
                dataPreviewLabel.Text = "Data Preview";
                dataPreviewLabel.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
                dataPreviewLabel.Location = new Point(10, 50);
                dataPreviewLabel.AutoSize = true;

                dataPreviewGrid = new DataGridView();
                dataPreviewGrid.ReadOnly = true;
                dataPreviewGrid.Size = new Size(980, 200);
                dataPreviewGrid.Location = new Point(10, 75);

                // Date Range Label
                lblDateRange = new Label();
                lblDateRange.Text = "Available date range: ";
                lblDateRange.Location = new Point(10, 285);
                lblDateRange.AutoSize = true;

                // Period Selection
                Label periodSelectionLabel = new Label();
                periodSelectionLabel.Text = "Period Selection";
                periodSelectionLabel.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
                periodSelectionLabel.Location = new Point(10, 320);
                periodSelectionLabel.AutoSize = true;

                // Test Period
                Label testLabel = new Label();
                testLabel.Text = "Test Period";
                testLabel.Font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold);
                testLabel.Location = new Point(10, 350);
                testLabel.AutoSize = true;

                Label testStartLabel = new Label();
                testStartLabel.Text = "Start:";
                testStartLabel.Location = new Point(10, 375);
                testStartLabel.AutoSize = true;

                dtpTestStart = new DateTimePicker();
                dtpTestStart.Location = new Point(10, 400);
                dtpTestStart.Size = new Size(150, 25);

                Label testEndLabel = new Label();
                testEndLabel.Text = "End:";
                testEndLabel.Location = new Point(10, 430);
                testEndLabel.AutoSize = true;

                dtpTestEnd = new DateTimePicker();
                dtpTestEnd.Location = new Point(10, 455);
                dtpTestEnd.Size = new Size(150, 25);

                // Train Period
                Label trainLabel = new Label();
                trainLabel.Text = "Train Period";
                trainLabel.Font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold);
                trainLabel.Location = new Point(200, 350);
                trainLabel.AutoSize = true;

                Label trainStartLabel = new Label();
                trainStartLabel.Text = "Start:";
                trainStartLabel.Location = new Point(200, 375);
                trainStartLabel.AutoSize = true;

                dtpTrainStart = new DateTimePicker();
                dtpTrainStart.Location = new Point(200, 400);
                dtpTrainStart.Size = new Size(150, 25);

                Label trainEndLabel = new Label();
                trainEndLabel.Text = "End:";
                trainEndLabel.Location = new Point(200, 430);
                trainEndLabel.AutoSize = true;

                dtpTrainEnd = new DateTimePicker();
                dtpTrainEnd.Location = new Point(200, 455);
                dtpTrainEnd.Size = new Size(150, 25);

                // Forward Period
                Label forwardLabel = new Label();
                forwardLabel.Text = "Forward Period";
                forwardLabel.Font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold);
                forwardLabel.Location = new Point(400, 350);
                forwardLabel.AutoSize = true;

                Label forwardStartLabel = new Label();
                forwardStartLabel.Text = "Start:";
                forwardStartLabel.Location = new Point(400, 375);
                forwardStartLabel.AutoSize = true;

                dtpForwardStart = new DateTimePicker();
                dtpForwardStart.Location = new Point(400, 400);
                dtpForwardStart.Size = new Size(150, 25);

                Label forwardEndLabel = new Label();
                forwardEndLabel.Text = "End:";
                forwardEndLabel.Location = new Point(400, 430);
                forwardEndLabel.AutoSize = true;

                dtpForwardEnd = new DateTimePicker();
                dtpForwardEnd.Location = new Point(400, 455);
                dtpForwardEnd.Size = new Size(150, 25);

                // Charts and Grids
                Label priceEvolutionLabel = new Label();
                priceEvolutionLabel.Text = "Price Evolution";
                priceEvolutionLabel.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
                priceEvolutionLabel.Location = new Point(10, 490);
                priceEvolutionLabel.AutoSize = true;

                priceEvolutionChart = new PlotView();
                priceEvolutionChart.Location = new Point(10, 515);
                priceEvolutionChart.Size = new Size(980, 300);

                // Add controls to tab
                tabData.Controls.Add(btnLoadCSV);
                tabData.Controls.Add(dataPreviewLabel);
                tabData.Controls.Add(dataPreviewGrid);
                tabData.Controls.Add(lblDateRange);
                tabData.Controls.Add(periodSelectionLabel);
                tabData.Controls.Add(testLabel);
                tabData.Controls.Add(testStartLabel);
                tabData.Controls.Add(dtpTestStart);
                tabData.Controls.Add(testEndLabel);
                tabData.Controls.Add(dtpTestEnd);
                tabData.Controls.Add(trainLabel);
                tabData.Controls.Add(trainStartLabel);
                tabData.Controls.Add(dtpTrainStart);
                tabData.Controls.Add(trainEndLabel);
                tabData.Controls.Add(dtpTrainEnd);
                tabData.Controls.Add(forwardLabel);
                tabData.Controls.Add(forwardStartLabel);
                tabData.Controls.Add(dtpForwardStart);
                tabData.Controls.Add(forwardEndLabel);
                tabData.Controls.Add(dtpForwardEnd);
                tabData.Controls.Add(priceEvolutionLabel);
                tabData.Controls.Add(priceEvolutionChart);

                // Add ScrollBar
                Panel scrollPanel = new Panel();
                scrollPanel.AutoScroll = true;
                scrollPanel.Dock = DockStyle.Fill;

                foreach (Control c in tabData.Controls)
                {
                    scrollPanel.Controls.Add(c);
                }

                tabData.Controls.Clear();
                tabData.Controls.Add(scrollPanel);
            }

            private void SetupFeatureSelectionTab()
            {
                // Side selection
                Label sideLabel = new Label();
                sideLabel.Text = "Select Side:";
                sideLabel.Location = new Point(10, 10);
                sideLabel.AutoSize = true;

                cmbSide = new ComboBox();
                cmbSide.Items.AddRange(new object[] { "long", "short" });
                cmbSide.SelectedIndex = 0;
                cmbSide.Location = new Point(10, 35);
                cmbSide.Size = new Size(150, 25);

                // Correlation threshold
                Label correlationLabel = new Label();
                correlationLabel.Text = "Correlation Threshold:";
                correlationLabel.Location = new Point(200, 10);
                correlationLabel.AutoSize = true;

                sldCorrelation = new TrackBar();
                sldCorrelation.Minimum = 75;
                sldCorrelation.Maximum = 100;
                sldCorrelation.Value = 95;
                sldCorrelation.TickFrequency = 1;
                sldCorrelation.Location = new Point(200, 35);
                sldCorrelation.Size = new Size(200, 45);

                Label correlationValueLabel = new Label();
                correlationValueLabel.Text = "0.95";
                correlationValueLabel.Location = new Point(410, 45);
                correlationValueLabel.AutoSize = true;
                sldCorrelation.ValueChanged += (s, e) => correlationValueLabel.Text = (sldCorrelation.Value / 100.0).ToString("N2");

                // Analyze Features Button
                btnAnalyzeFeatures = new Button();
                btnAnalyzeFeatures.Text = "Analyze Features";
                btnAnalyzeFeatures.Size = new Size(150, 30);
                btnAnalyzeFeatures.Location = new Point(10, 90);
                btnAnalyzeFeatures.Click += BtnAnalyzeFeatures_Click;

                // Progress Bar
                progressFeatures = new ProgressBar();
                progressFeatures.Size = new Size(980, 5);
                progressFeatures.Location = new Point(10, 130);
                progressFeatures.Visible = false;

                // Features Grid
                Label featuresLabel = new Label();
                featuresLabel.Text = "Selected Features:";
                featuresLabel.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
                featuresLabel.Location = new Point(10, 145);
                featuresLabel.AutoSize = true;

                featuresGrid = new DataGridView();
                featuresGrid.ReadOnly = true;
                featuresGrid.Size = new Size(980, 500);
                featuresGrid.Location = new Point(10, 170);
                featuresGrid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

                // Add controls to tab
                tabFeatures.Controls.Add(sideLabel);
                tabFeatures.Controls.Add(cmbSide);
                tabFeatures.Controls.Add(correlationLabel);
                tabFeatures.Controls.Add(sldCorrelation);
                tabFeatures.Controls.Add(correlationValueLabel);
                tabFeatures.Controls.Add(btnAnalyzeFeatures);
                tabFeatures.Controls.Add(progressFeatures);
                tabFeatures.Controls.Add(featuresLabel);
                tabFeatures.Controls.Add(featuresGrid);
            }

            private void SetupRuleExtractionTab()
            {
                // Monkey Distribution Chart
                monkeyDistributionChart = new PlotView();
                monkeyDistributionChart.Location = new Point(10, 10);
                monkeyDistributionChart.Size = new Size(980, 200);

                // Base Feature Selection
                Label baseFeatureLabel = new Label();
                baseFeatureLabel.Text = "Select a base feature:";
                baseFeatureLabel.Location = new Point(10, 220);
                baseFeatureLabel.AutoSize = true;

                cmbBaseFeature = new ComboBox();
                cmbBaseFeature.Location = new Point(10, 245);
                cmbBaseFeature.Size = new Size(200, 25);

                // Find Rules Button
                btnFindRules = new Button();
                btnFindRules.Text = "Find Rules";
                btnFindRules.Size = new Size(150, 30);
                btnFindRules.Location = new Point(10, 280);
                btnFindRules.Click += BtnFindRules_Click;

                // Progress
                progressRules = new ProgressBar();
                progressRules.Size = new Size(980, 5);
                progressRules.Location = new Point(10, 320);
                progressRules.Visible = false;

                lblRuleProgress = new Label();
                lblRuleProgress.Location = new Point(10, 330);
                lblRuleProgress.AutoSize = true;

                // Rules Grid
                rulesGrid = new DataGridView();
                rulesGrid.ReadOnly = true;
                rulesGrid.Size = new Size(980, 200);
                rulesGrid.Location = new Point(10, 360);
                rulesGrid.SelectionChanged += RulesGrid_SelectionChanged;

                // Rule Evolution Chart
                Label ruleEvolutionLabel = new Label();
                ruleEvolutionLabel.Text = "Select a rule to view its evolution:";
                ruleEvolutionLabel.Location = new Point(10, 570);
                ruleEvolutionLabel.AutoSize = true;

                ruleEvolutionChart = new PlotView();
                ruleEvolutionChart.Location = new Point(10, 595);
                ruleEvolutionChart.Size = new Size(980, 300);

                // Add controls to tab
                tabRules.Controls.Add(monkeyDistributionChart);
                tabRules.Controls.Add(baseFeatureLabel);
                tabRules.Controls.Add(cmbBaseFeature);
                tabRules.Controls.Add(btnFindRules);
                tabRules.Controls.Add(progressRules);
                tabRules.Controls.Add(lblRuleProgress);
                tabRules.Controls.Add(rulesGrid);
                tabRules.Controls.Add(ruleEvolutionLabel);
                tabRules.Controls.Add(ruleEvolutionChart);

                // Add ScrollBar
                Panel scrollPanel = new Panel();
                scrollPanel.AutoScroll = true;
                scrollPanel.Dock = DockStyle.Fill;

                foreach (Control c in tabRules.Controls)
                {
                    scrollPanel.Controls.Add(c);
                }

                tabRules.Controls.Clear();
                tabRules.Controls.Add(scrollPanel);
            }

            private void SetupValidationTab()
            {
                // Progress Bar
                progressValidation = new ProgressBar();
                progressValidation.Size = new Size(980, 5);
                progressValidation.Location = new Point(10, 10);
                progressValidation.Visible = false;

                // Validation Grid
                validationGrid = new DataGridView();
                validationGrid.ReadOnly = true;
                validationGrid.Size = new Size(980, 300);
                validationGrid.Location = new Point(10, 25);
                validationGrid.SelectionChanged += ValidationGrid_SelectionChanged;

                // Validation Rule Chart
                Label validationRuleLabel = new Label();
                validationRuleLabel.Text = "Select a rule to view its evolution in test:";
                validationRuleLabel.Location = new Point(10, 335);
                validationRuleLabel.AutoSize = true;

                validationRuleChart = new PlotView();
                validationRuleChart.Location = new Point(10, 360);
                validationRuleChart.Size = new Size(980, 300);

                // Add controls to tab
                tabValidation.Controls.Add(progressValidation);
                tabValidation.Controls.Add(validationGrid);
                tabValidation.Controls.Add(validationRuleLabel);
                tabValidation.Controls.Add(validationRuleChart);
            }

            private void SetupForwardTab()
            {
                // Validation Threshold
                Label thresholdLabel = new Label();
                thresholdLabel.Text = "Validation Threshold:";
                thresholdLabel.Location = new Point(10, 10);
                thresholdLabel.AutoSize = true;

                sldValidation = new TrackBar();
                sldValidation.Minimum = 0;
                sldValidation.Maximum = 100;
                sldValidation.Value = 90;
                sldValidation.TickFrequency = 5;
                sldValidation.Location = new Point(10, 35);
                sldValidation.Size = new Size(200, 45);
                sldValidation.ValueChanged += SldValidation_ValueChanged;

                lblValidationValue = new Label();
                lblValidationValue.Text = "90.0%";
                lblValidationValue.Location = new Point(220, 45);
                lblValidationValue.AutoSize = true;

                // Filtered Rules Grid
                Label filteredRulesLabel = new Label();
                filteredRulesLabel.Text = "Rules above validation threshold:";
                filteredRulesLabel.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
                filteredRulesLabel.Location = new Point(10, 90);
                filteredRulesLabel.AutoSize = true;

                filteredRulesGrid = new DataGridView();
                filteredRulesGrid.ReadOnly = true;
                filteredRulesGrid.Size = new Size(980, 300);
                filteredRulesGrid.Location = new Point(10, 115);
                filteredRulesGrid.SelectionChanged += FilteredRulesGrid_SelectionChanged;

                // Forward Rule Chart
                Label forwardRuleLabel = new Label();
                forwardRuleLabel.Text = "Select a rule to view its evolution in forward:";
                forwardRuleLabel.Location = new Point(10, 425);
                forwardRuleLabel.AutoSize = true;

                forwardRuleChart = new PlotView();
                forwardRuleChart.Location = new Point(10, 450);
                forwardRuleChart.Size = new Size(980, 300);

                // Add controls to tab
                tabForward.Controls.Add(thresholdLabel);
                tabForward.Controls.Add(sldValidation);
                tabForward.Controls.Add(lblValidationValue);
                tabForward.Controls.Add(filteredRulesLabel);
                tabForward.Controls.Add(filteredRulesGrid);
                tabForward.Controls.Add(forwardRuleLabel);
                tabForward.Controls.Add(forwardRuleChart);
            }

            private void SetupBacktestTab()
            {
                // Rule Selection
                Label ruleSelectionLabel = new Label();
                ruleSelectionLabel.Text = "Select a rule for backtest:";
                ruleSelectionLabel.Location = new Point(10, 10);
                ruleSelectionLabel.AutoSize = true;

                cmbRuleForBacktest = new ComboBox();
                cmbRuleForBacktest.Location = new Point(10, 35);
                cmbRuleForBacktest.Size = new Size(300, 25);
                cmbRuleForBacktest.SelectedIndexChanged += CmbRuleForBacktest_SelectionChanged;

                // Metrics GroupBoxes
                GroupBox tradingMetricsBox = new GroupBox();
                tradingMetricsBox.Text = "Trading Metrics";
                tradingMetricsBox.Location = new Point(10, 70);
                tradingMetricsBox.Size = new Size(480, 100);

                GroupBox riskMetricsBox = new GroupBox();
                riskMetricsBox.Text = "Risk Metrics";
                riskMetricsBox.Location = new Point(500, 70);
                riskMetricsBox.Size = new Size(480, 100);

                // Trading Metrics
                lblTotalTrades = new Label();
                lblTotalTrades.Text = "Number of trades: ";
                lblTotalTrades.Location = new Point(10, 20);
                lblTotalTrades.AutoSize = true;

                lblTotalReturn = new Label();
                lblTotalReturn.Text = "Total return: ";
                lblTotalReturn.Location = new Point(10, 40);
                lblTotalReturn.AutoSize = true;

                lblAvgReturn = new Label();
                lblAvgReturn.Text = "Average return: ";
                lblAvgReturn.Location = new Point(10, 60);
                lblAvgReturn.AutoSize = true;

                lblWinRate = new Label();
                lblWinRate.Text = "Win rate: ";
                lblWinRate.Location = new Point(10, 80);
                lblWinRate.AutoSize = true;

                // Risk Metrics
                lblSharpe = new Label();
                lblSharpe.Text = "Sharpe ratio: ";
                lblSharpe.Location = new Point(10, 20);
                lblSharpe.AutoSize = true;

                lblMaxDD = new Label();
                lblMaxDD.Text = "Maximum drawdown: ";
                lblMaxDD.Location = new Point(10, 40);
                lblMaxDD.AutoSize = true;

                lblBestTrade = new Label();
                lblBestTrade.Text = "Best trade: ";
                lblBestTrade.Location = new Point(10, 60);
                lblBestTrade.AutoSize = true;

                lblWorstTrade = new Label();
                lblWorstTrade.Text = "Worst trade: ";
                lblWorstTrade.Location = new Point(10, 80);
                lblWorstTrade.AutoSize = true;

                // Charts
                Label equityCurveLabel = new Label();
                equityCurveLabel.Text = "Equity Curve";
                equityCurveLabel.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
                equityCurveLabel.Location = new Point(10, 180);
                equityCurveLabel.AutoSize = true;

                equityCurveChart = new PlotView();
                equityCurveChart.Location = new Point(10, 205);
                equityCurveChart.Size = new Size(980, 250);

                Label returnsDistLabel = new Label();
                returnsDistLabel.Text = "Returns Distribution";
                returnsDistLabel.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
                returnsDistLabel.Location = new Point(10, 465);
                returnsDistLabel.AutoSize = true;

                returnsDistChart = new PlotView();
                returnsDistChart.Location = new Point(10, 490);
                returnsDistChart.Size = new Size(980, 250);

                // Add controls to groupboxes
                tradingMetricsBox.Controls.Add(lblTotalTrades);
                tradingMetricsBox.Controls.Add(lblTotalReturn);
                tradingMetricsBox.Controls.Add(lblAvgReturn);
                tradingMetricsBox.Controls.Add(lblWinRate);

                riskMetricsBox.Controls.Add(lblSharpe);
                riskMetricsBox.Controls.Add(lblMaxDD);
                riskMetricsBox.Controls.Add(lblBestTrade);
                riskMetricsBox.Controls.Add(lblWorstTrade);

                // Add controls to tab
                tabBacktest.Controls.Add(ruleSelectionLabel);
                tabBacktest.Controls.Add(cmbRuleForBacktest);
                tabBacktest.Controls.Add(tradingMetricsBox);
                tabBacktest.Controls.Add(riskMetricsBox);
                tabBacktest.Controls.Add(equityCurveLabel);
                tabBacktest.Controls.Add(equityCurveChart);
                tabBacktest.Controls.Add(returnsDistLabel);
                tabBacktest.Controls.Add(returnsDistChart);
            }

            private void BtnLoadCSV_Click(object sender, EventArgs e)
            {
                // Will be implemented in logic part
            }

            private void BtnAnalyzeFeatures_Click(object sender, EventArgs e)
            {
                // Will be implemented in logic part
            }

            private void BtnFindRules_Click(object sender, EventArgs e)
            {
                // Will be implemented in logic part
            }

            private void RulesGrid_SelectionChanged(object sender, EventArgs e)
            {
                // Will be implemented in logic part
            }

            private void ValidationGrid_SelectionChanged(object sender, EventArgs e)
            {
                // Will be implemented in logic part
            }

            private void SldValidation_ValueChanged(object sender, EventArgs e)
            {
                lblValidationValue.Text = $"{sldValidation.Value}.0%";
                // Additional logic will be implemented in logic part
            }

            private void FilteredRulesGrid_SelectionChanged(object sender, EventArgs e)
            {
                // Will be implemented in logic part
            }

            private void CmbRuleForBacktest_SelectionChanged(object sender, EventArgs e)
            {
                // Will be implemented in logic part
            }
        }
    }
}
