using System;
using System.Drawing;
using System.Windows.Forms;

namespace Сплайновые_кривые
{
    public partial class Form1 : Form
    {
        private Graphics G;
        private PointF[] Arr = new PointF[] { };
        private Pen pen;

        public Form1()
        {
            InitializeComponent();
            pen = new Pen(Color.Gold, 3);
            G = pictureBox1.CreateGraphics();
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        }

        private void Draw()
        {
            int j = 0;

            PointF[] result = new PointF[101];
            for (float t = 0; t < 1; t += 0.01f)
            {
                float ytmp = 0;
                float xtmp = 0;
                for (int i = 0; i < Arr.Length; i++)
                {
                    float b = Casteljo(i, Arr.Length - 1, t);
                    xtmp += Arr[i].X * b;
                    ytmp += Arr[i].Y * b;
                }
                result[j] = new PointF(xtmp, ytmp);
                j++;
            }
            G.DrawLines(pen, result);
        }

        float Casteljo(int i, int n, float t)
        {
            float res = 1;
            for (int j = 1; j <= i; j++)
                res *= (float)(n - j + 1) / j;
            res *= (float)Math.Pow(t, i) * (float)Math.Pow(1 - t, n - i);
            return res;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            PointF clickedPoint = e.Location;
            Array.Resize(ref Arr, Arr.Length + 1);
            Arr[Arr.Length - 1] = clickedPoint;
            pictureBox1.Refresh();
            Draw();

            foreach (PointF point in Arr)
            {
                G.FillEllipse(Brushes.Black, point.X - 3, point.Y - 3, 6, 6);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Arr.Length > 0)
            {
                Array.Resize(ref Arr, Arr.Length - 1);
                pictureBox1.Refresh();
                Draw();

                foreach (PointF point in Arr)
                {
                    G.FillEllipse(Brushes.Black, point.X - 3, point.Y - 3, 6, 6);
                }

            }
        }
    }
}
