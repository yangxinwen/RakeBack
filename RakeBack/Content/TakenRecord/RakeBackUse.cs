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
                if (!_info.OrderStatus.Trim().Equals("2"))
                {
                    MessageBoxHelper.ShowInfo(this, "订单状态不正常，请重新提取！");
                    return;
                }

                CommunicationHelper.AddOutMoneyOperateLog(ApplicationParam.UserInfo.UserId, ApplicationParam.UserInfo.UserName, _info.OrderId);

                string url = ApplicationParam.OutMoneyUrl;                
                string[] argg = new string[2];
                argg[0] = ApplicationParam.B2CSettleKey;
                argg[1] = _info.Id.ToString();
                try
                {
                    string result = (string)WSHelper.InvokeWebService(url, "OutMoney", argg);
                    MessageBoxHelper.ShowInfo(this,result);
                    //if (result.Contains("Http://"))
                    //{
                    //    System.Diagnostics.Process.Start(result);
                    //    this.DialogResult = DialogResult.OK;
                    //}
                    //else
                    //    new Exception(result);
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.ShowError(this, "提取失败:"+ex.Message);
                }
                this.DialogResult = DialogResult.OK;
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
