namespace MTT_Calculo
{
    public static class Kolmogorov_Smirnov
    {
        public static string KolmogorovSmirnov(List<double> data, List<double> distribution)
        {
            // Supongamos que tienes una colección de puntos almacenados en una lista
            // {data}

            // Supongamos que tienes otra distribución de puntos para comparar, por ejemplo, una distribución normal
            // Puedes definirla manualmente con algunos datos de ejemplo
            // {distribution}

            // Ordenar las listas
            data.Sort();
            distribution.Sort();

            // Calcular la diferencia máxima entre las dos distribuciones
            double maxDiff = 0.0;
            for (int i = 0; i < data.Count; i++)
            {
                double diff = Math.Abs(data[i] - distribution[i]);
                if (diff > maxDiff)
                    maxDiff = diff;
            }

            // Calcular el valor crítico de la prueba de Kolmogorov-Smirnov
            int n = data.Count;
            double ksCriticalValue = 1.36 / Math.Sqrt(n);

            // Imprimir el resultado
            string returnValue = $"Máxima diferencia observada: {maxDiff}\n";
            returnValue += $"Valor crítico: {ksCriticalValue}\n";
            returnValue += $"¿La hipótesis nula es rechazada? {maxDiff > ksCriticalValue}\n";

            // Dependiendo del valor crítico y del nivel de significancia elegido, puedes determinar si rechazas o no la hipótesis nula

            return returnValue;
        }
    }
}
