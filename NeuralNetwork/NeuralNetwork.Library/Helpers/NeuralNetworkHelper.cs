
using NeuralNetwork.Abstractions;
using NeuralNetwork.Library.Implementations;

namespace NeuralNetwork.Library.Helpers
{
    internal static class NeurnalNetworkHelper
    {
        public static IEnumerable<NeuronInfo> GetNeuronsInfo(INeuralNetwork network)
        {
            List<NeuronInfo> Result = [];

            int CurrentLayerIndex = 0;

            AddInfo(network.InputLayer, CurrentLayerIndex++);

            foreach (var Layer in network.HiddenLayers)
                AddInfo(Layer, CurrentLayerIndex++);

            AddInfo(network.OutputLayer, CurrentLayerIndex);

            return Result;

            void AddInfo(INeuron[] layer, int layerIndex)
            {
                for(int i=0;i < layer.Length; i++)
                {
                    Result.Add(new NeuronInfo(
                            layerIndex, i, 
                            layer[i].Bias,
                            layer[i].OutputValue,
                            layer[i].Axon.Terminals.Select(t => t.Weight).ToArray())
                        );
                }
            }
        }

        public static INeuron[] CreateInputLayer(int inputLayerNeuronCount, IInputFunction inputFunction, IActivationFunction activatioFunction)
        {
            INeuron[] Layer = new INeuron[inputLayerNeuronCount];

            Synapse Dentrite;
            Neuron Neuron;

            for(int i = 0; i< inputLayerNeuronCount; i++)
            {
                Dentrite = new Synapse();
                Neuron = new Neuron(inputFunction, activatioFunction);
                Neuron.AddDentrite(Dentrite);
                Layer[i] = Neuron;
            }

            return Layer;
        }

        public static INeuron[][] CreateHiddenLayers(
                int[] hiddenLayerNeuronsCount, 
                INeuron[] inputLayer, 
                IInputFunction inputFunction, 
                IActivationFunction activationFunction)
        {
            INeuron[][] HiddenLayers = new INeuron[hiddenLayerNeuronsCount.Length][];
            INeuron[] PreviousLayer = inputLayer;

            for(int i=0; i < hiddenLayerNeuronsCount.Length; i++)
            {
                HiddenLayers[i] = CreateLayer(PreviousLayer,
                    hiddenLayerNeuronsCount[i],
                    inputFunction, activationFunction);

                PreviousLayer = HiddenLayers[i];
            }

            return HiddenLayers;
        }

        public static INeuron[] CreateOutputLayer(
            int outputLayerNeuronsCount,
            INeuron[] lastHiddenLayer,
            IInputFunction inputFunction,
            IActivationFunction activationFunction) => CreateLayer(lastHiddenLayer, outputLayerNeuronsCount, inputFunction, activationFunction);

        public static INeuron[] CreateLayer(
                INeuron[] previousLayer, 
                int layerNeuronsCount, 
                IInputFunction inputFunction, 
                IActivationFunction activationFunction)
        {
            INeuron[] NewLayer = new INeuron[layerNeuronsCount];

            for(int i=0; i < layerNeuronsCount; i++)
            {
                NewLayer[i] = new Neuron(inputFunction, activationFunction);
            }

            Synapse Synapse;
            foreach(INeuron PresynapticNeuron in previousLayer)
            {
                foreach(var PostSynapticNeuron in NewLayer)
                {
                    Synapse = new Synapse();
                    PresynapticNeuron.AddTerminal(Synapse);
                    PostSynapticNeuron.AddDentrite(Synapse);
                }
            }

            return NewLayer;
        }
    }
}
