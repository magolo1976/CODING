using NeuralNetwork.Abstractions;

namespace ConsoleApp.SimpleDemo
{
    internal static class SimpleNeuralNetworkNetworkExample
    {
        public static void DoExample()
        {
            INeuralNetwork Network = new SimpleNeuralNetwork();

            double[] Outputs = Network.Predict([1, 2]);

           int[] data_afteSoftmax = AlgoritmoSoftmax.ApplySoftmax(Outputs);

            WriteneuonsInfo(Network);
        }

        public static void WriteneuonsInfo(INeuralNetwork neuralNetwork)
        {
            var NeuronsInfo = neuralNetwork.GetNeuronInfo();
            int CurrentLayer = -1;
            int CurrentNeuronIndex = -1;

            foreach(var Neuron in NeuronsInfo)
            {
                if (CurrentLayer != Neuron.LayerIndex)
                {
                    CurrentLayer = Neuron.LayerIndex;
                    Console.WriteLine($"Layer: {CurrentLayer}");
                    CurrentNeuronIndex = -1;
                }

                CurrentNeuronIndex = Neuron.NeuronIndex;
                Console.WriteLine($"    Neuron Index: {CurrentNeuronIndex}");
                Console.WriteLine($"        Output Value: {Neuron.OutputValue:f4}");
                Console.WriteLine($"        Bias: {Neuron.Bias}");

                if (Neuron.Weights.Any())
                {
                    Console.WriteLine($"        Weights: ");
                    for(int i = 0; i < Neuron.Weights.Count(); i++)
                    {
                        Console.WriteLine($"        Synapse {i}: {Neuron.Weights.ElementAt(i):f4}");
                    }
                }
            }

        }
    }
}
