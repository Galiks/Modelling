using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            //double lambda = 1 / 85.35;
            //Random random = new Random();
            //Console.WriteLine(-lambda * Math.Log(random.NextDouble(), Math.E));

            //Form1 form1 = new Form1();
            //form1.ShowDialog();
            //Task3 task3 = new Task3();
            //task3.MainMethod();
            //Task4 task4 = new Task4(3, 1000);
            //task4.MainMethod();
            //Task5 task5 = new Task5();
            //task5.MainMethod();
            //Task6 task6 = new Task6();
            //task6.MainMethod();
            QueuingSystem task = new QueuingSystem(1000, 10);
            task.Main();
            Console.WriteLine("THE END");
            Console.ReadKey();
        }
    }
}
