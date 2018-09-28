using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using tibs.stem.Configuration;
using tibs.stem.Web;

namespace tibs.stem.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class stemDbContextFactory : IDbContextFactory<stemDbContext>
    {
        public stemDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<stemDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            stemDbContextConfigurer.Configure(builder, configuration.GetConnectionString(stemConsts.ConnectionStringName));
            
            return new stemDbContext(builder.Options);
        }
    }
}