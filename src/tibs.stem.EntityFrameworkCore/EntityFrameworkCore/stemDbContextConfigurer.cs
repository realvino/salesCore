using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace tibs.stem.EntityFrameworkCore
{
    public static class stemDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<stemDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }
    }
}