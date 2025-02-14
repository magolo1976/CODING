namespace MTT_Calculo
{
    public class DistribucionNormal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueList">Lista de valores a calcular</param>
        /// <param name="x">Punto de la curva</param>
        /// <param name="alfa">Dato Alfa con el que medir si es una distribución. Por defecto el estandar es 0.05</param>
        /// <returns></returns>
        public static bool EsDistribucionNormal(List<double> valueList, double x, double alfa = 0.05)
        {
            double mean = valueList.Average();

            var squared = valueList.Sum(d => Math.Pow(d - mean, 2));

            double std = Math.Sqrt(squared / valueList.Count());

            double epart = Math.Pow(x - mean, 2) / (2 * Math.Pow(std, 2));

            double density = (1 / (std * 2.50662827463)) * (Math.Exp(0.0 - epart));

            return (density < alfa) ? true : false;
        }
    }
}
