
using System.Globalization;

namespace NeuralNetwork.Abstractions
{
    public interface INeuralNetwork
    {
        INeuron[] InputLayer { get; }
        INeuron[][] HiddenLayers { get; }
        INeuron[] OutputLayer { get; }

        double[] Predict(double[] input);
        void Train(double[][] trainData, double[][] targets, int epochs, double learningRate);
        IEnumerable<NeuronInfo> GetNeuronInfo();
        void SaveModel(string filePath);
        INeuralNetwork LoadModel(string filePath);
    }
}
