using RakeBack.Business;
using RakeBack.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace RakeBack
{
    public class BaseForm : DockContent
    {
        protected static int _outTime = 20 * 1000;

        private Timer _timer = null;

        public BaseForm()
        {
            this.Load += BaseForm_Load;
            this.MouseMove += BaseForm_MouseMove;
        }

        private void BaseForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (ApplicationParam.MainForm != null)
                ApplicationParam.MainForm._freeTime = 0;
        }
        private void BaseForm_Load(object sender, EventArgs e)
        {
            if (this.DesignMode == false)
                this.Icon = new System.Drawing.Icon(Application.StartupPath + "/Resource/RakeBack.ico");

            this.HideOnClose = true;
        }


        /// <summary>
        /// 设置tDataGridView显示样式
        /// </summary>
        /// <param name="view"></param>
        protected void SetDataGridViewStyle(DataGridView view)
        {
            view.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            view.AutoGenerateColumns = false;
            view.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            view.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn item in view.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

        }


        private void _timer_Tick(object sender, EventArgs e)
        {
            if (this.Enabled == false)
            {
                _timer.Stop();
                _timer = null;
                EndWait();
                MessageBoxHelper.ShowInfo(this, "超时");
            }
        }

        public void StartWait()
        {
            this.TabText += "...";
            this.Enabled = false;
            this.Activate();
            if (_timer != null && _timer.Enabled)
                _timer.Stop();
            _timer = new Timer();
            _timer.Interval = _outTime;
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        public void EndWait()
        {
            this.Invoke(new Action(() =>
            {
                Enabled = true;
                this.TabText = this.TabText.TrimEnd('.');
            }));

            if (_timer != null && _timer.Enabled)
            {
                _timer.Stop();
                _timer = null;
            }

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BaseForm";
            this.ResumeLayout(false);

        }
    }
}
