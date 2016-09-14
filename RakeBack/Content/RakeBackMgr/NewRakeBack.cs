using RakeBack.Helper;
using RakeBack.Business;
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
    /// 新建返佣
    /// </summary>
    public partial class NewRakeBack : RakeBack.BaseForm
    {
        public NewRakeBack()
        {
            InitializeComponent();

            dataGridViewW1.AutoGenerateColumns = false;
            dataGridViewW1.DataSource = new List<string> { "sdfsdf" };

            pager1.OnPageChanged += Pager1_OnPageChanged;
        }

        private void Pager1_OnPageChanged(object sender, EventArgs e)
        {
            Search(_code, _name, pager1.PageIndex);
        }


        private void dataGridViewW1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ("newCol".Equals(dataGridViewW1.Columns[e.ColumnIndex].Name))
            {
                var dialog = new AddRakeBackDialog();
                dialog.ShowDialog();
            }
        }

        private void buttonW1_Click(object sender, EventArgs e)
        {
            //每次查询需要从第一页开始
            Search(txtLoginCode.Text.Trim(), txtCustomerName.Text.Trim(), 1);
        }

        private string _code;
        private string _name;
        private void Search(string code, string name, int pageIndex)
        {
            _code = code;
            _name = name;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var ui = ApplicationParam.UserInfo;
            if (ui.RoleId == 0)//系统管理员
            {
                if (!string.IsNullOrEmpty(code))
                {
                    dic.Add("loginId", code);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    dic.Add("userName", name);
                }
            }
            else if (ui.RoleId == 2)//普通会员,只能看到自己
            {
                dic.Add("loginId", ui.LoginId);
            }
            else if (ui.RoleId == 3)//会员管理员，可以看到所有的普通会员
            {
                dic.Add("roleId", "2");
                if (!string.IsNullOrEmpty(code))
                {
                    dic.Add("loginId", code);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    dic.Add("userName", name);
                }
            }
            dic.Add("iseable", "1");

            //pager1.CurrentPage = 0;

            base.StartWait();

            ThreadHelper.StartNew(() =>
            {
                try
                {

                var client = CommunicationHelper.GetClient();
                if (client != null)
                {
                    var result = client.GetNewRakeBack(pager1.PageSize, pageIndex, dic);

                    this.BeginInvoke(new Action(() =>
                    {
                        if (result != null && result.IsSuccess)
                        {
                            dataGridViewW1.DataSource = result.Content;
                            //重新绘制分页控件
                            pager1.PageIndex = pageIndex;
                            pager1.DrawControl(result.Count);
                        }
                        else
                        {
                            MessageBox.Show("查询出错");
                        }
                    }));
                    base.EndWait();
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("查询出错:"+ ex.Message);
                }
            }, _outTime);
        }
    }
}
