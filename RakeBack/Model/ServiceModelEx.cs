using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakeBack.RakeBackService
{
    public partial class UserInfo
    {
        public string RoleName
        {
            get
            {
                var name = string.Empty;
                if (RoleId == 0)
                    name = "系统管理员";
                else if (RoleId == 3)
                    name = "会员管理员";
                else if (RoleId == 2)
                    name = "会员";
                return name;
            }
        }
    }

    public partial class OrderInfo
    {
        public string OrderStatusText
        {
            get
            {
                var txt = string.Empty;

                int o;

                int.TryParse(this.OrderStatus, out o);

                Model.OrderStatus status = (Model.OrderStatus)o;
                switch (status)
                {
                    case Model.OrderStatus.All:
                        txt = "-";
                        break;
                    case Model.OrderStatus.NewOrder:
                        txt = "新单";
                        break;
                    case Model.OrderStatus.Audited:
                        txt = "已审核";
                        break;
                    case Model.OrderStatus.Browse:
                        txt = "会员已浏览";
                        break;
                    case Model.OrderStatus.BankReturnSuccess:
                        txt = "银行已返回成功";
                        break;
                    case Model.OrderStatus.BankReturnFail:
                        txt = "银行已返回失败";
                        break;
                    case Model.OrderStatus.BankDealing:
                        txt = "银行处理中";
                        break;
                    default:
                        break;
                }

                return txt;
            }
        }
    }
}
