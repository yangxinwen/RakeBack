namespace RakeBack
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.panel = new XiaoCai.WinformUI.Panels.PanelW();
            this.pbValid = new System.Windows.Forms.PictureBox();
            this.btnCancel = new XiaoCai.WinformUI.ButtonW();
            this.btnLogin = new XiaoCai.WinformUI.ButtonW();
            this.txtValid = new XiaoCai.WinformUI.TextBoxW();
            this.lbError = new XiaoCai.WinformUI.LabelW();
            this.labelW3 = new XiaoCai.WinformUI.LabelW();
            this.txtPassword = new XiaoCai.WinformUI.TextBoxW();
            this.labelW2 = new XiaoCai.WinformUI.LabelW();
            this.txtUser = new XiaoCai.WinformUI.TextBoxW();
            this.labelW1 = new XiaoCai.WinformUI.LabelW();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbValid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.AssociatedSplitter = null;
            this.panel.BackColor = System.Drawing.Color.Transparent;
            this.panel.CaptionFont = new System.Drawing.Font("微软雅黑", 11.75F, System.Drawing.FontStyle.Bold);
            this.panel.CaptionHeight = 27;
            this.panel.Controls.Add(this.pbValid);
            this.panel.Controls.Add(this.btnCancel);
            this.panel.Controls.Add(this.btnLogin);
            this.panel.Controls.Add(this.txtValid);
            this.panel.Controls.Add(this.lbError);
            this.panel.Controls.Add(this.labelW3);
            this.panel.Controls.Add(this.txtPassword);
            this.panel.Controls.Add(this.labelW2);
            this.panel.Controls.Add(this.txtUser);
            this.panel.Controls.Add(this.labelW1);
            this.panel.Controls.Add(this.pictureBox1);
            this.panel.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.panel.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.panel.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.panel.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.panel.CustomColors.CaptionGradientEnd = System.Drawing.SystemColors.ButtonFace;
            this.panel.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.panel.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.panel.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.panel.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.panel.CustomColors.CollapsedCaptionText = System.Drawing.SystemColors.ControlText;
            this.panel.CustomColors.ContentGradientBegin = System.Drawing.SystemColors.ButtonFace;
            this.panel.CustomColors.ContentGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.panel.CustomColors.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel.Image = null;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.MinimumSize = new System.Drawing.Size(27, 27);
            this.panel.Name = "panel";
            this.panel.PanelStyle = XiaoCai.WinformUI.Panels.PanelStyle.Office2007;
            this.panel.ShowCaptionbar = false;
            this.panel.ShowCaptionbarBorder = false;
            this.panel.ShowTransparentBackground = false;
            this.panel.Size = new System.Drawing.Size(474, 250);
            this.panel.TabIndex = 11;
            this.panel.Text = "panelW4";
            this.panel.ToolTipTextCloseIcon = null;
            this.panel.ToolTipTextExpandIconPanelCollapsed = null;
            this.panel.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // pbValid
            // 
            this.pbValid.Location = new System.Drawing.Point(304, 92);
            this.pbValid.Name = "pbValid";
            this.pbValid.Size = new System.Drawing.Size(99, 24);
            this.pbValid.TabIndex = 4;
            this.pbValid.TabStop = false;
            this.pbValid.Click += new System.EventHandler(this.pbValid_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(153)))), ((int)(((byte)(194)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.IsSilver = false;
            this.btnCancel.Location = new System.Drawing.Point(362, 206);
            this.btnCancel.MaxImageSize = new System.Drawing.Point(0, 0);
            this.btnCancel.MenuPos = new System.Drawing.Point(0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = XiaoCai.WinformUI.Style.Office2007Blue;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.ToFocused = false;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            this.btnLogin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(153)))), ((int)(((byte)(194)))));
            this.btnLogin.ForeColor = System.Drawing.Color.Black;
            this.btnLogin.IsSilver = false;
            this.btnLogin.Location = new System.Drawing.Point(263, 206);
            this.btnLogin.MaxImageSize = new System.Drawing.Point(0, 0);
            this.btnLogin.MenuPos = new System.Drawing.Point(0, 0);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.Style = XiaoCai.WinformUI.Style.Office2007Blue;
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "登录";
            this.btnLogin.ToFocused = false;
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtValid
            // 
            this.txtValid.Location = new System.Drawing.Point(303, 131);
            this.txtValid.Name = "txtValid";
            this.txtValid.Size = new System.Drawing.Size(100, 21);
            this.txtValid.Style = XiaoCai.WinformUI.Style.Office2007Blue;
            this.txtValid.TabIndex = 3;
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.BackColor = System.Drawing.Color.Transparent;
            this.lbError.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbError.Location = new System.Drawing.Point(261, 173);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(35, 12);
            this.lbError.Style = XiaoCai.WinformUI.Style.Office2007Blue;
            this.lbError.TabIndex = 1;
            this.lbError.Text = "error";
            this.lbError.UseStyle = false;
            // 
            // labelW3
            // 
            this.labelW3.AutoSize = true;
            this.labelW3.BackColor = System.Drawing.Color.Transparent;
            this.labelW3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelW3.Location = new System.Drawing.Point(240, 134);
            this.labelW3.Name = "labelW3";
            this.labelW3.Size = new System.Drawing.Size(53, 12);
            this.labelW3.Style = XiaoCai.WinformUI.Style.Office2007Blue;
            this.labelW3.TabIndex = 1;
            this.labelW3.Text = "验证码：";
            this.labelW3.UseStyle = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(304, 59);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(100, 21);
            this.txtPassword.Style = XiaoCai.WinformUI.Style.Office2007Blue;
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Text = "88888";
            // 
            // labelW2
            // 
            this.labelW2.AutoSize = true;
            this.labelW2.BackColor = System.Drawing.Color.Transparent;
            this.labelW2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelW2.Location = new System.Drawing.Point(240, 62);
            this.labelW2.Name = "labelW2";
            this.labelW2.Size = new System.Drawing.Size(53, 12);
            this.labelW2.Style = XiaoCai.WinformUI.Style.Office2007Blue;
            this.labelW2.TabIndex = 1;
            this.labelW2.Text = "密  码：";
            this.labelW2.UseStyle = false;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(304, 25);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(100, 21);
            this.txtUser.Style = XiaoCai.WinformUI.Style.Office2007Blue;
            this.txtUser.TabIndex = 1;
            this.txtUser.Text = "1";
            // 
            // labelW1
            // 
            this.labelW1.AutoSize = true;
            this.labelW1.BackColor = System.Drawing.Color.Transparent;
            this.labelW1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelW1.Location = new System.Drawing.Point(239, 28);
            this.labelW1.Name = "labelW1";
            this.labelW1.Size = new System.Drawing.Size(53, 12);
            this.labelW1.Style = XiaoCai.WinformUI.Style.Office2007Blue;
            this.labelW1.TabIndex = 1;
            this.labelW1.Text = "用  户：";
            this.labelW1.UseStyle = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(193, 235);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(474, 250);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TabText = "欢迎使用线上返佣系统";
            this.Text = "欢迎使用线上返佣系统";
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbValid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private XiaoCai.WinformUI.Panels.PanelW panel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pbValid;
        private XiaoCai.WinformUI.ButtonW btnLogin;
        private XiaoCai.WinformUI.TextBoxW txtValid;
        private XiaoCai.WinformUI.LabelW labelW3;
        private XiaoCai.WinformUI.TextBoxW txtPassword;
        private XiaoCai.WinformUI.LabelW labelW2;
        private XiaoCai.WinformUI.TextBoxW txtUser;
        private XiaoCai.WinformUI.LabelW labelW1;
        private XiaoCai.WinformUI.ButtonW btnCancel;
        private XiaoCai.WinformUI.LabelW lbError;
    }
}