namespace NeuralNetwork.Library.Helpers
{
    public class FunctionHelper
    {
        public static double SigmoidDerivative(double x)
        {
            return x * (1 - x);
        }
    }
}
