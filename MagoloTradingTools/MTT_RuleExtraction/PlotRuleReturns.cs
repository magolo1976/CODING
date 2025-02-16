namespace MTT_RuleExtraction
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class PlotRuleReturns
    {
        /***** PYTHON 
def plot_rule_returns(df, rule, date_column, side, mask_train):
    """
    Grafica la evolución de los retornos acumulados para una regla en train y test
    """
    mask = df.eval(rule)
    df_filtered = df[mask].copy()
    
    if len(df_filtered) == 0:
        return None
        
    side_multiplier = 1 if side == 'long' else -1
    df_filtered['adjusted_returns'] = df_filtered['Target'] * side_multiplier
    
    # Asignar período (train/test) a cada fila
    df_filtered['period'] = 'test'
    df_filtered.loc[mask_train, 'period'] = 'train'
    
    # Calcular retornos acumulados por período
    df_filtered['cumulative_returns'] = df_filtered['adjusted_returns'].cumsum()
    
    fig = go.Figure()
    
    # Graficar train
    df_train = df_filtered[df_filtered['period'] == 'train']
    if not df_train.empty:
        fig.add_trace(go.Scatter(
            x=df_train[date_column],
            y=df_train['cumulative_returns'],
            mode='lines',
            name='Train',
            line=dict(color='blue')
        ))
    
    # Graficar test
    df_test = df_filtered[df_filtered['period'] == 'test']
    if not df_test.empty:
        fig.add_trace(go.Scatter(
            x=df_test[date_column],
            y=df_test['cumulative_returns'],
            mode='lines',
            name='Test',
            line=dict(color='gold')
        ))
    
    fig.update_layout(
        title='Evolución del Retorno Acumulado',
        xaxis_title='Fecha',
        yaxis_title='Retorno Acumulado (%)',
        showlegend=True
    )
    
    return fig
         */

        public class PlotData
        {
            public List<DateTime> TrainDates { get; set; } = new List<DateTime>();
            public List<double> TrainReturns { get; set; } = new List<double>();
            public List<DateTime> TestDates { get; set; } = new List<DateTime>();
            public List<double> TestReturns { get; set; } = new List<double>();
            public string Title { get; set; } = "Evolución del Retorno Acumulado";
            public string XAxisTitle { get; set; } = "Fecha";
            public string YAxisTitle { get; set; } = "Retorno Acumulado (%)";
        }

        public PlotData Plot_Rule_Returns(DataTable df, string rule, string dateColumn, string side, Func<DataRow, bool> maskTrain)
        {
            // Filter data based on rule
            DataTable dfFiltered = new DataTable();
            dfFiltered = df.Clone(); // Create same structure

            // Evaluate rule (simplified - in real code you'd need dynamic expression evaluation)
            foreach (DataRow row in df.Rows)
            {
                bool ruleResult = EvaluateRule(row, rule);
                if (ruleResult)
                {
                    dfFiltered.ImportRow(row);
                }
            }

            if (dfFiltered.Rows.Count == 0)
            {
                return null;
            }

            // Add period and adjusted returns columns
            dfFiltered.Columns.Add("period", typeof(string));
            dfFiltered.Columns.Add("adjusted_returns", typeof(double));
            dfFiltered.Columns.Add("cumulative_returns", typeof(double));

            double sideMultiplier = side == "long" ? 1 : -1;
            double cumulativeReturn = 0;

            // Calculate adjusted returns and assign periods
            foreach (DataRow row in dfFiltered.Rows)
            {
                // Set period (train/test)
                row["period"] = maskTrain(row) ? "train" : "test";

                // Calculate adjusted returns
                double target = Convert.ToDouble(row["Target"]);
                double adjustedReturn = target * sideMultiplier;
                row["adjusted_returns"] = adjustedReturn;

                // Calculate cumulative returns
                cumulativeReturn += adjustedReturn;
                row["cumulative_returns"] = cumulativeReturn;
            }

            // Prepare plot data
            PlotData plotData = new PlotData();

            // Extract train data
            var trainRows = dfFiltered.AsEnumerable()
                .Where(r => r.Field<string>("period") == "train")
                .OrderBy(r => r.Field<DateTime>(dateColumn))
                .ToList();

            foreach (var row in trainRows)
            {
                plotData.TrainDates.Add(row.Field<DateTime>(dateColumn));
                plotData.TrainReturns.Add(row.Field<double>("cumulative_returns"));
            }

            // Extract test data
            var testRows = dfFiltered.AsEnumerable()
                .Where(r => r.Field<string>("period") == "test")
                .OrderBy(r => r.Field<DateTime>(dateColumn))
                .ToList();

            foreach (var row in testRows)
            {
                plotData.TestDates.Add(row.Field<DateTime>(dateColumn));
                plotData.TestReturns.Add(row.Field<double>("cumulative_returns"));
            }

            return plotData;
        }

        private bool EvaluateRule(DataRow row, string rule)
        {
            // This is a simplified placeholder for rule evaluation
            // In a real implementation, you would need a proper expression evaluator
            // like System.Linq.Dynamic.Core or a custom parser

            // Example implementation (very simplified):
            if (rule == "Column1 > 0")
            {
                return Convert.ToDouble(row["Column1"]) > 0;
            }

            throw new NotImplementedException("Rule evaluation requires Dynamic Linq or custom expression parser");
        }
    }
}
