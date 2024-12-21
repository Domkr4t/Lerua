using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Lerua.Persistance
{
    public class LeruaDbContextFactory : IDesignTimeDbContextFactory<LeruaDbContext>
    {
        public LeruaDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();

            var builderConfig = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var connectionString = builderConfig.GetConnectionString("MSSQL");

            var builder = new DbContextOptionsBuilder<LeruaDbContext>();
            builder.UseSqlServer(connectionString);

            return new LeruaDbContext(builder.Options);
        }
    }
}
