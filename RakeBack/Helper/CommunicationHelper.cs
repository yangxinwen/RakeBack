using RakeBack.RakeBackService;
using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// 获取服务客户端代理
        /// </summary>
        /// <returns></returns>
        public static RakeBackServiceClient GetClient()
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
            return _client;
        }
    }
}
