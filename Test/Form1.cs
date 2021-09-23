using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestPingtu;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //PictureBox pic = new PictureBox();
            //pic.Location = new Point(50, 50);
            //pic.Size = new Size(150, 150);
            //pic.BackColor = Color.Red;
            //System.Drawing.Drawing2D.GraphicsPath g = new System.Drawing.Drawing2D.GraphicsPath();
            //g.AddEllipse(new Rectangle(0, 0, 150, 150));
            //pic.Region = new Region(g);
            //g.Dispose();
            //panel1.Controls.Add(pic);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            panel1.Controls.Clear();
            int local_X = 0;
            int local_Y = 0;
            for (int row = 0; row < 20; row++)
            {
                local_X = 0;
                for (int i = 0; i < 30; i++)
                {
                    ExCirclePanel pl = new ExCirclePanel();
                    pl.BackColor = Color.Blue;
                    pl.Size = new Size(24, 24);

                    pl.Location = new Point(local_X, local_Y);

                    panel1.Controls.Add(pl);

                    local_X += 24;
                }

                local_Y += 24;
            }
            DateTime end = DateTime.Now;
            TimeSpan ts = start.Subtract(end);
            tb_panel.Text = ts.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            panel1.Controls.Clear();
            int local_X = 0;
            int local_Y = 0;
            for (int row = 0; row < 31; row++)
            {
                local_X = 0;
                for (int i = 0; i < 23; i++)
                {
                    ExPictureBox pl = new ExPictureBox();
                    pl.BackColor = Color.Blue;
                    pl.Size = new Size(24, 24);

                    pl.Location = new Point(local_X, local_Y);

                    panel1.Controls.Add(pl);

                    local_X += 24;
                }

                local_Y += 24;
            }
            DateTime end = DateTime.Now;
            TimeSpan ts = start.Subtract(end);
            tb_button.Text = ts.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Graphics g = this.pl_test.CreateGraphics();
            //Rectangle rect = new Rectangle(ClientRectangle.Width / 2 - 80, ClientRectangle.Height / 2 - 80, 160, 160);
            //Pen p = new Pen(Color.Black);
            //g.DrawEllipse(p, rect);
            //Brush b = new SolidBrush(Color.Black);
            //g.FillEllipse(b, rect);

            //pictureBox1.BackColor = Color.Red;
            //System.Drawing.Drawing2D.GraphicsPath g = new System.Drawing.Drawing2D.GraphicsPath();
            //g.AddEllipse(new Rectangle(0, 0, 100, 100));
            //pictureBox1.Region = new Region(g);
            //g.Dispose();

            //pictureBox2.BackColor = Color.Red;
            //System.Drawing.Drawing2D.GraphicsPath g2 = new System.Drawing.Drawing2D.GraphicsPath();
            //g2.AddEllipse(new Rectangle(0, 0, 100, 100));
            //pictureBox2.Region = new Region(g2);
            //g2.Dispose();

            //pictureBox3.BackColor = Color.Red;
            //System.Drawing.Drawing2D.GraphicsPath g3 = new System.Drawing.Drawing2D.GraphicsPath();
            //g3.AddEllipse(new Rectangle(0, 0, 100, 100));
            //pictureBox3.Region = new Region(g3);
            //g3.Dispose();

            //Graphics gra = this.pictureBox1.CreateGraphics();
            //gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //Brush bush = new SolidBrush(Color.Green);//填充的颜色
            //gra.FillEllipse(bush, 0, 0, 100, 100);//画填充椭圆的方法，x坐标、y坐标、宽、高，如果是100，则半径为50

            //Graphics gra2 = this.pictureBox2.CreateGraphics();
            //gra2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //Brush bush2 = new SolidBrush(Color.Green);//填充的颜色
            //gra2.FillEllipse(bush2, 0, 0, 100, 100);//画填充椭圆的方法，x坐标、y坐标、宽、高，如果是100，则半径为50

            //Graphics gra3 = this.pictureBox3.CreateGraphics();
            //gra3.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //Brush bush3 = new SolidBrush(Color.Green);//填充的颜色
            //gra3.FillEllipse(bush3, 0, 0, 100, 100);//画填充椭圆的方法，x坐标、y坐标、宽、高，如果是100，则半径为50
        }

        private async void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.BackColor = Color.Red;
            System.Drawing.Drawing2D.GraphicsPath g = new System.Drawing.Drawing2D.GraphicsPath();
            g.AddEllipse(new Rectangle(0, 0, 10, 10));
            pictureBox1.Region = new Region(g);
            g.Dispose();

            System.Drawing.Drawing2D.GraphicsPath g2 = new System.Drawing.Drawing2D.GraphicsPath();
            g2.AddEllipse(new Rectangle(10, 10, 10, 10));
            pictureBox1.Region = new Region(g2);
            g2.Dispose();

        }

    }
}
