using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PearlBag
{
    public partial class TitleForm : Form
    {
        public string title;
        public TitleForm()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_title.Text.Trim()))
                title = "模板_" + DateTime.Now.ToString("yy年MM月dd日HH时mm分");
            else
                title = tb_title.Text.Trim();

            this.Close();
        }

        private void TitleForm_Load(object sender, EventArgs e)
        {
            //tb_title.Text = "模板" + DateTime.Now.ToString("mm分ss秒"); ;
        }
    }
}
