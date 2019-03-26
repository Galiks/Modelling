using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Task5
    {
        private Random random = new Random();
        private readonly int n = 1000;
        private readonly int valueOne = 5;
        private readonly int valueTwo = 5;

        public void MainMethod()
        {
            for (int i = 0; i < 3; i++)
            {
                var result = GetExpectedValueAndVariance();
                Console.WriteLine($"Мат ожидание: {result.Item1}");
                Console.WriteLine($"Среднее квадратическое отклонение: {result.Item2}");
                Console.WriteLine();
            }
        }

        private double GetMaxRandomValue()
        {
            double max = 0.0;
            for (int j = 0; j < 3; j++)
            {
                double sumR = 0;
                for (int i = 0; i < 12; i++)
                {
                    sumR += random.NextDouble();
                }
                var X = valueOne + valueTwo * (sumR - 6);
                if (X > max)
                {
                    max = X;
                }
            }
            return max;
        }

        /// <summary>
        /// Метод для подсчёта математического ожидания и дисперсии
        /// </summary>
        /// <returns>Метод возвращает пару, где первый элемент математическое ожидание, а второй - дисперсия</returns>
        private Tuple<double, double> GetExpectedValueAndVariance()
        {
            //мат ожидание
            double expectedValue = 0.0;
            //дисперсия
            double variance = 0.0;

            for (int i = 0; i < n; i++)
            {
                expectedValue += GetMaxRandomValue();
            }

            expectedValue = expectedValue / n;

            for (int i = 0; i < n; i++)
            {
                variance += Math.Pow(GetMaxRandomValue() - expectedValue, 2);
            }

            variance = Math.Sqrt(variance / (n - 1));


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

//Dictionary<double, int> pairs = new Dictionary<double, int>();
//for (int i = 0; i < n; i++)
//{
//    double x = random.Next(1, 11);
//    if (pairs.ContainsKey(x))
//    {
//        int count = pairs[x];
//        count++;
//        pairs.Remove(x);
//        pairs.Add(x, count);
//    }
//    else
//    {
//        pairs.Add(x, 1);
//    }
//}

//SortedDictionary<double, int> sortedPairs = new SortedDictionary<double, int>(pairs);

//foreach (var item in sortedPairs)
//{
//    //output
//    //Console.WriteLine($"{item.Key} - {(double)item.Value / n}");
//    expectedValue += item.Key * ((double)item.Value / n);
//    variance += item.Key * item.Key * (double)item.Value / n;
//}

//variance = variance - expectedValue * expectedValue;

////Console.WriteLine("Мат ожидание - " + expectedValue);
////Console.WriteLine("Дисперсия - " + variance);
////Console.WriteLine();