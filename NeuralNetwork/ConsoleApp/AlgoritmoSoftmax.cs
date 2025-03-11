namespace ConsoleApp
{
    public class AlgoritmoSoftmax
    {
        /// <summary>
        /// Sobrecarga que acepta un array de doubles
        /// </summary>
        /// <param name="values">Array de valores numéricos</param>
        /// <returns>Array con 1 en la posición del valor máximo y 0 en las demás</returns>
        public static int[] ApplySoftmax(double[] values)
        {
            List<int> result = SoftmaxThenSelect(values.ToList());

            return result.ToArray();
        }

        /// <summary>
        /// Versión que aplica primero Softmax y luego selecciona el máximo
        /// (en la práctica, equivale a seleccionar directamente el máximo)
        /// </summary>
        /// <param name="values">Lista de valores numéricos</param>
        /// <returns>Lista con 1 en la posición del valor máximo y 0 en las demás</returns>
        private static List<int> SoftmaxThenSelect(List<double> values)
        {
            if (values == null || values.Count == 0)
                throw new ArgumentException("La lista de valores no puede estar vacía o ser null");

            // Para evitar desbordamiento numérico, restamos el valor máximo
            double max = values.Max();

            // Calculamos el exponencial de cada valor (ajustado por el máximo)
            List<double> expValues = values.Select(x => Math.Exp(x - max)).ToList();

            // Calculamos la suma de todos los exponenciales
            double sumExp = expValues.Sum();

            // Dividimos cada valor exponencial por la suma para obtener el resultado de Softmax
            List<double> softmaxValues = expValues.Select(x => x / sumExp).ToList();

            // Después de Softmax, el valor máximo sigue en la misma posición
            return SelectMax(softmaxValues);
        }

        /// <summary>
        /// Convierte una lista de valores en una representación one-hot donde solo el valor máximo es 1
        /// y los demás son 0
        /// </summary>
        /// <param name="values">Lista de valores numéricos</param>
        /// <returns>Lista con 1 en la posición del valor máximo y 0 en las demás</returns>
        private static List<int> SelectMax(List<double> values)
        {
            if (values == null || values.Count == 0)
                throw new ArgumentException("La lista de valores no puede estar vacía o ser null");

            // Encontrar el índice del valor máximo
            int maxIndex = 0;
            double maxValue = values[0];

            for (int i = 1; i < values.Count; i++)
            {
                if (values[i] > maxValue)
                {
                    maxValue = values[i];
                    maxIndex = i;
                }
            }

            // Crear la lista de resultados con todos los valores a 0
            List<int> result = new List<int>(values.Count);
            for (int i = 0; i < values.Count; i++)
            {
                result.Add(0);
            }

            // Establecer a 1 solo el valor en la posición del máximo
            result[maxIndex] = 1;

            return result;
        }
    }
}
