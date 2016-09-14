using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakeBack.Model
{
    public enum OrderStatus
    {
        All=0,
        /// <summary>
        /// 新单
        /// </summary>
        NewOrder = 1,
        /// <summary>
        /// 已审核
        /// </summary>
        Audited = 6,
        /// <summary>
        /// 会员已浏览
        /// </summary>
        Browse = 2,
        /// <summary>
        /// 银行已返回成功
        /// </summary>
        BankReturnSuccess = 3,
        /// <summary>
        /// 银行已返回失败
        /// </summary>
        BankReturnFail = 4,
        /// <summary>
        /// 银行处理中
        /// </summary>
        BankDealing = 5,


    }
}
