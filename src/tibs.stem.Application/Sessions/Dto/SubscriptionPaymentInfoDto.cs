using Abp.AutoMapper;
using tibs.stem.MultiTenancy.Payments;

namespace tibs.stem.Sessions.Dto
{
    [AutoMapFrom(typeof(SubscriptionPayment))]
    public class SubscriptionPaymentInfoDto
    {
        public decimal Amount { get; set; }
    }
}