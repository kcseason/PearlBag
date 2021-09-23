using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Test2
{
    public partial class ImageButton : Control, IButtonControl
    {

        public DialogResult DialogResult { get; set; }

        public void NotifyDefault(bool value) { }
        //
        // Summary:
        //     Generates a System.Windows.Forms.Control.Click event for the control.
        public void PerformClick() { }

        public ImageButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            this.BackColor = Color.Transparent;

        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            //Graphics g = pevent.Graphics;
            //g.DrawRectangle(Pens.Black, this.ClientRectangle);

            Graphics g = pevent.Graphics;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.AntiAlias;//抗锯齿 

            RectangleF rect = new RectangleF(0, 0, 1, 1);
            var brush = new LinearGradientBrush(rect, Color.SkyBlue, Color.Blue, 2);
            g.FillEllipse(brush, rect);
 
        }



        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // don't call the base class
            //base.OnPaintBackground(pevent);
        }


        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_TRANSPARENT = 0x20;
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TRANSPARENT;
                return cp;
            }
        }

        // rest of class here...

    }
}
