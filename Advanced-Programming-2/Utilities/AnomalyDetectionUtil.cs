using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Programming_2.Utilities
{
    public class AnomalyDetectionUtil
    {
        public static double average(List<double> numbers)
        {
            double sum = 0;
            foreach (double x in numbers)
            {
                sum += x;
            }
            return sum / numbers.Count();
        }

        public static double variance(List<double> numbers)
        {
            double avg = average(numbers);
            double sum = 0;
            foreach (double x in numbers)
            {
                sum += Math.Pow(x, 2);
            }
            return sum / numbers.Count() - Math.Pow(avg, 2);
        }

        public static double covariance(List<double> numbers1, List<double> numbers2)
        {
            double sum = 0;
            for (int i = 0; i < numbers1.Count(); i++)
            {
                sum += numbers1[i] * numbers2[i];
            }

            return sum / numbers1.Count() - average(numbers1) * average(numbers2);
        }

        public static double pearson(List<double> numbers1, List<double> numbers2)
        {
            if ((Math.Sqrt(variance(numbers1))==0)|| (Math.Sqrt(variance(numbers2)) == 0)){
                return 0;
            }
                return covariance(numbers1, numbers2) / ((Math.Sqrt(variance(numbers1))) * (Math.Sqrt(variance(numbers2))));
        }

        public static Line linearRegression(List<double> numbers1, List<double> numbers2)
        {
            double a = covariance(numbers1, numbers2) / variance(numbers1);
            double b = average(numbers2) - a * (average(numbers1));

            return new Line(a, b);
        }
    }
}
