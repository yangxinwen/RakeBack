using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace RakeBack.Helper
{
    public class HelpCommon
    {
        public static string _bankType = System.Configuration.ConfigurationManager.AppSettings["sys_banktype"].ToString();
        //获取银行列表
        public static DataTable GetBank()
        {
            System.Collections.Specialized.NameValueCollection abc = System.Configuration.ConfigurationManager.AppSettings;
            DataTable dt = new DataTable();
            dt.Columns.Add("ckey",typeof(string));
            dt.Columns.Add("cvalue",typeof(string));

            dt.Rows.Add(new string[] {"无","0"});

            switch (_bankType)
            {
                case "allinfinance":
                    for (int i = 0; i < abc.AllKeys.Length; i++)
                    {
                        if (abc.Keys[i].ToString().Trim().Contains("allinfinance_bank_") && abc.Keys[i].ToString().Trim().Contains("_show_true"))
                        {
                            string ckey = abc.Keys[i].ToString().Split('_')[2];
                            string cvalue = abc[i].ToString();
                            DataRow dr = dt.NewRow();
                            dr["ckey"] = ckey;
                            dr["cvalue"] = cvalue;
                            dt.Rows.Add(dr);
                        }
                    }
                    break;
                case "allinpay":
                    for (int i = 0; i < abc.AllKeys.Length; i++)
                    {
                        if (abc.Keys[i].ToString().Trim().Contains("allinpay_bank_") && abc.Keys[i].ToString().Trim().Contains("_show_true"))
                        {
                            string ckey = abc.Keys[i].ToString().Split('_')[2];
                            string cvalue = abc[i].ToString();
                            DataRow dr = dt.NewRow();
                            dr["ckey"] = ckey;
                            dr["cvalue"] = cvalue;
                            dt.Rows.Add(dr);
                        }
                    }
                    break;
                case "sxyzf":
                    for (int i = 0; i < abc.AllKeys.Length; i++)
                    {
                        if (abc.Keys[i].ToString().Trim().Contains("sxyzf_bank_") && abc.Keys[i].ToString().Trim().Contains("_show_true"))
                        {
                            string ckey = abc.Keys[i].ToString().Split('_')[2];
                            string cvalue = abc[i].ToString();
                            DataRow dr = dt.NewRow();
                            dr["ckey"] = ckey;
                            dr["cvalue"] = cvalue;
                            dt.Rows.Add(dr);
                        }
                    }
                    break;
                default:
                    break;
            }
            return dt;
        }
    }
}