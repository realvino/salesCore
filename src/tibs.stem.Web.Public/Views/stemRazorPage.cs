using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace tibs.stem.Web.Public.Views
{
    public abstract class stemRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected stemRazorPage()
        {
            LocalizationSourceName = stemConsts.LocalizationSourceName;
        }
    }
}
