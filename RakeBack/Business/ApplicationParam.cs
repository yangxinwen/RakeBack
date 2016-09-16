using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RakeBack.Business
{
    public class ApplicationParam
    {
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public static RakeBackService.UserInfo UserInfo { get; set; }

        /// <summary>
        /// 保存主界面
        /// </summary>
        public static MainForm MainForm { get; set; }


        static ApplicationParam()
        {
            UserInfo = new RakeBackService.UserInfo();
            //UserInfo.LoginId = "123";
            //UserInfo.InputPerson = "1123";
        }
    }
}
