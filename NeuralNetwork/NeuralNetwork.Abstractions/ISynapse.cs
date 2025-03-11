
namespace NeuralNetwork.Abstractions
{
    public interface ISynapse
    {
        void ReceiveInputValue(double value);
        void SetWeightValue(double value);
        double Value { get; }
        double Weight { get; }
        event Action<ISynapse> OnInputValueReceived;
    }
}
