
using NeuralNetwork.Abstractions;

namespace NeuralNetwork.Library.Modelos
{
    internal class ModelParameters
    {
        public int InputLayersNeuronsCount { get; set; }
        public int[] HiddenLayersNeuronsCounts { get; set; }
        public int OutputLayersNeuronsCount { get; set; }

        public IEnumerable<NeuronInfo> NeuronsInfo { get; set; }
    }
}
