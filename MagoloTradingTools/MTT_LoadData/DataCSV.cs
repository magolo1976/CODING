using MTTCommon_objects;
using System.Data;

namespace MTT_LoadData
{
    public class DataCSV
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">CSV file Path</param>
        /// <param name="delimitation">By default is ','</param>
        /// <returns></returns>
        public static DataTable CsvToDataTable(string filePath, string delimitation = ",")
        {
            DataTable dt = new DataTable();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string[] headers = sr.ReadLine().Split(delimitation);

                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                int count = 0;

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');

                    DataRow dr = dt.NewRow();

                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }

                    dt.Rows.Add(dr);

                    count++;

                    if (count > 500)
                        break;
                }
            }
            return dt;
        }
    }
}
