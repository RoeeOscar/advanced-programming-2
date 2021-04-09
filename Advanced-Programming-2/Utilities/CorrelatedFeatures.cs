using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Programming_2.Utilities
{
    public class CorrelatedFeatures
    {
        private string feature1, feature2;
        private double correlation;
      //  private double threshold;

        public string getFeature1()
        {
            return feature1;
        }

        public string getFeature2()
        {
            return feature2;
        }

        public double getCorrelation()
        {
            return correlation;
        }

   //     public double getThreshold()
   //     {
   //         return threshold;
   //     }

        public void setFeature1(string feature)
        {
            feature1 = feature;
        }

        public void setFeature2(string feature)
        {
            feature2 = feature;
        }

        public void setCorrelation(double cor)
        {
            correlation = cor;
        }

        public void setThreshold(double th)
        {
     //       threshold = th;
        }
    }
}
