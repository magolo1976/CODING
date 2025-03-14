namespace Matematica.Estadistica.Descriptiva.Univariada
{
    public class Asimetria
    {
        #region ASIMETRÍAS

        #region TEST
        /*
        Asimetria intervalos = new Asimetria();
        List<double> datos = new List<double> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        // Calcular y mostrar cuartiles
        List<double> cuartiles = intervalos.CalcularCuartiles(datos);
        Console.WriteLine("Cuartiles: " + string.Join(", ", cuartiles));

        // Calcular y mostrar deciles
        List<double> deciles = intervalos.CalcularDeciles(datos);
        Console.WriteLine("Deciles: " + string.Join(", ", deciles));

        // Calcular y mostrar algunos percentiles (solo primeros 5 para no saturar)
        List<double> percentiles = intervalos.CalcularPercentiles(datos);
        Console.WriteLine("Primeros 5 Percentiles: " + string.Join(", ", percentiles.GetRange(0, 5)));
        */
        #endregion

        // Función auxiliar para ordenar la lista y calcular una posición interpolada
        private static double CalcularPosicion(List<double> datos, double posicion)
        {
            int n = datos.Count;
            double indice = posicion * (n - 1);
            int indiceBajo = (int)Math.Floor(indice);
            int indiceAlto = (int)Math.Ceiling(indice);

            if (indiceBajo == indiceAlto)
                return datos[indiceBajo];

            double fraccion = indice - indiceBajo;

            return datos[indiceBajo] + fraccion * (datos[indiceAlto] - datos[indiceBajo]);
        }

        // Rango Intercuartílico (IQR = Q3 - Q1)
        public static double RangoIntercuartilico(List<double> datos)
        {
            if (datos == null || datos.Count == 0)
                throw new ArgumentException("La lista no puede estar vacía o ser nula.");

            List<double> cuartiles = Cuartiles(datos);
            double q1 = cuartiles[0]; // Primer cuartil
            double q3 = cuartiles[2]; // Tercer cuartil

            return q3 - q1;
        }

        // Función para calcular los cuartiles (Q1, Q2, Q3)
        // Función auxiliar para calcular cuartiles (reusada de antes)
        public static List<double> Cuartiles(List<double> datos)
        {
            List<double> ordenados = new List<double>(datos);
            ordenados.Sort();
            int n = ordenados.Count;
            List<double> cuartiles = new List<double>();

            cuartiles.Add(CalcularPosicion(ordenados, 0.25)); // Q1
            cuartiles.Add(CalcularPosicion(ordenados, 0.50)); // Q2
            cuartiles.Add(CalcularPosicion(ordenados, 0.75)); // Q3
            return cuartiles;
        }

        // Función para calcular los deciles (D1 a D9)
        public static List<double> Deciles(List<double> datos)
        {
            if (datos == null || datos.Count == 0)
                throw new ArgumentException("La lista no puede estar vacía o ser nula.");

            List<double> deciles = new List<double>();

            // D1 (10%), D2 (20%), ..., D9 (90%)
            for (int i = 1; i <= 9; i++)
            {
                deciles.Add(CalcularPosicion(datos, i / 10.0));
            }

            return deciles;
        }

        // Función para calcular los percentiles (P1 a P99)
        public static List<double> Percentiles(List<double> datos)
        {
            if (datos == null || datos.Count == 0)
                throw new ArgumentException("La lista no puede estar vacía o ser nula.");

            List<double> percentiles = new List<double>();

            // P1 (1%), P2 (2%), ..., P99 (99%)
            for (int i = 1; i <= 99; i++)
            {
                percentiles.Add(CalcularPosicion(datos, i / 100.0));
            }

            return percentiles;
        }

        #endregion

        #region COEFICIENTES

        #region TEST
        /*
        Intervalos intervalos = new Intervalos();
        List<double> datos = new List<double> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        Console.WriteLine("Coeficiente de Variación: " + intervalos.CalcularCoeficienteVariacion(datos));
        Console.WriteLine("Coeficiente de Pearson: " + intervalos.CalcularCoeficientePearson(datos));
        Console.WriteLine("Medida de Yule-Bowley: " + intervalos.CalcularMedidaYuleBowley(datos));
        Console.WriteLine("Medida de Fisher: " + intervalos.CalcularMedidaFisher(datos));
         */
        #endregion

        // 1. Coeficiente de variación (CV = desviación estándar / media)
        public static double CoeficienteVariacion(List<double> datos)
        {
            if (datos == null || datos.Count == 0)
                throw new ArgumentException("La lista no puede estar vacía o ser nula.");

            double media = ResumenesNumericos.Media(datos);
            if (media == 0)
                throw new ArithmeticException("La media no puede ser cero para calcular el coeficiente de variación.");

            double desviacion = ResumenesNumericos.DesviacionEstandar(datos, media);

            return desviacion / media; // Generalmente se expresa como fracción, no porcentaje
        }

        // 2. Coeficiente de asimetría de Pearson (Sk = 3 * (media - mediana) / desviación estándar)
        public static double CoeficientePearson(List<double> datos)
        {
            if (datos == null || datos.Count == 0)
                throw new ArgumentException("La lista no puede estar vacía o ser nula.");

            double media = ResumenesNumericos.Media(datos);
            double mediana = CalcularPosicion(datos, 0.50); // Q2 o mediana
            double desviacion = ResumenesNumericos.DesviacionEstandar(datos, media);

            if (desviacion == 0)
                throw new ArithmeticException("La desviación estándar no puede ser cero.");

            return 3 * (media - mediana) / desviacion;
        }

        // 3. Medida de Yule-Bowley (asimetría basada en cuartiles: (Q3 + Q1 - 2*Q2) / (Q3 - Q1))
        public static double MedidaYuleBowley(List<double> datos)
        {
            if (datos == null || datos.Count == 0)
                throw new ArgumentException("La lista no puede estar vacía o ser nula.");

            List<double> cuartiles = Cuartiles(datos);
            double q1 = cuartiles[0];
            double q2 = cuartiles[1];
            double q3 = cuartiles[2];

            double denominador = q3 - q1;
            if (denominador == 0)
                throw new ArithmeticException("Q3 - Q1 no puede ser cero.");

            return (q3 + q1 - 2 * q2) / denominador;
        }

        // 4. Medida de asimetría de Fisher (momento de orden 3 estandarizado)
        public static double MedidaFisher(List<double> datos)
        {
            if (datos == null || datos.Count == 0)
                throw new ArgumentException("La lista no puede estar vacía o ser nula.");

            double media = ResumenesNumericos.Media(datos);
            double desviacion = ResumenesNumericos.DesviacionEstandar(datos, media);
            if (desviacion == 0)
                throw new ArithmeticException("La desviación estándar no puede ser cero.");

            double sumaCubos = 0;

            foreach (double valor in datos)
                sumaCubos += Math.Pow(valor - media, 3);

            double momento3 = sumaCubos / datos.Count;

            return momento3 / Math.Pow(desviacion, 3);
        }

        #endregion

        #region BOXPLOT

        // Datos para un Boxplot (mínimo, Q1, Q2, Q3, máximo, valores atípicos)
        public static Dictionary<string, double> Boxplot(List<double> datos)
        {
            if (datos == null || datos.Count == 0)
                throw new ArgumentException("La lista no puede estar vacía o ser nula.");

            List<double> ordenados = new List<double>(datos);
            ordenados.Sort();

            List<double> cuartiles = Cuartiles(datos);
            double q1 = cuartiles[0]; // Primer cuartil
            double q2 = cuartiles[1]; // Mediana
            double q3 = cuartiles[2]; // Tercer cuartil
            double iqr = q3 - q1;     // Rango intercuartílico

            // Límites para detectar valores atípicos
            double limiteInferior = q1 - 1.5 * iqr;
            double limiteSuperior = q3 + 1.5 * iqr;

            // Mínimo y máximo dentro de los límites (sin atípicos)
            double minimo = ordenados[0];
            double maximo = ordenados[ordenados.Count - 1];
            foreach (double valor in ordenados)
            {
                if (valor >= limiteInferior)
                {
                    minimo = valor;
                    break;
                }
            }
            for (int i = ordenados.Count - 1; i >= 0; i--)
            {
                if (ordenados[i] <= limiteSuperior)
                {
                    maximo = ordenados[i];
                    break;
                }
            }

            // Devolver los datos en un diccionario
            Dictionary<string, double> datosBoxplot = new Dictionary<string, double>
            {
                { "Minimo", minimo },
                { "Q1", q1 },
                { "Q2", q2 },
                { "Q3", q3 },
                { "Maximo", maximo }
            };

            return datosBoxplot;
        }

        #endregion
    }
}
