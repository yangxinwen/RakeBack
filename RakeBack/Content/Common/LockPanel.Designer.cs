namespace RakeBack.Content.Common
{
    partial class LockPanel
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
            this.SuspendLayout();
            // 
            // LockPanel
            // 
            this.ClientSize = new System.Drawing.Size(503, 268);
            this.Name = "LockPanel";
            this.TabText = "解锁";
            this.Tag = "lock";
            this.Text = "解锁";
            this.ResumeLayout(false);

        }

        #endregion

        private XiaoCai.WinformUI.Panels.PanelW panel;
    }
}
