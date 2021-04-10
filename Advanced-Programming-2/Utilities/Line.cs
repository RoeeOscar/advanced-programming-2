using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Programming_2.Utilities
{
    public class Line
    {
        double a, b;
        
        public Line()
        {
            a = 0;
            b = 0;
        }
        public Line(double a, double b)
        {
            this.a = a;
           this.b = b;
        }

        public double f(double x)
        {
            return a * x + b;
        }
    }
}
