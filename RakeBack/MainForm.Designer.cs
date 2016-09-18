namespace RakeBack
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.mainMenu = new XiaoCai.WinformUI.ToolStrips.MenuStripW(this.components);
            this.系统SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.窗口WToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCloseAllDocuments = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCloseOtherWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAboutSystemItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnHelpItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dockPanel1 = new XiaoCai.WinformUI.Docking.DockPanel();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统SToolStripMenuItem,
            this.窗口WToolStripMenuItem,
            this.帮助HToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(721, 25);
            this.mainMenu.Style = XiaoCai.WinformUI.Style.Office2007Blue;
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStripW1";
            // 
            // 系统SToolStripMenuItem
            // 
            this.系统SToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出XToolStripMenuItem});
            this.系统SToolStripMenuItem.Name = "系统SToolStripMenuItem";
            this.系统SToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.系统SToolStripMenuItem.Text = "系统(&S)";
            // 
            // 退出XToolStripMenuItem
            // 
            this.退出XToolStripMenuItem.Name = "退出XToolStripMenuItem";
            this.退出XToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.退出XToolStripMenuItem.Text = "退出(&X)";
            // 
            // 窗口WToolStripMenuItem
            // 
            this.窗口WToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCloseAllDocuments,
            this.btnCloseOtherWindows});
            this.窗口WToolStripMenuItem.Name = "窗口WToolStripMenuItem";
            this.窗口WToolStripMenuItem.Size = new System.Drawing.Size(64, 21);
            this.窗口WToolStripMenuItem.Text = "窗口(&W)";
            // 
            // btnCloseAllDocuments
            // 
            this.btnCloseAllDocuments.Name = "btnCloseAllDocuments";
            this.btnCloseAllDocuments.Size = new System.Drawing.Size(190, 22);
            this.btnCloseAllDocuments.Text = "关闭所有窗口(&A)";
            // 
            // btnCloseOtherWindows
            // 
            this.btnCloseOtherWindows.Name = "btnCloseOtherWindows";
            this.btnCloseOtherWindows.Size = new System.Drawing.Size(190, 22);
            this.btnCloseOtherWindows.Text = "除此之外全部关闭(&O)";
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAboutSystemItem,
            this.btnHelpItem});
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // btnAboutSystemItem
            // 
            this.btnAboutSystemItem.Name = "btnAboutSystemItem";
            this.btnAboutSystemItem.Size = new System.Drawing.Size(118, 22);
            this.btnAboutSystemItem.Text = "关于(A&)";
            // 
            // btnHelpItem
            // 
            this.btnHelpItem.Name = "btnHelpItem";
            this.btnHelpItem.Size = new System.Drawing.Size(118, 22);
            this.btnHelpItem.Text = "帮助(&O)";
            // 
            // dockPanel1
            // 
            this.dockPanel1.ActiveAutoHideContent = null;
            this.dockPanel1.AllowDrop = true;
            this.dockPanel1.AllowEndUserDocking = false;
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DocumentStyle = XiaoCai.WinformUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel1.Location = new System.Drawing.Point(0, 25);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(721, 374);
            this.dockPanel1.Style = XiaoCai.WinformUI.Style.Office2007Blue;
            this.dockPanel1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 399);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.mainMenu);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TabText = "MainForm";
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private XiaoCai.WinformUI.ToolStrips.MenuStripW mainMenu;
        private System.Windows.Forms.ToolStripMenuItem 系统SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 窗口WToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnCloseAllDocuments;
        private System.Windows.Forms.ToolStripMenuItem btnCloseOtherWindows;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnAboutSystemItem;
        private System.Windows.Forms.ToolStripMenuItem btnHelpItem;
        private XiaoCai.WinformUI.Docking.DockPanel dockPanel1;
    }
}

