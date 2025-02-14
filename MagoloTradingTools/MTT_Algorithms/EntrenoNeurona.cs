namespace MTT_Algorithms
{
    public class EntrenoNeurona
    {
        // Valores fijos
        private readonly Dictionary<string, int> fixedValues = new Dictionary<string, int>
        {
            {"A2", 1}, {"A3", 1}, {"A4", 1}, {"A5", 1},
            {"B3", 1}, {"B5", 1}, {"C4", 1}, {"C5", 1}, {"J5", 1}, {"E2", 1},
            {"B2", 0}, {"B4", 0}, {"C2", 0}, {"C3", 0}, {"J2", 0}, {"J3", 0}, {"J4", 0}
        };

        // Valores iniciales entre 0 y 1
        private double f2, g2, n2;

        public EntrenoNeurona(double f2, double g2, double n2)
        {
            this.f2 = f2;
            this.g2 = g2;
            this.n2 = n2;
        }

        // Método para calcular una fila
        private (double e, double f, double g, int i) CalculateRow(double a, double b, double c, double j,
                                                                   double prevE, double prevF, double prevG)
        {
            double h = (a * prevE) + (b * prevF) + (c * prevG);
            int i = Convert.ToInt32(h >= 0);

            double k = (j - i) * n2 * a;
            double l = (j - i) * n2 * b;
            double m = (j - i) * n2 * c;

            return (prevE + k, prevF + l, prevG + m, i);
        }

        private (double e, double f, double g, int i) GetFirstIteration(double v, double f2, double g2, bool first)
        {
            List<int> jList = new List<int> { 0, 0, 0, 1 };

            int x = 0;

            //  FILA 1º
            var result = CalculateRow(fixedValues["A2"], fixedValues["B2"], fixedValues["C2"], jList[0], v, f2, g2);
            double e = Math.Round(result.e, 2);
            double f = Math.Round(result.f, 2);
            double g = Math.Round(result.g, 2);
            if (result.i == jList[0]) x++;

            //  FILA 2º
            result = CalculateRow(fixedValues["A3"], fixedValues["B3"], fixedValues["C3"], jList[1], e, f, g);
            e = Math.Round(result.e, 2);
            f = Math.Round(result.f, 2);
            g = Math.Round(result.g, 2);
            if (result.i == jList[1]) x++;

            //  FILA 3º
            result = CalculateRow(fixedValues["A4"], fixedValues["B4"], fixedValues["C4"], jList[2], e, f, g);
            e = Math.Round(result.e, 2);
            f = Math.Round(result.f, 2);
            g = Math.Round(result.g, 2);
            if (result.i == jList[2]) x++;

            if (first)
                return (e, f, g, x);

            //  FILA 4º
            result = CalculateRow(fixedValues["A5"], fixedValues["B5"], fixedValues["C5"], jList[3], e, f, g);
            e = Math.Round(result.e, 2);
            f = Math.Round(result.f, 2);
            g = Math.Round(result.g, 2);
            if (result.i == jList[3]) x++;

            return (e, f, g, x);
        }

        // Método principal para simular el proceso
        public (double f, double g, int iterations) Simulate()
        {
            var result = GetFirstIteration(1, f2, g2, true);
            double e = Math.Round(result.e, 2);
            double f = Math.Round(result.f, 2);
            double g = Math.Round(result.g, 2);
            int x = 0;

            for (int iteration = 2; ; iteration++)
            {
                result = GetFirstIteration(e, f, g, false);
                e = Math.Round(result.e, 2);
                f = Math.Round(result.f, 2);
                g = Math.Round(result.g, 2);
                x = result.i;

                if (x == 4)
                    return (f, g, iteration);
            }
        }
    }
}
