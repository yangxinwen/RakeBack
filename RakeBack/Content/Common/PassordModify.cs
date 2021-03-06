﻿using RakeBack.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RakeBack.Content.Common
{
    public partial class PassordModify : RakeBack.BaseForm
    {
        public PassordModify()
        {
            InitializeComponent();
        }

        private bool IsValid()
        {

            if (string.IsNullOrEmpty(txtOld.Text))
            {
                MessageBoxHelper.ShowInfo(this, "请输入原密码");
                txtOld.Focus();
                return false;

            }
            if (string.IsNullOrEmpty(txtNew.Text))
            {
                MessageBoxHelper.ShowInfo(this, "请输入新密码");
                txtNew.Focus();
                return false;

            }
            if (string.IsNullOrEmpty(txtNewConf.Text))
            {
                MessageBoxHelper.ShowInfo(this, "请输入确认密码");
                txtNewConf.Focus();
                return false;

            }

            if (txtNewConf.Text.Equals(txtNew.Text) == false)
            {
                MessageBoxHelper.ShowInfo(this, "确认密码不一致");
                return false;

            }

            return true;
        }


        private void UpdataPwd(int userId, string oldPwd, string newPwd)
        {
            base.StartWait();
            ThreadHelper.StartNew(() =>
            {
                try
                {

                    var result = CommunicationHelper.UpdateUserPassword(userId, oldPwd, newPwd);
                    base.EndWait();
                    this.Invoke(new Action(() =>
                    {
                        if (result != null && result.IsSuccess)
                        {
                            MessageBoxHelper.ShowInfo(this, "更新成功,您需要重新登录");
                            Application.Restart();
                        }
                        else if (result != null)
                        {
                            MessageBoxHelper.ShowError(this, "更新失败:" + result.ErrorMsg);
                        }
                    }));
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        base.EndWait();
                        MessageBoxHelper.ShowError(this, "删除失败:" + ex.Message);
                    }
                    ));
                }
            }, _outTime);
            base.EndWait();
        }

        private void buttonW1_Click(object sender, EventArgs e)
        {
            if (IsValid() == false) return;
            string oldPwd = DesSecurityCommon.Encrypt(txtOld.Text);
            string newPwd = DesSecurityCommon.Encrypt(txtNew.Text); ;
            UpdataPwd(Business.ApplicationParam.UserInfo.UserId, oldPwd, newPwd);
        }

        private void buttonW2_Click(object sender, EventArgs e)
        {
            txtOld.Text = string.Empty;
            txtNew.Text = string.Empty;
            txtNewConf.Text = string.Empty;
        }
    }
}
