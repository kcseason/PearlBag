namespace PearlBag
{
    partial class Wait_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wait_Form));
            this.pb_bg = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_bg)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_bg
            // 
            this.pb_bg.BackColor = System.Drawing.Color.Transparent;
            this.pb_bg.Image = ((System.Drawing.Image)(resources.GetObject("pb_bg.Image")));
            this.pb_bg.Location = new System.Drawing.Point(104, 1);
            this.pb_bg.Name = "pb_bg";
            this.pb_bg.Size = new System.Drawing.Size(66, 66);
            this.pb_bg.TabIndex = 0;
            this.pb_bg.TabStop = false;
            this.pb_bg.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(-1, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "正在生成珠子图案，清稍候...";
            // 
            // Wait_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(277, 95);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pb_bg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Wait_Form";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WaitForm";
            this.Load += new System.EventHandler(this.frm_wait_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_bg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_bg;
        private System.Windows.Forms.Label label1;


    }
}