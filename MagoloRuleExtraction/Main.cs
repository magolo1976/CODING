using MagoloRuleExtraction.Sections.Backtest;
using MagoloRuleExtraction.Sections.Data;
using MagoloRuleExtraction.Sections.FeatureSelection;
using MagoloRuleExtraction.Sections.Forward;
using MagoloRuleExtraction.Sections.RuleExtraction;
using MagoloRuleExtraction.Sections.Validation;

namespace MagoloRuleExtraction
{
    public partial class Main : Form
    {
        #region Private Properties

        private int CurrentIndex = -1;

        #endregion

        public Main()
        {
            InitializeComponent();
        }

        #region SECTIONS

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadControlIntoMain(new DataUC());

        }

        private void featureSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadControlIntoMain(new FeatureSelectionUC());

        }

        private void ruleExtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadControlIntoMain(new RuleExtractionUC());

        }

        private void validationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadControlIntoMain(new ValidationUC());

        }

        private void forwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadControlIntoMain(new ForwardUC());

        }

        private void backtestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadControlIntoMain(new BacktestUC());

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
