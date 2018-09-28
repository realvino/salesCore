using Microsoft.AspNetCore.Mvc;
using tibs.stem.Web.Controllers;

namespace tibs.stem.Web.Public.Controllers
{
    public class HomeController : stemControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}