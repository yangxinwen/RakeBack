using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RakeBack.Content.Common
{
    public partial class MainPage : RakeBack.BaseForm
    {
        public MainPage()
        {
            InitializeComponent();
        }
        

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://120.76.78.5:81/webadmin/login.aspx ");
        }
    }
}
