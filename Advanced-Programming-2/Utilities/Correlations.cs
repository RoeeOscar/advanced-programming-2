using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Advanced_Programming_2.Utilities
{
    class Correlations
    {
        List<CorrelatedFeatures> cf = new List<CorrelatedFeatures>();

        public Correlations(Dictionary<string, List<double>> values)
        {
            foreach (string key in values.Keys)
            {
                string maxCorrelationAttribute = null;
                double maxCorrelation = -1;
                foreach(string otherKey in values.Keys)
                {
                    if (key != otherKey)
                    {
                        float p = Math.Abs((float)AnomalyDetectionUtil.pearson(values[key], values[otherKey]));
                        if (p > maxCorrelation)
                        {
                            maxCorrelation = p;
                            maxCorrelationAttribute = otherKey;
                        }
                    }
                }
                if (maxCorrelation >= 0)
                {
                    CorrelatedFeatures correlatedFeatures = new CorrelatedFeatures();
                    correlatedFeatures.setFeature1(key);
                    correlatedFeatures.setFeature2(maxCorrelationAttribute);
                    correlatedFeatures.setCorrelation(maxCorrelation);
                    correlatedFeatures.setRegressionLine(AnomalyDetectionUtil.linearRegression(values[key], values[maxCorrelationAttribute]));
               //     Console.WriteLine(key + "  to  " + maxCorrelationAttribute);
               //     Console.WriteLine("the pearson is: " + maxCorrelation);
                    double maxX = values[key][0];
                    double minX = values[key][0];
                    for (int i = 1; i < values[key].Count(); i++)
                    {
                        if (values[key][i] > maxX)
                        {
                            maxX = values[key][i];
                        }
                        if (values[key][i] < minX)
                        {
                            minX = values[key][i];
                        }
                    }
                    correlatedFeatures.setMinXY(new Point(minX, correlatedFeatures.getRegressionLine().f(minX)));
                    correlatedFeatures.setMaxXY(new Point(maxX, correlatedFeatures.getRegressionLine().f(maxX)));

                    cf.Add(correlatedFeatures);
                }
            }
        }

        public List<CorrelatedFeatures> getCorrelations() {
            return cf;
        }
    }
}
