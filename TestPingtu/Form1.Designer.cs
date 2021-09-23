namespace TestPingtu
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miRect = new System.Windows.Forms.ToolStripMenuItem();
            this.miCircle = new System.Windows.Forms.ToolStripMenuItem();
            this.三角ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miTriTop = new System.Windows.Forms.ToolStripMenuItem();
            this.miTriBot = new System.Windows.Forms.ToolStripMenuItem();
            this.miTriLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.miTriRight = new System.Windows.Forms.ToolStripMenuItem();
            this.miTriLeftTop = new System.Windows.Forms.ToolStripMenuItem();
            this.miTriLeftBot = new System.Windows.Forms.ToolStripMenuItem();
            this.miTriRightTop = new System.Windows.Forms.ToolStripMenuItem();
            this.miTriRightBot = new System.Windows.Forms.ToolStripMenuItem();
            this.弧形三角ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCurvedLeftTop = new System.Windows.Forms.ToolStripMenuItem();
            this.miCurvedLeftBot = new System.Windows.Forms.ToolStripMenuItem();
            this.miCurvedRightTop = new System.Windows.Forms.ToolStripMenuItem();
            this.miCurvedRightBot = new System.Windows.Forms.ToolStripMenuItem();
            this.pbDraw = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miSet = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDraw)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miRect,
            this.miCircle,
            this.三角ToolStripMenuItem,
            this.弧形三角ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(901, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miRect
            // 
            this.miRect.Name = "miRect";
            this.miRect.Size = new System.Drawing.Size(44, 21);
            this.miRect.Text = "矩形";
            this.miRect.Click += new System.EventHandler(this.miRect_Click);
            // 
            // miCircle
            // 
            this.miCircle.Name = "miCircle";
            this.miCircle.Size = new System.Drawing.Size(44, 21);
            this.miCircle.Text = "圆形";
            this.miCircle.Click += new System.EventHandler(this.miCircle_Click);
            // 
            // 三角ToolStripMenuItem
            // 
            this.三角ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miTriTop,
            this.miTriBot,
            this.miTriLeft,
            this.miTriRight,
            this.miTriLeftTop,
            this.miTriLeftBot,
            this.miTriRightTop,
            this.miTriRightBot});
            this.三角ToolStripMenuItem.Name = "三角ToolStripMenuItem";
            this.三角ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.三角ToolStripMenuItem.Text = "三角形";
            // 
            // miTriTop
            // 
            this.miTriTop.Name = "miTriTop";
            this.miTriTop.Size = new System.Drawing.Size(100, 22);
            this.miTriTop.Text = "上";
            this.miTriTop.Click += new System.EventHandler(this.miTriTop_Click);
            // 
            // miTriBot
            // 
            this.miTriBot.Name = "miTriBot";
            this.miTriBot.Size = new System.Drawing.Size(100, 22);
            this.miTriBot.Text = "下";
            this.miTriBot.Click += new System.EventHandler(this.miTriBot_Click);
            // 
            // miTriLeft
            // 
            this.miTriLeft.Name = "miTriLeft";
            this.miTriLeft.Size = new System.Drawing.Size(100, 22);
            this.miTriLeft.Text = "左";
            this.miTriLeft.Click += new System.EventHandler(this.miTriLeft_Click);
            // 
            // miTriRight
            // 
            this.miTriRight.Name = "miTriRight";
            this.miTriRight.Size = new System.Drawing.Size(100, 22);
            this.miTriRight.Text = "右";
            this.miTriRight.Click += new System.EventHandler(this.miTriRight_Click);
            // 
            // miTriLeftTop
            // 
            this.miTriLeftTop.Name = "miTriLeftTop";
            this.miTriLeftTop.Size = new System.Drawing.Size(100, 22);
            this.miTriLeftTop.Text = "左上";
            this.miTriLeftTop.Click += new System.EventHandler(this.miTriLeftTop_Click);
            // 
            // miTriLeftBot
            // 
            this.miTriLeftBot.Name = "miTriLeftBot";
            this.miTriLeftBot.Size = new System.Drawing.Size(100, 22);
            this.miTriLeftBot.Text = "左下";
            this.miTriLeftBot.Click += new System.EventHandler(this.miTriLeftBot_Click);
            // 
            // miTriRightTop
            // 
            this.miTriRightTop.Name = "miTriRightTop";
            this.miTriRightTop.Size = new System.Drawing.Size(100, 22);
            this.miTriRightTop.Text = "右上";
            this.miTriRightTop.Click += new System.EventHandler(this.miTriRightTop_Click);
            // 
            // miTriRightBot
            // 
            this.miTriRightBot.Name = "miTriRightBot";
            this.miTriRightBot.Size = new System.Drawing.Size(100, 22);
            this.miTriRightBot.Text = "右下";
            this.miTriRightBot.Click += new System.EventHandler(this.miTriRightBot_Click);
            // 
            // 弧形三角ToolStripMenuItem
            // 
            this.弧形三角ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCurvedLeftTop,
            this.miCurvedLeftBot,
            this.miCurvedRightTop,
            this.miCurvedRightBot});
            this.弧形三角ToolStripMenuItem.Name = "弧形三角ToolStripMenuItem";
            this.弧形三角ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.弧形三角ToolStripMenuItem.Text = "弧形三角";
            // 
            // miCurvedLeftTop
            // 
            this.miCurvedLeftTop.Name = "miCurvedLeftTop";
            this.miCurvedLeftTop.Size = new System.Drawing.Size(100, 22);
            this.miCurvedLeftTop.Text = "左上";
            this.miCurvedLeftTop.Click += new System.EventHandler(this.miCurvedLeftTop_Click);
            // 
            // miCurvedLeftBot
            // 
            this.miCurvedLeftBot.Name = "miCurvedLeftBot";
            this.miCurvedLeftBot.Size = new System.Drawing.Size(100, 22);
            this.miCurvedLeftBot.Text = "左下";
            this.miCurvedLeftBot.Click += new System.EventHandler(this.miCurvedLeftBot_Click);
            // 
            // miCurvedRightTop
            // 
            this.miCurvedRightTop.Name = "miCurvedRightTop";
            this.miCurvedRightTop.Size = new System.Drawing.Size(100, 22);
            this.miCurvedRightTop.Text = "右上";
            this.miCurvedRightTop.Click += new System.EventHandler(this.miCurvedRightTop_Click);
            // 
            // miCurvedRightBot
            // 
            this.miCurvedRightBot.Name = "miCurvedRightBot";
            this.miCurvedRightBot.Size = new System.Drawing.Size(100, 22);
            this.miCurvedRightBot.Text = "右下";
            this.miCurvedRightBot.Click += new System.EventHandler(this.miCurvedRightBot_Click);
            // 
            // pbDraw
            // 
            this.pbDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbDraw.Location = new System.Drawing.Point(0, 25);
            this.pbDraw.Name = "pbDraw";
            this.pbDraw.Size = new System.Drawing.Size(901, 569);
            this.pbDraw.TabIndex = 1;
            this.pbDraw.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSet});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // miSet
            // 
            this.miSet.Name = "miSet";
            this.miSet.Size = new System.Drawing.Size(100, 22);
            this.miSet.Text = "设置";
            this.miSet.Click += new System.EventHandler(this.miSet_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 594);
            this.Controls.Add(this.pbDraw);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDraw)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miRect;
        private System.Windows.Forms.ToolStripMenuItem miCircle;
        private System.Windows.Forms.ToolStripMenuItem 三角ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miTriTop;
        private System.Windows.Forms.ToolStripMenuItem miTriBot;
        private System.Windows.Forms.ToolStripMenuItem miTriLeft;
        private System.Windows.Forms.ToolStripMenuItem miTriRight;
        private System.Windows.Forms.ToolStripMenuItem miTriLeftTop;
        private System.Windows.Forms.ToolStripMenuItem miTriLeftBot;
        private System.Windows.Forms.ToolStripMenuItem miTriRightTop;
        private System.Windows.Forms.ToolStripMenuItem miTriRightBot;
        private System.Windows.Forms.ToolStripMenuItem 弧形三角ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miCurvedLeftTop;
        private System.Windows.Forms.ToolStripMenuItem miCurvedLeftBot;
        private System.Windows.Forms.ToolStripMenuItem miCurvedRightTop;
        private System.Windows.Forms.ToolStripMenuItem miCurvedRightBot;
        private System.Windows.Forms.PictureBox pbDraw;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miSet;


    }
}

