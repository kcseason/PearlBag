using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test3
{
    public partial class Form1 : Form
    {

        bool bDrawStart = false;

        Point pointStart = Point.Empty;

        Point pointContinue = Point.Empty;
        Dictionary<Point, Point> dicPoints = new Dictionary<Point, Point>();
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //if (bDrawStart)
            //{

            //    bDrawStart = false;

            //}

            //else
            //{

            //    bDrawStart = true;

            //    pointStart = e.Location;

            //}
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //if (bDrawStart)
            //{

            //    pointContinue = e.Location;

            //    Refresh();

            //}
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //if (bDrawStart)
            //{

            //    dicPoints.Add(pointStart, pointContinue);

            //}



            //bDrawStart = false;
        }

        bool paint = false;
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Refresh();
            int y = 0;
            for (int row = 1; row <= 31; row++)
            {
                int x = 0;
                for (int col = 1; col <= 23; col++)
                {
                    // 第一行第一列后退一半宽度开始添加
                    if (col == 1 && IsOdd(row))
                        x = x + 12 + 6;

                    Graphics gra = this.pictureBox1.CreateGraphics();
                    gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    Brush bush = new SolidBrush(Color.Green);//填充的颜色
                    gra.FillEllipse(bush, x, y, 24, 24);//
                    x += 24 + 12;
                }
                y += 17;
            }
        }

        private void PaintCircle(Graphics g, Brush bush, int x, int y)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillEllipse(bush, x, y, 28, 28);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int y = 0;
            for (int row = 1; row <= 31; row++)
            {
                int x = 0;
                for (int col = 1; col <= 23; col++)
                {
                    // 第一行第一列后退一半宽度开始添加
                    if (col == 1 && IsOdd(row))
                        x = x + 12 + 6;

                    Graphics gra = this.pictureBox1.CreateGraphics();
                    gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    Brush bush = new SolidBrush(Color.Green);//填充的颜色
                    gra.FillEllipse(bush, x, y, 24, 24);
                    gra.Dispose();
                    x += 24 + 12;
                }
                y += 17;
            }
        }
        // 判断奇偶数
        private bool IsOdd(int n)
        {
            return Convert.ToBoolean(n % 2);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Graphics gra = this.pictureBox1.CreateGraphics();
            gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush bush = new SolidBrush(Color.Green);//填充的颜色
            gra.FillEllipse(bush, 150, 0, 100, 100);//
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Graphics gra = this.pictureBox1.CreateGraphics();
            gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush bush = new SolidBrush(Color.Green);//填充的颜色
            gra.FillEllipse(bush, 75, 70, 100, 100);//
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        int[] xy = new int[2];
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int y = e.Y;
            int row = y / 17 + 1;
            int col;
            label2.Text = "行" + row;

            if (row % 2 != 0)
            {
                int x = e.X - 6;
                col = x / 12 + 1;
                if ((col - 1) % 3 == 0)
                {
                    label1.Text = "列：";
                    xy[1] = -1;
                    return;
                }

                if ((col + 1) % 3 == 0)
                    col = (col + 1) / 3;
                else if (col % 3 == 0)
                    col = col / 3;

                label1.Text = "列" + col;
                xy[1] = col;
            }
            else
            {
                int x = e.X;
                col = x / 12 + 1;
                if (col % 3 == 0)
                {
                    label1.Text = "列：";
                    xy[1] = -1;
                    return;
                }

                if ((col - 1) % 3 == 0)
                    col = (col + 2) / 3;
                else if ((col + 1) % 3 == 0)
                    col = (col + 1) / 3;

                label1.Text = "列" + col;
                xy[1] = col;
            }

            if (xy[1] == -1)
                return;

            int newx;
            // 奇数行
            if (row % 2 != 0)
                newx = col * (24 + 12) - 24 + 6;
            else
                newx = (col - 1) * (24 + 12);

            int newy = (row - 1) * 17;

            Graphics gra = this.pictureBox1.CreateGraphics();
            gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush bush = new SolidBrush(Color.Blue);//填充的颜色
            gra.FillEllipse(bush, newx, newy, 24, 24);//
        }

    }
}
