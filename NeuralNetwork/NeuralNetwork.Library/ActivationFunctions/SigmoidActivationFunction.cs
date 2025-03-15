using NeuralNetwork.Abstractions;

namespace NeuralNetwork.Library.ActivationFunctions
{
    // DISEÑADA PARA CASOS EN LOS QUE 
    // SE DEVUELVAN SOLAMENTE VALORES 0 ò 1
    public class SigmoidActivationFunction : IActivationFunction
    {
        public double CalculateOutput(double input) =>  1 / (1 + Math.Exp(-input));
    }
}
