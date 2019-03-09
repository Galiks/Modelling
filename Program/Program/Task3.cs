using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Program
{
    class Task3
    {
        private Random random;

        /// <summary>
        /// Основной конструктор
        /// </summary>
        public Task3()
        {
            random = new Random();
            NumbersOfTests = new int[] { 20, 50, 500, 1000, 10000 };
        }

        public int[] NumbersOfTests { get; set; }
        private int NotRepairs { get; set; }
        private int Repairs { get; set; }


        public void MainMethod()
        {
            for (int i = 0; i < NumbersOfTests.Length; i++)
            {
                for (int j = 0; j < NumbersOfTests[i]; j++)
                {
                    if ((GetRandomInt() % 10) >= 2)
                    {
                        NotRepairs++;
                    }
                    else
                    {
                        Repairs++;
                    }
                }
                Console.WriteLine($"Ответ при {NumbersOfTests[i]} испытаниях: {Environment.NewLine}Не надо в ремонт: {(double)NotRepairs / NumbersOfTests[i] * 100}%{Environment.NewLine}В ремонт: {(double)Repairs / NumbersOfTests[i] * 100}%{Environment.NewLine}");
                NotRepairs = 0;
                Repairs = 0;
            }
        }

        private Int32 GetRandomInt()
        {
            //18 23 36
            int result = Math.Abs((18 * random.Next() + 23) % 36);
            return result;
        }
    }
}
