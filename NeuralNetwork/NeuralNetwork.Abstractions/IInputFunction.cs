
namespace NeuralNetwork.Abstractions
{
    public interface IInputFunction
    {
        double CalculateInput(IEnumerable<ISynapse> dentrites, double bias);
    }
}
