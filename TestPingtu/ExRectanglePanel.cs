using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace TestPingtu
{
    public class ExRectanglePanel:Panel,IPanelSet
    {
        private List<AnchorPoint> lstAnchorPoint;
        private int AnchorPointRange;
        private MoveDirect curMoveDirect = MoveDirect.Default;
        private bool isEditing = false;


        private bool _IsEditable = false;
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

        private enum MoveDirect
        {
            LeftTop = 0,
            LeftMiddle,
            LeftBottom,
            Top,
            Bottom,
            RightTop,
            RightMiddle,
            RightBottom,
            Default,
        }

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

        private class AnchorPoint
        {
            public Rectangle AnchorRec;
            public MoveDirect AnchorDirect;
        }


        private Point mouse_offset;

        public ExRectanglePanel()
        {
            AnchorPointRange = 5;
            InitialAnchorPoint();
        }

        public ExRectanglePanel(Color defaultColor,bool isEditable)
        {
            DefaultColor = defaultColor;
            IsEditable = isEditable;

            AnchorPointRange = 5;
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

        private void InitialAnchorPoint()
        {
            lstAnchorPoint = new List<AnchorPoint>();

            AnchorPoint ap = new AnchorPoint()
            {
                AnchorRec = new Rectangle(0, 0, AnchorPointRange, AnchorPointRange),
                AnchorDirect = MoveDirect.LeftTop,
            };
            lstAnchorPoint.Add(ap);

            ap = new AnchorPoint()
            {
                AnchorRec = new Rectangle(0, 0 + this.Height / 2, AnchorPointRange, AnchorPointRange),
                AnchorDirect = MoveDirect.LeftMiddle,
            };
            lstAnchorPoint.Add(ap);

            ap = new AnchorPoint()
            {
                AnchorRec = new Rectangle(0, 0 + this.Height - AnchorPointRange-1, AnchorPointRange, AnchorPointRange),
                AnchorDirect = MoveDirect.LeftBottom,
            };
            lstAnchorPoint.Add(ap);

            ap = new AnchorPoint()
            {
                AnchorRec = new Rectangle(0 + this.Width / 2, 0, AnchorPointRange, AnchorPointRange),
                AnchorDirect = MoveDirect.Top,
            };
            lstAnchorPoint.Add(ap);

            ap = new AnchorPoint()
            {
                AnchorRec = new Rectangle(0 + this.Width / 2, 0 + this.Height - AnchorPointRange - 1, AnchorPointRange, AnchorPointRange),
                AnchorDirect = MoveDirect.Bottom,
            };
            lstAnchorPoint.Add(ap);

            ap = new AnchorPoint()
            {
                AnchorRec = new Rectangle(0 + this.Width - AnchorPointRange - 1, 0, AnchorPointRange, AnchorPointRange),
                AnchorDirect = MoveDirect.RightTop,
            };
            lstAnchorPoint.Add(ap);

            ap = new AnchorPoint()
            {
                AnchorRec = new Rectangle(0 + this.Width - AnchorPointRange - 1, 0 + this.Height / 2, AnchorPointRange, AnchorPointRange),
                AnchorDirect = MoveDirect.RightMiddle,
            };
            lstAnchorPoint.Add(ap);

            ap = new AnchorPoint()
            {
                AnchorRec = new Rectangle(0 + this.Width - AnchorPointRange - 1, 0 + this.Height - AnchorPointRange - 1, AnchorPointRange, AnchorPointRange),
                AnchorDirect = MoveDirect.RightBottom,
            };
            lstAnchorPoint.Add(ap);
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
                Graphics g = this.CreateGraphics();
                g.FillRectangle(new SolidBrush(this.BackColor), new Rectangle(0, 0, this.Width, this.Height));
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
                else if (curMoveDirect == MoveDirect.LeftMiddle || curMoveDirect == MoveDirect.RightMiddle)
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
            else if (curMoveDirect == MoveDirect.LeftMiddle)
            {
                this.Left = this.Left + e.X;
                this.Width = this.Width - e.X;
            }
            else if (curMoveDirect == MoveDirect.RightMiddle)
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

            this.Cursor = Cursors.Default;
        }
    }
}
