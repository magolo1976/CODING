using OxyPlot.WindowsForms;

namespace MTT01_winforms.UControls.RuleExtraction
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
        private PlotView priceEvolutionChart;

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

            // Disable other tabs initially
            tabFeatures.Enabled = false;
            tabRules.Enabled = false;
            tabValidation.Enabled = false;
            tabForward.Enabled = false;
            tabBacktest.Enabled = false;

            // Setup each tab
            SetupDataTab();
            SetupFeatureSelectionTab();
            SetupRuleExtractionTab();
            SetupValidationTab();
            SetupForwardTab();
            SetupBacktestTab();

            // Add tabs to control
            mainTabControl.Controls.Add(tabData);
            mainTabControl.Controls.Add(tabFeatures);
            mainTabControl.Controls.Add(tabRules);
            mainTabControl.Controls.Add(tabValidation);
            mainTabControl.Controls.Add(tabForward);
            mainTabControl.Controls.Add(tabBacktest);

            // Header and layout
            Label headerLabel = new Label
            {
                Text = "🤖 RULE EXTRACTOR",
                Font = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 10)
            };

            Panel separatorPanel = new Panel
            {
                Height = 2,
                BackColor = Color.LightGray,
                Dock = DockStyle.Top
            };

            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3
            };
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
            btnLoadCSV = new Button
            {
                Text = "Load CSV File",
                Size = new Size(150, 30),
                Location = new Point(10, 10)
            };
            btnLoadCSV.Click += BtnLoadCSV_Click;

            Label dataPreviewLabel = new Label
            {
                Text = "Data Preview",
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
                Location = new Point(10, 50),
                AutoSize = true
            };

            dataPreviewGrid = new DataGridView
            {
                ReadOnly = true,
                Size = new Size(980, 200),
                Location = new Point(10, 75)
            };

            lblDateRange = new Label
            {
                Text = "Available date range: ",
                Location = new Point(10, 285),
                AutoSize = true
            };

            Label periodSelectionLabel = new Label
            {
                Text = "Period Selection",
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
                Location = new Point(10, 320),
                AutoSize = true
            };

            // Set up date pickers for Test, Train, Forward periods
            SetUpDatePickers();

            Label priceEvolutionLabel = new Label
            {
                Text = "Price Evolution",
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
                Location = new Point(10, 490),
                AutoSize = true
            };

            priceEvolutionChart = new PlotView
            {
                Location = new Point(10, 515),
                Size = new Size(980, 300)
            };

            // Panel scroll implementation
            Panel scrollPanel = new Panel
            {
                AutoScroll = true,
                Dock = DockStyle.Fill
            };
            foreach (Control c in tabData.Controls)
            {
                scrollPanel.Controls.Add(c);
            }

            tabData.Controls.Clear();
            tabData.Controls.Add(scrollPanel);
        }

        private void SetUpDatePickers()
        {
            // Test Period
            CreateLabelAndPicker(ref dtpTestStart, "Test Period", new Point(10, 350), new Point(10, 375), new Point(10, 400));
            CreateLabelAndPicker(ref dtpTestEnd, null, new Point(10, 430), new Point(10, 455), new Point(10, 380));

            // Train Period
            CreateLabelAndPicker(ref dtpTrainStart, "Train Period", new Point(200, 350), new Point(200, 375), new Point(200, 400));
            CreateLabelAndPicker(ref dtpTrainEnd, null, new Point(200, 430), new Point(200, 455), new Point(200, 380));

            // Forward Period
            CreateLabelAndPicker(ref dtpForwardStart, "Forward Period", new Point(400, 350), new Point(400, 375), new Point(400, 400));
            CreateLabelAndPicker(ref dtpForwardEnd, null, new Point(400, 430), new Point(400, 455), new Point(400, 380));
        }

        private void CreateLabelAndPicker(ref DateTimePicker dateTimePicker, string labelText, Point labelLocation, Point startLabelLocation, Point pickerLocation)
        {
            if (labelText != null)
            {
                Label periodLabel = new Label
                {
                    Text = labelText,
                    Font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold),
                    Location = labelLocation,
                    AutoSize = true
                };
                tabData.Controls.Add(periodLabel);
            }

            Label startLabel = new Label
            {
                Text = "Start:",
                Location = startLabelLocation,
                AutoSize = true
            };
            tabData.Controls.Add(startLabel);

            dateTimePicker = new DateTimePicker
            {
                Location = pickerLocation,
                Size = new Size(150, 25)
            };
            tabData.Controls.Add(dateTimePicker);
        }

        private void SetupFeatureSelectionTab()
        {
            CreateLabelAndComboBox();

            // Analyze Features Button
            btnAnalyzeFeatures = new Button
            {
                Text = "Analyze Features",
                Size = new Size(150, 30),
                Location = new Point(10, 90)
            };
            btnAnalyzeFeatures.Click += BtnAnalyzeFeatures_Click;

            // Progress Bar
            progressFeatures = new ProgressBar
            {
                Size = new Size(980, 5),
                Location = new Point(10, 130),
                Visible = false
            };

            // Features Grid
            Label featuresLabel = new Label
            {
                Text = "Selected Features:",
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
                Location = new Point(10, 145),
                AutoSize = true
            };

            featuresGrid = new DataGridView
            {
                ReadOnly = true,
                Size = new Size(980, 500),
                Location = new Point(10, 170),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            // Add controls to the Features tab
            tabFeatures.Controls.Add(featuresLabel);
            tabFeatures.Controls.Add(featuresGrid);
            tabFeatures.Controls.Add(btnAnalyzeFeatures);
            tabFeatures.Controls.Add(progressFeatures);
        }

        private void CreateLabelAndComboBox()
        {
            // Side selection
            Label sideLabel = new Label
            {
                Text = "Select Side:",
                Location = new Point(10, 10),
                AutoSize = true
            };
            tabFeatures.Controls.Add(sideLabel);

            cmbSide = new ComboBox
            {
                Items = { "long", "short" },
                SelectedIndex = 0,
                Location = new Point(10, 35),
                Size = new Size(150, 25)
            };
            tabFeatures.Controls.Add(cmbSide);

            // Correlation threshold
            Label correlationLabel = new Label
            {
                Text = "Correlation Threshold:",
                Location = new Point(200, 10),
                AutoSize = true
            };
            tabFeatures.Controls.Add(correlationLabel);

            sldCorrelation = CreateTrackBar(new Point(200, 35), 75, 100, 95);
            sldCorrelation.ValueChanged += (s, e) => correlationLabel.Text = (sldCorrelation.Value / 100.0).ToString("N2");
            tabFeatures.Controls.Add(sldCorrelation);
        }

        private TrackBar CreateTrackBar(Point location, int minimum, int maximum, int value)
        {
            return new TrackBar
            {
                Minimum = minimum,
                Maximum = maximum,
                Value = value,
                TickFrequency = 1,
                Location = location,
                Size = new Size(200, 45)
            };
        }

        private void SetupRuleExtractionTab()
        {
            monkeyDistributionChart = new PlotView
            {
                Location = new Point(10, 10),
                Size = new Size(980, 200)
            };

            // Base Feature Selection
            Label baseFeatureLabel = new Label
            {
                Text = "Select a base feature:",
                Location = new Point(10, 220),
                AutoSize = true
            };
            tabRules.Controls.Add(baseFeatureLabel);

            cmbBaseFeature = new ComboBox
            {
                Location = new Point(10, 245),
                Size = new Size(200, 25)
            };
            tabRules.Controls.Add(cmbBaseFeature);

            // Find Rules Button
            btnFindRules = new Button
            {
                Text = "Find Rules",
                Size = new Size(150, 30),
                Location = new Point(10, 280)
            };
            btnFindRules.Click += BtnFindRules_Click;
            tabRules.Controls.Add(btnFindRules);

            progressRules = CreateProgressBar(new Point(10, 320));
            tabRules.Controls.Add(progressRules);

            lblRuleProgress = new Label
            {
                Location = new Point(10, 330),
                AutoSize = true
            };
            tabRules.Controls.Add(lblRuleProgress);

            rulesGrid = CreateDataGridView(new Point(10, 360), 980, 200);
            tabRules.Controls.Add(rulesGrid);

            Label ruleEvolutionLabel = new Label
            {
                Text = "Select a rule to view its evolution:",
                Location = new Point(10, 570),
                AutoSize = true
            };
            tabRules.Controls.Add(ruleEvolutionLabel);

            ruleEvolutionChart = new PlotView
            {
                Location = new Point(10, 595),
                Size = new Size(980, 300)
            };
            tabRules.Controls.Add(ruleEvolutionChart);
        }

        private ProgressBar CreateProgressBar(Point location)
        {
            return new ProgressBar
            {
                Size = new Size(980, 5),
                Location = location,
                Visible = false
            };
        }

        private DataGridView CreateDataGridView(Point location, int width, int height)
        {
            return new DataGridView
            {
                ReadOnly = true,
                Size = new Size(width, height),
                Location = location
            };
        }

        private void SetupValidationTab()
        {
            progressValidation = CreateProgressBar(new Point(10, 10));
            tabValidation.Controls.Add(progressValidation);

            validationGrid = CreateDataGridView(new Point(10, 25), 980, 300);
            tabValidation.Controls.Add(validationGrid);

            Label validationRuleLabel = new Label
            {
                Text = "Select a rule to view its evolution in test:",
                Location = new Point(10, 335),
                AutoSize = true
            };
            tabValidation.Controls.Add(validationRuleLabel);

            validationRuleChart = new PlotView
            {
                Location = new Point(10, 360),
                Size = new Size(980, 300)
            };
            tabValidation.Controls.Add(validationRuleChart);
        }

        private void SetupForwardTab()
        {
            Label thresholdLabel = new Label
            {
                Text = "Validation Threshold:",
                Location = new Point(10, 10),
                AutoSize = true
            };
            tabForward.Controls.Add(thresholdLabel);

            sldValidation = CreateTrackBar(new Point(10, 35), 0, 100, 90);
            sldValidation.ValueChanged += SldValidation_ValueChanged;
            tabForward.Controls.Add(sldValidation);

            lblValidationValue = new Label
            {
                Text = "90.0%",
                Location = new Point(220, 45),
                AutoSize = true
            };
            tabForward.Controls.Add(lblValidationValue);

            Label filteredRulesLabel = new Label
            {
                Text = "Rules above validation threshold:",
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
                Location = new Point(10, 90),
                AutoSize = true
            };
            tabForward.Controls.Add(filteredRulesLabel);

            filteredRulesGrid = CreateDataGridView(new Point(10, 115), 980, 300);
            tabForward.Controls.Add(filteredRulesGrid);

            Label forwardRuleLabel = new Label
            {
                Text = "Select a rule to view its evolution in forward:",
                Location = new Point(10, 425),
                AutoSize = true
            };
            tabForward.Controls.Add(forwardRuleLabel);

            forwardRuleChart = new PlotView
            {
                Location = new Point(10, 450),
                Size = new Size(980, 300)
            };
            tabForward.Controls.Add(forwardRuleChart);
        }

        private void SetupBacktestTab()
        {
            Label ruleSelectionLabel = new Label
            {
                Text = "Select a rule for backtest:",
                Location = new Point(10, 10),
                AutoSize = true
            };
            tabBacktest.Controls.Add(ruleSelectionLabel);

            cmbRuleForBacktest = new ComboBox
            {
                Location = new Point(10, 35),
                Size = new Size(300, 25)
            };
            cmbRuleForBacktest.SelectedIndexChanged += CmbRuleForBacktest_SelectionChanged;
            tabBacktest.Controls.Add(cmbRuleForBacktest);

            GroupBox tradingMetricsBox = CreateMetricsGroupBox("Trading Metrics", new Point(10, 70), new Size(480, 100));
            GroupBox riskMetricsBox = CreateMetricsGroupBox("Risk Metrics", new Point(500, 70), new Size(480, 100));

            // Trading Metrics
            lblTotalTrades = CreateMetricLabel("Number of trades: ", new Point(10, 20));
            lblTotalReturn = CreateMetricLabel("Total return: ", new Point(10, 40));
            lblAvgReturn = CreateMetricLabel("Average return: ", new Point(10, 60));
            lblWinRate = CreateMetricLabel("Win rate: ", new Point(10, 80));

            tradingMetricsBox.Controls.Add(lblTotalTrades);
            tradingMetricsBox.Controls.Add(lblTotalReturn);
            tradingMetricsBox.Controls.Add(lblAvgReturn);
            tradingMetricsBox.Controls.Add(lblWinRate);

            // Risk Metrics
            lblSharpe = CreateMetricLabel("Sharpe ratio: ", new Point(10, 20));
            lblMaxDD = CreateMetricLabel("Maximum drawdown: ", new Point(10, 40));
            lblBestTrade = CreateMetricLabel("Best trade: ", new Point(10, 60));
            lblWorstTrade = CreateMetricLabel("Worst trade: ", new Point(10, 80));

            riskMetricsBox.Controls.Add(lblSharpe);
            riskMetricsBox.Controls.Add(lblMaxDD);
            riskMetricsBox.Controls.Add(lblBestTrade);
            riskMetricsBox.Controls.Add(lblWorstTrade);

            tabBacktest.Controls.Add(tradingMetricsBox);
            tabBacktest.Controls.Add(riskMetricsBox);

            Label equityCurveLabel = new Label
            {
                Text = "Equity Curve",
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
                Location = new Point(10, 180),
                AutoSize = true
            };
            tabBacktest.Controls.Add(equityCurveLabel);

            equityCurveChart = new PlotView
            {
                Location = new Point(10, 205),
                Size = new Size(980, 250)
            };
            tabBacktest.Controls.Add(equityCurveChart);

            Label returnsDistLabel = new Label
            {
                Text = "Returns Distribution",
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
                Location = new Point(10, 465),
                AutoSize = true
            };
            tabBacktest.Controls.Add(returnsDistLabel);

            returnsDistChart = new PlotView
            {
                Location = new Point(10, 490),
                Size = new Size(980, 250)
            };
            tabBacktest.Controls.Add(returnsDistChart);
        }

        private GroupBox CreateMetricsGroupBox(string text, Point location, Size size)
        {
            return new GroupBox
            {
                Text = text,
                Location = location,
                Size = size
            };
        }

        private Label CreateMetricLabel(string text, Point location)
        {
            return new Label
            {
                Text = text,
                Location = location,
                AutoSize = true
            };
        }

        private void BtnLoadCSV_Click(object sender, EventArgs e)
        {
            // Logic to load CSV will be implemented here
        }

        private void BtnAnalyzeFeatures_Click(object sender, EventArgs e)
        {
            // Logic to analyze features will be implemented here
        }

        private void BtnFindRules_Click(object sender, EventArgs e)
        {
            // Logic to find rules will be implemented here
        }

        private void RulesGrid_SelectionChanged(object sender, EventArgs e)
        {
            // Logic when rules grid selection changes will be implemented here
        }

        private void ValidationGrid_SelectionChanged(object sender, EventArgs e)
        {
            // Logic when validation grid selection changes will be implemented here
        }

        private void SldValidation_ValueChanged(object sender, EventArgs e)
        {
            lblValidationValue.Text = $"{sldValidation.Value}.0%";
            // Additional logic can be implemented here
        }

        private void FilteredRulesGrid_SelectionChanged(object sender, EventArgs e)
        {
            // Logic when filtered rules grid selection changes will be implemented here
        }

        private void CmbRuleForBacktest_SelectionChanged(object sender, EventArgs e)
        {
            // Logic when rule for backtest selection changes will be implemented here
        }
    }
}
