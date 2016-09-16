using RakeBack.Helper;
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
    /// 返佣统计
    /// </summary>
    public partial class RakeBackTotal : RakeBack.BaseForm
    {
        public RakeBackTotal()
        {
            InitializeComponent();

            this.Load += RakeBackTotal_Load;
        }

        private void RakeBackTotal_Load(object sender, EventArgs e)
        {
            SetDataGridViewStyle(dataGridViewW1);
            buttonW1_Click(sender, e);
        }

        private void Search(string startDate, string endDate)
        {
            base.StartWait();
            var mod = new Model.RakeBackTakenModel();
            ThreadHelper.StartNew(() =>
            {
                try
                {
                    var client = CommunicationHelper.GetClient();
                    if (client != null)
                    {

                        Dictionary<string, string> baseDic = new Dictionary<string, string>();
                        baseDic.Add("startDate", startDate);
                        baseDic.Add("endDate", endDate);
                        baseDic.Add("iseable", "1");


                        //all
                        var dic = new Dictionary<string, string>(baseDic);
                        var result = client.GetAmountStatistics(dic);
                        if (result != null && result.IsSuccess)
                        {
                            mod.ALlCount = result.Content.Item1;
                            mod.ALl = result.Content.Item2;
                        }


                        //newOrder
                        dic = new Dictionary<string, string>(baseDic);
                        dic.Add("orderstatus", "" + (int)Model.OrderStatus.NewOrder);
                        result = client.GetAmountStatistics(dic);
                        if (result != null && result.IsSuccess)
                        {
                            mod.NewOrderCount = result.Content.Item1;
                            mod.NewOrder = result.Content.Item2;
                        }
                        //Audited
                        dic = new Dictionary<string, string>(baseDic);
                        dic.Add("orderstatus", "" + (int)Model.OrderStatus.Audited);
                        result = client.GetAmountStatistics(dic);
                        if (result != null && result.IsSuccess)
                        {
                            mod.AuditedCount = result.Content.Item1;
                            mod.Audited = result.Content.Item2;
                        }
                        //Browse
                        dic = new Dictionary<string, string>(baseDic);
                        dic.Add("orderstatus", "" + (int)Model.OrderStatus.Browse);
                        result = client.GetAmountStatistics(dic);
                        if (result != null && result.IsSuccess)
                        {
                            mod.BrowsedCount = result.Content.Item1;
                            mod.Browsed = result.Content.Item2;
                        }
                        //BankReturnSuccess
                        dic = new Dictionary<string, string>(baseDic);
                        dic.Add("orderstatus", "" + (int)Model.OrderStatus.BankReturnSuccess);
                        result = client.GetAmountStatistics(dic);
                        if (result != null && result.IsSuccess)
                        {
                            mod.BankReturnSuccessCount = result.Content.Item1;
                            mod.BankReturnSuccess = result.Content.Item2;
                        }
                        //BankReturnFail
                        dic = new Dictionary<string, string>(baseDic);
                        dic.Add("orderstatus", "" + (int)Model.OrderStatus.BankReturnFail);
                        result = client.GetAmountStatistics(dic);
                        if (result != null && result.IsSuccess)
                        {
                            mod.BankReturnFailCount = result.Content.Item1;
                            mod.BankReturnFail = result.Content.Item2;
                        }
                        //BankDealing
                        dic = new Dictionary<string, string>(baseDic);
                        dic.Add("orderstatus", "" + (int)Model.OrderStatus.BankDealing);
                        result = client.GetAmountStatistics(dic);
                        if (result != null && result.IsSuccess)
                        {
                            mod.BankDealingCount = result.Content.Item1;
                            mod.BankDealing = result.Content.Item2;
                        }

                        this.Invoke(new Action(() =>
                        {
                            base.EndWait();
                            dataGridViewW1.DataSource = new List<Model.RakeBackTakenModel>() { mod };
                            lbResult.Text = string.Format("查询时间范围为：{0}至{1}的返佣资金统计表", startDate, endDate);
                        }));
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

        private void buttonW1_Click(object sender, EventArgs e)
        {
            if (dtStart.Value.Date > dtEnd.Value.Date)
            {
                MessageBoxHelper.ShowInfo(this, "查询截止时间不能早于起始时间！");
                return;
            }

            Search(dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));

        }
    }
}
