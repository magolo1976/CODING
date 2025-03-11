using NeuralNetwork.Abstractions;
using NeuralNetwork.Library.Implementations;

namespace ConsoleApp.SimpleDemo
{
    public class SimpleNeuralNetwork : NeuralNetworkBase
    {
        public SimpleNeuralNetwork()
        {
            CreateNeuralNetwork(2, [4,2,2], 3);
        }

        public override double[] Predict(double[] inputs)
        {
            int InputIndex = 0;
            foreach(var Dentrite in InputDentrites)
            {
                Dentrite.ReceiveInputValue(inputs[InputIndex++]);
            }

            var Outputs = OutputLayer.Select(n => n.OutputValue);

            return Outputs.ToArray();
        }

        public override void Train(double[][] trainingData, double[][] targets, int epochs, double learningRate)
        {
            throw new NotImplementedException();
        }

        protected override void Initialize()
        {
            int Value = 1;

            foreach (INeuron Neuron in InputLayer)
                foreach (ISynapse Synapse in Neuron.Axon.Terminals)
                    Synapse.SetWeightValue(Value++ / 100.0);

            foreach (INeuron[] NeuronLayer in HiddenLayers)
                foreach (INeuron Neuron in NeuronLayer)
                    foreach (ISynapse Synapse in Neuron.Axon.Terminals)
                        Synapse.SetWeightValue(Value++ / 100.0);

            foreach (INeuron[] NeuronLayer in HiddenLayers)
                foreach (INeuron Neuron in NeuronLayer)
                    Neuron.SetBiasValue(Value++ / 100.0);

            foreach (INeuron Neuron in OutputLayer)
                Neuron.SetBiasValue(Value++ / 100.0);
        }
    }
}
