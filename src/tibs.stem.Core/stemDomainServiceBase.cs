using Abp.Domain.Services;

namespace tibs.stem
{
    public abstract class stemDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected stemDomainServiceBase()
        {
            LocalizationSourceName = stemConsts.LocalizationSourceName;
        }
    }
}
