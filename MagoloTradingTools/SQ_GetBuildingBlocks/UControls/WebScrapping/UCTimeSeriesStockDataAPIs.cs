using System.Net;
using Newtonsoft.Json;


namespace MTT01_winforms.UControls.WebScrapping
{
    public partial class UCTimeSeriesStockDataAPIs : UserControl
    {
        //******************************************
        //https://www.alphavantage.co/documentation/
        //******************************************
        private string API_KEY_Alphavantage = "HO2EQSVGL9NP2F1N";

        public UCTimeSeriesStockDataAPIs()
        {
            InitializeComponent();

            //TIME_SERIES_INTRADAY("IBM", "5min");
        }

        /// <summary>
        /// The API will return the most recent 100 intraday OHLCV bars by default when the outputsize parameter is not set
        /// https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=IBM&interval=5min&apikey=demo
        /// Query the most recent full 30 days of intraday data by setting outputsize=full
        /// https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=IBM&interval=5min&outputsize=full&apikey=demo
        /// Query intraday data for a given month in history (e.g., 2009-01). Any month in the last 20+ years (since 2000-01) is supported
        /// https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=IBM&interval=5min&month=2009-01&outputsize=full&apikey=demo
        /// Downloadable CSV file:
        /// https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=IBM&interval=5min&apikey=demo&datatype=csv
        /// </summary>
        private void TIME_SERIES_INTRADAY(string _symbol, string _timeFrame)
        {
            // replace the "demo" apikey below with your own key from https://www.alphavantage.co/support/#api-key
            string QUERY_URL =
                    string.Format(
                        "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={0}&interval={1}&apikey={2}",
                        _symbol,
                        _timeFrame,
                        API_KEY_Alphavantage);

            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                string client_str = client.DownloadString(queryUri);

                Dictionary<string, dynamic> json_data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(client_str);

            }
        }

        private void TxtSearchEndpoint_KeyUp(object sender, KeyEventArgs e)
        {
            string QUERY_URL =
                    string.Format(
                        "https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords={0}&apikey={1}",
                        TxtSearchEndpoint.Text,
                        API_KEY_Alphavantage);

            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                string client_str = client.DownloadString(queryUri);

                Dictionary<string, dynamic> json_data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(client_str);

                ListSearchEndpoint.Items.Clear();

                /*{
                  '1. symbol': 'IB.TRV',
                  '2. name': 'IBC Advanced Alloys Corp',
                  '3. type': 'Equity',
                  '4. region': 'Toronto Venture',
                  '5. marketOpen': '09:30',
                  '6. marketClose': '16:00',
                  '7. timezone': 'UTC-05',
                  '8. currency': 'CAD',
                  '9. matchScore': '0.6667'
                 }*/
                foreach (KeyValuePair<string, dynamic> json in json_data)
                {
                    var value = json.Value.ToString();

                    try
                    {
                        var jsonArray = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(value);

                        foreach (var item in jsonArray)
                        {
                            if (item.ContainsKey("1. symbol"))
                            {
                                ListSearchEndpoint.Items.Add(item["1. symbol"]);
                            }
                        }
                    }
                    catch 
                    {
                        ListSearchEndpoint.Items.Clear();
                        ListSearchEndpoint.Items.Add(value);

                        continue; 
                    }
                    
                }
            }
        }
    }
}
