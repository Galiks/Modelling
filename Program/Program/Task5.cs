using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Task5
    {
        public double ElementOne { get; set; }
        public double ElementTwo { get; set; }
        public double ElementThree { get; set; }
        private Random random = new Random();
        private int n = 1000;

        public void MainMethod()
        {
            int sumR = 0;
            for (int i = 0; i < 12; i++)
            {
                sumR += random.Next(0, 5);
            }
            var pair = GetExpectedValueAndVariance();
            var X = pair.Item1 + pair.Item2 * (sumR - 6);
            Console.WriteLine(X);
        }

        /// <summary>
        /// Метод для подсчёта математического ожидания и дисперсии
        /// </summary>
        /// <returns>Метод возвращает пару, где первый элемент математическое ожидание, а второй - дисперсия</returns>
        private Tuple<double, double> GetExpectedValueAndVariance()
        {
            Dictionary<double, int> pairs = new Dictionary<double, int>();
            for (int i = 0; i < n; i++)
            {
                double x = random.Next(1, 11);
                if (pairs.ContainsKey(x))
                {
                    int count = pairs[x];
                    count++;
                    pairs.Remove(x);
                    pairs.Add(x, count);
                }
                else
                {
                    pairs.Add(x, 1);
                }
            }

            SortedDictionary<double, int> sortedPairs = new SortedDictionary<double, int>(pairs);

            //мат ожидание
            double expectedValue = 0.0;
            //дисперсия
            double variance = 0.0;

            foreach (var item in sortedPairs)
            {
                //output
                //Console.WriteLine($"{item.Key} - {(double)item.Value / n}");
                expectedValue += item.Key * (double)item.Value / n;
                variance += item.Key * item.Key * (double)item.Value / n;
            }

            variance = variance - expectedValue * expectedValue;

            Console.WriteLine(expectedValue);
            Console.WriteLine(variance);
            Console.WriteLine();

            return new Tuple<double, double>(expectedValue, variance);
        }

        /// <summary>
        /// Задание Тани
        /// </summary>
        public void Tanya()
        {
            double mA = 15;
            double mB = 30;
            double dA = 0.5;
            double dB = 0.5;
            double A;
            double B;
            double C;
            Random rand = new Random();
            double sumR;
            List<double> zk = new List<double>();

            double sumZ = 0;
            double tmp;
            int n = 1000;
            for (int i = 0; i < n; i++)
            {
                sumR = 0;
                for (int j = 0; j < 12; j++)
                {
                    sumR += rand.NextDouble();
                }
                A = mA + dA * (sumR - 6);

                sumR = 0;
                for (int j = 0; j < 12; j++)
                {
                    sumR += rand.NextDouble();
                }
                B = mB + dB * (sumR - 6);

                tmp = rand.NextDouble();
                if (tmp < 0.1)
                {
                    C = 10;
                }
                else if (tmp < 0.35)
                {
                    C = 20;
                }
                else if (tmp < 0.85)
                {
                    C = 30;
                }
                else
                {
                    C = 40;
                }
                zk.Add((A + B) / C);
                sumZ += (A + B) / C;
            }

            double mZ;
            double dZ;
            mZ = sumZ / n;
            double sumD = 0;
            for (int i = 0; i < n; i++)
            {
                sumD += Math.Pow((zk[i] - mZ), 2);
            }
            dZ = sumD / (n - 1);

            Console.WriteLine("Мат ожидание: {0}", mZ);
            Console.WriteLine("Дисперсия: {0}", dZ);

        }

    }
}

