using RakeBack.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RakeBack.Content.Common
{
    public partial class ReLogin : RakeBack.BaseForm
    {
        /// <summary>
        /// 重新登录
        /// </summary>
        public ReLogin()
        {
            InitializeComponent();

            Application.Exit();
            System.Diagnostics.Process.Start(Application.StartupPath+"/RakeBack.exe","sdf");
        }
    }
}
