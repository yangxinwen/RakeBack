using RakeBack.Helper;
using RakeBack.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RakeBack.RakeBackService;

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

            pager1.OnPageChanged += Pager1_OnPageChanged;
            this.Load += NewRakeBack_Load;

        }

        private void NewRakeBack_Load(object sender, EventArgs e)
        {
            SetDataGridViewStyle(dataGridViewW1);
            buttonW1_Click(sender, e);
        }

        private void Pager1_OnPageChanged(object sender, EventArgs e)
        {
            Search(_code, _name, pager1.PageIndex);
        }


        private void dataGridViewW1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ("newCol".Equals(dataGridViewW1.Columns[e.ColumnIndex].Name))
            {
                var mod = dataGridViewW1.Rows[e.RowIndex].DataBoundItem as UserInfo;
                var dialog = new AddRakeBackDialog() { UserInfo = mod };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (ApplicationParam.MainForm != null)
                    {
                        var f= ApplicationParam.MainForm.ShowForm("RakeBackMgr.RakeBackMgr");
                        if (f != null)
                        {
                            (f as RakeBackMgr).AutoSearch();
                        }
                    }
                }
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
               
                        var result = CommunicationHelper.GetNewRakeBack(pager1.PageSize, pageIndex, dic);

                        this.Invoke(new Action(() =>
                        {
                            if (result != null && result.IsSuccess)
                            {
                                dataGridViewW1.DataSource = result.Content;
                                //重新绘制分页控件
                                pager1.PageIndex = pageIndex;
                                pager1.DrawControl(result.Count);
                            }
                            else if (result != null)
                            {
                                MessageBoxHelper.ShowError(this, "查询出错:"+result.ErrorMsg);
                            }
                        }));
                        base.EndWait();
                    
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
