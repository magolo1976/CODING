using MagoloRuleExtraction.Sections.Backtest;
using MagoloRuleExtraction.Sections.Data;
using MagoloRuleExtraction.Sections.FeatureSelection;
using MagoloRuleExtraction.Sections.Forward;
using MagoloRuleExtraction.Sections.RuleExtraction;
using MagoloRuleExtraction.Sections.Validation;
using System.Data;

namespace MagoloRuleExtraction
{
    public partial class Main : Form
    {
        #region Properties

        private int CurrentIndex = -1;
        private DataUC _data = null;
        private FeatureSelectionUC _featureSelection = null;
        private RuleExtractionUC _ruleExtraction = null;
        private ValidationUC _validation= null;
        private ForwardUC _forward = null;
        private BacktestUC _backtest = null;

        private DataUC _DataUC { get { if (_data == null) { _data = new DataUC(); }; return _data; } set { _data = value; } }
        private FeatureSelectionUC _FeatureSelectionUC { get { if (_featureSelection == null) { _featureSelection = new FeatureSelectionUC(); }; return _featureSelection; } set { _featureSelection = value; } }
        private RuleExtractionUC _RuleExtractionUC { get { if (_ruleExtraction == null) { _ruleExtraction = new RuleExtractionUC(); }; return _ruleExtraction; } set { _ruleExtraction = value; } }
        private ValidationUC _ValidationUC { get { if (_validation == null) { _validation = new ValidationUC(); }; return _validation; } set { _validation = value; } }
        private ForwardUC _ForwardUC { get { if (_forward == null) { _forward = new ForwardUC(); }; return _forward; } set { _forward = value; } }
        private BacktestUC _BacktestUC { get { if (_backtest == null) { _backtest = new BacktestUC(); }; return _backtest; } set { _backtest = value; } }

        #endregion

        public Main()
        {
            InitializeComponent();
        }

        #region SECTIONS

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadControlIntoMain(_DataUC);

        }

        private void featureSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _FeatureSelectionUC.Initialize(_DataUC.DataTable_Train, _DataUC.DateColumnName);

            LoadControlIntoMain(_FeatureSelectionUC);

        }

        private void ruleExtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //// Método 1: Usando Merge
            //DataTable resultado = new DataTable();
            //resultado.Merge(_DataUC.DataTable_Train);
            //resultado.Merge(_DataUC.DataTable_Test);
            _RuleExtractionUC = new RuleExtractionUC();
            _RuleExtractionUC.Initialize(
                _DataUC.DataTable_Train,
                _DataUC.DataTable_Test,
                _DataUC.TrainReturns,
                _FeatureSelectionUC.PrimaryRules,
                _DataUC.DateColumnName,
                _FeatureSelectionUC.SideSelected,
                _DataUC.MaskTrain,
                _DataUC.MaskTest,
                _FeatureSelectionUC.CorrelationThreshold
                );

            LoadControlIntoMain(_RuleExtractionUC);

        }

        private void validationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LoadControlIntoMain(_ValidationUC);

        }

        private void forwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LoadControlIntoMain(_ForwardUC);

        }

        private void backtestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LoadControlIntoMain(_BacktestUC);

        }

        #endregion

        #region Private Functions

        private void LoadControlIntoMain(UserControl control)
        {
            //  ClearForm
            if (CurrentIndex > -1)
                Controls.RemoveAt(CurrentIndex);

            Width = control.Width + 20;
            Height = control.Height + 20;

            Padding = new Padding(0);

            control.Padding = new Padding(10,25,10,10);

            Controls.Add(control);

            CurrentIndex = Controls.GetChildIndex(control);
        }

        #endregion
    }
}
