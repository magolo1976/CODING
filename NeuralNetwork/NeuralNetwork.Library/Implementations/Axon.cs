using NeuralNetwork.Abstractions;

namespace NeuralNetwork.Library.Implementations
{
    internal class Axon : IAxon
    {
        public List<ISynapse> Terminals { get; } = [];

        public void AddTerminal(ISynapse terminal)
        {
            Terminals.Add(terminal);
        }

        public void SendOutputValueToTerminals(double value)
        {
            foreach(ISynapse terminal in Terminals)
            {
                terminal.ReceiveInputValue(value);
            }
        }
    }
}
