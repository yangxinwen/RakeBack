using RakeBack.Business;
using RakeBack.Helper;
using RakeBack.Model;
using RakeBack.RakeBackService;
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
            Search(_orderCode, _loginCode, _memberName, _orderStatus, _startDate, _endDate, pager1.PageIndex);
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
                    var order = dataGridViewW1.Rows[e.RowIndex].DataBoundItem as OrderInfo;
                    if (order == null) return;
                    DelOrder(order);
                }
            }
            else if ("auditCol".Equals(dataGridViewW1.Columns[e.ColumnIndex].Name))
            {
                if (MessageBox.Show("确认审核？", "确认对话框", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    var order = dataGridViewW1.Rows[e.RowIndex].DataBoundItem as OrderInfo;
                    if (order == null) return;
                    AuditOrder(order);
                }
            }
            else if ("browseCol".Equals(dataGridViewW1.Columns[e.ColumnIndex].Name))
            {
                if (MessageBox.Show("确认标记为已查看？", "确认对话框", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    var order = dataGridViewW1.Rows[e.RowIndex].DataBoundItem as OrderInfo;
                    if (order == null) return;
                    BrowseOrder(order);
                }
            }
            else if ("useCol".Equals(dataGridViewW1.Columns[e.ColumnIndex].Name))
            {
                if (MessageBox.Show("确认提取？", "确认对话框", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    var order = dataGridViewW1.Rows[e.RowIndex].DataBoundItem as OrderInfo;
                    if (order == null) return;
                    var url = "http://218.17.162.159:18888/WebService.asmx";
                    var args = new string[] { "123", order.OrderId };



                    try
                    {

                        string result = (string)WSHelper.InvokeWebService(url, "OutMoney", args);

                        System.Diagnostics.Process.Start("http://baidu.com");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("提取失败");
                    }


                }
            }
            else if ("logCol".Equals(dataGridViewW1.Columns[e.ColumnIndex].Name))
            {

            }
        }

        private bool isVaildOrder(OrderInfo order)
        {
            if (order.OrderStatus == "2")
            {
                MessageBox.Show(this, "客户已经浏览该笔订单了，不能处理！");
                return false;
            }
            else if (order.OrderStatus == "3")
            {
                MessageBox.Show(this, "该笔订单银行已经返回成功了，不能处理！");
                return false;
            }
            else if (order.OrderStatus == "4")
            {
                MessageBox.Show(this, "该笔订单银行已经返回失败了，不能处理！");
                return false;
            }
            else if (order.OrderStatus == "5")
            {
                MessageBox.Show(this, "该笔订单正在等待银行返回处理结果，不能处理！");
                return false;
            }

            return true;
        }

        private void AuditOrder(OrderInfo order)
        {
            if (isVaildOrder(order) == false) return;

            order.OrderStatus = ((int)OrderStatus.Audited).ToString();
            order.InputPerson = ApplicationParam.UserInfo.InputPerson;
            order.UpdateTime = DateTime.Now;
            order.SubmitTime = DateTime.Now;

            base.StartWait();
            ThreadHelper.StartNew(() =>
            {
                try
                {
                    var client = CommunicationHelper.GetClient();
                    if (client != null)
                    {
                        var result = client.UpdateOrderInfo(order);
                        base.EndWait();
                        this.Invoke(new Action(() =>
                        {
                            if (result != null && result.IsSuccess)
                            {
                                MessageBox.Show("审核成功");
                            }
                            else if (result != null)
                            {
                                MessageBox.Show("审核失败：" + result.ErrorMsg);
                            }
                        }));
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        base.EndWait();
                        MessageBox.Show("审核失败:" + ex.Message);
                    }
                    ));
                }
            }, _outTime);

        }

        private void DelOrder(OrderInfo order)
        {
            if (isVaildOrder(order) == false) return;

            order.Iseable = "0";//0为已删除 1为正常启用
            order.InputPerson = ApplicationParam.UserInfo.InputPerson;
            order.UpdateTime = DateTime.Now;

            base.StartWait();
            ThreadHelper.StartNew(() =>
            {
                try
                {
                    var client = CommunicationHelper.GetClient();
                    if (client != null)
                    {
                        var result = client.DelOrderInfo(order);
                        base.EndWait();
                        this.Invoke(new Action(() =>
                        {
                            if (result != null && result.IsSuccess)
                            {
                                MessageBox.Show("删除成功");
                            }
                            else if (result != null)
                            {
                                MessageBox.Show("删除失败：" + result.ErrorMsg);
                            }
                        }));
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        base.EndWait();
                        MessageBox.Show("删除失败:" + ex.Message);
                    }
                    ));
                }
            }, _outTime);
        }


        private void BrowseOrder(OrderInfo order)
        {
            if (isVaildOrder(order) == false) return;

            if (order.OrderStatus != ((int)OrderStatus.Audited).ToString())
            {
                MessageBox.Show(this, "只可以标记已审核状态的订单！");
                return;
            }


            order.OrderStatus = ((int)OrderStatus.Browse).ToString();
            order.InputPerson = ApplicationParam.UserInfo.InputPerson;
            order.UpdateTime = DateTime.Now;
            order.SubmitTime = DateTime.Now;

            base.StartWait();
            ThreadHelper.StartNew(() =>
            {
                try
                {
                    var client = CommunicationHelper.GetClient();
                    if (client != null)
                    {
                        var result = client.UpdateOrderInfo(order);
                        base.EndWait();
                        this.Invoke(new Action(() =>
                        {
                            if (result != null && result.IsSuccess)
                            {
                                MessageBox.Show("标记成功");
                            }
                            else if (result != null)
                            {
                                MessageBox.Show("标记失败：" + result.ErrorMsg);
                            }
                        }));
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        base.EndWait();
                        MessageBox.Show("标记失败:" + ex.Message);
                    }
                    ));
                }
            }, _outTime);

        }

        private void buttonW1_Click(object sender, EventArgs e)
        {
            int orderStatus = 0;
            if (cbx_OrderStatus.SelectedItem != null)
                orderStatus = (int)cbx_OrderStatus.SelectedValue;
            //每次查询需要从第一页开始
            Search(txtOrder.Text.Trim(), txtLogin.Text.Trim(), txtMember.Text.Trim(), orderStatus, dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), 1);
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

            var dic = MakeConditions(orderCode, loginCode, memberName, orderStatus, startDate, endDat);

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
