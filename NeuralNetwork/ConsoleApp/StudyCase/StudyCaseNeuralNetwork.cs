using NeuralNetwork.Abstractions;
using NeuralNetwork.Library.ActivationFunctions;
using NeuralNetwork.Library.Implementations;
using NeuralNetwork.Library.Initializers;
using NeuralNetwork.Library.Trainers;

namespace ConsoleApp.StudyCase
{
    public class StudyCaseNeuralNetwork : NeuralNetworkBase, INeuralNetwork
    {
        public StudyCaseNeuralNetwork()
        {
            HiddenLayerActivationFunction = new SigmoidActivationFunction();
            OutputLayerActivationFunction = new SigmoidActivationFunction();

            CreateNeuralNetwork(2, [8], 1);
        }

        protected override void Initialize()
        {
            // Metodo que calcula la inicialización de los pesos
            XavierInitializer.InitializeUniform(this);
        }

        public override double[] Predict(double[] inputs)
        {
            int InputIndex = 0;
            foreach(var Dentrite in InputDentrites)
            {
                Dentrite.ReceiveInputValue(inputs[InputIndex++]);
            }

            var Result = OutputLayer.Select(n => n.OutputValue).ToArray();

            return Result;
        }

        public override void Train(
            double[][] trainingData, 
            double[][] targets, 
            int epochs, 
            double learningRate)
        {
            for(int Epoch = 0; Epoch < epochs; Epoch++)
            {
                double TotalLoss = 0.0;

                for (int i = 0; i < trainingData.Length; i++)
                {
                    TotalLoss += BackPropagationTrainer.ApplyBackPropagation(
                        this, trainingData[i], targets[i], learningRate,
                        Predict
                        );
                }

                double Mse = TotalLoss / trainingData.Length;
                Console.WriteLine($"Epoch {Epoch + 1}/{epochs} - Loss (MSE): {Mse:F6}");
            }
        }
    }
}
