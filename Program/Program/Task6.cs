using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Task6
    {

        private int N = 700;
        //шаг вероятности для FindDependency
        private readonly double h = 0.1;
        // наша батарейка, i-тый символ будет содержать секунду, на которой он засек i-тую частицу
        private int[] battery;
        private Random random;
        private readonly double lambda = 1 / 85.35;

        public Task6()
        {
            battery = new int[N];
            random = new Random();
            TimeAndN = new List<Tuple<double, double>>();
        }

        public int[] ResultBattery { get; set; }
        public List<Tuple<int, double>> XY { get; set; }
        public List<Tuple<double, double>> TimeAndN { get; set; }


        public void MainMethod()
        {
            //ResultBattery = FindWorkTime(battery);
            //XY = FindDependency();
            ThirdTry();
        }

        #region LastTry
        //// battery после выполнения метода будет содержать в себе время обнаружения каждой частицы, индекс - заряд в этот момент
        //public int[] FindWorkTime(int[] battery)
        //{
        //    int i = 0;
        //    int time = 0;
        //    while (i < N)
        //    {
        //        time++;
        //        //засёк ли прибор частицу
        //        if (random.Next(0, 101) > GetExponentionValue() * 100)
        //        {
        //            battery[i++] = time;
        //        }
        //    }
        //    return battery;
        //}

        //public List<Tuple<int, double>> FindDependency()
        //{
        //    int time = 0;
        //    int i = 0;
        //    List<Tuple<int, double>> XY = new List<Tuple<int, double>>();
        //    double r = GetExponentionValue();
        //    for (r = h; r < 1; r += h)
        //    {
        //        time = 0;
        //        i = 0;
        //        // счетчик удачных засеканий частицы на отметке в 700 мы узнаем, что батарейка разрядилась, нас интересует время разрядки
        //        while (i < N)
        //        {
        //            time++;
        //            if (random.Next(0, 101) > GetExponentionValue() * 100)
        //            {
        //                i++;
        //            }
        //        }
        //        XY.Add(new Tuple<int, double>(time, r));
        //    }

        //    return XY;
        //}

        //private void SecondTry()
        //{
        //    double lambda = 1 / 85.75;
        //    double time = 0.0;
        //    double N = 700;
        //    TimeAndN.Add(new Tuple<double, double>(time, N));
        //    int i = 0;
        //    while (N > 0)
        //    {
        //        N = N + lambda * Math.Log(random.NextDouble(), Math.E);
        //        time++;
        //        //Console.WriteLine($"{time} - {N}");
        //        i++;

        //        if (i % 100 == 0)
        //        {
        //            TimeAndN.Add(new Tuple<double, double>(time, N)); 
        //        }
        //    }
        //} 
        #endregion

        private void ThirdTry()
        {
            double time = 0.0;
            TimeAndN.Add(new Tuple<double, double>(time, N));
            for (int i = N; i >= 0; i--)
            {
                time += GetExponentionValue();
                TimeAndN.Add(new Tuple<double, double>(time, i));
            }
        }

        private double GetExponentionValue()
        {
            return -(lambda * Math.Log(random.NextDouble(), Math.E));
        }
    }
}

//double lambda = 1 / 85.75;
//int n = 700;
//double time = 0;
//Random random = new Random();
//for (int i = 0; i < n; i++)
//{
//    time += (-1 / lambda) * Math.Log(random.NextDouble());
//}

//Console.WriteLine(time);

//List<Tuple<int, bool>> experiments = new List<Tuple<int, bool>>();
//Random random = new Random();
//for (int i = 0; i < 700; i++)
//{
//    int rnd;
//    if ((rnd = random.Next(0,2)) == 1)
//    {
//        experiments.Add(new Tuple<int, bool>(i, true));
//    }
//    else
//    {
//        experiments.Add(new Tuple<int, bool>(i, false));
//    }
//}

//var list = from s in experiments
//           where s.Item2 == true
//           select s;

//foreach (var item in list)
//{
//    Console.WriteLine($"{item.Item1} - {item.Item2}");
//}

//for (double i = 0.05; i < 1; i += 0.05)
//{

//}