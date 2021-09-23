using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TestPingtu
{
    public class ExCirclePanel : Panel, IPanelSet
    {
        //private List<AnchorPoint> lstAnchorPoint;
        //private int AnchorPointRange;
        //private MoveDirect curMoveDirect = MoveDirect.Default;
        //private bool isEditing = false;
        //private bool _IsEditable = false;

        /// <summary>
        /// 控件当前是否处于可编辑状态
        /// </summary>
        //public bool IsEditable
        //{
        //    get
        //    {
        //        return _IsEditable;
        //    }
        //    set
        //    {
        //        _IsEditable = value;
        //    }
        //}

        //private enum MoveDirect
        //{
        //    Left = 0,
        //    Right,
        //    Top,
        //    Bottom,
        //    Default,
        //}

        //private class AnchorPoint
        //{
        //    public Rectangle AnchorRec;
        //    public MoveDirect AnchorDirect;
        //}

        private Color _DefaultColor;
        /// <summary>
        /// 默认背影色
        /// </summary>
        public Color DefaultColor
        {
            get
            {
                return _DefaultColor;
            }
            set
            {
                this.BackColor = value;
                _DefaultColor = value;
            }
        }

        //private Point mouse_offset;

        public ExCirclePanel()
        {
            this.Height = 100;
            this.Width = 100;
            //AnchorPointRange = 5;
            //InitialAnchorPoint();
        }

        public ExCirclePanel(Color defaultColor, bool isEditable, Size size)
        {
            DefaultColor = defaultColor;
            //IsEditable = isEditable;
            this.Height = size.Height;
            this.Width = size.Width;
            //AnchorPointRange = 5;
            //InitialAnchorPoint();
        }

        public void SetBackColor(Color backColor)
        {
            this.DefaultColor = backColor;
        }

        public void SetBoldShow(bool isShow)
        {
            if (isShow)
                this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            else
                this.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        //private void InitialAnchorPoint()
        //{
        //    lstAnchorPoint = new List<AnchorPoint>();

        //    AnchorPoint ap = new AnchorPoint()
        //    {
        //        AnchorRec = new Rectangle(0, this.Height / 2 - AnchorPointRange / 2, AnchorPointRange, AnchorPointRange),
        //        AnchorDirect = MoveDirect.Left,
        //    };
        //    lstAnchorPoint.Add(ap);

        //    ap = new AnchorPoint()
        //    {
        //        AnchorRec = new Rectangle(this.Width - AnchorPointRange, this.Height / 2 - AnchorPointRange / 2, AnchorPointRange, AnchorPointRange),
        //        AnchorDirect = MoveDirect.Right,
        //    };
        //    lstAnchorPoint.Add(ap);

        //    ap = new AnchorPoint()
        //    {
        //        AnchorRec = new Rectangle(this.Width / 2 - AnchorPointRange / 2, 0, AnchorPointRange, AnchorPointRange),
        //        AnchorDirect = MoveDirect.Top,
        //    };
        //    lstAnchorPoint.Add(ap);

        //    ap = new AnchorPoint()
        //    {
        //        AnchorRec = new Rectangle(this.Width / 2 - AnchorPointRange / 2, this.Height - AnchorPointRange - 1, AnchorPointRange, AnchorPointRange),
        //        AnchorDirect = MoveDirect.Bottom,
        //    };
        //    lstAnchorPoint.Add(ap);
        //}

        //protected override void OnMouseEnter(EventArgs e)
        //{
        //    base.OnMouseEnter(e);

        //    //if (_IsEditable)
        //    //{
        //    //    Graphics g = this.CreateGraphics();
        //    //    foreach (AnchorPoint ap in lstAnchorPoint)
        //    //    {
        //    //        g.FillRectangle(Brushes.White, ap.AnchorRec);
        //    //        g.DrawRectangle(Pens.Black, ap.AnchorRec);
        //    //    }
        //    //}
        //}

        //protected override void OnMouseLeave(EventArgs e)
        //{
        //    base.OnMouseLeave(e);

        //    if (_IsEditable)
        //    {
        //        Graphics g = this.CreateGraphics();
        //        g.FillRectangle(new SolidBrush(this.BackColor), new Rectangle(0, 0, this.Width, this.Height));
        //    }
        //}

        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    base.OnMouseMove(e);
        //    if (!isEditing)
        //    {
        //        curMoveDirect = MoveDirect.Default;

        //        foreach (AnchorPoint ap in lstAnchorPoint)
        //        {
        //            if (e.X > ap.AnchorRec.Left - AnchorPointRange && e.X < ap.AnchorRec.Left + ap.AnchorRec.Width + AnchorPointRange
        //                && e.Y > ap.AnchorRec.Top - AnchorPointRange && e.Y < ap.AnchorRec.Top + ap.AnchorRec.Height + AnchorPointRange)
        //            {
        //                curMoveDirect = ap.AnchorDirect;
        //                break;
        //            }
        //        }

        //        if (curMoveDirect == MoveDirect.Default)
        //            this.Cursor = Cursors.SizeAll;
        //        else if (curMoveDirect == MoveDirect.Top || curMoveDirect == MoveDirect.Bottom)
        //            this.Cursor = Cursors.SizeNS;
        //        else if (curMoveDirect == MoveDirect.Left || curMoveDirect == MoveDirect.Right)
        //            this.Cursor = Cursors.SizeWE;

        //    }
        //}

        //protected override void OnMouseDown(MouseEventArgs e)
        //{
        //    base.OnMouseDown(e);

        //    if (_IsEditable)
        //    {
        //        isEditing = true;
        //        mouse_offset = new Point(-e.X, -e.Y);
        //    }
        //}

        //protected override void OnMouseUp(MouseEventArgs e)
        //{
        //    base.OnMouseUp(e);

        //    if (!_IsEditable)
        //        return;

        //    if (e.Button != System.Windows.Forms.MouseButtons.Left)
        //        return;

        //    if (this.Cursor == Cursors.SizeAll)
        //    {
        //        Point mousePos = Control.MousePosition;
        //        mousePos.Offset(mouse_offset.X, mouse_offset.Y);
        //        ((Control)this).Location = ((Control)this).Parent.PointToClient(mousePos);
        //    }
        //    else if (curMoveDirect == MoveDirect.Top)
        //    {
        //        this.Top = this.Top + e.Y;
        //        this.Height = this.Height - e.Y;
        //    }
        //    else if (curMoveDirect == MoveDirect.Bottom)
        //    {
        //        this.Height = e.Y;
        //    }
        //    else if (curMoveDirect == MoveDirect.Left)
        //    {
        //        this.Left = this.Left + e.X;
        //        this.Width = this.Width - e.X;
        //    }
        //    else if (curMoveDirect == MoveDirect.Right)
        //    {
        //        this.Width = e.X;
        //    }

        //    curMoveDirect = MoveDirect.Default;
        //    isEditing = false;
        //    InitialAnchorPoint();
        //    OnPaint(new PaintEventArgs(this.CreateGraphics(), new Rectangle()));
        //    this.Cursor = Cursors.Default;
        //}

        protected override void OnPaint(PaintEventArgs pevent)
        {
            //base.OnPaint(pevent);
            //System.Drawing.Drawing2D.GraphicsPath g = new System.Drawing.Drawing2D.GraphicsPath();
            //System.Drawing.Rectangle newRectangle = this.ClientRectangle;
            //newRectangle.Inflate(-1, -1);
            //pevent.Graphics.DrawEllipse(System.Drawing.Pens.Transparent, newRectangle);
            //newRectangle.Inflate(1, 1);
            //g.AddEllipse(newRectangle);
            //this.Region = new System.Drawing.Region(g);

            //this.BackColor = Color.Red;
            //System.Drawing.Drawing2D.GraphicsPath g = new System.Drawing.Drawing2D.GraphicsPath();
            //g.AddEllipse(new Rectangle(0, 0, this.Width, this.Height));
            //this.Region = new Region(g);
            //g.Dispose();

            //base.OnPaint(pevent);
            //Graphics g = pevent.Graphics;
            //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //g.SmoothingMode = SmoothingMode.AntiAlias;//抗锯齿  
            //System.Drawing.Rectangle newRectangle = this.ClientRectangle;
            //newRectangle.Inflate(-2, -2);
            //pevent.Graphics.DrawEllipse(System.Drawing.Pens.Transparent, newRectangle);
            //newRectangle.Inflate(1, 1);
            //var brush = new LinearGradientBrush(newRectangle, Color.SkyBlue, Color.Blue, 90);
            //g.FillEllipse(brush, newRectangle);
            //this.Region = new System.Drawing.Region(g);

            base.OnPaint(pevent);

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Rectangle newRectangle = this.ClientRectangle;
            newRectangle.Inflate(-1, -1);
            pevent.Graphics.DrawEllipse(System.Drawing.Pens.Transparent, newRectangle);
            newRectangle.Inflate(1, 1);
            path.AddEllipse(newRectangle);
            this.Region = new System.Drawing.Region(path);
        }
    }
}
