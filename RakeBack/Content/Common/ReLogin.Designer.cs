﻿namespace RakeBack.Content.Common
{
    partial class ReLogin
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
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.AssociatedSplitter = null;
            this.panel.BackColor = System.Drawing.Color.Transparent;
            this.panel.CaptionFont = new System.Drawing.Font("微软雅黑", 11.75F, System.Drawing.FontStyle.Bold);
            this.panel.CaptionHeight = 27;
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
            this.panel.Size = new System.Drawing.Size(477, 228);
            this.panel.TabIndex = 10;
            this.panel.Text = "panelW4";
            this.panel.ToolTipTextCloseIcon = null;
            this.panel.ToolTipTextExpandIconPanelCollapsed = null;
            this.panel.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // ReLogin
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(477, 228);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HideOnClose = true;
            this.Name = "ReLogin";
            this.TabText = "注销";
            this.Text = "注销";
            this.ResumeLayout(false);

        }

        #endregion

        private XiaoCai.WinformUI.Panels.PanelW panel;
    }
}
