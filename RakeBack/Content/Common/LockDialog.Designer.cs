namespace RakeBack.Content.Common
{
    partial class LockDialog
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel = new XiaoCai.WinformUI.Panels.PanelW();
            this.labelW1 = new XiaoCai.WinformUI.LabelW();
            this.buttonW1 = new XiaoCai.WinformUI.ButtonW();
            this.textBoxW1 = new XiaoCai.WinformUI.TextBoxW();
            this.buttonW2 = new XiaoCai.WinformUI.ButtonW();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.AssociatedSplitter = null;
            this.panel.BackColor = System.Drawing.Color.Transparent;
            this.panel.CaptionFont = new System.Drawing.Font("微软雅黑", 11.75F, System.Drawing.FontStyle.Bold);
            this.panel.CaptionHeight = 27;
            this.panel.Controls.Add(this.labelW1);
            this.panel.Controls.Add(this.buttonW2);
            this.panel.Controls.Add(this.buttonW1);
            this.panel.Controls.Add(this.textBoxW1);
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
            this.panel.Size = new System.Drawing.Size(434, 161);
            this.panel.TabIndex = 11;
            this.panel.Text = "panelW4";
            this.panel.ToolTipTextCloseIcon = null;
            this.panel.ToolTipTextExpandIconPanelCollapsed = null;
            this.panel.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // labelW1
            // 
            this.labelW1.AutoSize = true;
            this.labelW1.BackColor = System.Drawing.Color.Transparent;
            this.labelW1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelW1.Location = new System.Drawing.Point(50, 69);
            this.labelW1.Name = "labelW1";
            this.labelW1.Size = new System.Drawing.Size(41, 12);
            this.labelW1.Style = XiaoCai.WinformUI.Style.Office2007Blue;
            this.labelW1.TabIndex = 5;
            this.labelW1.Text = "密码：";
            this.labelW1.UseStyle = false;
            // 
            // buttonW1
            // 
            this.buttonW1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(153)))), ((int)(((byte)(194)))));
            this.buttonW1.ForeColor = System.Drawing.Color.Black;
            this.buttonW1.IsSilver = false;
            this.buttonW1.Location = new System.Drawing.Point(248, 62);
            this.buttonW1.MaxImageSize = new System.Drawing.Point(0, 0);
            this.buttonW1.MenuPos = new System.Drawing.Point(0, 0);
            this.buttonW1.Name = "buttonW1";
            this.buttonW1.Size = new System.Drawing.Size(75, 23);
            this.buttonW1.Style = XiaoCai.WinformUI.Style.Office2007Blue;
            this.buttonW1.TabIndex = 4;
            this.buttonW1.Text = "解锁";
            this.buttonW1.ToFocused = false;
            this.buttonW1.UseVisualStyleBackColor = true;
            this.buttonW1.Click += new System.EventHandler(this.buttonW1_Click);
            // 
            // textBoxW1
            // 
            this.textBoxW1.Location = new System.Drawing.Point(95, 64);
            this.textBoxW1.Name = "textBoxW1";
            this.textBoxW1.PasswordChar = '*';
            this.textBoxW1.Size = new System.Drawing.Size(126, 21);
            this.textBoxW1.Style = XiaoCai.WinformUI.Style.Office2007Blue;
            this.textBoxW1.TabIndex = 3;
            // 
            // buttonW2
            // 
            this.buttonW2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(153)))), ((int)(((byte)(194)))));
            this.buttonW2.ForeColor = System.Drawing.Color.Black;
            this.buttonW2.IsSilver = false;
            this.buttonW2.Location = new System.Drawing.Point(345, 62);
            this.buttonW2.MaxImageSize = new System.Drawing.Point(0, 0);
            this.buttonW2.MenuPos = new System.Drawing.Point(0, 0);
            this.buttonW2.Name = "buttonW2";
            this.buttonW2.Size = new System.Drawing.Size(75, 23);
            this.buttonW2.Style = XiaoCai.WinformUI.Style.Office2007Blue;
            this.buttonW2.TabIndex = 4;
            this.buttonW2.Text = "退出系统";
            this.buttonW2.ToFocused = false;
            this.buttonW2.UseVisualStyleBackColor = true;
            this.buttonW2.Click += new System.EventHandler(this.buttonW2_Click);
            // 
            // LockDialog
            // 
            this.AcceptButton = this.buttonW1;
            this.ClientSize = new System.Drawing.Size(434, 161);
            this.Controls.Add(this.panel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HideOnClose = true;
            this.MaximizeBox = false;
            this.Name = "LockDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TabText = "解锁";
            this.Text = "解锁";
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private XiaoCai.WinformUI.Panels.PanelW panel;
        private XiaoCai.WinformUI.LabelW labelW1;
        private XiaoCai.WinformUI.ButtonW buttonW1;
        private XiaoCai.WinformUI.TextBoxW textBoxW1;
        private XiaoCai.WinformUI.ButtonW buttonW2;
    }
}
