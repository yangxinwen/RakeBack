using RakeBack.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakeBack.Business
{
    public class BllHelper
    {
        public static void OutLogin(int userId, string userName)
        {
            ThreadHelper.StartNew(() =>
            {
                try
                {
                    var client = CommunicationHelper.GetClient();
                    if (client != null)
                    {
                        var result = client.OutLogin(userId,userName);
                    }
                }
                catch (Exception ex) { }
            });
        }
    }
}
