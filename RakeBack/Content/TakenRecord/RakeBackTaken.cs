using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RakeBack.Content.TakenRecord
{
    /// <summary>
    /// 返佣提取
    /// </summary>
    public partial class RakeBackTaken : RakeBack.BaseForm
    {
        public RakeBackTaken()
        {
            InitializeComponent();
            dataGridViewW1.AutoGenerateColumns = false;
            dataGridViewW1.DataSource = new List<string> { "sdfsdf" };
        }


        private void dataGridViewW1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
