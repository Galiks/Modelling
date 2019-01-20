using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program
{
    public partial class Form1 : Form
    {
        private double eNumber = 2.71828;
        private double epsilon = 100;
        private double resistance = 100;
        private double capasity = 10;
        private Dictionary<Int32, Double> points;

        public Form1()
        {
            InitializeComponent();
            points = new Dictionary<int, double>();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            int i = 1;
            double voltage = 0.0;
            while (voltage < epsilon)
            {
                double temp = -(i / (resistance * capasity));
                voltage = epsilon * (1 - Math.Pow(eNumber, temp));
                i++;
                points.Add(i, voltage);
            }

            foreach (var item in points)
            {
                chart1.Series[0].Points.AddXY(item.Key, item.Value);
            }
            //chart1.Series[0].Points.AddXY(i, Math.Sin(i));

        }
    }
}
