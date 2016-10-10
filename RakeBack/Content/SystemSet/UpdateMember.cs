using RakeBack.Business;
using RakeBack.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RakeBack.Content.SystemSet
{
    /// <summary>
    /// 新增会员信息
    /// </summary>
    public partial class UpdateMember : RakeBack.BaseForm
    {
        private RakeBackService.UserInfo _oldInfo = null;

        public RakeBackService.UserInfo OldInfo
        {
            get
            {
                return _oldInfo;
            }
            set
            {
                _oldInfo = value;
                InitOldData();
            }
        }

        public UpdateMember()
        {
            InitializeComponent();

            InitRoleCombox();
            InitBankCombox();
        }


        private void InitRoleCombox()
        {
            try
            {

                var result = CommunicationHelper.GetRoleInfo();

                    if (result != null)
                    {
                        if (result.IsSuccess == false)
                        {
                            MessageBoxHelper.ShowError(this, "角色信息查询出错:" + result.ErrorMsg);
                            return;
                        }
                        var dic = new Dictionary<int, string>();

                        if (ApplicationParam.UserInfo.RoleId == 0)
                        {
                            dic.Add(0, "系统管理员");
                            foreach (var item in result.Content)
                            {
                                dic.Add(item.RoleId, item.RoleName);
                            }
                        }
                        else if (ApplicationParam.UserInfo.RoleId == 3)
                        {
                            foreach (var item in result.Content)
                            {
                                if (item.RoleId != 3)
                                {
                                    dic.Add(item.RoleId, item.RoleName);
                                }
                            }
                        }

                        BindingSource bs = new BindingSource();
                        cbxRole.DisplayMember = "Value";
                        cbxRole.ValueMember = "Key";
                        bs.DataSource = dic;
                        cbxRole.DataSource = bs;

                    }
                    else if (result != null)
                    {
                        MessageBoxHelper.ShowError(this, "角色信息查询出错:" + result.ErrorMsg);
                    }
                
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowError(this, "角色信息查询出错:" + ex.Message);

            }
        }

        private void InitBankCombox()
        {
            var dt = HelpCommon.GetBank();

            cbxOpenBank.DisplayMember = "ckey";
            cbxOpenBank.ValueMember = "cvalue";
            cbxOpenBank.DataSource = dt;

        }

        private void InitOldData()
        {
            if (_oldInfo == null)
                return;

            txtLogin.Text = _oldInfo.LoginId;
            txtLoginPwd.Text = DesSecurityCommon.Decrypt(_oldInfo.UserPwd);
            txtCustomer.Text = _oldInfo.UserName;
            txtTelphone.Text = _oldInfo.UserPhone;
            cbxRole.SelectedValue = _oldInfo.RoleId;
            cbxOpenBank.Text = _oldInfo.CreateBank;
            txtBranchBank.Text = _oldInfo.BranchBank;
            txtBranchBankZH.Text = _oldInfo.BranchBankZH;
            txtBankNo.Text = _oldInfo.BankNumber;
            txtRemark.Text = _oldInfo.Remark;

            if (_oldInfo.MenuId != null)
            {
                if (_oldInfo.MenuId.Contains("1"))
                    ckBackRake.Checked = true;
                if (_oldInfo.MenuId.Contains("2"))
                    ckInfoManager.Checked = true;

            }          

        }

        public bool IsValid()
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtLogin.Text.Trim()))
                {
                    MessageBoxHelper.ShowInfo(this, "请输入登陆账号");
                    txtLogin.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(this.txtLoginPwd.Text.Trim()))
                {
                    MessageBoxHelper.ShowInfo(this, "请输入用户密码");
                    txtLoginPwd.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(this.txtCustomer.Text.Trim()))
                {
                    MessageBoxHelper.ShowInfo(this, "请输入用户名称");
                    txtCustomer.Focus();
                    return false;
                }
                //如果是系统管理员或会员管理员的时候，不必做限制
                if (!this.cbxRole.SelectedValue.Equals(0) && !this.cbxRole.SelectedValue.Equals(3))
                {
                    if (cbxOpenBank.SelectedItem == null || cbxOpenBank.SelectedValue.ToString().Equals("0"))
                    {
                        MessageBoxHelper.ShowInfo(this, "请选择开户银行！");
                        cbxOpenBank.Focus();
                        return false;
                    }
                    //if (HelpCommon._bankType.Equals("allinfinance"))
                    //{
                    //    if (string.IsNullOrEmpty(this.txtBranchBank.Text.Trim()))
                    //    {
                    //        MessageBoxHelper.ShowInfo(this, "开户分行为空！");
                    //        txtBranchBank.Focus();
                    //        return false;
                    //    }
                    //}
                    //if (HelpCommon._bankType.Equals("allinfinance"))
                    //{
                    //    if (string.IsNullOrEmpty(this.txtBranchBankZH.Text.Trim()))
                    //    {
                    //        MessageBoxHelper.ShowInfo(this, "开户支行信息为空！");
                    //        txtBranchBankZH.Focus();
                    //        return false;
                    //    }
                    //}
                    if (string.IsNullOrEmpty(this.txtBankNo.Text.Trim()))
                    {
                        MessageBoxHelper.ShowInfo(this, "请输入银行卡号！");
                        txtBankNo.Focus();
                        return false;
                    }

                    long bank = 0;
                    if (long.TryParse(this.txtBankNo.Text, out bank) == false)
                    {
                        MessageBoxHelper.ShowInfo(this, "银行卡号必须全部是数字！");
                        txtBankNo.Focus();
                        return false;
                    }             
                }

                if (plPermission.Visible)
                {
                    if (ckBackRake.Checked == false && ckInfoManager.Checked == false)
                    {
                        MessageBoxHelper.ShowInfo(this, "请勾选会员管理员权限！");
                        ckBackRake.Focus();
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (IsValid() == false)
                return;

            if (_oldInfo == null)
                return;

            var user = new RakeBackService.UserInfo();
            user.UserId = _oldInfo.UserId;
            user.UserName = txtCustomer.Text.Trim();
            user.UserPhone = txtTelphone.Text.Trim();
            user.UserPwd = DesSecurityCommon.Encrypt(this.txtLoginPwd.Text.Trim());
            user.Remark = this.txtRemark.Text.Trim();
            if (this.cbxRole.SelectedItem == null)
            {
                user.RoleId = 0;
            }
            else
            {
                user.RoleId = Convert.ToInt32(this.cbxRole.SelectedValue);
            }
            user.LoginId = this.txtLogin.Text;
            user.CreateBank = this.cbxOpenBank.Text;
            user.BranchBank = txtBranchBank.Text;
            user.BranchBankZH = this.txtBranchBankZH.Text;
            user.BankNumber = this.txtBankNo.Text;
            user.Api = cbxOpenBank.SelectedValue.ToString();
            user.Province = "广东";
            user.City = "深圳";
            user.BelongTo = "PERSONAL";
            user.Createtime = _oldInfo.Createtime;
            user.Updatetime = DateTime.Now.ToString();
            user.Api = cbxOpenBank.SelectedValue.ToString();
            user.IsUpdatePass = _oldInfo.IsUpdatePass;
            user.InputPerson = ApplicationParam.UserInfo.LoginId;

            user.MenuId = string.Empty;
            if (ckBackRake.Checked)
                user.MenuId += "1";
            if (ckInfoManager.Checked)
                user.MenuId += "2";

            user.Iseable = _oldInfo.Iseable;

            try
            {
               
                    var result = CommunicationHelper.UpdateUserInfo(user, ApplicationParam.UserInfo.LoginId);

                    if (result != null && result.IsSuccess)
                    {
                        MessageBoxHelper.ShowInfo(this, "修改成功");
                        this.DialogResult = DialogResult.OK;
                    }
                    else if (result != null)
                    {
                        MessageBoxHelper.ShowError(this, "修改失败:" + result.ErrorMsg);
                    }
                
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowError(this, "添加失败:" + ex.Message);

            }

        }

        private void cbxRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            ckBackRake.Checked = false;
            ckInfoManager.Checked = false;

            if (cbxRole.SelectedValue!=null&&cbxRole.SelectedValue.ToString().Equals("3"))
            {
                plPermission.Visible = true;
            }
            else
                plPermission.Visible = false;
        }
    }
}
