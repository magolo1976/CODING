using MTTCommon_objects;
using System.Data;
using System.Globalization;
using static MTTCommon_objects.Enumerations;

namespace MTT_LoadData
{
    public static class DataCandle
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static List<CandleMT4Data> ListFromDataTable(DataTable dataTable)
        {
            List<CandleMT4Data> candleList = new List<CandleMT4Data>();

            int count = 0;

            foreach (DataRow row in dataTable.Rows)
            {
                DateTime date = DateTime.ParseExact(row[0].ToString().Replace("-", ".").Replace("_", ".") + " " + row[1], "yyyy.MM.dd HH:mm", CultureInfo.InvariantCulture);
                double open = double.Parse(row[2].ToString(), CultureInfo.InvariantCulture);
                double high = double.Parse(row[3].ToString(), CultureInfo.InvariantCulture);
                double low = double.Parse(row[4].ToString(), CultureInfo.InvariantCulture);
                double close = double.Parse(row[5].ToString(), CultureInfo.InvariantCulture);
                int volume = int.Parse(row[6].ToString(), CultureInfo.InvariantCulture);

                candleList.Add(new CandleMT4Data()
                {
                    Index = count,
                    Date = date,
                    Open = open,
                    High = high,
                    Low = low,
                    Close = close,
                    Volume = volume
                });

                count++;
            }

            return candleList; // candleList.OrderBy(o => o.Date).ToList();

        }

    }
}
