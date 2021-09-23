using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestPingtu
{
    public partial class Form1 : Form
    {
        Color defaultColor = Color.Blue;
        object curSelectedObj;

        public Form1()
        {
            InitializeComponent();
        }

        private void miRect_Click(object sender, EventArgs e)
        {
            ExRectanglePanel rect = new ExRectanglePanel(defaultColor,true);
            pbDraw.Controls.Add(rect);
            rect.ContextMenuStrip = contextMenuStrip1;
            rect.MouseDown += new MouseEventHandler(MouseDown);
        }

        private void miCircle_Click(object sender, EventArgs e)
        {
            ExCirclePanel circ = new ExCirclePanel(defaultColor, true);
            pbDraw.Controls.Add(circ);
            circ.ContextMenuStrip = contextMenuStrip1;
            circ.MouseDown += new MouseEventHandler(MouseDown);
        }

        private void miTriTop_Click(object sender, EventArgs e)
        {
            ExTrianglePanel tri = new ExTrianglePanel(ExTrianglePanel.ArrowDirection.Up, defaultColor, true);
            pbDraw.Controls.Add(tri);
            tri.ContextMenuStrip = contextMenuStrip1;
            tri.MouseDown += new MouseEventHandler(MouseDown);
        }

        private void miTriBot_Click(object sender, EventArgs e)
        {
            ExTrianglePanel tri = new ExTrianglePanel(ExTrianglePanel.ArrowDirection.Down, defaultColor, true);
            pbDraw.Controls.Add(tri);
            tri.ContextMenuStrip = contextMenuStrip1;
            tri.MouseDown += new MouseEventHandler(MouseDown);
        }

        private void miTriLeft_Click(object sender, EventArgs e)
        {
            ExTrianglePanel tri = new ExTrianglePanel(ExTrianglePanel.ArrowDirection.Left, defaultColor, true);
            pbDraw.Controls.Add(tri);
            tri.ContextMenuStrip = contextMenuStrip1;
            tri.MouseDown += new MouseEventHandler(MouseDown);
        }

        private void miTriRight_Click(object sender, EventArgs e)
        {
            ExTrianglePanel tri = new ExTrianglePanel(ExTrianglePanel.ArrowDirection.Right, defaultColor, true);
            pbDraw.Controls.Add(tri);
            tri.ContextMenuStrip = contextMenuStrip1;
            tri.MouseDown += new MouseEventHandler(MouseDown);
        }

        private void miTriLeftTop_Click(object sender, EventArgs e)
        {
            ExTrianglePanel tri = new ExTrianglePanel(ExTrianglePanel.ArrowDirection.UpLeft, defaultColor, true);
            pbDraw.Controls.Add(tri);
            tri.ContextMenuStrip = contextMenuStrip1;
            tri.MouseDown += new MouseEventHandler(MouseDown);
        }

        private void miTriLeftBot_Click(object sender, EventArgs e)
        {
            ExTrianglePanel tri = new ExTrianglePanel(ExTrianglePanel.ArrowDirection.DownLeft, defaultColor, true);
            pbDraw.Controls.Add(tri);
            tri.ContextMenuStrip = contextMenuStrip1;
            tri.MouseDown += new MouseEventHandler(MouseDown);
        }

        private void miTriRightTop_Click(object sender, EventArgs e)
        {
            ExTrianglePanel tri = new ExTrianglePanel(ExTrianglePanel.ArrowDirection.UpRight, defaultColor, true);
            pbDraw.Controls.Add(tri);
            tri.ContextMenuStrip = contextMenuStrip1;
            tri.MouseDown += new MouseEventHandler(MouseDown);
        }

        private void miTriRightBot_Click(object sender, EventArgs e)
        {
            ExTrianglePanel tri = new ExTrianglePanel(ExTrianglePanel.ArrowDirection.DownRight, defaultColor, true);
            pbDraw.Controls.Add(tri);
            tri.ContextMenuStrip = contextMenuStrip1;
            tri.MouseDown += new MouseEventHandler(MouseDown);
        }

        private void miCurvedLeftTop_Click(object sender, EventArgs e)
        {
            ExCurvedPanel curv = new ExCurvedPanel(ExCurvedPanel.ArrowDirection.UpLeft, defaultColor, true);
            pbDraw.Controls.Add(curv);
            curv.ContextMenuStrip = contextMenuStrip1;
            curv.MouseDown += new MouseEventHandler(MouseDown);
        }

        private void miCurvedLeftBot_Click(object sender, EventArgs e)
        {
            ExCurvedPanel curv = new ExCurvedPanel(ExCurvedPanel.ArrowDirection.DownLeft, defaultColor, true);
            pbDraw.Controls.Add(curv);
            curv.ContextMenuStrip = contextMenuStrip1;
            curv.MouseDown += new MouseEventHandler(MouseDown);
        }

        private void miCurvedRightTop_Click(object sender, EventArgs e)
        {
            ExCurvedPanel curv = new ExCurvedPanel(ExCurvedPanel.ArrowDirection.UpRight, defaultColor, true);
            pbDraw.Controls.Add(curv);
            curv.ContextMenuStrip = contextMenuStrip1;
            curv.MouseDown += new MouseEventHandler(MouseDown);
        }

        private void miCurvedRightBot_Click(object sender, EventArgs e)
        {
            ExCurvedPanel curv = new ExCurvedPanel(ExCurvedPanel.ArrowDirection.DownRight, defaultColor, true);
            pbDraw.Controls.Add(curv);
            curv.ContextMenuStrip = contextMenuStrip1;
            curv.MouseDown += new MouseEventHandler(MouseDown);
        }

        private void miSet_Click(object sender, EventArgs e)
        {
            SetForm setform = new SetForm();
            if (setform.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            IPanelSet set = curSelectedObj as IPanelSet;
            if (set == null)
                return;

            set.SetBackColor(setform.DefaultColor);
            set.SetBoldShow(setform.IsShowBold);
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Right)
                return;

            curSelectedObj = sender;
        }


    }
}
