using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakeBack.Model
{
    /// <summary>
    /// 返佣统计model
    /// </summary>
    public class RakeBackTakenModel
    {

        public string ALl { get; set; }
        public string ALlCount { get; set; }

        public string AllText
        {
            get
            {
                return string.Format("共{0}元 共{1}笔订单", ALl, ALlCount);
            }
        }


        public string NewOrder { get; set; }
        public string NewOrderCount { get; set; }
        public string NewOrderText
        {
            get
            {
                return string.Format("共{0}元 共{1}笔订单", NewOrder, NewOrderCount);
            }
        }


        public string Audited { get; set; }
        public string AuditedCount { get; set; }
        public string AuditedText
        {
            get
            {
                return string.Format("共{0}元 共{1}笔订单", Audited, AuditedCount);
            }
        }

        public string Browsed { get; set; }
        public string BrowsedCount { get; set; }
        public string BrowsedText
        {
            get
            {
                return string.Format("共{0}元 共{1}笔订单", Browsed, BrowsedCount);
            }
        }

        public string BankDealing { get; set; }
        public string BankDealingCount { get; set; }
        public string BankDealingText
        {
            get
            {
                return string.Format("共{0}元 共{1}笔订单", BankDealing, BankDealingCount);
            }
        }

        public string BankReturnSuccess { get; set; }
        public string BankReturnSuccessCount { get; set; }
        public string BankReturnSuccessText
        {
            get
            {
                return string.Format("共{0}元 共{1}笔订单", BankReturnSuccess, BankReturnSuccessCount);
            }
        }

        public string BankReturnFail { get; set; }
        public string BankReturnFailCount { get; set; }
        public string BankReturnFailText
        {
            get
            {
                return string.Format("共{0}元 共{1}笔订单", BankReturnFail, BankReturnFailCount);
            }
        }
    }
}
