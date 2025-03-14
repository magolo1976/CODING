using Matematica.Estadistica.Descriptiva.Bivariada;
using Matematica.Estadistica.Descriptiva.Univariada;
using Matematica.Probabilidad;

namespace Matematica
{
    class INDEX
    {
#region ESTADISTICA
    #region DESCRIPTIVA
        #region UNIVARIADA
            #region ASIMETRIA
            Asimetria asimetría = new Asimetria();
            //		- CalcularPosicion
            //		+ RangoIntercuartilico
            //		+ Cuartiles
            //		+ Deciles
            //		+ Percentiles
            //		+ CoeficienteVariacion
            //		+ CoeficientePearson
            //		+ MedidaYuleBowley
            //		+ MedidaFisher
            //		+ Boxplot
            #endregion
            #region FRECUENCIAS
            Frecuencias<double> frecuencias = new Frecuencias<double>();
            //		+ FrecuenciaAbsoluta
            //		+ FrecuenciaAbsolutaAcumulada
            //		+ FrecuenciaRelativa
            //		+ FrecuenciaRelativaAcumulada
            #endregion
            #region RESUMENESNEMERICOS
            ResumenesNumericos resumenes = new ResumenesNumericos();
            //		+ Media
            //		+ Mediana
            //		+ Moda
            //		+ Varianza
            //		+ Covarianza
            //		+ DesviacionTipica
            //		+ DesviacionEstandar
            #endregion
        #endregion
        #region BIVARIADA
            Coeficientes coeficientes = new Coeficientes();
            //      + EvaluarRelacionCovarianza
            //      + CoeficienteCorrelacionPearson
            //      + CoeficienteDeterminacion
            //      + EvaluarCorrelacion
            //      + RectaRegresion
            //      + PredecirValorUsandoRectaRegresion
        #endregion
    #endregion
#endregion
#region PROBABILIDAD
    Probabilidades probabilidad = new Probabilidades();
    //      + ProbabilidadSimple
    //      + ProbabilidadEmpírica
    //      + ProbabilidadUnion
    //      + ProbabilidadUnionExcluyente
    //      + ProbabilidadCondicionada
    //      + ProbabilidadCondicionadaFrecuencias
    //      + SonEventosIndependientes
    //      + TeoremaBayes
    //      + TeoremaByesMultiplesHipotesis
    //      + TeoremaProbabilidadTotal
    //      - ValidarProbabilidad
#endregion
    }
}
