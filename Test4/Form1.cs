using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool bDrawStart = false;
        Point pointStart = Point.Empty;
        Point pointContinue = Point.Empty;
        Dictionary<Point, Point> dicPoints = new Dictionary<Point, Point>();

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (bDrawStart)
            {
                bDrawStart = false;
            }
            else
            {
                bDrawStart = true;
                pointStart = e.Location;
            }

        }



        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (bDrawStart)
            {
                pointContinue = e.Location;
                Refresh();
            }

        }



        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (bDrawStart)
            {
                dicPoints.Add(pointStart, pointContinue);

            }
            bDrawStart = false;
        }



        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Pen pen = new System.Drawing.Pen(Color.LimeGreen);
            pen.Width = 2;
            if (bDrawStart)
            {
                Graphics gra = e.Graphics;
                gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Brush bush = new SolidBrush(Color.Green);//填充的颜色
                gra.FillEllipse(bush, pointStart.X, pointStart.Y, pointContinue.X - pointStart.X, pointContinue.Y - pointStart.Y);
                //gra.Dispose();
            }
            //实时的画之前已经画好的矩形
            foreach (var item in dicPoints)
            {
                Graphics gra = e.Graphics;
                gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Brush bush = new SolidBrush(Color.Green);//填充的颜色
                Point p1 = item.Key;
                Point p2 = item.Value;
                gra.FillEllipse(bush, p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
                //gra.Dispose();
            }
            pen.Dispose();
        }
    }
}
