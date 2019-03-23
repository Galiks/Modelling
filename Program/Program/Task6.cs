using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Task6
    {

        private readonly int N = 700;
        private readonly double S = 0.05;
        private int time = 0;
        private int[] battery;
        private int i = 0;
        private double r = 0.0;
        private Random random;

        public Task6()
        {
            battery = new int[N];
            random = new Random();
            r = random.NextDouble();
            ResultBattery = FindWorkTime(battery);
            XY = FindDependency();
        }

        public int[] ResultBattery { get; set; }
        public List<Tuple<int, double>> XY { get; set; }


        public void MainMethod()
        {
            Console.WriteLine("Ха-ха! ТУТ ПУСТО! Всё в конструкторе!");
        }

        private int[] FindWorkTime(int[] battery)
        {
            while (i < N)
            {
                time++;
                if (random.Next(0,101) > r * 100)
                {
                    battery[i++] = time;
                }
            }
            return battery;
        }

        private List<Tuple<int, double>> FindDependency()
        {
            List<Tuple<int, double>> XY = new List<Tuple<int, double>>();
            for (r = S; r < 1; r+=S)
            {
                time = 0;
                i = 0;
                while (i < N)
                {
                    time++;
                    if (random.Next(0,101) > r * 100)
                    {
                        i++;
                    }
                }
                XY.Add(new Tuple<int, double>(time, r));
            }

            return XY;
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