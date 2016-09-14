using RakeBack.Business;
using RakeBack.Helper;
using RakeBack.Model;
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
            pager1.OnPageChanged += Pager1_OnPageChanged;

            InitCombox();
        }

        private void Pager1_OnPageChanged(object sender, EventArgs e)
        {
            Search(_orderCode,_loginCode,_memberName,_orderStatus,_startDate,_endDate, pager1.PageIndex);
        }


        private void InitCombox()
        {
            var dic = new Dictionary<int, string>();
            dic.Add((int)OrderStatus.All, "全部");
            dic.Add((int)OrderStatus.NewOrder, "新单");
            dic.Add((int)OrderStatus.Audited, "已审核");
            dic.Add((int)OrderStatus.Browse, "会员已浏览");
            dic.Add((int)OrderStatus.BankReturnSuccess, "银行已返回成功");
            dic.Add((int)OrderStatus.BankReturnFail, "银行已返回失败");
            dic.Add((int)OrderStatus.BankDealing, "银行处理中");

            BindingSource bs = new BindingSource();
            cbx_OrderStatus.DisplayMember = "Value";
            cbx_OrderStatus.ValueMember = "Key";
            bs.DataSource = dic;
            cbx_OrderStatus.DataSource = bs;
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


        private void buttonW1_Click(object sender, EventArgs e)
        {
            int orderStatus = 0;
            if (cbx_OrderStatus.SelectedItem != null)
                orderStatus = (int)cbx_OrderStatus.SelectedValue;
            //每次查询需要从第一页开始
            Search(txtOrder.Text.Trim(),txtLogin.Text.Trim(),txtMember.Text.Trim(),1,dtStart.Value.ToString("yyyy-MM-dd"),dtEnd.Value.ToString("yyyy-MM-dd"), 1);
        }

        private string _orderCode, _loginCode, _memberName, _startDate, _endDate;
        int _orderStatus;
        private Dictionary<string, string> MakeConditions(string orderCode, string loginCode, string memberName, int orderStatus, string startDate, string endDate)
        {
            _orderCode = orderCode;
            _loginCode = loginCode;
            _memberName = memberName;
            _startDate = startDate;
            _endDate = endDate;
            _orderStatus = orderStatus;

            Dictionary<string, string> conditions = new Dictionary<string, string>();
            var ui = ApplicationParam.UserInfo;
            if (!string.IsNullOrEmpty(_orderCode))
            {
                conditions.Add("orderid", _orderCode);
            }
            if (ui.RoleId == 0)//系统管理员
            {
                if (!string.IsNullOrEmpty(_loginCode))
                {
                    conditions.Add("loginId", _loginCode);
                }
                if (!string.IsNullOrEmpty(_memberName))
                {
                    conditions.Add("userName", _memberName);
                }
            }
            else if (ui.RoleId == 2)//普通会员,只能看到自己
            {
                conditions.Add("loginId", ui.LoginId);
            }
            else if (ui.RoleId == 3)//会员管理员，可以看到所有的普通会员
            {
                if (!string.IsNullOrEmpty(_loginCode))
                {
                    conditions.Add("loginId", _loginCode);
                }
                if (!string.IsNullOrEmpty(_memberName))
                {
                    conditions.Add("userName", _memberName);
                }
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                conditions.Add("startDate", startDate);
            }
            if (!string.IsNullOrEmpty(_endDate))
            {
                conditions.Add("endDate", _endDate);
            }
            if (_orderStatus != 0)
            {
                conditions.Add("orderstatus", _orderStatus.ToString());
            }

            if (!string.IsNullOrEmpty(_startDate) && !string.IsNullOrEmpty(_endDate))
            {
                if (DateTime.Parse(_endDate) < DateTime.Parse(_startDate))
                {
                    MessageBox.Show(this, "查询截止时间不能早于起始时间！");
                    return null;
                }
            }
            conditions.Add("iseable", "1");
            return conditions;
        }

        private void Search(string orderCode, string loginCode, string memberName, int orderStatus, string startDate, string endDat, int pageIndex)
        {
            
            var dic = MakeConditions(orderCode,loginCode,memberName,orderStatus,startDate,endDat);

            if (dic == null)
                return;

            base.StartWait();
            ThreadHelper.StartNew(() =>
            {
                try
                {
                    var client = CommunicationHelper.GetClient();
                    if (client != null)
                    {
                        var result = client.GetRakeBack(pager1.PageSize, pageIndex, dic);

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
