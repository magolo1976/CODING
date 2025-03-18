
using NeuralNetwork.Abstractions;
using NeuralNetwork.Library.Helpers;
using NeuralNetwork.Library.Implementations;

namespace NeuralNetwork.Library.Trainers
{
    public class BackPropagationTrainer
    {
        public static double ApplyBackPropagation(INeuralNetwork netwotrk, 
            double[] inputs, 
            double[] targets, 
            double learningRate)
        {
            double Loss = 0.0;

            for(int i=0; i < netwotrk.OutputLayer.Length; i++)
            {
                INeuron Neuron = netwotrk.OutputLayer[i];

                double Error = Neuron.OutputValue - targets[i];
                double Delta = Error * FunctionHelper.SigmoidDerivative(Neuron.OutputValue);
                Neuron.Delta = Delta;

                Neuron.SetBiasValue(Neuron.Bias - learningRate * Delta);

                Loss += Error * Error;

                for(int j = 0; j < netwotrk.HiddenLayers[^1].Length; j++)
                {
                    INeuron HiddenNeuron = netwotrk.HiddenLayers[^1][j];
                    double CurrentWeight = HiddenNeuron.Axon.Terminals[i].Weight;

                    double NewWeight =
                        CurrentWeight - (learningRate * Delta * HiddenNeuron.OutputValue);

                    HiddenNeuron.Axon.Terminals[i].SetWeightValue(NewWeight);
                }
            }

            INeuron[] NextLayer = netwotrk.OutputLayer;

            for(int LayerIndex = netwotrk.HiddenLayers.Length - 1; LayerIndex >= 0; LayerIndex--)
            {
                for(int NeuronIndex = 0; NeuronIndex < netwotrk.HiddenLayers[LayerIndex].Length; NeuronIndex++)
                {
                    INeuron CurrentNeuron =
                        netwotrk.HiddenLayers[LayerIndex][NeuronIndex];
                    double Error = 0.0;

                    for(int NextNeuronIndex = 0; NextNeuronIndex < NextLayer.Length; NextNeuronIndex++)
                    {
                        Error += CurrentNeuron.Axon.Terminals[NextNeuronIndex].Weight * NextLayer[NextNeuronIndex].Delta; 
                    }

                    double Delta = Error * FunctionHelper.SigmoidDerivative(CurrentNeuron.OutputValue);

                    CurrentNeuron.SetBiasValue(CurrentNeuron.Bias - learningRate * Delta);
                    CurrentNeuron.Delta = Delta;

                    INeuron[] PreviousLayers;
                    if (LayerIndex == 0)
                    {
                        PreviousLayers = netwotrk.InputLayer;
                    }
                    else
                    {
                        PreviousLayers = netwotrk.HiddenLayers[LayerIndex - 1];
                    }

                    for(int PrevNeuronIndex = 0; PrevNeuronIndex < PreviousLayers.Length; PrevNeuronIndex++)
                    {
                        INeuron PreviousNeuron = PreviousLayers[PrevNeuronIndex];
                        double CurrentWeight = PreviousNeuron.Axon.Terminals[NeuronIndex].Weight;
                        double NewWeight = CurrentWeight - learningRate * Delta * PreviousNeuron.OutputValue;
                        
                        PreviousNeuron.Axon.Terminals[NeuronIndex].SetWeightValue(NewWeight);
                    }
                }

                NextLayer = netwotrk.HiddenLayers[LayerIndex];
            }

            return Loss / netwotrk.OutputLayer.Length;
        }
    }
}
