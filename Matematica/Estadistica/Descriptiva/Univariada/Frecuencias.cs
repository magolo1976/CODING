namespace Matematica.Estadistica.Descriptiva.Univariada
{
    public class Frecuencias<T>
    {
        /// <summary>
        /// Calcula la frecuencia absoluta de cada elemento en la colección
        /// </summary>
        /// <param name="datos">Colección de datos a analizar</param>
        /// <returns>Diccionario con cada elemento y su frecuencia absoluta</returns>
        public static Dictionary<T, int> FrecuenciaAbsoluta(IEnumerable<T> datos)
        {
            if (datos == null)
                throw new ArgumentNullException(nameof(datos), "La colección de datos no puede ser nula");

            Dictionary<T, int> frecuencias = new Dictionary<T, int>();

            // Recorrer cada elemento de la colección
            foreach (T elemento in datos)
            {
                // Si el elemento ya existe en el diccionario, incrementar su contador
                if (frecuencias.ContainsKey(elemento))
                {
                    frecuencias[elemento]++;
                }
                // Si no existe, agregarlo con frecuencia 1
                else
                {
                    frecuencias[elemento] = 1;
                }
            }

            return frecuencias;
        }

        /// <summary>
        /// Calcula la frecuencia absoluta acumulada
        /// </summary>
        /// <param name="frecuenciaAbsoluta">Diccionario con la frecuencia absoluta</param>
        /// <returns>Diccionario con la frecuencia absoluta acumulada</returns>
        public static Dictionary<T, int> FrecuenciaAbsolutaAcumulada(Dictionary<T, int> frecuenciaAbsoluta)
        {
            if (frecuenciaAbsoluta == null)
                throw new ArgumentNullException(nameof(frecuenciaAbsoluta), "El diccionario de frecuencias absolutas no puede ser nulo");

            Dictionary<T, int> frecuenciaAcumulada = new Dictionary<T, int>();
            int acumulado = 0;

            // Ordenamos las claves para asegurar un orden consistente en el cálculo acumulado
            // (Importante: para tipos numéricos o con orden natural)
            var elementosOrdenados = frecuenciaAbsoluta.Keys.ToList();

            // Para tipos genéricos podríamos necesitar una función de comparación específica
            // Esta implementación asume un orden por defecto basado en ToString() si no hay orden natural
            try
            {
                if (typeof(IComparable).IsAssignableFrom(typeof(T)))
                {
                    elementosOrdenados.Sort();
                }
            }
            catch
            {
                // Si no se puede ordenar, usamos el orden original del diccionario
                elementosOrdenados = frecuenciaAbsoluta.Keys.ToList();
            }

            // Calculamos la frecuencia acumulada
            foreach (T elemento in elementosOrdenados)
            {
                acumulado += frecuenciaAbsoluta[elemento];
                frecuenciaAcumulada[elemento] = acumulado;
            }

            return frecuenciaAcumulada;
        }

        /// <summary>
        /// Calcula la frecuencia relativa (dividiendo cada frecuencia absoluta por el total de datos)
        /// </summary>
        /// <param name="frecuenciaAbsoluta">Diccionario con la frecuencia absoluta</param>
        /// <param name="totalDatos">Total de datos en la colección original</param>
        /// <returns>Diccionario con la frecuencia relativa</returns>
        public static Dictionary<T, double> FrecuenciaRelativa(Dictionary<T, int> frecuenciaAbsoluta, int? totalDatos = null)
        {
            if (frecuenciaAbsoluta == null)
                throw new ArgumentNullException(nameof(frecuenciaAbsoluta), "El diccionario de frecuencias absolutas no puede ser nulo");

            // Si no se proporciona el total, calcularlo sumando todas las frecuencias absolutas
            int total = totalDatos ?? frecuenciaAbsoluta.Values.Sum();

            if (total <= 0)
                throw new ArgumentException("El total de datos debe ser mayor que cero");

            Dictionary<T, double> frecuenciaRelativa = new Dictionary<T, double>();

            foreach (var par in frecuenciaAbsoluta)
            {
                // Dividimos la frecuencia absoluta entre el total para obtener la frecuencia relativa
                frecuenciaRelativa[par.Key] = (double)par.Value / total;
            }

            if (frecuenciaRelativa.Values.Sum() > 1)
                throw new ArgumentException("El total no puede sumar mas de 1");

            return frecuenciaRelativa;
        }

        /// <summary>
        /// Calcula la frecuencia relativa acumulada
        /// </summary>
        /// <param name="frecuenciaRelativa">Diccionario con la frecuencia relativa</param>
        /// <returns>Diccionario con la frecuencia relativa acumulada</returns>
        public static Dictionary<T, double> FrecuenciaRelativaAcumulada(Dictionary<T, double> frecuenciaRelativa)
        {
            if (frecuenciaRelativa == null)
                throw new ArgumentNullException(nameof(frecuenciaRelativa), "El diccionario de frecuencias relativas no puede ser nulo");

            Dictionary<T, double> frecuenciaRelativaAcumulada = new Dictionary<T, double>();
            double acumulado = 0.0;

            // Ordenamos las claves (similar a la frecuencia absoluta acumulada)
            var elementosOrdenados = frecuenciaRelativa.Keys.ToList();

            try
            {
                if (typeof(IComparable).IsAssignableFrom(typeof(T)))
                {
                    elementosOrdenados.Sort();
                }
            }
            catch
            {
                // Si no se puede ordenar, usamos el orden original del diccionario
                elementosOrdenados = frecuenciaRelativa.Keys.ToList();
            }

            // Calculamos la frecuencia relativa acumulada
            foreach (T elemento in elementosOrdenados)
            {
                acumulado += frecuenciaRelativa[elemento];
                frecuenciaRelativaAcumulada[elemento] = acumulado;
            }

            return frecuenciaRelativaAcumulada;
        }
    }
}
