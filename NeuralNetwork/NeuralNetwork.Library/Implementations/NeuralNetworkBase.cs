
using NeuralNetwork.Abstractions;
using NeuralNetwork.Library.ActivationFunctions;
using NeuralNetwork.Library.Helpers;
using NeuralNetwork.Library.InputFunctions;
using NeuralNetwork.Library.Modelos;
using System.Text.Json;

namespace NeuralNetwork.Library.Implementations
{
    public abstract class NeuralNetworkBase : INeuralNetwork
    {
        public INeuron[] InputLayer { get; private set; }
        public INeuron[][] HiddenLayers { get; private set; }
        public INeuron[] OutputLayer { get; private set; }

        public abstract double[] Predict(double[] inputs);

        public abstract void Train(double[][] trainingData, double[][] targets, int epochs, double learningRate);

        public IEnumerable<NeuronInfo> GetNeuronInfo() => NeurnalNetworkHelper.GetNeuronsInfo(this);

        public void SaveModel(string filePath)
        {
            var Model = new ModelParameters
            {
                InputLayersNeuronsCount = InputLayer.Count(),
                HiddenLayersNeuronsCounts = HiddenLayers.Select(l => l.Length).ToArray(),
                OutputLayersNeuronsCount = OutputLayer.Count(),
                NeuronsInfo = NeurnalNetworkHelper.GetNeuronsInfo(this)

            };

            File.WriteAllText(filePath, JsonSerializer.Serialize(Model));
        }

        public INeuralNetwork LoadModel(string filePath)
        {
            string Content = File.ReadAllText(filePath);

            var Model = JsonSerializer.Deserialize<ModelParameters>(Content);

            CreateNeuralNetwork(Model.InputLayersNeuronsCount,
                Model.HiddenLayersNeuronsCounts, Model.OutputLayersNeuronsCount);

            int OutputLayersIndex = Model.HiddenLayersNeuronsCounts.Length + 1;
            INeuron Neuron;

            foreach(var Item in Model.NeuronsInfo)
            {
                if (Item.LayerIndex == 0)
                    Neuron = InputLayer[Item.NeuronIndex];
                else if (Item.LayerIndex == OutputLayersIndex)
                    Neuron = OutputLayer[Item.NeuronIndex];
                else
                    Neuron = HiddenLayers[Item.LayerIndex - 1][Item.NeuronIndex];

                Neuron.SetBiasValue(Item.Bias);
                for(int i=0; i<Item.Weights.Count(); i++)
                {
                    Neuron.Axon.Terminals[i].SetWeightValue(Item.Weights.ElementAt(i));
                }
            }

            return this;
        }

        #region INPUT AND ACTIVATION FUNCTIONS

        protected IInputFunction InputLayerInputFunction { get; set; } = null;
        protected IActivationFunction InputLayerActivationFunction { get; set; } = null;

        protected IInputFunction HiddenLayerInputFunction { get; set; } = new WeightedSumInputFunction();
        protected IActivationFunction HiddenLayerActivationFunction { get; set; } = new HyperbolicTangentActivationFunction();

        protected IInputFunction OutputLayerInputFunction { get; set; } = new WeightedSumInputFunction();
        protected IActivationFunction OutputLayerActivationFunction { get; set; } = null;

        #endregion

        protected IEnumerable<ISynapse> InputDentrites => InputLayer.Select(n => n.Dentrites.First());

        protected void CreateNeuralNetwork(
            int inputLayerNeuronsCount,
            int[] hiddenLayerNeuronsCount,
            int outputLayerNeuronsCount)
        {
            InputLayer = NeurnalNetworkHelper.CreateInputLayer(
                inputLayerNeuronsCount,
                InputLayerInputFunction,
                InputLayerActivationFunction);

            HiddenLayers = NeurnalNetworkHelper.CreateHiddenLayers(
                    hiddenLayerNeuronsCount,
                    InputLayer,
                    HiddenLayerInputFunction, 
                    HiddenLayerActivationFunction
                );

            OutputLayer = NeurnalNetworkHelper.CreateOutputLayer(
                outputLayerNeuronsCount,
                HiddenLayers[^1],
                OutputLayerInputFunction,
                OutputLayerActivationFunction);

            Initialize();
        }

        protected abstract void Initialize();
    }
}
