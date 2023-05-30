using System;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Drawing2D;

namespace Ufo
{
    public partial class Form1 : Form
    {
        private const string LEGEND_NAME = "Series";
        private bool isGraphDrawn = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (isGraphDrawn)
            {
                int stepSize = 2;
                Point startPoint = new Point(50, 50);
                Point endPoint = new Point(300, 300);
                double tolerance = 0.5;
                DrawGraph(startPoint, endPoint, stepSize, tolerance, e.Graphics);
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            InitializeChart();
            int stepSize = 1;
            Point startPoint = new Point(1, 10);
            Point endPoint = new Point(510, 530);

            for (double i = 1; i <= 101; i += 5)
            {
                int seriesMembers = CountSeriesMembers(startPoint, endPoint, stepSize, i);
                AddDataToChart(i, seriesMembers);
            }
        }

        private void InitializeChart()
        {
            chart1.ChartAreas.Clear();
            chart1.Series.Clear();
            chart1.ChartAreas.Add("mainArea");
            chart1.Series.Add(LEGEND_NAME);
            chart1.Series[LEGEND_NAME].ChartArea = "mainArea";
            chart1.Series[LEGEND_NAME].ChartType = SeriesChartType.Spline;
            chart1.Series[LEGEND_NAME].Color = Color.Brown;
            chart1.ChartAreas[0].AxisX.Title = "Number of Series Members";
            chart1.ChartAreas[0].AxisY.Title = "Accuracy";
            chart1.ChartAreas[0].AxisX.Minimum = 0;
        }

        private void DrawGraph(Point startPoint, Point endPoint, int stepSize, double tolerance, Graphics g)
        {
            DrawPoint(startPoint, g, tolerance);
            DrawPoint(endPoint, g, tolerance);

            double angle = CalculateAngle(startPoint, endPoint);
            double distance = CalculateDistance(startPoint, endPoint);
            double lastDistance = distance;
            double x = startPoint.X;
            double y = startPoint.Y;

            while (distance <= lastDistance)
            {
                Thread.Sleep(50);
                ClearPoint(x, y, g, tolerance);
                lastDistance = CalculateDistance(new Point((int)x, (int)y), endPoint);
                x += stepSize * MyFunctions.Cos(angle, 1);
                y += stepSize * MyFunctions.Sin(angle, 1);
                DrawPoint(new Point((int)x, (int)y), g, tolerance);
                distance = CalculateDistance(new Point((int)x, (int)y), endPoint);
                if (distance <= tolerance)
                {
                    break;
                }
            }
        }

        private void ClearPoint(double X, double Y, Graphics g, double radius)
        {
            using (Pen pen = new Pen(Color.White))
            {
                g.DrawEllipse(pen, (float)X, (float)Y, (float)radius, (float)radius);
            }
        }

        private void DrawPoint(Point point, Graphics g, double radius)
        {
            using (Pen pen = new Pen(Color.Black))
            {
                g.DrawEllipse(pen, point.X, point.Y, (float)radius, (float)radius);
            }
        }

        private void AddDataToChart(double x, double y)
        {
            chart1.Series[LEGEND_NAME].Points.AddXY(x, y);
        }

        private int CountSeriesMembers(Point startPoint, Point endPoint, int stepSize, double tolerance)
        {
            int n = 1;
            bool isFinished = false;
            double angle;

            while (n < 50 && !isFinished)
            {
                double ratio = CalculateRatio(startPoint, endPoint);
                if (ratio <= 1)
                {
                    angle = MyFunctions.Arctan(ratio, n);
                }
                else
                {
                    ratio = 1 / ratio;
                    angle = Math.PI / 2 - MyFunctions.Arctan(ratio, n);
                }

                double distance = CalculateDistance(startPoint, endPoint);
                double lastDistance = distance;
                double x = startPoint.X;
                double y = startPoint.Y;

                while (distance <= lastDistance)
                {
                    lastDistance = CalculateDistance(new Point((int)x, (int)y), endPoint);
                    x += stepSize * MyFunctions.Cos(angle, n);
                    y += stepSize * MyFunctions.Sin(angle, n);
                    distance = CalculateDistance(new Point((int)x, (int)y), endPoint);
                    if (distance <= tolerance)
                    {
                        isFinished = true;
                        break;
                    }
                }

                n++;
            }

            return n;
        }

        private double CalculateAngle(Point startPoint, Point endPoint)
        {
            int dx = Math.Abs(endPoint.X - startPoint.X);
            int dy = Math.Abs(endPoint.Y - startPoint.Y);

            double ratio = dy / (double)dx;
            if (ratio <= 1)
            {
                return MyFunctions.Arctan(ratio, 1);
            }
            else
            {
                ratio = dx / (double)dy;
                return Math.PI / 2 - MyFunctions.Arctan(ratio, 1);
            }
        }

        private double CalculateDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow((point1.X - point2.X), 2) + Math.Pow((point1.Y - point2.Y), 2));
        }

        private double CalculateRatio(Point startPoint, Point endPoint)
        {
            int dx = Math.Abs(endPoint.X - startPoint.X);
            int dy = Math.Abs(endPoint.Y - startPoint.Y);
            return dy / (double)dx;
        }
    }
}
