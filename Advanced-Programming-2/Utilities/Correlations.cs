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
                double maxCorrelation = 0;
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
                if (maxCorrelation > 0)
                {
                    CorrelatedFeatures correlatedFeatures = new CorrelatedFeatures();
                    correlatedFeatures.setFeature1(key);
                    correlatedFeatures.setFeature2(maxCorrelationAttribute);
                    correlatedFeatures.setCorrelation(maxCorrelation);
                    MessageBox.Show(correlatedFeatures.getFeature1());
                    cf.Add(correlatedFeatures);
                }
            }
        }

        public List<CorrelatedFeatures> getCorrelations() {
            return cf;
        }
    }
}
