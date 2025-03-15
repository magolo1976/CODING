
namespace NeuralNetwork.Abstractions
{
    public interface INeuron
    {
        IEnumerable<ISynapse> Dentrites { get; }
        IAxon Axon { get; }
        double Bias { get; }
        double Delta { get; set; }
        double OutputValue { get; }
        void SetBiasValue(double value);
        void AddDentrite(ISynapse dentrite);
        void AddTerminal(ISynapse terminal);
        IInputFunction InputFunction { get; }
        IActivationFunction ActivationFunction { get; }
    }
}
