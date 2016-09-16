using RakeBack.Business;
using RakeBack.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RakeBack.Content.SystemSet
{
    /// <summary>
    /// 会员信息管理
    /// </summary>
    public partial class MemberMgr : RakeBack.BaseForm
    {
        public MemberMgr()
        {
            InitializeComponent();
            dataGridViewW1.AutoGenerateColumns = false;
            InitCombox();
            pager1.OnPageChanged += Pager1_OnPageChanged;
        }

        private void Pager1_OnPageChanged(object sender, EventArgs e)
        {
            Search(_code, _name, _roleId, pager1.PageIndex);
        }

        private void InitCombox()
        {
            ThreadHelper.StartNew(() =>
            {
                try
                {
                    var client = CommunicationHelper.GetClient();
                    if (client != null)
                    {
                        var result = client.GetRoleInfo();

                        this.Invoke(new Action(() =>
                        {
                            if (result != null && result.IsSuccess)
                            {
                                var dic = new Dictionary<int, string>();

                                if (ApplicationParam.UserInfo.RoleId == 0)
                                {
                                    dic.Add(-1, "全部");
                                    dic.Add(0, "系统管理员");
                                    foreach (var item in result.Content)
                                    {
                                        dic.Add(item.RoleId, item.RoleName);
                                    }
                                }
                                else if (ApplicationParam.UserInfo.RoleId == 2 ||
                                    ApplicationParam.UserInfo.RoleId == 3)
                                {
                                    foreach (var item in result.Content)
                                    {
                                        if (item.RoleId != 3)
                                        {
                                            dic.Add(item.RoleId, item.RoleName);
                                        }
                                    }
                                }

                                BindingSource bs = new BindingSource();
                                cbx_Role.DisplayMember = "Value";
                                cbx_Role.ValueMember = "Key";
                                bs.DataSource = dic;
                                cbx_Role.DataSource = bs;

                            }
                            else
                            {
                                MessageBoxHelper.ShowError(this, "角色信息查询出错");
                            }
                        }));
                        base.EndWait();
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBoxHelper.ShowError(this, "查询出错:" + ex.Message);
                    }
                    ));
                }
            });
        }


        private void dataGridViewW1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ("editCol".Equals(dataGridViewW1.Columns[e.ColumnIndex].Name))
            {
                var dilog = new UpdateMember() { OldInfo = dataGridViewW1.Rows[e.RowIndex].DataBoundItem as RakeBackService.UserInfo };
                dilog.ShowDialog();
            }
            else if ("delCol".Equals(dataGridViewW1.Columns[e.ColumnIndex].Name))
            {
                if (MessageBoxHelper.ShowConf(this, "确认删除？") == DialogResult.OK)
                {
                    var mod = dataGridViewW1.Rows[e.RowIndex].DataBoundItem as RakeBackService.UserInfo;
                    if (mod == null)
                        return;
                    ThreadHelper.StartNew(() =>
                    {
                        try
                        {
                            var client = CommunicationHelper.GetClient();
                            if (client != null)
                            {
                                var result = client.DelUserInfo(mod.UserId, ApplicationParam.UserInfo.LoginId);

                                base.EndWait();
                                this.Invoke(new Action(() =>
                                {
                                    if (result != null && result.IsSuccess)
                                    {
                                        MessageBoxHelper.ShowInfo(this, "删除成功");
                                    }
                                    else if (result != null)
                                    {
                                        MessageBoxHelper.ShowError(this, "删除失败:" + result.ErrorMsg);
                                    }
                                }));
                            }
                        }
                        catch (Exception ex)
                        {
                            this.Invoke(new Action(() =>
                            {
                                base.EndWait();
                                MessageBoxHelper.ShowError(this, "删除失败:" + ex.Message);
                            }
                            ));
                        }
                    }, _outTime);

                }
            }
            else if ("logCol".Equals(dataGridViewW1.Columns[e.ColumnIndex].Name))
            {
                var mod = dataGridViewW1.Rows[e.RowIndex].DataBoundItem as RakeBackService.UserInfo;
                if (mod == null)
                    return;
                OpenLog(mod.UserId);
            }

        }

        private void OpenLog(int userId)
        {
            var dialog = new OperateLog() {  UserId=userId};
            dialog.ShowDialog();
        }


        private void buttonW1_Click(object sender, EventArgs e)
        {
            int roleId = 0;
            if (cbx_Role.SelectedItem != null)
                roleId = (int)cbx_Role.SelectedValue;
            //每次查询需要从第一页开始
            Search(txtLogin.Text.Trim(), txtCustomer.Text.Trim(), roleId, 1);
        }

        private string _code;
        private string _name;
        private int _roleId;
        private Dictionary<string, string> MakeConditions(string code, string name, int roleId)
        {
            _code = code;
            _name = name;
            _roleId = roleId;
            var ui = ApplicationParam.UserInfo;
            Dictionary<string, string> conditions = new Dictionary<string, string>();
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
                if (roleId != -1)
                {
                    conditions.Add("roleId", roleId.ToString());
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

        private void Search(string code, string name, int roleId, int pageIndex)
        {
            var dic = MakeConditions(code, name, roleId);
            base.StartWait();
            ThreadHelper.StartNew(() =>
            {
                try
                {
                    var client = CommunicationHelper.GetClient();
                    if (client != null)
                    {
                        var result = client.GetUserInfo(pager1.PageSize, pageIndex, dic);

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
                                MessageBoxHelper.ShowError(this, "查询出错");
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
                        MessageBoxHelper.ShowError(this, "查询出错:" + ex.Message);
                    }
                    ));
                }
            }, _outTime);
        }
    }
}
