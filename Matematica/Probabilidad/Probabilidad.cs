namespace Matematica.Probabilidad
{
    public class Probabilidades
    {
        /// <summary>
        /// Calcula la probabilidad simple de un evento.
        /// Útil para determinar la frecuencia relativa de ocurrencia de un evento
        /// respecto al total de posibles resultados.
        /// </summary>
        /// <param name="casosFavorables">Número de casos favorables</param>
        /// <param name="casosTotales">Número total de casos posibles</param>
        /// <returns>Probabilidad del evento</returns>
        public static double ProbabilidadSimple(int casosFavorables, int casosTotales)
        {
            if (casosTotales <= 0)
                throw new ArgumentException("El número de casos totales debe ser mayor que cero.");

            if (casosFavorables < 0 || casosFavorables > casosTotales)
                throw new ArgumentException("El número de casos favorables debe estar entre 0 y el total de casos.");

            return (double)casosFavorables / casosTotales;
        }

        /// <summary>
        /// Calcula la probabilidad simple basada en frecuencias observadas.
        /// Útil cuando se tienen datos empíricos y se quiere estimar la probabilidad.
        /// </summary>
        /// <param name="ocurrenciasEvento">Número de veces que ocurrió el evento</param>
        /// <param name="totalObservaciones">Número total de observaciones</param>
        /// <returns>Probabilidad estimada del evento</returns>
        public static double ProbabilidadEmpírica(int ocurrenciasEvento, int totalObservaciones)
        {
            if (totalObservaciones <= 0)
                throw new ArgumentException("El número total de observaciones debe ser mayor que cero.");

            if (ocurrenciasEvento < 0 || ocurrenciasEvento > totalObservaciones)
                throw new ArgumentException("El número de ocurrencias debe estar entre 0 y el total de observaciones.");

            return (double)ocurrenciasEvento / totalObservaciones;
        }

        /// <summary>
        /// Calcula la probabilidad de la unión de dos eventos.
        /// Útil para determinar la probabilidad de que ocurra al menos uno de dos eventos.
        /// </summary>
        /// <param name="probA">Probabilidad del evento A</param>
        /// <param name="probB">Probabilidad del evento B</param>
        /// <param name="probInterseccion">Probabilidad de la intersección A ∩ B</param>
        /// <returns>Probabilidad de la unión A ∪ B</returns>
        public static double ProbabilidadUnion(double probA, double probB, double probInterseccion)
        {
            ValidarProbabilidad(probA);
            ValidarProbabilidad(probB);
            ValidarProbabilidad(probInterseccion);

            if (probInterseccion > Math.Min(probA, probB))
                throw new ArgumentException("La probabilidad de intersección no puede ser mayor que el mínimo de las probabilidades individuales.");

            // P(A ∪ B) = P(A) + P(B) - P(A ∩ B)
            return probA + probB - probInterseccion;
        }

        /// <summary>
        /// Calcula la probabilidad de la unión de dos eventos mutuamente excluyentes.
        /// Útil cuando se sabe que dos eventos no pueden ocurrir simultáneamente.
        /// </summary>
        /// <param name="probA">Probabilidad del evento A</param>
        /// <param name="probB">Probabilidad del evento B</param>
        /// <returns>Probabilidad de la unión A ∪ B para eventos mutuamente excluyentes</returns>
        public static double ProbabilidadUnionExcluyente(double probA, double probB)
        {
            ValidarProbabilidad(probA);
            ValidarProbabilidad(probB);

            if (probA + probB > 1)
                throw new ArgumentException("La suma de probabilidades de eventos mutuamente excluyentes no puede superar 1.");

            // P(A ∪ B) = P(A) + P(B) cuando A y B son mutuamente excluyentes
            return probA + probB;
        }

        /// <summary>
        /// Calcula la probabilidad condicionada de A dado B.
        /// Útil para determinar cómo la ocurrencia de un evento afecta la probabilidad de otro.
        /// </summary>
        /// <param name="probInterseccion">Probabilidad de la intersección A ∩ B</param>
        /// <param name="probB">Probabilidad del evento condicionante B</param>
        /// <returns>Probabilidad condicionada P(A|B)</returns>
        public static double ProbabilidadCondicionada(double probInterseccion, double probB)
        {
            ValidarProbabilidad(probInterseccion);
            ValidarProbabilidad(probB);

            if (probB == 0)
                throw new ArgumentException("La probabilidad del evento condicionante no puede ser cero.");

            if (probInterseccion > probB)
                throw new ArgumentException("La probabilidad de intersección no puede ser mayor que la probabilidad del evento condicionante.");

            // P(A|B) = P(A ∩ B) / P(B)
            return probInterseccion / probB;
        }

        /// <summary>
        /// Calcula la probabilidad condicionada mediante la definición alternativa.
        /// Útil cuando se conocen directamente las frecuencias de ocurrencias conjuntas.
        /// </summary>
        /// <param name="ocurrenciasConjuntas">Número de ocurrencias conjuntas de A y B</param>
        /// <param name="ocurrenciasB">Número de ocurrencias del evento B</param>
        /// <returns>Probabilidad condicionada P(A|B)</returns>
        public static double ProbabilidadCondicionadaFrecuencias(int ocurrenciasConjuntas, int ocurrenciasB)
        {
            if (ocurrenciasB <= 0)
                throw new ArgumentException("El número de ocurrencias del evento condicionante debe ser mayor que cero.");

            if (ocurrenciasConjuntas < 0 || ocurrenciasConjuntas > ocurrenciasB)
                throw new ArgumentException("El número de ocurrencias conjuntas debe estar entre 0 y el número de ocurrencias del evento condicionante.");

            return (double)ocurrenciasConjuntas / ocurrenciasB;
        }

        /// <summary>
        /// Verifica si dos eventos son independientes.
        /// Útil para determinar si la ocurrencia de un evento no afecta la probabilidad del otro.
        /// </summary>
        /// <param name="probA">Probabilidad del evento A</param>
        /// <param name="probB">Probabilidad del evento B</param>
        /// <param name="probInterseccion">Probabilidad de la intersección A ∩ B</param>
        /// <param name="tolerancia">Tolerancia para comparaciones de punto flotante</param>
        /// <returns>True si los eventos son independientes, False en caso contrario</returns>
        public static bool SonEventosIndependientes(double probA, double probB, double probInterseccion, double tolerancia = 1e-10)
        {
            ValidarProbabilidad(probA);
            ValidarProbabilidad(probB);
            ValidarProbabilidad(probInterseccion);

            // A y B son independientes si P(A ∩ B) = P(A) * P(B)
            return Math.Abs(probInterseccion - (probA * probB)) < tolerancia;
        }

        /// <summary>
        /// Aplica el teorema de Bayes para calcular la probabilidad condicionada inversa.
        /// Útil para actualizar probabilidades a partir de nueva evidencia o
        /// para calcular la probabilidad de causas dadas sus consecuencias.
        /// </summary>
        /// <param name="probB_DadoA">Probabilidad de B dado A, P(B|A)</param>
        /// <param name="probA">Probabilidad a priori de A, P(A)</param>
        /// <param name="probB">Probabilidad marginal de B, P(B)</param>
        /// <returns>Probabilidad de A dado B, P(A|B)</returns>
        public static double TeoremaBayes(double probB_DadoA, double probA, double probB)
        {
            ValidarProbabilidad(probB_DadoA);
            ValidarProbabilidad(probA);
            ValidarProbabilidad(probB);

            if (probB == 0)
                throw new ArgumentException("La probabilidad marginal no puede ser cero.");

            // P(A|B) = P(B|A) * P(A) / P(B)
            return (probB_DadoA * probA) / probB;
        }

        /// <summary>
        /// Aplica el teorema de Bayes con múltiples hipótesis.
        /// Útil para problemas de clasificación o diagnóstico donde hay
        /// varias causas posibles para un evento observado.
        /// </summary>
        /// <param name="probB_DadoA">Probabilidad de B dado A, P(B|A)</param>
        /// <param name="probA">Probabilidad a priori de A, P(A)</param>
        /// <param name="probB_DadoHipotesis">Diccionario con probabilidades P(B|Hi) para cada hipótesis Hi</param>
        /// <param name="probHipotesis">Diccionario con probabilidades a priori P(Hi) para cada hipótesis Hi</param>
        /// <returns>Probabilidad de A dado B, P(A|B)</returns>
        public static double TeoremaByesMultiplesHipotesis(
            double probB_DadoA,
            double probA,
            Dictionary<string, double> probB_DadoHipotesis,
            Dictionary<string, double> probHipotesis)
        {
            ValidarProbabilidad(probB_DadoA);
            ValidarProbabilidad(probA);

            if (probB_DadoHipotesis == null || probHipotesis == null)
                throw new ArgumentNullException("Los diccionarios de probabilidades no pueden ser nulos.");

            if (probB_DadoHipotesis.Count == 0 || probHipotesis.Count == 0)
                throw new ArgumentException("Los diccionarios de probabilidades no pueden estar vacíos.");

            if (probB_DadoHipotesis.Count != probHipotesis.Count)
                throw new ArgumentException("Los diccionarios deben tener el mismo número de elementos.");

            // Verificar que todas las probabilidades sean válidas
            foreach (var prob in probB_DadoHipotesis.Values.Concat(probHipotesis.Values))
            {
                ValidarProbabilidad(prob);
            }

            // Verificar que la suma de las probabilidades a priori sea 1
            double sumaProbHipotesis = probHipotesis.Values.Sum();
            if (Math.Abs(sumaProbHipotesis - 1.0) > 1e-10)
                throw new ArgumentException("La suma de las probabilidades a priori debe ser igual a 1.");

            // Calcular el denominador: P(B) = Σ P(B|Hi) * P(Hi)
            double probB = 0;
            foreach (var hipotesis in probHipotesis.Keys)
            {
                probB += probB_DadoHipotesis[hipotesis] * probHipotesis[hipotesis];
            }

            if (probB == 0)
                throw new ArgumentException("La probabilidad marginal calculada no puede ser cero.");

            // P(A|B) = P(B|A) * P(A) / P(B)
            return (probB_DadoA * probA) / probB;
        }

        /// <summary>
        /// Calcula la probabilidad total a partir de probabilidades condicionadas y a priori.
        /// Útil para calcular la probabilidad de un evento que puede ocurrir por múltiples causas.
        /// </summary>
        /// <param name="probB_DadoHipotesis">Diccionario con probabilidades P(B|Hi) para cada hipótesis Hi</param>
        /// <param name="probHipotesis">Diccionario con probabilidades a priori P(Hi) para cada hipótesis Hi</param>
        /// <returns>Probabilidad total de B, P(B)</returns>
        public static double TeoremaProbabilidadTotal(
            Dictionary<string, double> probB_DadoHipotesis,
            Dictionary<string, double> probHipotesis)
        {
            if (probB_DadoHipotesis == null || probHipotesis == null)
                throw new ArgumentNullException("Los diccionarios de probabilidades no pueden ser nulos.");

            if (probB_DadoHipotesis.Count == 0 || probHipotesis.Count == 0)
                throw new ArgumentException("Los diccionarios de probabilidades no pueden estar vacíos.");

            if (probB_DadoHipotesis.Count != probHipotesis.Count)
                throw new ArgumentException("Los diccionarios deben tener el mismo número de elementos.");

            // Verificar que todas las probabilidades sean válidas
            foreach (var prob in probB_DadoHipotesis.Values.Concat(probHipotesis.Values))
            {
                ValidarProbabilidad(prob);
            }

            // Verificar que la suma de las probabilidades a priori sea 1
            double sumaProbHipotesis = probHipotesis.Values.Sum();
            if (Math.Abs(sumaProbHipotesis - 1.0) > 1e-10)
                throw new ArgumentException("La suma de las probabilidades a priori debe ser igual a 1.");

            // P(B) = Σ P(B|Hi) * P(Hi)
            double probB = 0;
            foreach (var hipotesis in probHipotesis.Keys)
            {
                probB += probB_DadoHipotesis[hipotesis] * probHipotesis[hipotesis];
            }

            return probB;
        }

        /// <summary>
        /// Valida que un valor sea una probabilidad válida (entre 0 y 1).
        /// </summary>
        /// <param name="probabilidad">Valor a validar</param>
        private static void ValidarProbabilidad(double probabilidad)
        {
            if (probabilidad < 0 || probabilidad > 1)
                throw new ArgumentException("La probabilidad debe estar en el rango [0,1].");
        }
    }
}
