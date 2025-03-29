using System.Text.RegularExpressions;

namespace MagoloRuleExtraction.Classes
{
    public class Utils
    {
        public static string CleanExpression(string expression)
        {
            string expressionCleaned = expression.Replace("`", "");

            // Corregir el formato de los números (reemplazar comas por puntos)
            expressionCleaned = Regex.Replace(expressionCleaned,
                @"([-]?\d+),(\d+)",
                "$1.$2");

            // Eliminar separadores de miles si existen
            expressionCleaned = Regex.Replace(expressionCleaned,
                @"(\d),(\d{3}[^\d])",
                "$1$2");

            return expressionCleaned;
        }
    }
}
