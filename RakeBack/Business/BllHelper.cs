using RakeBack.Helper;
using RakeBack.Model;
using RakeBack.RakeBackService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakeBack.Business
{
    public class BllHelper
    {
        public static void LoadConfig()
        {
            ThreadHelper.StartNew(() =>
            {
                try
                {
                    var client = CommunicationHelper.GetClient();
                    if (client != null)
                    {
                        var result = client.GetConfig("OutMoneyUrl");
                        if (result != null && result.IsSuccess)
                            ApplicationParam.OutMoneyUrl = result.Content;

                        result = client.GetConfig("B2CSettleKey");
                        if (result != null && result.IsSuccess)
                            ApplicationParam.B2CSettleKey = result.Content;
                    }
                }
                catch (Exception ex) { }
            });
        }

        public static void OutLogin(int userId, string userName)
        {
            ThreadHelper.StartNew(() =>
            {
                try
                {
                    var client = CommunicationHelper.GetClient();
                    if (client != null)
                    {
                        var result = client.OutLogin(userId, userName);
                    }
                }
                catch (Exception ex) { }
            });
        }
        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="order"></param>
        /// <param name="status"></param>
        public static void UpdateOrderStatus(OrderInfo order, OrderStatus status)
        {
            ThreadHelper.StartNew(() =>
            {
                try
                {
                    var client = CommunicationHelper.GetClient();
                    if (client != null)
                    {
                        order.OrderStatus = (int)status + "";
                        order.InputPerson = ApplicationParam.UserInfo.InputPerson;
                        order.UpdateTime = DateTime.Now;
                        order.SubmitTime = DateTime.Now;

                        var result = client.UpdateOrderInfo(order, ApplicationParam.UserInfo.LoginId);
                    }
                }
                catch (Exception ex) { }
            });
        }
    }
}
