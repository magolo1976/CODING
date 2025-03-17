using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagoloRuleExtraction.Sections.Data
{
    public partial class ConclusionesForm: Form
    {
        public ConclusionesForm(double totalReturns, double pValue)
        {
            InitializeComponent();

            ManageTotalReturns(totalReturns, pValue);
        }

        private void ManageTotalReturns(double totalReturns, double pValue)
        {
            //  Estabilidad del Activo
            if (pValue > 0.60)
            {
                lblEstabilidad.Text = "🌟 **Activo Muy Adecuado para Minería de Reglas**\n" +
                                 "Las distribuciones de train y test son altamente similares,\n" +
                                 "lo que sugiere una gran estabilidad en el comportamiento del activo.";
                lblEstabilidad.ForeColor = Color.LimeGreen;
            }
            else if (pValue > 0.30)
            {
                lblEstabilidad.Text = "✅ **Activo Adecuado para Minería de Reglas**\n" +
                  "Existe una similitud razonable entre las distribuciones de train y test,\n" +
                  "permitiendo la búsqueda de reglas con cierta confianza.";
                lblEstabilidad.ForeColor = Color.DeepSkyBlue;
            }
            else
            {
                lblEstabilidad.Text = "⚠️ **Activo Poco Adecuado para Minería de Reglas**\n" +
                 "Las diferencias significativas entre train y test sugieren\n" +
                 "inestabilidad en el comportamiento del activo.";
                lblEstabilidad.ForeColor = Color.DarkOrange;
            }

            //  Comportamiento Tendencial
            if (totalReturns > 15)
            {
                lblComportamiento.Text = "📈 **Sesgo Alcista Significativo**\n" +
                  "El activo muestra una fuerte tendencia alcista en el período analizado,\n" +
                  "con un retorno neto superior al 15%.";
                lblComportamiento.ForeColor = Color.LimeGreen;
            }
            else if (totalReturns < -15)
            {
                lblComportamiento.Text = "📉 **Sesgo Bajista Significativo**\n" +
                  "El activo muestra una fuerte tendencia bajista en el período analizado,\n" +
                  "con un retorno neto inferior al -15%.";
                lblComportamiento.ForeColor = Color.DeepSkyBlue;
            }
            else
            {
                lblComportamiento.Text = "↔️ **Sin Sesgo Tendencial Claro**\n" +
                  "El activo no muestra un sesgo tendencial significativo,\n" +
                  "con un retorno neto entre -15% y 15%.";
                lblComportamiento.ForeColor = Color.DarkOrange;
            }

        }
    }
}
