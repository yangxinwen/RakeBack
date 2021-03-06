﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RakeBack.Business
{
    public class ApplicationParam
    {

        public static string OutMoneyUrl { get; set; }
        public static string B2CSettleKey { get; set; }
        public static string Version { get; set; }

        /// <summary>
        /// 输入密码
        /// </summary>
        public static string InputPwd { get; set; }
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
            Version=Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
