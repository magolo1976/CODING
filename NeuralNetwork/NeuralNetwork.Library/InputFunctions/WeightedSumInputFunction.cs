using NeuralNetwork.Abstractions;

namespace NeuralNetwork.Library.InputFunctions
{
    public class WeightedSumInputFunction : IInputFunction
    {
        public double CalculateInput(IEnumerable<ISynapse> dentrites, double bias) =>
            dentrites.Sum(d => d.Value * d.Weight) + bias;
    }
}
