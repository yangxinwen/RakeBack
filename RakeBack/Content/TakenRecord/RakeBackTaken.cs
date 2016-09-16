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
            AddOperateColumn();
            this.Load += RakeBackTaken_Load;
            InitCombox();
            pager1.OnPageChanged += Pager1_OnPageChanged;
        }

        private void RakeBackTaken_Load(object sender, EventArgs e)
        {
            SetDataGridViewStyle(dataGridViewW1);
            buttonW1_Click(sender, e);
        }

        private void Pager1_OnPageChanged(object sender, EventArgs e)
        {
            Search(_startDt, _endDt, _orderStatus, pager1.PageIndex);
        }


        /// <summary>
        /// 添加操作列
        /// </summary>
        private void AddOperateColumn()
        {
            var role = Business.ApplicationParam.UserInfo.RoleId;
            if (role == 2)
            {
                var column = new DataGridViewLinkColumn();
                column.HeaderText = string.Empty;
                column.Text = "提取";
                column.UseColumnTextForLinkValue = true;
                column.Name = "useCol";
                dataGridViewW1.Columns.Add(column);
            }


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
            else if ("useCol".Equals(dataGridViewW1.Columns[e.ColumnIndex].Name))
            {
                var order = dataGridViewW1.Rows[e.RowIndex].DataBoundItem as OrderInfo;
                if (order == null) return;

                if (order.OrderStatus != ((int)OrderStatus.Audited).ToString()&&
                    order.OrderStatus != ((int)OrderStatus.Browse).ToString())
                {
                    MessageBoxHelper.ShowInfo(this, "订单已审核后才允许提取");
                    return;
                }
                BrowseOrder(order);

                var form = new RakeBackUse();
                form.InitOrderInfo(order);
                if (form.ShowDialog() == DialogResult.OK)
                    buttonW1_Click(null, null);                          
            }
        }



        private void OpenLog(string id)
        {
            var dialog = new OrderLog() { OrderId = id };
            dialog.ShowDialog();
        }



        private void BrowseOrder(OrderInfo order)
        {
            if (order.OrderStatus != ((int)OrderStatus.Audited).ToString())
            {
                return;
            }


            order.OrderStatus = ((int)OrderStatus.Browse).ToString();
            order.InputPerson = ApplicationParam.UserInfo.InputPerson;
            order.UpdateTime = DateTime.Now;
            order.SubmitTime = DateTime.Now;

            //base.StartWait();
            ThreadHelper.StartNew(() =>
            {
                try
                {
                    var client = CommunicationHelper.GetClient();
                    if (client != null)
                    {
                        var result = client.UpdateOrderInfo(order, ApplicationParam.UserInfo.LoginId);
                        //base.EndWait();
                        //this.Invoke(new Action(() =>
                        //{
                        //    if (result != null && result.IsSuccess)
                        //    {
                        //        MessageBoxHelper.ShowInfo(this, "标记成功");
                        //    }
                        //    else if (result != null)
                        //    {
                        //        MessageBoxHelper.ShowError(this, "标记失败：" + result.ErrorMsg);
                        //    }
                        //}));
                    }
                }
                catch (Exception ex)
                {
                    //this.Invoke(new Action(() =>
                    //{
                    //    base.EndWait();
                    //    MessageBoxHelper.ShowError(this, "标记失败:" + ex.Message);
                    //}
                    //));
                }
            });

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
