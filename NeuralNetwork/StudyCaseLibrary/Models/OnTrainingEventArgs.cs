namespace StudyCaseLibrary.Models
{
    public class OnTrainingEventArgs(int totalEpochs, int currentEpoch, double mse): EventArgs
    {
        public int TotalEpochs => totalEpochs;
        public int CurrentEpoch => currentEpoch;

        // Porcentaje de Errores
        public double Mse => mse;
    }
}
