using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RakeBack.Content.RakeBackMgr
{
    /// <summary>
    /// 返佣统计
    /// </summary>
    public partial class RakeBackTotal : RakeBack.BaseForm
    {
        public RakeBackTotal()
        {
            InitializeComponent();
            dataGridViewW1.AutoGenerateColumns = false;
            dataGridViewW1.DataSource = new List<string> { "sdfsdf" };
        } 

        
    }
}
