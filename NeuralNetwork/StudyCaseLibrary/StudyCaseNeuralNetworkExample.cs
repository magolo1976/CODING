using CVSLibrary;
using NeuralNetwork.Abstractions;
using StudyCaseLibrary.Models;

namespace StudyCaseLibrary
{
    public class StudyCaseNeuralNetworkExample
    {
        public event Action<OnTrainingEventArgs> OnTrainingEvent;
        public event Action<int, double[]> OnPredictionEvent;
        public event Action<double[][]> OnGetTrainingDataEvent;

        public void ExecuteExample()
        {
            var Data = GetFileData();

            (var TrainingData, var TestData) = GetTrainAndTestData(Data);

            double[][] TrainingInputs = ExtractInputs(TrainingData);

            OnGetTrainingDataEvent?.Invoke(TrainingInputs);

            double[][] TrainingTargets = ExtractTargets(TrainingData);

            var Network = new StudyCaseNeuralNetwork();

            Network.OnPredictionEvent += OnPredictionEvent;
            Network.OnTrainingEvent += OnTrainingEvent;

            Network.Train(TrainingInputs, TrainingTargets, 1000, 0.25);

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
            double INSAMPLE_DATA = 0.7;

            Random Rnd = new();
            var ShuffleData = data.OrderBy(x => Rnd.Next()).ToList();
            int TrainSize = (int)(ShuffleData.Count * INSAMPLE_DATA);
            var TrainingData = ShuffleData.Take(TrainSize).ToList();
            var TestData = ShuffleData.Skip(TrainSize).ToList();

            return (TrainingData, TestData);
        }

        private static StudyData[] GetFileData()
        {
            string FileName = "C:\\MAGOLO\\gitCODING\\NeuralNetwork\\StudyCaseLibrary\\Assets\\StudyData2.csv";

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
