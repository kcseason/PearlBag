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
    public partial class SetForm : Form
    {
        public Color DefaultColor;
        public bool IsShowBold = false;

        public SetForm()
        {
            InitializeComponent();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            textBox1.BackColor = colorDialog1.Color;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DefaultColor = textBox1.BackColor;
            this.IsShowBold = chkBold.Checked;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
