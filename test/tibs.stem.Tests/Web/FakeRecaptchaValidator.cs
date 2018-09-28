using System.Threading.Tasks;
using tibs.stem.Security.Recaptcha;

namespace tibs.stem.Tests.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public async Task ValidateAsync(string captchaResponse)
        {
            
        }
    }
}
