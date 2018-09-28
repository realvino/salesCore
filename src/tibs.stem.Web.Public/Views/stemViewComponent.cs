using Abp.AspNetCore.Mvc.ViewComponents;

namespace tibs.stem.Web.Public.Views
{
    public abstract class stemViewComponent : AbpViewComponent
    {
        protected stemViewComponent()
        {
            LocalizationSourceName = stemConsts.LocalizationSourceName;
        }
    }
}