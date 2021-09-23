using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestAsyn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            cancelSource = new CancellationTokenSource();
            IProgress<int> progress = new Progress<int>((progressValue) => { progressBar1.Value = progressValue; });

            textBox1.Text = "Requesting string, please wait...";
            button1.Enabled = false; button2.Enabled = true;

            WasteTimeObject ad = new WasteTimeObject();

            try
            {
                string result = await Task.Run(() => ad.GetSlowString(1, 10, progress, cancelSource.Token),
                    cancelSource.Token);
                //Update UI to display the result
                textBox1.Text = result;
                button2.Enabled = false;  //Disable cancel button
            }
            catch (OperationCanceledException)
            {
                textBox1.Text = "You canceled the operation.";
            }

        }

    }
}
