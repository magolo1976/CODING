
namespace NeuralNetwork.Abstractions
{
    public interface IAxon
    {
        List<ISynapse> Terminals { get; }
        void AddTerminal(ISynapse terminal);
        void SendOutputValueToTerminals(double value);

    }
}
