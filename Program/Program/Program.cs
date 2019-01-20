using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Program
    {
        static double e = 2.71828; 
        static double epsilon = 100;
        static double resistance = 100;
        static double capasity = 10;


        static void Main(string[] args)
        {
            //Function();

            Form1 form1 = new Form1();
            form1.ShowDialog();
            //form1.Show();

            Console.Read();
        }

        private static void Function()
        {
            int i = 1;
            bool flag = true;
            double voltage = 0.0;
            while (voltage < epsilon)
            {
                double temp = -(i / (resistance * capasity));
                //Console.WriteLine(temp);
                //Console.WriteLine(Math.Pow(e, temp));
                voltage = epsilon * (1 - Math.Pow(e, temp));
                i++;
                Console.WriteLine(voltage);
                Console.WriteLine();
            }
        }
    }
}
