using RakeBack.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XiaoCai.WinformUI.Docking;

namespace RakeBack
{
    public class BaseForm : DockContent
    {
        protected static int _outTime = 20 * 1000;

        private Timer _timer = null;

        public BaseForm()
        {
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
            this.Enabled = false;

            this.TabText += "...";
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
    }
}
