using RakeBack.Business;
using RakeBack.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RakeBack.Content.Common
{
    public partial class LockDialog : RakeBack.BaseForm
    {
        public LockDialog()
        {
            InitializeComponent();
            this.FormClosed += LockDialog_FormClosed;
        }

        private void LockDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void buttonW1_Click(object sender, EventArgs e)
        {
            if (textBoxW1.Text.Equals(Business.ApplicationParam.InputPwd))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBoxHelper.ShowInfo(this, "密码错误");
            }
        }
    }
}
