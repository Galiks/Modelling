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
        private const double h = 0.1;

        public Form1()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Method3();
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
            double x = 10;
            double y = 10;
            double a = 10;// x'
            double b = 10;// y'
            double t = 0.0;
            int i = 0;
            double xFromH = a * h + x; //  x(h) = a(0)/h + x(t)
            double yFromH = b * h + y;
            while (t < 50)
            {
                double tempA = (-5 * xFromH + 2 * yFromH) * h + a; // a(h) = f(x)*h + a(0)
                double nextX = tempA * h + xFromH;
                xFromH = nextX;
                a = tempA;

                double tempB = (2 * xFromH - 8 * yFromH) * h + b;
                double nextY = tempB * h + yFromH;
                yFromH = nextY;
                b = tempB;

                Console.WriteLine($"ONE: {nextX}{Environment.NewLine}TWO: {nextY}{Environment.NewLine}");

                t = t + h;

                chart1.Series[0].Points.AddXY(t, xFromH);
                chart1.Series[1].Points.AddXY(t, yFromH);



                i++;

                //double tempA = -5 * x + 2 * y;
                //double tempB = 2 * x - 8 * y;

                //double nextA = tempA * h + x;
                //double nextB = tempB * h + y;
                //t = t + h;

                //x = nextA;
                //y = nextB;

                //chart1.Series[0].Points.AddXY(t, nextA);
                //chart1.Series[1].Points.AddXY(t, nextB);

                //i++;
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