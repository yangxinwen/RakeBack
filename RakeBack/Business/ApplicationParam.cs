using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakeBack.Business
{
    public class ApplicationParam
    {
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public static RakeBackService.UserInfo UserInfo { get; set; }

        static ApplicationParam()
        {
            UserInfo = new RakeBackService.UserInfo();
            //UserInfo.LoginId = "123";
            //UserInfo.InputPerson = "1123";
        }
    }
}
