using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ufo
{
    public partial class Form1 : Form
    {
        private const int CellSize = 20; // Размер одной клетки в пикселях
        private const int GridSize = 30; // Размер сетки (количество клеток в ширину и высоту)

        private double startValue = 0.0; // Начальное значение x
        private double endValue = 1.0; // Конечное значение x

        public Form1()
        {
            InitializeComponent();
            InitializeGrid();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private float CalculateAngle(Point start, Point end)
        {
            float dx = end.X - start.X;
            float dy = end.Y - start.Y;
            return (float)Math.Atan2(dy, dx);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void InitializeGrid()
        {
            // Установка размера формы в зависимости от размера сетки
            int formWidth = CellSize * GridSize + 1;
            int formHeight = CellSize * GridSize + 1;
            ClientSize = new Size(formWidth, formHeight);

            // Создание и отрисовка сетки
            var grid = new bool[GridSize, GridSize];
            using (var graphics = CreateGraphics())
            {
                for (int x = 0; x < GridSize; x++)
                {
                    for (int y = 0; y < GridSize; y++)
                    {
                        int cellX = x * CellSize;
                        int cellY = y * CellSize;
                        var cellRect = new Rectangle(cellX, cellY, CellSize, CellSize);
                        graphics.DrawRectangle(Pens.Black, cellRect);
                        if (grid[x, y])
                            graphics.FillRectangle(Brushes.Blue, cellRect);
                    }
                }
            }
        }

        private void DrawTrajectory(Func<double, double> function)
        {
            // Очищение сетки и отрисовка траектории функции
            var grid = new bool[GridSize, GridSize];
            using (var graphics = CreateGraphics())
            {
                graphics.Clear(Color.White);
                for (int x = 0; x < GridSize; x++)
                {
                    double value = startValue + (double)x / GridSize * (endValue - startValue);
                    double functionValue = function(value);
                    int y = (int)(functionValue * GridSize);
                    if (x < GridSize && y < GridSize && x >= 0 && y >= 0)
                    {
                        grid[x, y] = true;
                    }

                    int cellX = x * CellSize;
                    int cellY = y * CellSize;
                    var cellRect = new Rectangle(cellX, cellY, CellSize, CellSize);
                    graphics.DrawRectangle(Pens.Black, cellRect);
                    if (x < GridSize && y < GridSize && x >= 0 && y >= 0 && grid[x, y])
                        graphics.FillRectangle(Brushes.Blue, cellRect);
                }
            }
        }

        private void btnCos_Click(object sender, EventArgs e)
        {
            DrawTrajectory(Math.Cos);
        }

        private void btnXToThePowerOfX_Click(object sender, EventArgs e)
        {
            DrawTrajectory(x => Math.Pow(x, x));
        }
    }
}
