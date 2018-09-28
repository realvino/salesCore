using Abp.Dependency;

namespace tibs.stem.MultiTenancy.Payments
{
    public interface IPaymentGatewayManagerFactory
    {
        IDisposableDependencyObjectWrapper<IPaymentGatewayManager> Create(SubscriptionPaymentGatewayType gateway);
    }
}