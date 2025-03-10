namespace Matematica.Estadistica.DescriptivaUnivariada
{
    public class ResumenesNumericos
    {
        /// <summary>
        /// Calcula la media aritmética de una colección de números
        /// </summary>
        /// <param name="datos">Colección de datos numéricos</param>
        /// <returns>Media aritmética</returns>
        public static double Media(IEnumerable<double> datos)
        {
            if (datos == null)
                throw new ArgumentNullException(nameof(datos), "La colección de datos no puede ser nula");

            List<double> listaValores = datos.ToList();

            if (listaValores.Count == 0)
                throw new ArgumentException("La colección de datos no puede estar vacía", nameof(datos));

            double suma = 0;
            int contador = 0;

            foreach (double valor in listaValores)
            {
                suma += valor;
                contador++;
            }

            return suma / contador;
        }

        /// <summary>
        /// Calcula la mediana de una colección de números
        /// </summary>
        /// <param name="datos">Colección de datos numéricos</param>
        /// <returns>Mediana</returns>
        public static double Mediana(IEnumerable<double> datos)
        {
            if (datos == null)
                throw new ArgumentNullException(nameof(datos), "La colección de datos no puede ser nula");

            List<double> listaValores = datos.ToList();

            if (listaValores.Count == 0)
                throw new ArgumentException("La colección de datos no puede estar vacía", nameof(datos));

            // Ordenamos los valores
            listaValores.Sort();

            int n = listaValores.Count;

            // Si la cantidad de elementos es par, promediamos los dos centrales
            if (n % 2 == 0)
            {
                int indiceInferior = n / 2 - 1;
                int indiceSuperior = n / 2;

                return (listaValores[indiceInferior] + listaValores[indiceSuperior]) / 2.0;
            }
            // Si la cantidad es impar, tomamos el elemento central
            else
            {
                int indiceCentral = n / 2;

                return listaValores[indiceCentral];
            }
        }

        /// <summary>
        /// Calcula la moda (valor más frecuente) de una colección de números
        /// </summary>
        /// <param name="datos">Colección de datos numéricos</param>
        /// <returns>Moda o modas (si hay varias)</returns>
        public static List<double> Moda(IEnumerable<double> datos)
        {
            if (datos == null)
                throw new ArgumentNullException(nameof(datos), "La colección de datos no puede ser nula");

            List<double> listaValores = datos.ToList();

            if (listaValores.Count == 0)
                throw new ArgumentException("La colección de datos no puede estar vacía", nameof(datos));

            // Calculamos la frecuencia de cada valor
            Dictionary<double, int> frecuencias = Frecuencias<double>.FrecuenciaAbsoluta(listaValores);

            // Encontramos la frecuencia máxima
            int frecuenciaMaxima = 0;

            foreach (var par in frecuencias)
            {
                if (par.Value > frecuenciaMaxima)
                {
                    frecuenciaMaxima = par.Value;
                }
            }

            // Si ningún valor se repite (todos tienen frecuencia 1), no hay moda
            if (frecuenciaMaxima == 1)
            {
                return new List<double>();
            }

            // Recopilamos todos los valores con la frecuencia máxima (podría haber varios)
            List<double> modas = new List<double>();

            foreach (var par in frecuencias)
            {
                if (par.Value == frecuenciaMaxima)
                {
                    modas.Add(par.Key);
                }
            }

            return modas;
        }

        /// <summary>
        /// Calcula la varianza de una colección de números
        /// </summary>
        /// <param name="datos">Colección de datos numéricos</param>
        /// <param name="esPoblacion">Indica si los datos representan la población completa (true) o una muestra (false)</param>
        /// <returns>Varianza</returns>
        public static double Varianza(IEnumerable<double> datos, bool esPoblacion = false)
        {
            if (datos == null)
                throw new ArgumentNullException(nameof(datos), "La colección de datos no puede ser nula");

            List<double> listaValores = datos.ToList();

            if (listaValores.Count == 0)
                throw new ArgumentException("La colección de datos no puede estar vacía", nameof(datos));

            // Para una muestra, necesitamos al menos 2 valores para calcular la varianza
            if (!esPoblacion && listaValores.Count < 2)
                throw new ArgumentException("Para calcular la varianza de una muestra, se necesitan al menos dos valores", nameof(datos));

            // Calculamos la media
            double media = Media(listaValores);

            // Calculamos la suma de los cuadrados de las diferencias
            double sumaCuadradosDiferencias = 0;

            foreach (double valor in listaValores)
            {
                double diferencia = valor - media;
                sumaCuadradosDiferencias += diferencia * diferencia;
            }

            // Dividimos por n (población) o n-1 (muestra)
            int divisor = esPoblacion ? listaValores.Count : listaValores.Count - 1;

            return sumaCuadradosDiferencias / divisor;
        }

        // Función para calcular la covarianza entre dos listas
        public static double Covarianza(List<double> datosX, List<double> datosY)
        {
            if (datosX == null || datosY == null || datosX.Count == 0 || datosY.Count == 0)
                throw new ArgumentException("Las listas no pueden estar vacías o ser nulas.");

            if (datosX.Count != datosY.Count)
                throw new ArgumentException("Las listas deben tener la misma longitud.");

            int n = datosX.Count;
            double mediaX = Media(datosX);
            double mediaY = Media(datosY);

            double sumaProducto = 0;
            for (int i = 0; i < n; i++)
            {
                sumaProducto += (datosX[i] - mediaX) * (datosY[i] - mediaY);
            }

            // Covarianza muestral (n - 1)
            return sumaProducto / (n - 1);
        }

        /// <summary>
        /// Calcula la desviación típica (estándar) de una colección de números
        /// </summary>
        /// <param name="datos">Colección de datos numéricos</param>
        /// <param name="esPoblacion">Indica si los datos representan la población completa (true) o una muestra (false)</param>
        /// <returns>Desviación típica</returns>
        public static double DesviacionTipica(IEnumerable<double> datos, bool esPoblacion = false)
        {
            // La desviación típica es la raíz cuadrada de la varianza
            double varianza = Varianza(datos, esPoblacion);

            return Math.Sqrt(varianza);
        }

        // Función auxiliar para calcular la desviación estándar (poblacional)
        public static double DesviacionEstandar(List<double> datos, double media)
        {
            double sumaCuadrados = 0;

            foreach (double valor in datos)
                sumaCuadrados += Math.Pow(valor - media, 2);

            return Math.Sqrt(sumaCuadrados / datos.Count);
        }
    }
}
