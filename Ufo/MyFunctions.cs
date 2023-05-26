namespace Ufo
{
    class MyFunctions
    {
        public static double Exp(double x, int n)
        {
            double result = 1.0;
            double term = 1.0;

            for (int i = 1; i <= n; i++)
            {
                term *= x / i;
                result += term;
            }

            return result;
        }

        public static double Sin(double x, int n)
        {
            double result = x;
            double term = x;

            for (int i = 1, j = 2; i < n; i++, j += 2)
            {
                term *= -x * x / (j * (j + 1));
                result += term;
            }

            return result;
        }

        public static double Cos(double x, int n)
        {
            double result = 1.0;
            double term = 1.0;

            for (int i = 1, j = 2; i < n; i++, j += 2)
            {
                term *= -x * x / (j * (j - 1));
                result += term;
            }

            return result;
        }

        public static double Sh(double x, int n)
        {
            double result = x;
            double term = x;

            for (int i = 1; i <= n; i++)
            {
                term *= x * x / ((2 * i) * (2 * i + 1));
                result += term;
            }

            return result;
        }

        public static double Ch(double x, int n)
        {
            double result = 1.0;
            double term = 1.0;

            for (int i = 1; i <= n; i++)
            {
                term *= x * x / ((2 * i) * (2 * i - 1));
                result += term;
            }

            return result;
        }

        public static double PowerSeries(double x, double m, int n)
        {
            double result = 1.0;
            double term = 1.0;

            for (int i = 1; i <= n; i++)
            {
                term *= m * x / i;
                result += term;
            }

            return result;
        }

        public static double OnePlusX(double x, int n)
        {
            double result = 1.0;
            double term = 1.0;

            for (int i = 1; i <= n; i++)
            {
                term *= x / i;
                result += term;
            }

            return result;
        }

        public static double LnOnePlusX(double x, int n)
        {
            double result = 0.0;
            double term = x;
            int sgn = -1;

            for (int i = 1; i <= n; i++)
            {
                term *= sgn * x;
                sgn = -sgn;

                result += term / i;
            }

            return result;
        }

        public static double Arcsin(double x, int n)
        {
            double result = x;
            double term = x;

            for (int i = 1, j = 3; i < n; i++, j += 2)
            {
                term *= -(x * x * (j - 2)) / (j * (j - 1));
                result += term;
            }

            return result;
        }

        public static double Arctan(double x, int n)
        {
            double result = x;
            double term = x;
            int sgn = -1;

            for (int i = 1, j = 3; i < n; i++, j += 2)
            {
                term *= sgn * (x * x * (j - 2)) / (j * (j - 1));
                sgn = -sgn;
                result += term;
            }

            return result;
        }

        public static double Tan(double x, int n)
        {
            double result = x;
            double term = x;

            for (int i = 1, j = 3; i < n; i++, j += 2)
            {
                term *= -x * x / (j * (j - 1));
                result += term;
            }

            return result;
        }
    }
}
