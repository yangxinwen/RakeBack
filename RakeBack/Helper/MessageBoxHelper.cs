using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RakeBack.Helper
{
   public class MessageBoxHelper
    {
        public static DialogResult ShowError(IWin32Window owner, string message,string cation="错误")
        {
           return MessageBoxEx.Show(owner, message, cation,MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
        public static DialogResult ShowInfo(IWin32Window owner, string message, string cation = "提示")
        {
            return MessageBoxEx.Show(owner, message, cation, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static DialogResult ShowConf(IWin32Window owner, string message, string cation = "确认")
        {
            return MessageBoxEx.Show(owner, message, cation, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }
    }
}
