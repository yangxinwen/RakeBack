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

            System.Diagnostics.Process.Start(Application.ExecutablePath);
            Application.Exit();
        }
    }
}
