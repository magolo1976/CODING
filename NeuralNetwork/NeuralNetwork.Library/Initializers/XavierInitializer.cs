using NeuralNetwork.Abstractions;

namespace NeuralNetwork.Library.Initializers
{
    public class XavierInitializer
    {
        public static void InitializeUniform(INeuralNetwork network)
        {
            InitializeWeightWithXavierInitializer(network, (nin, nout) =>
            {
                double limit = Math.Sqrt(6) / Math.Sqrt(nin + nout);
                return (-limit, limit);
            });
        }

        public static void InitializeNormal(INeuralNetwork network)
        {
            InitializeWeightWithXavierInitializer(network, (nin, nout) =>
            {
                double limit = Math.Sqrt(2) / Math.Sqrt(nin + nout);
                return (0, limit);
            });
        }

        static void InitializeWeightWithXavierInitializer(INeuralNetwork network, 
            Func<int, int, (double, double)> initializer)
        {
            var Rnd = new Random();

            Initialize(network.InputLayer, network.HiddenLayers[0], Rnd, initializer);

            for(int i=0; i < network.HiddenLayers.Length -1; i++)
            {
                Initialize(network.HiddenLayers[i],
                    network.HiddenLayers[i + 1], Rnd, initializer);
            }

            Initialize(network.HiddenLayers[^1], network.OutputLayer, Rnd, initializer);
        }

        static void Initialize(INeuron[] currentLayer, INeuron[] nextLayer,
            Random rnd, Func<int, int, (double, double)> initializer)        
        {
            int InNeuronsCount = currentLayer.Length;
            int OutNeuronsCount = nextLayer.Length;

            (double LowerLimit, double UpperLimit) =
                initializer(InNeuronsCount, OutNeuronsCount);

            foreach (INeuron Neuron in currentLayer)
                foreach (var Terminal in Neuron.Axon.Terminals)
                    Terminal.SetWeightValue(LowerLimit + (rnd.NextDouble() * (UpperLimit - LowerLimit)));
        }
    }
}
