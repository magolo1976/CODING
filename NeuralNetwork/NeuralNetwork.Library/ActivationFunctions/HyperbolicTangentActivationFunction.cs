using NeuralNetwork.Abstractions;

namespace NeuralNetwork.Library.ActivationFunctions
{
    public class HyperbolicTangentActivationFunction : IActivationFunction
    {
        public double CalculateOutput(double input) => Math.Tanh(input);
    }
}
