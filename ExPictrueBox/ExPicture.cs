using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExPictrueBox
{
    public partial class ExPictureBox : PictureBox
    {

        private Size _cellSize = new Size(24, 24);
        public Size CellSize
        {
            get { return _cellSize; }
            set { _cellSize = value; }
        }

        public ExPictureBox()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            //this.BackColor = Color.Red;
            //System.Drawing.Drawing2D.GraphicsPath g = new System.Drawing.Drawing2D.GraphicsPath();
            //g.AddEllipse(new Rectangle(0, 0, this.Width, this.Height));
            //this.Region = new Region(g);
            //g.Dispose();

            Graphics gra = this.CreateGraphics();
            gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush bush = new SolidBrush(Color.Green);//填充的颜色
            gra.FillEllipse(bush, 0, 0, 100, 100);//
        }
    }
}
