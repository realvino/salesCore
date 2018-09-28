using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;

namespace tibs.stem.Web.Controllers
{
    public class HomeController : stemControllerBase
    {
        [DisableAuditing]
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
