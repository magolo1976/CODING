
using ConsoleApp.StudyCase.Models;
using CVSLibrary;
using NeuralNetwork.Abstractions;

namespace ConsoleApp.StudyCase
{
    public class StudyCaseNeuralNetworkExample
    {
        public static void ExecuteExample()
        {
            var Data = GetFileData();

            (var TrainingData, var TestData) = GetTrainAndTestData(Data);

            double[][] TrainingInputs = ExtractInputs(TrainingData);
            double[][] TrainingTargets = ExtractTargets(TrainingData);

            INeuralNetwork Network = new StudyCaseNeuralNetwork();

            Network.Train(TrainingInputs, TrainingTargets, 2000, 0.25);

            (double[] Data, int Expected)[] SampleData =
                [
                ([.4035, .7300], 1),
                ([.4035, .650], 0),
                ([.523, .712], 1),
                ([.347, .435], 0),
                ([.712, .655], 1),
                ];

            foreach(var Item in SampleData)
            {
                var Predicted = Network.Predict(Item.Data)[0];
                Console.WriteLine($"[{string.Join(", ", Item.Data)}], Expected/Predicted: {Item.Expected}, {(Predicted >= .5 ? 1 : 0)} ({Predicted})");
            }
        }

        private static double[][] ExtractTargets(IEnumerable<StudyData> data) =>
        
            data.Select(d => new double[]
            {
                d.Expected
            }).ToArray();
        

        private static double[][] ExtractInputs(IEnumerable<StudyData> data) =>
            data.Select(d => new double[] 
            { 
                d.StudyHours,
                d.SleepingHours
            }).ToArray();

        private static (IEnumerable<StudyData> TrainingGata, IEnumerable<StudyData> TestData) GetTrainAndTestData(StudyData[] data)
        {
            Random Rnd = new();
            var ShuffleData = data.OrderBy(x => Rnd.Next()).ToList();
            int TrainSize = (int)(ShuffleData.Count * 0.7);
            var TrainingData = ShuffleData.Take(TrainSize).ToList();
            var TestData = ShuffleData.Skip(TrainSize).ToList();

            return (TrainingData, TestData);
        }

        private static StudyData[] GetFileData()
        {
            string FileName = "C:\\MAGOLO\\gitCODING\\NeuralNetwork\\ConsoleApp\\StudyCase\\Assets\\StudyData.csv";

            var RawData = CsvReader.Read(FileName);

            List<StudyData> Data = new();
            foreach(var Item in RawData)
            {
                Data.Add(new StudyData(
                    double.Parse(Item[0]),
                    double.Parse(Item[1]),
                    int.Parse(Item[2])
                    ));
            }

            return Data.ToArray();
        }
    }
}
