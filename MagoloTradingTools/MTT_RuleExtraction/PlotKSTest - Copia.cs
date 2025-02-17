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

    public class PlotKSTest_Copia
    {
        public class KSTestResult
        {
            public double KSStatistic { get; set; }
            public double PValue { get; set; }
            public List<double> TrainX { get; set; }
            public List<double> TrainY { get; set; }
            public List<double> TestX { get; set; }
            public List<double> TestY { get; set; }
            public double MaxDiffX { get; set; }
            public double MaxDiffY1 { get; set; }
            public double MaxDiffY2 { get; set; }
            public string Title { get; set; }
            public string XAxisTitle { get; set; }
            public string YAxisTitle { get; set; }
            public string Interpretation { get; set; }
        }

        public KSTestResult PerformKSTest(List<double> trainReturns, List<double> testReturns)
        {
            // Perform KS test
            (double ksStatistic, double pValue) = KolmogorovSmirnovTest(trainReturns, testReturns);

            // Create empirical CDFs
            (List<double> xTrain, List<double> yTrain) = CalculateEmpiricalCDF(trainReturns);
            (List<double> xTest, List<double> yTest) = CalculateEmpiricalCDF(testReturns);

            // Find point of maximum difference
            (double xMax, double y1Max, double y2Max) = FindMaxDifferencePoint(xTrain, yTrain, xTest, yTest);

            // Determine interpretation
            string interpretation = pValue < 0.05
                ? "Las distribuciones son significativamente diferentes"
                : "No hay evidencia de diferencias significativas";

            // Return result
            return new KSTestResult
            {
                KSStatistic = ksStatistic,
                PValue = pValue,
                TrainX = xTrain,
                TrainY = yTrain,
                TestX = xTest,
                TestY = yTest,
                MaxDiffX = xMax,
                MaxDiffY1 = y1Max,
                MaxDiffY2 = y2Max,
                Title = $"Test Kolmogorov-Smirnov\nEstadístico: {ksStatistic:F3}, p-valor: {pValue:E3}",
                XAxisTitle = "Retornos",
                YAxisTitle = "Probabilidad Acumulada",
                Interpretation = interpretation
            };
        }

        private (double KSStatistic, double PValue) KolmogorovSmirnovTest(List<double> sample1, List<double> sample2)
        {
            // Sort the samples
            sample1 = sample1.OrderBy(x => x).ToList();
            sample2 = sample2.OrderBy(x => x).ToList();

            int n1 = sample1.Count;
            int n2 = sample2.Count;

            // Combine samples to find all possible x values
            var allValues = sample1.Concat(sample2).OrderBy(x => x).Distinct().ToList();

            double maxDiff = 0;

            // Count cumulative occurrences in each sample
            int idx1 = 0, idx2 = 0;
            foreach (double x in allValues)
            {
                // Count values <= x in sample1
                while (idx1 < n1 && sample1[idx1] <= x)
                    idx1++;

                // Count values <= x in sample2
                while (idx2 < n2 && sample2[idx2] <= x)
                    idx2++;

                // Calculate CDFs at this point
                double cdf1 = (double)idx1 / n1;
                double cdf2 = (double)idx2 / n2;

                // Update maximum difference
                double diff = Math.Abs(cdf1 - cdf2);
                if (diff > maxDiff)
                    maxDiff = diff;
            }

            // Calculate KS statistic
            double ksStatistic = maxDiff;

            // Calculate p-value using asymptotic distribution
            double en = Math.Sqrt((double)(n1 * n2) / (n1 + n2));
            double lambda = (en + 0.12 + 0.11 / en) * ksStatistic;

            // p-value calculation (approximate)
            double pValue = Math.Exp(-2 * lambda * lambda);

            return (ksStatistic, pValue);
        }

        private (List<double> X, List<double> Y) CalculateEmpiricalCDF(List<double> data)
        {
            var sortedData = data.OrderBy(x => x).ToList();
            var x = sortedData;
            var y = new List<double>();

            for (int i = 0; i < sortedData.Count; i++)
            {
                y.Add((i + 1.0) / sortedData.Count);
            }

            return (x, y);
        }

        private (double X, double Y1, double Y2) FindMaxDifferencePoint(
            List<double> x1, List<double> y1, List<double> x2, List<double> y2)
        {
            // Combine and sort all x values
            var allX = x1.Concat(x2).OrderBy(x => x).Distinct().ToList();

            // Interpolate y values for each data set
            var y1Interp = Interpolate(allX, x1, y1);
            var y2Interp = Interpolate(allX, x2, y2);

            // Find maximum difference
            double maxDiff = 0;
            int maxIdx = 0;

            for (int i = 0; i < allX.Count; i++)
            {
                double diff = Math.Abs(y1Interp[i] - y2Interp[i]);
                if (diff > maxDiff)
                {
                    maxDiff = diff;
                    maxIdx = i;
                }
            }

            return (allX[maxIdx], y1Interp[maxIdx], y2Interp[maxIdx]);
        }

        private List<double> Interpolate(List<double> newX, List<double> x, List<double> y)
        {
            var result = new List<double>();

            foreach (double xi in newX)
            {
                // Find position where xi would be inserted in x
                int idx = x.BinarySearch(xi);
                if (idx < 0)
                {
                    idx = ~idx;
                    if (idx == 0)
                    {
                        // Extrapolate below
                        result.Add(y[0]);
                    }
                    else if (idx >= x.Count)
                    {
                        // Extrapolate above
                        result.Add(y[y.Count - 1]);
                    }
                    else
                    {
                        // Interpolate
                        double x0 = x[idx - 1];
                        double x1 = x[idx];
                        double y0 = y[idx - 1];
                        double y1 = y[idx];
                        double yi = y0 + (xi - x0) * (y1 - y0) / (x1 - x0);
                        result.Add(yi);
                    }
                }
                else
                {
                    // Exact match
                    result.Add(y[idx]);
                }
            }

            return result;
        }
    }

}
