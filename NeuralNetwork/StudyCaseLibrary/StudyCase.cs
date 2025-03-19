using CVSLibrary;
using StudyCaseLibrary.Models;

namespace StudyCaseLibrary
{
    public class StudyCase
    {
        public event Action<OnTrainingEventArgs> OnTrainingEvent;
        public event Action<int, double[]> OnPredictionEvent;
        public event Action<double[][]> OnGetTrainingDataEvent;

        StudyData[] Data;
        IEnumerable<StudyData> TrainingData;
        IEnumerable<StudyData> TestData;
        StudyCaseNeuralNetwork Network;

        public void Train(int epochs, double learningRate, string filePath)
        {
            Data ??= GetFileData(filePath); // Equivalenta a: Data = Data ?? GetFileData();

            (TrainingData, TestData) = GetTrainAndTestData(Data);

            double[][] TrainingInputs = ExtractInputs(TrainingData);

            OnGetTrainingDataEvent?.Invoke(TrainingInputs);

            double[][] TrainingTargets = ExtractTargets(TrainingData);

            Network = new StudyCaseNeuralNetwork();

            Network.OnPredictionEvent += OnPredictionEvent;
            Network.OnTrainingEvent += OnTrainingEvent;

            Network.Train(TrainingInputs, TrainingTargets, epochs, learningRate);

        }

        public TestResult Test()
        {
            var TestInputs = ExtractInputs(TestData);
            var TestTargets = ExtractTargets(TestData);

            var Predictions = TestInputs.Select(input => Network.Predict(input)).ToArray();

            int CorrectPredictions = 0;
            int TotalPredictions = TestTargets.Length;

            for(int i=0; i < TotalPredictions; i++)
            {
                var PredictedLabel = Predictions[i][0] > 0.5 ? 1 : 0;
                var ActualLabel = TestTargets[i][0];

                if (PredictedLabel == ActualLabel) CorrectPredictions++;
            }

            return new TestResult(TotalPredictions, CorrectPredictions);
        }

        public double Predict(double study, double sleep)
        {
            return Network.Predict([study, sleep])[0];
        }

        public void SaveModel(string filePath)
        {
            Network.SaveModel(filePath);
        }

        public void LoadModel(string filePath)
        {
            Network = new StudyCaseNeuralNetwork(filePath);
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

        private static StudyData[] GetFileData(string filePath)
        {
            //string FileName = "C:\\MAGOLO\\gitCODING\\NeuralNetwork\\StudyCaseLibrary\\Assets\\StudyData2.csv";

            var RawData = CsvReader.Read(filePath);

            List<StudyData> Data = new();
            foreach(var Item in RawData)
            {
                try
                {
                    Data.Add(new StudyData(
                        double.Parse(Item[0]),
                        double.Parse(Item[1]),
                        int.Parse(Item[2])
                        ));
                }
                catch
                {

                }
            }

            return Data.ToArray();
        }

        public static double NormalizeDateTime(DateTime time)
        {
            int minutes = time.Hour * 60 + time.Minute;

            double normalizede = Math.Round(minutes / 595.0, 4);

            return normalizede;
        }
    }
}
