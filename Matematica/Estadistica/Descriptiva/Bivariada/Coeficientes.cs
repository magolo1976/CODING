using Matematica.Estadistica.Descriptiva.Univariada;

namespace Matematica.Estadistica.Descriptiva.Bivariada
{
    public class Coeficientes
    {
        /// <summary>
        /// Evalúa el tipo de relación basada en la covarianza.
        /// Útil para interpretar fácilmente el resultado de la covarianza en términos de la dirección de la relación.
        /// </summary>
        /// <param name="covarianza">Valor de la covarianza calculada</param>
        /// <returns>Cadena que describe el tipo de relación</returns>
        public static string EvaluarRelacionCovarianza(double covarianza)
        {
            if (covarianza > 0)
                return "Relación Directa: ambas variables tienden a aumentar o disminuir juntas.";
            else if (covarianza < 0)
                return "Relación Negativa: cuando una variable aumenta, la otra tiende a disminuir.";
            else
                return "No existe relación lineal clara entre las variables.";
        }

        /// <summary>
        /// Calcula el coeficiente de correlación de Pearson.
        /// Útil para medir la fuerza y dirección de la relación lineal entre dos variables.
        /// El valor oscila entre -1 y 1, donde:
        /// 1 indica una correlación positiva perfecta
        /// -1 indica una correlación negativa perfecta
        /// 0 indica ausencia de correlación lineal
        /// </summary>
        /// <param name="x">Valores de la variable independiente</param>
        /// <param name="y">Valores de la variable dependiente</param>
        /// <returns>Coeficiente de correlación de Pearson</returns>
        public static double CoeficienteCorrelacionPearson(List<double> x, List<double> y)
        {
            if (x == null || y == null || x.Count == 0 || y.Count == 0)
                throw new ArgumentException("Los arreglos no pueden estar vacíos.");

            if (x.Count != y.Count)
                throw new ArgumentException("Los arreglos deben tener la misma longitud.");

            double covarianza = ResumenesNumericos.Covarianza(x, y);
            double desviacionX = ResumenesNumericos.DesviacionEstandar(x, ResumenesNumericos.Media(x));
            double desviacionY = ResumenesNumericos.DesviacionEstandar(y, ResumenesNumericos.Media(y));

            // Evitar división por cero
            if (desviacionX == 0 || desviacionY == 0)
                return 0; // No hay variabilidad en al menos una de las variables

            return covarianza / (desviacionX * desviacionY);
        }

        /// <summary>
        /// Evalúa la fuerza y tipo de correlación basada en el coeficiente de Pearson.
        /// Útil para interpretar la magnitud y dirección de la correlación en términos descriptivos.
        /// </summary>
        /// <param name="coeficiente">Valor del coeficiente de correlación</param>
        /// <returns>Descripción de la fuerza y tipo de correlación</returns>
        public static string EvaluarCorrelacion(double coeficiente)
        {
            double valorAbsoluto = Math.Abs(coeficiente);

            string fuerza;
            string direccion;

            // Evaluar fuerza
            if (valorAbsoluto >= 0.7)
                fuerza = "fuerte";
            else if (valorAbsoluto >= 0.3)
                fuerza = "moderada";
            else
                fuerza = "débil";

            // Evaluar dirección
            if (coeficiente > 0)
                direccion = "directa";
            else if (coeficiente < 0)
                direccion = "inversa";
            else
                return "Correlación nula: no existe relación lineal entre las variables.";

            return $"Correlación {fuerza} y {direccion} (r = {coeficiente:F4}): " +
                   (direccion == "directa"
                        ? "ambas variables tienden a aumentar o disminuir juntas."
                        : "cuando una variable aumenta, la otra tiende a disminuir.");
        }

        /// <summary>
        /// Calcula el coeficiente de determinación (R²).
        /// Útil para determinar qué proporción de la variación en la variable dependiente
        /// puede ser explicada por la variable independiente.
        /// Un valor de 1 indica que el modelo explica toda la variabilidad,
        /// mientras que un valor de 0 indica que no explica nada de la variabilidad.
        /// </summary>
        /// <param name="x">Valores de la variable independiente</param>
        /// <param name="y">Valores de la variable dependiente</param>
        /// <returns>Coeficiente de determinación (R²)</returns>
        public static double CoeficienteDeterminacion(List<double> x, List<double> y)
        {
            double r = CoeficienteCorrelacionPearson(x, y);
            return r * r;
        }

        /// <summary>
        /// Calcula los parámetros de la recta de regresión lineal (y = mx + b).
        /// Útil para predecir valores de la variable dependiente basados en la independiente.
        /// </summary>
        /// <param name="x">Valores de la variable independiente</param>
        /// <param name="y">Valores de la variable dependiente</param>
        /// <returns>Tupla con pendiente (m) e intersección (b) de la recta</returns>
        public (double pendiente, double interseccion) RectaRegresion(List<double> x, List<double> y)
        {
            if (x == null || y == null || x.Count == 0 || y.Count == 0)
                throw new ArgumentException("Los arreglos no pueden estar vacíos.");

            if (x.Count != y.Count)
                throw new ArgumentException("Los arreglos deben tener la misma longitud.");

            double mediaX = ResumenesNumericos.Media(x);
            double mediaY = ResumenesNumericos.Media(y);

            double numerador = 0;
            double denominador = 0;

            for (int i = 0; i < x.Count; i++)
            {
                numerador += (x[i] - mediaX) * (y[i] - mediaY);
                denominador += Math.Pow(x[i] - mediaX, 2);
            }

            // Evitar división por cero
            if (denominador == 0)
                throw new InvalidOperationException("No se puede calcular la pendiente cuando no hay variabilidad en x.");

            double pendiente = numerador / denominador;
            double interseccion = mediaY - pendiente * mediaX;

            return (pendiente, interseccion);
        }

        /// <summary>
        /// Predice el valor de y para un valor dado de x usando la recta de regresión.
        /// Útil para estimar valores futuros o desconocidos basados en el modelo lineal.
        /// </summary>
        /// <param name="x">Valor de la variable independiente</param>
        /// <param name="pendiente">Pendiente de la recta de regresión</param>
        /// <param name="interseccion">Intersección de la recta de regresión</param>
        /// <returns>Valor predicho para y</returns>
        public double PredecirValorUsandoRectaRegresion(double x, double pendiente, double interseccion)
        {
            return pendiente * x + interseccion;
        }
    }
}
