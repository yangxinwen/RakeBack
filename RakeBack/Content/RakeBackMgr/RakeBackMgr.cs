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
    /// 返佣管理
    /// </summary>
    public partial class RakeBackMgr : RakeBack.BaseForm
    {
        public RakeBackMgr()
        {
            InitializeComponent();


            dataGridViewW1.AutoGenerateColumns = false;
            dataGridViewW1.DataSource = new List<string> { "sdfsdf" };
        }


        private void InitCombox()
        {
            cbx_OrderStatus.Items.Add("");
        }


        private void dataGridViewW1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if ("delCol".Equals(dataGridViewW1.Columns[e.ColumnIndex].Name))
            {
                if (MessageBox.Show("确认删除？", "确认对话框", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    MessageBox.Show("删除成功");
                }
            }
            else if ("logCol".Equals(dataGridViewW1.Columns[e.ColumnIndex].Name))
            {

            }
        }
    }
}
