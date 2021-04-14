namespace Advanced_Programming_2.Utilities

{
    // Class line
    public class Line
    {
        // Two points
        double a, b;
        // Initialize
        public Line()
        {
            a = 0;
            b = 0;
        }
        // Constructor
        public Line(double a, double b)
        {
           this.a = a;
           this.b = b;
        }
        // Equation
        public double f(double x)
        {
            return a * x + b;
        }
    }
}
