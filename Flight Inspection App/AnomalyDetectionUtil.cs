using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Flight_Inspection_App
{
    class AnomalyDetectionUtil
    {
        double Avg(IList<double> x, int size)
        {
            double sum = 0;
            for (int i = 0; i < size; sum += x[i], i++) ;
            return sum / size;
        }

        // returns the variance of X and Y
        double Var(IList<double> x, int size)
        {
            double av = Avg(x, size);
            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * x[i];
            }
            return sum / size - av * av;
        }

        // returns the covariance of X and Y
        double Cov(IList<double> x, IList<double> y, int size)
        {
            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * y[i];
            }
            sum /= size;

            return sum - Avg(x, size) * Avg(y, size);
        }


        // returns the Pearson correlation coefficient of X and Y
        public double Pearson(IList<double> x, IList<double> y, int size)
        {
            double s = Math.Sqrt(Var(x, size)) * Math.Sqrt(Var(y, size));
            if (s == 0) return 0;
            return Cov(x, y, size) /s;
        }

        // performs a linear regression and returns the line equation
        public Line LinearReg(IList<DataPoint> points, int size)
        {
            List<double> x = new(), y = new();
            for (int i = 0; i < size; i++)
            {
                x.Add(points[i].X);
                y.Add(points[i].Y);
            }
            double a = Cov(x, y, size) / Var(x, size);
            double b = Avg(y, size) - a * (Avg(x, size));

            return new Line(a, b);
        }

        // returns the deviation between point p and the line equation of the points
        double Dev(DataPoint p, IList<DataPoint> points, int size)
        {
            Line l = LinearReg(points, size);
            return Dev(p, l);
        }

        // returns the deviation between point p and the line
        double Dev(DataPoint p, Line l)
        {
            double x = p.Y - l.f(p.X);
            if (x < 0)
                x *= -1;
            return x;
        }
    }

public class Line
    {
        double _a, _b;
        public Line()
        {
            _a = 0;
            _b = 0;
            
        }
        public Line(double a, double b)
        {
            _a = a;
            _b = b;

        }
        public double f(double x)
        {
            return _a * x + _b;
        }

    }
}


