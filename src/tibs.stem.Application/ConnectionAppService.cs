using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem
{
    public class ConnectionAppService
    {
        public static IConfigurationRoot Configuration { get; set; }



            //MVC
        public string ConnectionString()
        {

            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            var data = Configuration["ConnectionStrings:Default"];
            return data;

        }
        public string root()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            var data = Configuration["App:ServerRootAddress"];
            return data;

        }
    }
}
