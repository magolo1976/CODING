namespace StudyCaseLibrary.Models
{
    public class TestResult(int totalPredictions, int correctPredictions)
    {
        public int TotalPrediction => totalPredictions;
        public int CorrectPredictions => correctPredictions;
        public double Accuracy => Math.Round((double)CorrectPredictions / TotalPrediction * 100, 2);

    }
}
