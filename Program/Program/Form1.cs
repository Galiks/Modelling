using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Program
{
    public partial class Form1 : Form
    {
        private const double eNumber = 2.71828;
        private const double epsilon = 100;
        private const double resistance = 100;
        private const double capasity = 10;
        private const double h = 0.01;

        public Form1()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Method1();
            //Method2();
            
            Method3();

            //double x1 = -1;
            //double y1 = 1;
            //Method3(x1, y1, t, k);

            //double x2 = 1;
            //double y2 = -1;
            //Method3(x2, y2, t, k);

            //double x3 = 1;
            //double y3 = 1;
            //Method3(x3, y3, t, k);

        }

        /// <summary>
        /// Аналитическое решение первого задания
        /// </summary>
        private void Method1()
        {
            Dictionary<Int32, Double> points = new Dictionary<int, double>();
            int i = 0;
            double voltage = 0.0;

            while (voltage < epsilon)
            {
                points.Add(i, voltage);
                double temp = -(i / (resistance * capasity));
                voltage = epsilon * (1 - Math.Pow(eNumber, temp));
                i++;
            }

            foreach (var item in points)
            {
                chart1.Series[0].Points.AddXY(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Численное решение первого задания
        /// </summary>
        private void Method2()
        {
            double time = 0.0;
            double voltage = 0.0;
            int i = 0;

            while (voltage < 100)
            {
                if (i > 400000)
                {
                    break;
                }
                double newVoltage = ((epsilon - voltage) / (resistance * capasity)) * h + voltage;

                double newTime = time + h; 

                if (i % 100 == 0)
                {
                    chart1.Series[0].Points.AddXY(newTime, newVoltage); 
                }

                voltage = newVoltage;
                time = newTime;
                i++;
            }

            Console.WriteLine($"{time} + {i}");
        }

        /// <summary>
        /// Третье задание
        /// </summary>
        private void Method3()
        {
            double x = 0;
            double y = 0;
            double t = 0.0;
            int i = 0;
            while (t < 30)
            {
                Console.WriteLine(t);
                double tempA = -5 * x + 2 * y;
                double tempB = 2 * x - 8 * y;

                double nextA = tempA * h + x;
                double nextB = tempB * h + y;
                t = t + h;

                x = nextA;
                y = nextB;

                chart1.Series[0].Points.AddXY(t, nextA);
                chart1.Series[1].Points.AddXY(t, nextB);

                i++;
            }


            #region another
            //double fx = (-5 * x) + (2 * y);
            //double fy = (2 * x) - (8 * y);
            //chart1.Series[0].Points.AddXY(x, y);
            //while (t <= 5)
            //{
            //    double x1 = x + k * fx;
            //    double y1 = y + k * fy;
            //    t = t + k;
            //    x = x1;
            //    y = y1;
            //    fx = -x;
            //    fy = y;
            //    if (Math.Abs(y) > 0.01)
            //    {
            //        chart1.Series[0].Points.AddXY(x, y);
            //    }
            //} 
            #endregion
        }
    }
}
