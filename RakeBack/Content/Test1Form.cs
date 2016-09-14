using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RakeBack.Content
{
    public partial class Test1Form : RakeBack.BaseForm
    {
        public Test1Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.StartWait();

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5 * 1000);
                this.EndWait();
             
            });

        }
    }
}
