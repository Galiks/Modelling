using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Task6
    {
        public void MainMethod()
        {
            double lambda = 1 / 85.75;
            int n = 700;
            double time = 0;
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                time += (-1 / lambda) * Math.Log(random.NextDouble());
            }

            Console.WriteLine(time);
        }
    }
}
