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
    /// <summary>
    /// 锁屏
    /// </summary>
    public partial class LockPanel : RakeBack.BaseForm
    {
        /// <summary>
        /// 重新登录
        /// </summary>
        public LockPanel()
        {
            InitializeComponent();
            this.Shown += LockPanel_Shown;
        }

        private void LockPanel_Shown(object sender, EventArgs e)
        {
            ApplicationParam.MainForm.LockPanel();
            var dialog = new LockDialog();
            dialog.ShowDialog();
            ApplicationParam.MainForm.UnLockPanel(this);
        }

        private void LockPanel_Load(object sender, EventArgs e)
        {
           
        }
    }
}
