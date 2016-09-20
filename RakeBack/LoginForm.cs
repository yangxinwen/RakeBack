using RakeBack.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RakeBack
{
    public partial class LoginForm : BaseForm
    {
        ValidCode _validCode;


        public LoginForm()
        {
            InitializeComponent();
            _validCode = new ValidCode(4, ValidCode.CodeType.Alphas);
            pbValid.Image = Bitmap.FromStream(_validCode.CreateCheckCodeImage());
            lbError.ForeColor = Color.Red;
            lbError.Text = string.Empty;

            CommunicationHelper.SessionId = Guid.NewGuid().ToString();
        }

        private void MakeValidCode()
        {
            pbValid.Image = Bitmap.FromStream(_validCode.CreateCheckCodeImage());
        }

        private void pbValid_Click(object sender, EventArgs e)
        {
            MakeValidCode();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (IsValid() == false)
                return;

            Login(txtUser.Text, DesSecurityCommon.Encrypt(txtPassword.Text));

        }
        private bool IsValid()
        {
            lbError.Text = string.Empty;
            lbError.Visible = true;
            if (string.IsNullOrEmpty(txtUser.Text))
            {
                lbError.Text = "请输入用户名称！";
                txtUser.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                lbError.Text = "请输入用户密码！";
                txtPassword.Focus();
                return false;
            }

            if (_validCode.CheckCode.Equals(txtValid.Text, StringComparison.OrdinalIgnoreCase) == false)
            {
                lbError.Text = "验证码错误";
                txtValid.Focus();
                return false;
            }
            MakeValidCode();

            return true;
        }

        private void Login(string loginCode, string password)
        {
            btnLogin.Enabled = false;
            lbError.Text = "登录中...";

            try
            {
                var result = CommunicationHelper.Login(loginCode, password);

                if (result != null && result.IsSuccess)
                {
                    Business.ApplicationParam.InputPwd = txtPassword.Text;
                    Business.ApplicationParam.UserInfo = result.Content;
                    this.DialogResult = DialogResult.OK;
                }
                else if (result != null)
                {
                    lbError.Text = "登录失败:" + result.ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                lbError.Text = "登录失败";
            }
            btnLogin.Enabled = true;

        }

    }
}
