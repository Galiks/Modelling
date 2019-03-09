using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Task4
    {
        public int Radius { get; set; }
        public double AreaOfCircle { get; set; }
        public double AreaOfTriangle { get; set; }

        public Task4(int radius)
        {
            Radius = radius;
        }

        public void MainMethod()
        {
            double resultForOnePoint = GetAreaOfTriangle() / GetAreaOfCircle();
            Console.WriteLine($"Вероятность того, что наугад поставленная в данном круге точка окажется внутри треугольника: {resultForOnePoint * 100}%");
            Console.WriteLine($"Вероятность того, что все четыре наугад поставленные в данном круге точки окажутся внутри треугольника: {Math.Pow(resultForOnePoint, 4) * 100}%");
        }

        private double GetAreaOfCircle()
        {
            return Math.PI * Radius * Radius;
        }

        private double GetAreaOfTriangle()
        {
            double a = Radius * Math.Sqrt(3);
            return a * a * Math.Sqrt(3) / 4;
        }
    }
}
