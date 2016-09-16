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
    public partial class RakeBackUse : RakeBack.BaseForm
    {
        private OrderInfo _info;
        ValidCode _validCode;

        public RakeBackUse()
        {
            InitializeComponent();

            _validCode = new ValidCode(4, ValidCode.CodeType.Alphas);
            pbValid.Image = Bitmap.FromStream(_validCode.CreateCheckCodeImage());
        }

        private void buttonW1_Click(object sender, EventArgs e)
        {
            if (false == _validCode.CheckCode.Equals(txtValid.Text, StringComparison.CurrentCultureIgnoreCase))
            {
                MessageBoxHelper.ShowInfo(this, "验证码输入错误");
                return;
            }



            if (MessageBoxHelper.ShowConf(this, "确认提取？") == DialogResult.OK)
            {
                var url = "http://218.17.162.159:18888/WebService.asmx";
                var args = new string[] { "123", _info.OrderId };
                try
                {

                    string result = (string)WSHelper.InvokeWebService(url, "OutMoney", args);

                    System.Diagnostics.Process.Start("http://baidu.com");
                }
                catch (Exception)
                {
                    MessageBoxHelper.ShowError(this, "提取失败");
                }
            }
        }

        private void MakeValidCode()
        {
            pbValid.Image = Bitmap.FromStream(_validCode.CreateCheckCodeImage());
        }

        public void InitOrderInfo(OrderInfo info)
        {
            if (info == null) return;
            _info = info;
            lbOrderId.Text = info.OrderId;
            lbLogin.Text = info.LoginId;
            lbCustomer.Text = info.UserName;
            lbAcoumt.Text = info.Amount + "元";
            lbBankNo.Text = info.BankNumber;
            lbCreateBank.Text = info.CreateBank;
            lbBranchBank.Text = info.BranchBank;
            lbBankZH.Text = info.BranchBankZH;
            lbCreateTime.Text = info.CreateTime.ToString("yyyy-MM-dd");
            lbRemark.Text = info.Remark;
        }

        private void pbValid_Click(object sender, EventArgs e)
        {
            MakeValidCode();
        }
    }
}
