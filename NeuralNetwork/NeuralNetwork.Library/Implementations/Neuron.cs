using NeuralNetwork.Abstractions;

namespace NeuralNetwork.Library.Implementations
{
    internal class Neuron(
        IInputFunction inputFunction,
        IActivationFunction activationFunction) : INeuron
    {
        public IEnumerable<ISynapse> Dentrites => DentritesFields;
        public List<ISynapse> DentritesFields = [];

        public IAxon Axon { get; } = new Axon();

        public double Bias { get; private set; }

        public double OutputValue { get; private set; }

        public IInputFunction InputFunction => inputFunction;

        public IActivationFunction ActivationFunction => activationFunction;

        public void AddDentrite(ISynapse dentrite)
        {
            dentrite.OnInputValueReceived += Dentrite_OnInputValueReceived;
            DentritesFields.Add(dentrite);
        }

        public void AddTerminal(ISynapse terminal)
        {
            Axon.AddTerminal(terminal);
        }

        public void SetBiasValue(double value) => Bias = value;

        private void Dentrite_OnInputValueReceived(ISynapse dentrite)
        {
            double InputValue = InputFunction != default
                ? inputFunction.CalculateInput(DentritesFields, Bias)
                : dentrite.Value;

            OutputValue = activationFunction != default
                ? activationFunction.CalculateOutput(InputValue)
                : InputValue;

            Axon.SendOutputValueToTerminals(OutputValue);
        }
    }
}
