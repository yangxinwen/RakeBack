using RakeBack.Business;
using RakeBack.RakeBackService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace RakeBack.Helper
{
    /// <summary>
    /// 链路管理
    /// </summary>
    public class CommunicationHelper
    {
        private static RakeBackServiceClient _client = null;

        private static object lockObject = new object();

        /// <summary>
        /// 获取服务客户端代理
        /// </summary>
        /// <returns></returns>
        public static RakeBackServiceClient GetClient()
        {
            lock (lockObject)
            {
                try
                {
                    if (_client == null ||
                        _client.State == System.ServiceModel.CommunicationState.Closed ||
                        _client.State == System.ServiceModel.CommunicationState.Closing ||
                        _client.State == System.ServiceModel.CommunicationState.Faulted)
                    {
                        _client = new RakeBackServiceClient();
                        _client.Open();
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("创建链路失败:" + ex.Message);
                }

                if (ApplicationParam.MainForm != null)
                    ApplicationParam.MainForm._freeTime = 0;
                return _client;
            }
        }

        public static string SessionId;

        private static void AddMessageHead(IContextChannel channel)
        {
            string sessionId = Guid.NewGuid().ToString();
            using (OperationContextScope scope = new OperationContextScope(channel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", sessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
            }
        }



        public static RakeBack.RakeBackService.ResponseBaseOfArrayOfUserInfoYgFqSxnr GetNewRakeBack(int pageSize, int pageIndex, System.Collections.Generic.Dictionary<string, string> conditions)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.GetNewRakeBack(pageSize, pageIndex, conditions);
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfboolean AddRakeBack(RakeBack.RakeBackService.OrderInfo info, string operateLoginId)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.AddRakeBack(info, operateLoginId);
            }

        }

        public static RakeBack.RakeBackService.ResponseBaseOfArrayOfOrderInfoYgFqSxnr GetRakeBack(int pageSize, int pageIndex, System.Collections.Generic.Dictionary<string, string> conditions)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.GetRakeBack(pageSize, pageIndex, conditions);
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfArrayOfRoleInfoYgFqSxnr GetRoleInfo()
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.GetRoleInfo();
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfArrayOfUserInfoYgFqSxnr GetUserInfo(int pageSize, int pageIndex, System.Collections.Generic.Dictionary<string, string> conditions)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.GetUserInfo(pageSize, pageIndex, conditions);
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfUserInfoYgFqSxnr AddUserInfo(RakeBack.RakeBackService.UserInfo info, string operateLoginId)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.AddUserInfo(info, operateLoginId);
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfUserInfoYgFqSxnr UpdateUserInfo(RakeBack.RakeBackService.UserInfo info, string operateLoginId)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.UpdateUserInfo(info, operateLoginId);
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfboolean DelUserInfo(int id, string operateLoginId)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.DelUserInfo(id, operateLoginId);
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfboolean UpdateOrderInfo(RakeBack.RakeBackService.OrderInfo info, string operateLoginId)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.UpdateOrderInfo(info, operateLoginId);
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfboolean DelOrderInfo(RakeBack.RakeBackService.OrderInfo info, string operateLoginId)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.DelOrderInfo(info, operateLoginId);
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfUserInfoYgFqSxnr Login(string loginCode, string password)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.Login(loginCode, password);
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfboolean OutLogin(int userId, string userName)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.OutLogin(userId, userName);
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfboolean UpdateUserPassword(int userId, string oldPwd, string newPwd)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.UpdateUserPassword(userId, oldPwd, newPwd);
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfTupleOfstringstring5F2dSckg GetAmountStatistics(System.Collections.Generic.Dictionary<string, string> conditions)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.GetAmountStatistics(conditions);
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfArrayOfOperInfoYgFqSxnr GetOperateLog(int pageSize, int pageIndex, System.Collections.Generic.Dictionary<string, string> conditions)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.GetOperateLog(pageSize, pageIndex, conditions);
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfArrayOfFlowInfoYgFqSxnr GetOrderLog(int pageSize, int pageIndex, System.Collections.Generic.Dictionary<string, string> conditions)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.GetOrderLog(pageSize, pageIndex, conditions);
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfstring GetConfig(string key)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.GetConfig(key);
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfboolean AddOrderFlowLog(RakeBack.RakeBackService.OrderFlowLogType logType, string orderId)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.AddOrderFlowLog(logType, orderId);
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfUserInfoYgFqSxnr GetUserInfoById(int userId)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.GetUserInfoById(userId);
            }
        }

        public static RakeBack.RakeBackService.ResponseBaseOfboolean AddOutMoneyOperateLog(int userId, string userName, string orderId)
        {
            var client = GetClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                var header = MessageHeader.CreateHeader("sessionId", "HeadMessage", SessionId);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return client.AddOutMoneyOperateLog(userId, userName, orderId);
            }
        }








    }
}
