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
    public partial class AddRakeBackDialog : RakeBack.BaseForm
    {

        private RakeBackService.UserInfo _userInfo = null;
        public RakeBackService.UserInfo UserInfo
        {
            get
            {
                return _userInfo;
            }
            set
            {
                _userInfo = value;
                InitOrderInfo();

            }
        }

        public AddRakeBackDialog()
        {
            InitializeComponent();
        }

        private void buttonW1_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void InitOrderInfo()
        {
            if (_userInfo == null) return;

            lbLogin.Text = _userInfo.LoginId;
            lbCustomer.Text = _userInfo.UserName;
            lbTelphone.Text = _userInfo.UserPhone;
            lbCreateBank.Text = _userInfo.CreateBank;
            lbBranchBank.Text = _userInfo.BranchBank;
            lbBankZH.Text = _userInfo.BranchBankZH;
            lbBankNo.Text = _userInfo.BankNumber;
            lbMemberRemark.Text = _userInfo.Remark;
        }

        private void Add()
        {
            double amount = 0;
            if (double.TryParse(txtAmount.Text, out amount) == false || amount <= 0)
            {
                MessageBoxHelper.ShowInfo(this, "请输入正确的金额");
                return;
            }
            


            var order = new OrderInfo();

            //随机字符用来生成批次号
            int randomCount = 4;
            order.OrderId = StringHelper.RandomString(randomCount, false) + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string bankType = System.Configuration.ConfigurationManager.AppSettings["sys_banktype"];
            switch (bankType)
            {
                case "allinfinance":
                    order.BatchNo = string.Format("{0}{1}", StringHelper.RandomString(randomCount), order.OrderId);
                    break;
                case "allinpay":
                    order.BatchNo = System.Configuration.ConfigurationManager.AppSettings["allinpay_out_merchantid"] + DateTime.Now.ToString("yyyyMMddHHmmssfff");//批次号
                    break;
                default:
                    break;
            }
            order.Amount = txtAmount.Text.Trim();
            order.CreateTime = DateTime.Now;
            order.SubmitTime = DateTime.Now;
            order.BatchContent = string.Format("{0}{1}{2}", _userInfo.CreateBank, _userInfo.BranchBank, _userInfo.BranchBankZH);
            order.Uid = _userInfo.UserId;
            order.OrderStatus = ((int)OrderStatus.NewOrder).ToString();
            order.Remark = txtRemark.Text.Trim();
            order.Iseable = "1";
            order.UpdateTime = DateTime.Now;
            order.CreateBank = _userInfo.CreateBank;
            order.BranchBank = _userInfo.BranchBank;
            order.BranchBankZH = _userInfo.BranchBankZH;
            order.BankNumber = _userInfo.BankNumber;
            order.BelongTo = _userInfo.BelongTo;
            order.Api = _userInfo.Api;
            order.UserName = _userInfo.UserName;
            order.LoginId = _userInfo.LoginId;
            order.InputPerson = _userInfo.InputPerson;

            try
            {
              
                    var result = CommunicationHelper.AddRakeBack(order, ApplicationParam.UserInfo.LoginId);


                    if (result != null && result.IsSuccess)
                    {
                        MessageBoxHelper.ShowInfo(this, "新单创建成功");
                        this.DialogResult = DialogResult.OK;
                    }
                    else if (result != null)
                    {
                        MessageBoxHelper.ShowError(this, "新单创建失败:" + result.ErrorMsg);
                    }

                
            }
            catch (Exception)
            {

            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            lbAmountUp.Text = StringHelper.GetCnString(txtAmount.Text);
        }
    }
}
