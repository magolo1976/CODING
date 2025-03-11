
namespace NeuralNetwork.Abstractions
{
    public class NeuronInfo
    {
        public NeuronInfo(int layerIndex, int neuronIndex, double bias, double outputValue, double[] weights)
        {
            LayerIndex = layerIndex;
            NeuronIndex = neuronIndex;
            Bias = bias;
            OutputValue = outputValue;
            Weights = weights;
        }

        public int LayerIndex { get; set; }
        public int NeuronIndex { get; set; }
        public double Bias { get; set; }
        public double OutputValue { get; set; }
        public double[] Weights { get; set; }
    }
}
