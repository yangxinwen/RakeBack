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
        private Dictionary<string, string> MakeConditions(string code, string name)
        {
            _code = code;
            _name = name;
            Dictionary<string, string> conditions = new Dictionary<string, string>();
            var ui = ApplicationParam.UserInfo;
            if (ui.RoleId == 0)//系统管理员
            {
                if (!string.IsNullOrEmpty(code))
                {
                    conditions.Add("loginId", code);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    conditions.Add("userName", name);
                }
            }
            else if (ui.RoleId == 2)//普通会员,只能看到自己
            {
                conditions.Add("loginId", ui.LoginId);
            }
            else if (ui.RoleId == 3)//会员管理员，可以看到所有的普通会员
            {
                conditions.Add("roleId", "2");
                if (!string.IsNullOrEmpty(code))
                {
                    conditions.Add("loginId", code);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    conditions.Add("userName", name);
                }
            }
            conditions.Add("iseable", "1");
            return conditions;
        }

        private void Search(string code, string name, int pageIndex)
        {
            var dic = MakeConditions(code, name);
            base.StartWait();
            ThreadHelper.StartNew(() =>
            {
                try
                {
                    var client = CommunicationHelper.GetClient();
                    if (client != null)
                    {
                        var result = client.GetNewRakeBack(pager1.PageSize, pageIndex, dic);

                        this.Invoke(new Action(() =>
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
                    this.Invoke(new Action(() =>
                    {
                        base.EndWait();
                        MessageBox.Show("查询出错:" + ex.Message); 
                    }
                    ));
                }
            }, _outTime);
        }
    }
}
