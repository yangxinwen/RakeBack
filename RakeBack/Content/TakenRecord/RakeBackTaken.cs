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

            InitCombox();
            dataGridViewW1.AutoGenerateColumns = false;
            pager1.OnPageChanged += Pager1_OnPageChanged;
        }

        private void Pager1_OnPageChanged(object sender, EventArgs e)
        {
            Search(_startDt, _endDt, _orderStatus, pager1.PageIndex);
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
            if ("logCol".Equals(dataGridViewW1.Columns[e.ColumnIndex].Name))
            {
                var mod = dataGridViewW1.Rows[e.RowIndex].DataBoundItem as OrderInfo;
                if (mod == null)
                    return;
                OpenLog(mod.OrderId);
            }
        }
        private void OpenLog(string id)
        {
            var dialog = new OrderLog() { OrderId = id };
            dialog.ShowDialog();
        }

        private void buttonW1_Click(object sender, EventArgs e)
        {
            int orderStatus = 0;
            if (cbx_OrderStatus.SelectedItem != null)
                orderStatus = (int)cbx_OrderStatus.SelectedValue;
            //每次查询需要从第一页开始
            Search(dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), orderStatus, 1);
        }

        private string _startDt;
        private string _endDt;
        private int _orderStatus;
        private Dictionary<string, string> MakeConditions(string startDt, string endDt, int orderStatus)
        {
            _startDt = startDt;
            _endDt = endDt;
            _orderStatus = orderStatus;
            var ui = ApplicationParam.UserInfo;
            Dictionary<string, string> conditions = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(startDt))
            {
                conditions.Add("startDate", startDt);
            }
            if (!string.IsNullOrEmpty(endDt))
            {
                conditions.Add("endDate", endDt);
            }
            if (orderStatus != 0)
            {
                conditions.Add("orderstatus", orderStatus.ToString());
            }
            if (ui.RoleId == 2)
            {
                conditions.Add("loginId", ui.LoginId);
            }
            if (!string.IsNullOrEmpty(startDt) && !string.IsNullOrEmpty(endDt))
            {
                if (DateTime.Parse(endDt) < DateTime.Parse(startDt))
                {
                    return null;
                }
            }
            conditions.Add("iseable", "1");
            return conditions;
        }

        private void Search(string startDt, string endDt, int orderStatus, int pageIndex)
        {
            var dic = MakeConditions(startDt, endDt, orderStatus);
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
