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
    public partial class AddMember : RakeBack.BaseForm
    {
        public AddMember()
        {
            InitializeComponent();

            InitRoleCombox();
            InitBankCombox();
        }


        private void InitRoleCombox()
        {
            //ThreadHelper.StartNew(() =>
            //{
            try
            {
                var result = CommunicationHelper.GetRoleInfo();

                //this.Invoke(new Action(() =>
                //{
                if (result != null && result.IsSuccess)
                {
                    var dic = new Dictionary<int, string>();



                    foreach (var item in result.Content)
                    {
                        if (item.RoleId == 0)
                            continue;

                        if (ApplicationParam.UserInfo.RoleId == 0)
                        {
                            dic.Add(item.RoleId, item.RoleName);
                        }
                        else if (ApplicationParam.UserInfo.RoleId == 3)
                        {
                            if (item.RoleId == 2)
                                dic.Add(item.RoleId, item.RoleName);
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
                    MessageBoxHelper.ShowError(this, "角色信息查询出错" + result.ErrorMsg);
                }
                //}));
                //base.EndWait();

            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    MessageBoxHelper.ShowError(this, "查询出错:" + ex.Message);
                }
                ));
            }
            //});
        }

        private void InitBankCombox()
        {
            var dt = HelpCommon.GetBank();

            cbxOpenBank.DisplayMember = "ckey";
            cbxOpenBank.ValueMember = "cvalue";
            cbxOpenBank.DataSource = dt;

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
                    //        MessageBoxHelper.ShowInfo(this, "开户分行信息为空！");
                    //        txtBranchBank.Focus();
                    //        return false;
                    //    }
                    //    if (string.IsNullOrEmpty(this.txtBankZH.Text.Trim()))
                    //    {
                    //        MessageBoxHelper.ShowInfo(this, "开户支行信息为空！");
                    //        txtBankZH.Focus();
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


            var user = new RakeBackService.UserInfo();
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
            user.BranchBank = this.txtBranchBank.Text;
            user.BranchBankZH = this.txtBankZH.Text;
            user.BankNumber = this.txtBankNo.Text;
            user.Province = "广东";
            user.City = "深圳";
            user.BelongTo = "PERSONAL";
            user.Createtime = DateTime.Now.ToString();
            user.Updatetime = DateTime.Now.ToString();
            user.Api = cbxOpenBank.SelectedValue.ToString();
            user.IsUpdatePass = "0";//新增的账户首次登陆必须改密码
            user.InputPerson = ApplicationParam.UserInfo.LoginId;
            user.Iseable = "1";//新增用户必须是启用状态

            user.MenuId = string.Empty;
            if (ckBackRake.Checked)
                user.MenuId += "1";
            if (ckInfoManager.Checked)
                user.MenuId += "2";


            base.StartWait();

            ThreadHelper.StartNew(() =>
            {
                try
                {

                    var result = CommunicationHelper.AddUserInfo(user, ApplicationParam.UserInfo.LoginId);

                    base.EndWait();
                    this.Invoke(new Action(() =>
                    {
                        if (result != null && result.IsSuccess)
                        {
                            MessageBoxHelper.ShowInfo(this, "添加成功");

                            if (ApplicationParam.MainForm != null)
                            {
                                var f = ApplicationParam.MainForm.ShowForm("SystemSet.MemberMgr");
                                if (f != null)
                                {
                                    (f as MemberMgr).AutoSearch();
                                    this.Hide();
                                }
                            }


                        }
                        else if (result != null)
                        {
                            MessageBoxHelper.ShowError(this, "添加失败:" + result.ErrorMsg);
                        }
                    }));

                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        base.EndWait();
                        MessageBoxHelper.ShowError(this, "添加失败:" + ex.Message);
                    }
                    ));
                }
            });

        }

        private void cbxRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            ckBackRake.Checked = false;
            ckInfoManager.Checked = false;

            if (cbxRole.SelectedValue != null && cbxRole.SelectedValue.ToString().Equals("3"))
            {
                plPermission.Visible = true;
            }
            else
                plPermission.Visible = false;
        }
    }
}
