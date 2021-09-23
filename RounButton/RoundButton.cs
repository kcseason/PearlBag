using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace RoundButton.Forms
{
    public partial class RoundButton : Button
    {
        #region --成员变量--

        RectangleF rect = new RectangleF();//控件矩形
        bool mouseEnter;//鼠标是否进入控件区域的标志
        bool buttonPressed;//按钮是否按下
        bool buttonClicked;//按钮是否被点击
        #endregion

        #region --属性--

        #region 形状

        /// <summary>
        /// 设置或获取圆形按钮的圆的边距离方框边的距离
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        [Category("Appearance")]
        public int DistanceToBorder { get; set; }

        #endregion

        #region 填充色

        /// <summary>
        /// 获取或设置按钮主体颜色
        /// </summary>
        /// <value>The color of the focus.</value>
        [Browsable(true), DefaultValue(typeof(Color), "DodgerBlue"), Description("按钮主体渐变起始颜色")]
        [Category("Appearance")]
        public Color ButtonCenterColorEnd { get; set; }

        /// <summary>
        /// 获取或设置按钮主体颜色
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Color), "CornflowerBlue"), Description("按钮主体渐变终点颜色")]
        [Category("Appearance")]
        public Color ButtonCenterColorStart { get; set; }

        /// <summary>
        /// 获取或设置按钮主体颜色渐变方向
        /// </summary>
        [Browsable(true), DefaultValue(90), Description("按钮主体颜色渐变方向，X轴顺时针开始")]
        [Category("Appearance")]
        public int GradientAngle { get; set; }

        #endregion

        #region 边框

        /// <summary>
        /// 获取或设置边框大小
        /// </summary>
        [Browsable(true), DefaultValue(0), Description("按钮边框大小")]
        [Category("Appearance")]
        public int BorderWidth { get; set; }

        /// <summary>
        /// 获取或设置按钮边框颜色
        /// </summary>
        /// <value>The color of the focus.</value>
        [Browsable(true), DefaultValue(typeof(Color), "Black"), Description("按钮边框颜色")]
        [Category("Appearance")]
        public Color BorderColor { get; set; }

        /// <summary>
        /// 获取或设置边框透明度
        /// </summary>
        [Browsable(true), DefaultValue(255), Description("设置边框透明度：0-255")]
        [Category("Appearance")]
        public int BorderTransparent { get; set; }

        /// <summary>
        /// 获取或设置按钮获取焦点后边框颜色
        /// </summary>
        /// <value>The color of the focus.</value>
        [Browsable(true), DefaultValue(typeof(Color), "Orange"), Description("按钮获得焦点后的边框颜色")]
        [Category("Appearance")]
        public Color FocusBorderColor { get; set; }

        #endregion

        #endregion

        #region --构造函数--
        /// <summary>
        /// 构造函数
        /// </summary>
        public RoundButton()
        {
            // 控件风格
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            //初始值设定
            this.Height = this.Width = 80;

            this.BackColor = Color.Transparent;

            DistanceToBorder = 0;
            ButtonCenterColorStart = Color.CornflowerBlue;
            ButtonCenterColorEnd = Color.DodgerBlue;
            BorderColor = Color.Black;
            FocusBorderColor = Color.Orange;
            BorderWidth = 0;
            BorderTransparent = 200;
            GradientAngle = 90;

            mouseEnter = false;
            buttonPressed = false;
            buttonClicked = false;

            InitializeComponent();
        }
        #endregion

        #region --重写部分事件--

        #region OnPaint事件

        /// <summary>
        /// 控件绘制
        /// </summary>
        /// <param name="pevent"></param>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            //base.OnPaint(pevent);
            base.OnPaintBackground(pevent);

            Graphics g = pevent.Graphics;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.AntiAlias;//抗锯齿         

            myResize();//调整圆形区域

            var brush = new LinearGradientBrush(rect, ButtonCenterColorStart, ButtonCenterColorEnd, GradientAngle);

            PaintShape(g, brush, rect);//绘制按钮中心区域

            DrawBorder(g);//绘制边框            

            if (mouseEnter)
            {
                DrawHighLight(g);//绘制高亮区域
            }

        }

        #endregion

        #region 鼠标

        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            buttonClicked = !buttonClicked;
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button != MouseButtons.Left) return;
            buttonPressed = false;
            base.Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left) return;
            buttonPressed = true;
        }

        /// <summary>
        /// 鼠标进入按钮
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            mouseEnter = true;
        }

        /// <summary>
        /// 鼠标离开控件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            mouseEnter = false;
        }

        #endregion
        #endregion

        #region --自定义函数--

        /// <summary>
        /// 重新确定控件大小
        /// </summary>
        protected void myResize()
        {
            int x = DistanceToBorder;
            int y = DistanceToBorder;
            int width = this.Width - 2 * DistanceToBorder;
            int height = this.Height - 2 * DistanceToBorder;
            rect = new RectangleF(x, y, width, height);
        }

        /// <summary>
        /// 绘制高亮效果
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void DrawHighLight(Graphics g)
        {
            RectangleF highlightRect = rect;
            highlightRect.Inflate(-BorderWidth / 2, -BorderWidth / 2);
            Brush brush = Brushes.DodgerBlue;
            if (buttonPressed)
            {
                brush = new LinearGradientBrush(rect, ButtonCenterColorStart, ButtonCenterColorEnd, GradientAngle);
            }

            else
            {
                brush = new LinearGradientBrush(rect, Color.FromArgb(60, Color.White),
              Color.FromArgb(60, Color.White), GradientAngle);
            }
            PaintShape(g, brush, highlightRect);
        }

        /// <summary>
        /// 绘制边框
        /// </summary>
        /// <param name="g">Graphics对象</param>
        protected virtual void DrawBorder(Graphics g)
        {
            Pen p = new Pen(BorderColor);
            if (Focused)
            {
                p.Color = FocusBorderColor;//外圈获取焦点后的颜色
                p.Width = BorderWidth;
                PaintShape(g, p, rect);
            }
            else
            {
                p.Width = BorderWidth;
                PaintShape(g, p, rect);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void DrawPressState(Graphics g)
        {
            RectangleF pressedRect = rect;
            pressedRect.Inflate(-2, -2);
            Brush brush = new LinearGradientBrush(rect, Color.FromArgb(60, Color.White),
                Color.FromArgb(60, Color.White), GradientAngle);
            PaintShape(g, brush, pressedRect);
        }

        /// <summary>
        /// 绘制图形
        /// </summary>
        /// <param name="g">Graphics对象</param>
        /// <param name="pen">Pen对象</param>
        /// <param name="rect">RectangleF对象</param>
        protected virtual void PaintShape(Graphics g, Pen pen, RectangleF rect)
        {
            g.DrawEllipse(pen, rect);
        }

        /// <summary>
        /// 绘制图形 
        /// </summary>
        /// <param name="g">Graphics对象</param>
        /// <param name="brush">Brush对象</param>
        /// <param name="rect">Rectangle对象</param>
        protected virtual void PaintShape(Graphics g, Brush brush, RectangleF rect)
        {
            g.FillEllipse(brush, rect);

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

        #endregion
    }
}
