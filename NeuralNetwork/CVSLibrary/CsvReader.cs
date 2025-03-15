using System.Collections.Concurrent;

namespace CVSLibrary
{
    public static class CsvReader
    {
        public static IEnumerable<string[]> Read(string path, bool hasHeader = false)
        {
            ConcurrentBag<string[]> Data = new();
            IEnumerable<string> DataRows;

            if (hasHeader)
            {
                DataRows = File.ReadLines(path).Skip(1);
            }
            else
            {
                DataRows = File.ReadLines(path);
            }

            Parallel.ForEach(DataRows, row =>
            {
                try
                {
                    Data.Add(row.Split(";", StringSplitOptions.TrimEntries));
                }
                catch(Exception ex)
                {
                    throw new Exception($"Error procession line; {row}. Error {ex.Message}");
                }
            });

            return Data;
        }
    }
}
