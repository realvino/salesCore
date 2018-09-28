using Abp.AutoMapper;
using tibs.stem.Web.Authentication.External;

namespace tibs.stem.Web.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
