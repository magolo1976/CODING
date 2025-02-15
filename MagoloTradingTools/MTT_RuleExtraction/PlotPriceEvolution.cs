using System.Data;
using System.Globalization;

namespace MTT_RuleExtraction
{
    internal class PlotPriceEvolution
    {
        /* PYTHON
         * 
        def plot_price_evolution(df, date_column, train_start, train_end, test_start, test_end, forward_start, forward_end):
            """
            Crea un gráfico de la evolución del precio con diferentes colores por período
            """
            # Convertir todas las fechas a datetime64[ns]
            train_start = pd.to_datetime(train_start)
            train_end = pd.to_datetime(train_end)
            test_start = pd.to_datetime(test_start)
            test_end = pd.to_datetime(test_end)
            forward_start = pd.to_datetime(forward_start)
            forward_end = pd.to_datetime(forward_end)
    
            # Asegurar que la columna de fecha está en el formato correcto
            df[date_column] = pd.to_datetime(df[date_column])

            fig = go.Figure()

            # Test period
            mask_test = (df[date_column] >= test_start) & (df[date_column] <= test_end)
            df_test = df[mask_test]
            fig.add_trace(go.Scatter(
                x=df_test[date_column],
                y=df_test['Open'],
                name='Test',
                line=dict(color='#FFD700')  # amarillo
            ))

            # Train period
            mask_train = (df[date_column] >= train_start) & (df[date_column] <= train_end)
            df_train = df[mask_train]
            fig.add_trace(go.Scatter(
                x=df_train[date_column],
                y=df_train['Open'],
                name='Train',
                line=dict(color='#0000FF')  # azul
            ))

            # Forward period
            mask_forward = (df[date_column] >= forward_start) & (df[date_column] <= forward_end)
            df_forward = df[mask_forward]
            fig.add_trace(go.Scatter(
                x=df_forward[date_column],
                y=df_forward['Open'],
                name='Forward',
                line=dict(color='#FF0000')  # rojo
            ))

            fig.update_layout(
                title='Evolución del precio por período',
                xaxis_title='Fecha',
                yaxis_title='Precio de apertura',
                hovermode='x unified'
            )

            return fig 
        */
        public class PriceData
        {
            public DateTime Date { get; set; }
            public decimal Open { get; set; }
        }

        public class PeriodData
        {
            public List<PriceData> PriceDatas { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
        }

        public static List<PeriodData> Plot_Price_Evolution(DataTable df, string dateColumn,
            string trainStart, string trainEnd, string testStart, string testEnd,
            string forwardStart, string forwardEnd)
        {
            // Convert date strings to DateTime
            DateTime trainStartDateTime = DateTime.Parse(trainStart);
            DateTime trainEndDateTime = DateTime.Parse(trainEnd);
            DateTime testStartDateTime = DateTime.Parse(testStart);
            DateTime testEndDateTime = DateTime.Parse(testEnd);
            DateTime forwardStartDateTime = DateTime.Parse(forwardStart);
            DateTime forwardEndDateTime = DateTime.Parse(forwardEnd);

            // Prepare the output data
            var periods = new List<PeriodData>();

            // Process Test period
            var testPeriodData = FilterData(df, dateColumn, testStartDateTime, testEndDateTime);
            periods.Add(new PeriodData { PriceDatas = testPeriodData, Name = "Test", Color = "#FFD700" });

            // Process Train period
            var trainPeriodData = FilterData(df, dateColumn, trainStartDateTime, trainEndDateTime);
            periods.Add(new PeriodData { PriceDatas = trainPeriodData, Name = "Train", Color = "#0000FF" });

            // Process Forward period
            var forwardPeriodData = FilterData(df, dateColumn, forwardStartDateTime, forwardEndDateTime);
            periods.Add(new PeriodData { PriceDatas = forwardPeriodData, Name = "Forward", Color = "#FF0000" });

            return periods;
        }

        private static List<PriceData> FilterData(DataTable df, string dateColumn, DateTime startDate, DateTime endDate)
        {
            var result = new List<PriceData>();

            foreach (DataRow row in df.Rows)
            {
                DateTime rowDate = DateTime.Parse(row[dateColumn].ToString());
                if (rowDate >= startDate && rowDate <= endDate)
                {
                    result.Add(new PriceData
                    {
                        Date = rowDate,
                        Open = decimal.Parse(row["Open"].ToString(), CultureInfo.InvariantCulture)
                    });
                }
            }

            return result;
        }

    }
}
