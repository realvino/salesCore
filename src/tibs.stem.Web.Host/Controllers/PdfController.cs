using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using tibs.stem.EntityFrameworkCore;
using tibs.stem.Web.Controllers;

namespace tibs.stem.Web.Host.Controllers
{
    public class PdfController : PdfControllerBase
    {
        public PdfController(IAppFolders appFolders, stemDbContext stemDbContext, IHostingEnvironment hostingEnvironment)
               : base(appFolders, stemDbContext, hostingEnvironment)
        {
        }
    }
}