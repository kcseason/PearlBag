using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace TestPingtu
{
    public class ExCurvedPanel:Panel,IPanelSet
    {

        private List<AnchorPoint> lstAnchorPoint;
        private int AnchorPointRange;
        private Point[] points;
        private MoveDirect curMoveDirect = MoveDirect.Default;
        private ArrowDirection _arrowDirect;
        private bool isEditing = false;
        private Point mouse_offset;

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

        private bool _IsEditable = true;
        /// <summary>
        /// 控件当前是否处于可编辑状态
        /// </summary>
        public bool IsEditable
        {
            get
            {
                return _IsEditable;
            }
            set
            {
                _IsEditable = value;
            }
        }

        private class AnchorPoint
        {
            public Rectangle AnchorRec;
            public MoveDirect AnchorDirect;
        }

        private enum MoveDirect
        {
            Left = 0,
            LeftTop,
            LeftBottom,
            Top,
            Bottom,
            Right,
            RightTop,
            RightBottom,
            Default,
        }

        /// <summary>
        /// 三角形，箭头所指方向
        /// </summary>
        public enum ArrowDirection
        {
            Up = 0,
            Down,
            Left,
            Right,
            UpLeft,
            UpRight,
            DownLeft,
            DownRight,
        }

        public ExCurvedPanel()
        {
            _arrowDirect = ArrowDirection.DownRight;
            AnchorPointRange = 5;
            InitialVertexes();
            InitialAnchorPoint();
        }

        public ExCurvedPanel(ArrowDirection arrowDirect, Color color,bool isEditable)
        {
            _arrowDirect = arrowDirect;
            DefaultColor = color;
            _IsEditable = isEditable;
            AnchorPointRange = 5;
            InitialVertexes();
            InitialAnchorPoint();
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

        /// <summary>
        /// 初始化三角形的3个顶点坐标
        /// </summary>
        private void InitialVertexes()
        {
            points = new Point[3];
            switch (_arrowDirect)
            {
                case ArrowDirection.UpLeft:
                    points[0].X = this.Width/2;
                    points[0].Y = 0;
                    points[1].X = 0;                //--向左上
                    points[1].Y = 0;
                    points[2].X = 0;
                    points[2].Y = this.Height/2;
                    break;
                case ArrowDirection.DownLeft:
                    points[0].X = 0;                //--向左下
                    points[0].Y = this.Height / 2;
                    points[1].X = 0;
                    points[1].Y = this.Height;
                    points[2].X = this.Width/2;
                    points[2].Y = this.Height;
                    break;
                case ArrowDirection.UpRight:
                    points[0].X = this.Width/2;                //--向右上
                    points[0].Y = 0;
                    points[1].X = this.Width ;
                    points[1].Y = 0;
                    points[2].X = this.Width;
                    points[2].Y = this.Height/2;
                    break;
                default:
                    points[0].X = this.Width/2;                //--向右下
                    points[0].Y = this.Height;
                    points[1].X = this.Width;
                    points[1].Y = this.Height;
                    points[2].X = this.Width;
                    points[2].Y = this.Height/2;
                    break;
            }
        }     

        private void InitialAnchorPoint()
        {
            lstAnchorPoint = new List<AnchorPoint>();

            switch (_arrowDirect)
            {
                case ArrowDirection.UpLeft:
                    {
                        AnchorPoint ap = new AnchorPoint()
                        {
                            AnchorRec = new Rectangle(this.Width/4, 0, AnchorPointRange, AnchorPointRange),
                            AnchorDirect = MoveDirect.Top,
                        };
                        lstAnchorPoint.Add(ap);

                        ap = new AnchorPoint()
                        {
                            AnchorRec = new Rectangle(0, this.Height/4, AnchorPointRange, AnchorPointRange),
                            AnchorDirect = MoveDirect.Left,
                        };
                        lstAnchorPoint.Add(ap);
                        break;
                    }
                case ArrowDirection.UpRight:
                    {
                        AnchorPoint ap = new AnchorPoint()
                        {
                            AnchorRec = new Rectangle(this.Width/4*3, 0, AnchorPointRange, AnchorPointRange),
                            AnchorDirect = MoveDirect.Top,
                        };
                        lstAnchorPoint.Add(ap);

                        ap = new AnchorPoint()
                        {
                            AnchorRec = new Rectangle(this.Width-AnchorPointRange, this.Height/4, AnchorPointRange, AnchorPointRange),
                            AnchorDirect = MoveDirect.Right,
                        };
                        lstAnchorPoint.Add(ap);
                        break;
                    }
                case ArrowDirection.DownLeft:
                    {
                        AnchorPoint ap = new AnchorPoint()
                        {
                            AnchorRec = new Rectangle(0, this.Height/4*3, AnchorPointRange, AnchorPointRange),
                            AnchorDirect = MoveDirect.Left,
                        };
                        lstAnchorPoint.Add(ap);

                        ap = new AnchorPoint()
                        {
                            AnchorRec = new Rectangle(this.Width/4, this.Height - AnchorPointRange, AnchorPointRange, AnchorPointRange),
                            AnchorDirect = MoveDirect.Bottom,
                        };
                        lstAnchorPoint.Add(ap);
                        break;
                    }
                case ArrowDirection.DownRight:
                    {
                        AnchorPoint ap = new AnchorPoint()
                        {
                            AnchorRec = new Rectangle(this.Width/4*3, this.Height-AnchorPointRange, AnchorPointRange, AnchorPointRange),
                            AnchorDirect = MoveDirect.Bottom,
                        };
                        lstAnchorPoint.Add(ap);

                        ap = new AnchorPoint()
                        {
                            AnchorRec = new Rectangle(this.Width-AnchorPointRange, this.Height/4*3, AnchorPointRange, AnchorPointRange),
                            AnchorDirect = MoveDirect.Right,
                        };
                        lstAnchorPoint.Add(ap);

                        ap = new AnchorPoint()
                        {
                            AnchorRec = new Rectangle(this.Width - AnchorPointRange, this.Height-AnchorPointRange, AnchorPointRange, AnchorPointRange),
                            AnchorDirect = MoveDirect.RightBottom,
                        };
                        lstAnchorPoint.Add(ap);
                        break;
                    }
                default:
                    break;
            }
        }


        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Graphics gc = e.Graphics;

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Rectangle newRectangle = this.ClientRectangle;
            gc.DrawPolygon(System.Drawing.Pens.Transparent,points);

            path.AddLine(points[0], points[1]);
            path.AddLine(points[1], points[2]);
            switch (_arrowDirect)
            {
                case ArrowDirection.UpLeft:
                    {
                        path.AddArc(0, 0, this.Width, this.Height, 180, 90);
                        break;
                    }
                case ArrowDirection.UpRight:
                    {
                        path.AddArc(0, 0, this.Width, this.Height, 270, 90);
                        break;
                    }
                case ArrowDirection.DownLeft:
                    {
                        path.AddArc(0, 0, this.Width, this.Height, 90, 90);
                        break;
                    }
                default:
                    {
                        path.AddArc(0, 0, this.Width, this.Height, 0,90);
                        break;
                    }
            }
            
            gc.DrawPath(System.Drawing.Pens.Black, path);
            this.Region = new System.Drawing.Region(path);
        }  

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            if (_IsEditable)
            {
                Graphics g = this.CreateGraphics();
                foreach (AnchorPoint ap in lstAnchorPoint)
                {
                    g.FillRectangle(Brushes.White, ap.AnchorRec);
                    g.DrawRectangle(Pens.Black, ap.AnchorRec);
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (_IsEditable)
            {
                Graphics gc = this.CreateGraphics();
                /// 设置绘图的颜色
                Brush brush = new SolidBrush(this._DefaultColor);
                gc.FillPolygon(brush, points);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!isEditing)
            {
                curMoveDirect = MoveDirect.Default;

                foreach (AnchorPoint ap in lstAnchorPoint)
                {
                    if (e.X > ap.AnchorRec.Left - AnchorPointRange && e.X < ap.AnchorRec.Left + ap.AnchorRec.Width + AnchorPointRange
                        && e.Y > ap.AnchorRec.Top - AnchorPointRange && e.Y < ap.AnchorRec.Top + ap.AnchorRec.Height + AnchorPointRange)
                    {
                        curMoveDirect = ap.AnchorDirect;
                        break;
                    }
                }

                if (curMoveDirect == MoveDirect.Default)
                    this.Cursor = Cursors.SizeAll;
                else if (curMoveDirect == MoveDirect.Top || curMoveDirect == MoveDirect.Bottom)
                    this.Cursor = Cursors.SizeNS;
                else if (curMoveDirect == MoveDirect.Left || curMoveDirect == MoveDirect.Right)
                    this.Cursor = Cursors.SizeWE;
                else if (curMoveDirect == MoveDirect.LeftTop || curMoveDirect == MoveDirect.RightBottom)
                    this.Cursor = Cursors.SizeNWSE;
                else if (curMoveDirect == MoveDirect.LeftBottom || curMoveDirect == MoveDirect.RightTop)
                    this.Cursor = Cursors.SizeNESW;

            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (_IsEditable)
            {
                isEditing = true;
                mouse_offset = new Point(-e.X, -e.Y);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (!_IsEditable)
                return;

            if (e.Button != System.Windows.Forms.MouseButtons.Left)
                return;

            if (this.Cursor == Cursors.SizeAll)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                ((Control)this).Location = ((Control)this).Parent.PointToClient(mousePos);
            }
            else if (curMoveDirect == MoveDirect.Top)
            {
                this.Top = this.Top + e.Y;
                this.Height = this.Height - e.Y;
            }
            else if (curMoveDirect == MoveDirect.Bottom)
            {
                this.Height = e.Y;
            }
            else if (curMoveDirect == MoveDirect.Left)
            {
                this.Left = this.Left + e.X;
                this.Width = this.Width - e.X;
            }
            else if (curMoveDirect == MoveDirect.Right)
            {
                this.Width = e.X;
            }
            else if (curMoveDirect == MoveDirect.LeftTop)
            {
                this.Left = this.Left + e.X;
                this.Top = this.Top + e.Y;
                this.Width = this.Width - e.X;
                this.Height = this.Height - e.Y;
            }
            else if (curMoveDirect == MoveDirect.LeftBottom)
            {
                this.Left = this.Left + e.X;
                this.Width = this.Width - e.X;
                this.Height = e.Y;
            }
            else if (curMoveDirect == MoveDirect.RightTop)
            {
                this.Top = this.Top + e.Y;
                this.Height = this.Height - e.Y;
                this.Width = e.X;
            }
            else if (curMoveDirect == MoveDirect.RightBottom)
            {
                this.Width = e.X;
                this.Height = e.Y;
            }

            curMoveDirect = MoveDirect.Default;
            isEditing = false;
            InitialAnchorPoint();
            InitialVertexes();
            OnPaint(new PaintEventArgs(this.CreateGraphics(), new Rectangle()));
            this.Cursor = Cursors.Default;
        }
    }
}
