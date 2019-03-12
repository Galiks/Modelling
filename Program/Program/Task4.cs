using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Task4
    {
        #region Старая версия
        //public int Radius { get; set; }
        //public double AreaOfCircle { get; set; }
        //public double AreaOfTriangle { get; set; }

        //public Task4(int radius)
        //{
        //    Radius = radius;
        //}

        //public void MainMethod()
        //{
        //    double resultForOnePoint = GetAreaOfTriangle() / GetAreaOfCircle();
        //    Console.WriteLine($"Вероятность того, что наугад поставленная в данном круге точка окажется внутри треугольника: {resultForOnePoint * 100}%");
        //    Console.WriteLine($"Вероятность того, что все четыре наугад поставленные в данном круге точки окажутся внутри треугольника: {Math.Pow(resultForOnePoint, 4) * 100}%");
        //}

        //private double GetAreaOfCircle()
        //{
        //    return Math.PI * Radius * Radius;
        //}

        //private double GetAreaOfTriangle()
        //{
        //    double a = Radius * Math.Sqrt(3);
        //    Console.WriteLine(a);
        //    Console.WriteLine(a * a * Math.Sqrt(3) / 4);
        //    return a * a * Math.Sqrt(3) / 4;
        //} 
        #endregion

        private Coordinate[] Coordinates { get; set; }
        private double Radius { get; set; }
        public Random random { get; set; }
        //не знал как назвать переменную, которая отвечает за количество успехов во всех экспериментах
        public double Temp { get; set; }
        public int CountOfExperiments { get; set; }

        public Task4(double radius, int countOfExperiments)
        {
            Radius = radius;
            Coordinates = new Coordinate[3];
            Coordinates[0] = new Coordinate(0, Radius);
            Coordinates[1] = new Coordinate(-(Math.Sqrt(3) * Radius) / 2, -Radius / 2);
            Coordinates[2] = new Coordinate((Math.Sqrt(3) * Radius) / 2, -Radius / 2);
            random = new Random();
            CountOfExperiments = countOfExperiments;
        }

        public void MainMethod()
        {
            //сейчас будет весело, потому что я не продумал то, что будет дальше))))))))))
            for (int i = 0; i < CountOfExperiments; i++)
            {
                double notMiss = 0;
                double miss = 0;
                for (int j = 0; j < 4; j++)
                {
                    if (IsPointInTriange())
                    {
                        notMiss++;
                    }
                    else
                    {
                        miss++;
                    } 
                }
                double inTriangle = notMiss / 4;
                double notInTriangle = miss / 4;
                Console.WriteLine($"Вероятность попаданияв в триугольник на {i} эксперименте: {inTriangle * 100}%");
                if (inTriangle >= notInTriangle)
                {
                    Temp++;
                }
            }

            Console.WriteLine($"Процент успехов на {CountOfExperiments} экспериментов: {(Temp / CountOfExperiments) * 100}%");
        }

        private Boolean IsPointInTriange()
        {
            var point = GetPoint();
            var a = (Coordinates[0].X - point.X) * (Coordinates[1].Y - Coordinates[0].Y) - (Coordinates[1].X - Coordinates[0].X) * (Coordinates[0].Y - point.Y);
            var b = (Coordinates[1].X - point.X) * (Coordinates[2].Y - Coordinates[1].Y) - (Coordinates[2].X - Coordinates[1].X) * (Coordinates[1].Y - point.Y);
            var c = (Coordinates[2].X - point.X) * (Coordinates[0].Y - Coordinates[2].Y) - (Coordinates[0].X - Coordinates[2].X) * (Coordinates[2].Y - point.Y);
            if ((a >= 0 & b >= 0 & c >= 0) || (a < 0 & b < 0 & c < 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Coordinate GetPoint()
        {
            int x = random.Next(-5, 5);
            int y = random.Next(-5, 5);
            return new Coordinate(x, y);
        }
    }

    class Coordinate
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Coordinate(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
