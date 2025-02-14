
using MTTCommon_objects;
using static MTTCommon_objects.Enumerations;

namespace MTT_Calculo
{
    public static class Estadistica
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public static double Media(List<double> datos)
        {
            return datos.Sum() / datos.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public static double DesviacionEstandar(List<double> datos)
        {
            double media = Media(datos);

            return DesviacionEstandar(datos, media);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="media"></param>
        /// <returns></returns>
        public static double DesviacionEstandar(List<double> datos, double media)
        {
            double sumaCuadrados = datos.Sum(dato => Math.Pow(dato - media, 2));

            double varianza = sumaCuadrados / datos.Count;

            return Math.Sqrt(varianza);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public static List<double> CampanaDeGauss(List<double> datos)
        {
            double media = Media(datos);

            double desviacionEstandar = DesviacionEstandar(datos, media);

            List<double> resultados = new List<double>();

            for (int i = 0; i < datos.Count; i++)
            {
                double x = datos[i];

                double coeficiente = 1 / (desviacionEstandar * Math.Sqrt(2 * Math.PI));

                double exponente = -((x - media) * (x - media)) / (2 * desviacionEstandar * desviacionEstandar);

                resultados.Add(coeficiente * Math.Exp(exponente));
            }

            return resultados;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candleList"></param>
        /// <param name="datoEspecifico"></param>
        /// <returns></returns>
        public static Dictionary<DateTime, double> CampanaDeGaussExtendida(List<CandleMT4Data> candleList, DatoCalulado datoEspecifico)
        {
            List<double> datos = new List<double>();

            foreach (CandleMT4Data candle in candleList)
            {
                switch (datoEspecifico)
                {
                    case DatoCalulado.Open:
                        datos.Add(candle.Open);
                        break;
                    case DatoCalulado.Close:
                        datos.Add(candle.Close);
                        break;
                    case DatoCalulado.High:
                        datos.Add(candle.High);
                        break;
                    case DatoCalulado.Low:
                        datos.Add(candle.Low);
                        break;
                    case DatoCalulado.High_Low:
                        datos.Add(candle.High - candle.Low);
                        break;
                    case DatoCalulado.Open_Close:
                        datos.Add(candle.Open - candle.Close);
                        break;
                }
            }

            double media = Media(datos);

            double desviacionEstandar = DesviacionEstandar(datos, media);

            Dictionary<DateTime, double> resultados = new Dictionary<DateTime, double>();

            for (int i = 0; i < datos.Count; i++)
            {
                double x = datos[i];

                double coeficiente = 1 / (desviacionEstandar * Math.Sqrt(2 * Math.PI));

                double exponente = -((x - media) * (x - media)) / (2 * desviacionEstandar * desviacionEstandar);

                resultados.Add(candleList[i].Date, (coeficiente * Math.Exp(exponente)));
            }

            return resultados;
        }

    }
}
