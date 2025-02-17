namespace MTT_RuleExtraction
{
    /***** PYTHON 
def plot_ks_test(train_returns, test_returns):
    """
    Realiza el test KS y crea una visualización comparativa
    Returns:
        fig: figura de plotly
        ks_statistic: estadístico KS
        p_value: p-valor del test
    """
    from scipy import stats
    
    # Realizar test KS
    ks_statistic, p_value = stats.ks_2samp(train_returns, test_returns)
    
    # Crear CDFs empíricas
    def empirical_cdf(data):
        x = np.sort(data)
        y = np.arange(1, len(data) + 1) / len(data)
        return x, y
    
    x_train, y_train = empirical_cdf(train_returns)
    x_test, y_test = empirical_cdf(test_returns)
    
    # Encontrar el punto de máxima diferencia
    def find_max_diff_point(x1, y1, x2, y2):
        # Combinar todos los puntos x y ordenarlos
        all_x = np.sort(np.unique(np.concatenate([x1, x2])))
        
        # Interpolar los valores y para cada conjunto
        y1_interp = np.interp(all_x, x1, y1)
        y2_interp = np.interp(all_x, x2, y2)
        
        # Encontrar la diferencia máxima
        diff = np.abs(y1_interp - y2_interp)
        max_diff_idx = np.argmax(diff)
        
        return all_x[max_diff_idx], y1_interp[max_diff_idx], y2_interp[max_diff_idx]
    
    x_max, y1_max, y2_max = find_max_diff_point(x_train, y_train, x_test, y_test)
    
    # Crear gráfico
    fig = go.Figure()
    
    # Añadir CDFs
    fig.add_trace(go.Scatter(
        x=x_train, y=y_train,
        name='Train CDF',
        line=dict(color='blue')
    ))
    
    fig.add_trace(go.Scatter(
        x=x_test, y=y_test,
        name='Test CDF',
        line=dict(color='gold')
    ))
    
    # Añadir línea de máxima diferencia
    fig.add_trace(go.Scatter(
        x=[x_max, x_max],
        y=[y1_max, y2_max],
        mode='lines',
        name='KS Distance',
        line=dict(color='red', dash='dash')
    ))
    
    # Actualizar layout
    fig.update_layout(
        title=f'Test Kolmogorov-Smirnov<br>Estadístico: {ks_statistic:.3f}, p-valor: {p_value:.3e}',
        xaxis_title='Retornos',
        yaxis_title='Probabilidad Acumulada',
        showlegend=True
    )
    
    # Añadir anotación interpretativa
    interpretation = "Las distribuciones son significativamente diferentes" if p_value < 0.05 else "No hay evidencia de diferencias significativas"
    fig.add_annotation(
        text=interpretation,
        xref="paper", yref="paper",
        x=0.5, y=1.05,
        showarrow=False,
        font=dict(size=12)
    )
    
    return fig, ks_statistic, p_value
     */

    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PlotKSTest
    {
        public (List<double> xValues, List<double> yValues) EmpiricalCDF(double[] data)
        {
            Array.Sort(data);
            double[] yValues = Enumerable.Range(1, data.Length).Select(i => (double)i / data.Length).ToArray();
            return (data.ToList(), yValues.ToList());
        }

        public (double ksStatistic, double pValue) KS2Sample(double[] sample1, double[] sample2)
        {
            // Calculate empirical CDFs for both samples
            var (x1, y1) = EmpiricalCDF(sample1);
            var (x2, y2) = EmpiricalCDF(sample2);

            // Combine all unique x values
            var allX = x1.Concat(x2).Distinct().OrderBy(x => x).ToArray();

            // Interpolate y values for each unique x
            List<double> interpY1 = Interpolate(allX, x1, y1);
            List<double> interpY2 = Interpolate(allX, x2, y2);

            // Calculate the KS statistic
            double maxDifference = interpY1.Zip(interpY2, (y1Value, y2Value) => Math.Abs(y1Value - y2Value)).Max();
            double ksStatistic = maxDifference;

            // Calculate the p-value using a placeholder, the logistic approximation or exact calculation would normally be required.
            double n1 = sample1.Length;
            double n2 = sample2.Length;
            double pValue = Math.Exp(-2 * Math.Pow(ksStatistic, 2) * (n1 * n2) / (n1 + n2));

            return (ksStatistic, pValue);
        }

        public (double xMax, double y1Max, double y2Max) FindMaxDiffPoint(List<double> x1, List<double> y1, List<double> x2, List<double> y2)
        {
            var allX = x1.Concat(x2).Distinct().OrderBy(x => x).ToArray();

            List<double> y1Interp = Interpolate(allX, x1, y1);
            List<double> y2Interp = Interpolate(allX, x2, y2);

            double maxDiff = 0;
            double xMax = 0;
            double y1Max = 0;
            double y2Max = 0;

            for (int i = 0; i < allX.Length; i++)
            {
                double diff = Math.Abs(y1Interp[i] - y2Interp[i]);
                if (diff > maxDiff)
                {
                    maxDiff = diff;
                    xMax = allX[i];
                    y1Max = y1Interp[i];
                    y2Max = y2Interp[i];
                }
            }

            return (xMax, y1Max, y2Max);
        }

        private List<double> Interpolate(double[] xValues, List<double> x, List<double> y)
        {
            List<double> interpolated = new List<double>();
            for (int i = 0; i < xValues.Length; i++)
            {
                interpolated[i] = InterpolateValue(xValues[i], x, y);
            }
            return interpolated;
        }

        private double InterpolateValue(double value, List<double> x, List<double> y)
        {
            if (value <= x[0]) return y[0];
            if (value >= x[x.Count - 1]) return y[y.Count - 1];

            int idx = Array.BinarySearch(x.ToArray(), value);
            if (idx >= 0) return y[idx];

            idx = ~idx; // Get the insertion point
            double x0 = x[idx - 1];
            double y0 = y[idx - 1];
            double x1 = x[idx];
            double y1 = y[idx];

            return y0 + (y1 - y0) * (value - x0) / (x1 - x0);
        }

        public (List<double> xTrain, List<double> yTrain, List<double> xTest, List<double> yTest,
            (double xMax, double y1Max, double y2Max) maxDiff, double ksStatistic, double pValue)
            Plot_KS_Test(double[] trainReturns, double[] testReturns)
        {
            var (ksStatistic, pValue) = KS2Sample(trainReturns, testReturns);
            var (xTrain, yTrain) = EmpiricalCDF(trainReturns);
            var (xTest, yTest) = EmpiricalCDF(testReturns);

            var maxDiff = FindMaxDiffPoint(xTrain, yTrain, xTest, yTest);

            return (xTrain, yTrain, xTest, yTest, maxDiff, ksStatistic, pValue);
        }
    }
}
