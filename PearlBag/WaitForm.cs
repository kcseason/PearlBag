using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PearlBag
{
    public partial class Wait_Form : Form
    {
        public Wait_Form()
        {
            InitializeComponent();
        }

        private void frm_wait_Load(object sender, EventArgs e)
        {
            pb_bg.Visible = true;
        }
    }
}
