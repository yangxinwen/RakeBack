using RakeBack.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RakeBack.Content
{ 
    /// <summary>
    /// 操作详情
    /// </summary>
    public partial class OrderLog : RakeBack.BaseForm
    {

        private string _orderId;
        public string OrderId
        {
            get
            {
                return _orderId;
            }
            set
            {
                _orderId = value;
            }
        }

        public OrderLog()
        {
            InitializeComponent();
            dataGridViewW1.AutoGenerateColumns = false;
            pager1.OnPageChanged += Pager1_OnPageChanged;
            this.Shown += OperateLog_Shown;
        }

        private void OperateLog_Shown(object sender, EventArgs e)
        {
            SetDataGridViewStyle(dataGridViewW1);
            Search(_orderId, 1);
        }

        private void Pager1_OnPageChanged(object sender, EventArgs e)
        {
            Search(_orderId, pager1.PageIndex);
        }

        private void Search(string orderId, int pageIndex)
        {
            var dic = new Dictionary<string, string>();
            dic.Add("orderid", orderId);
            base.StartWait();
            ThreadHelper.StartNew(() =>
            {
                try
                {
              
                        var result = CommunicationHelper.GetOrderLog(pager1.PageSize, pageIndex, dic);

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
