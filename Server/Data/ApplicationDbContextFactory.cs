using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Server.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDBContext>
    {
        public ApplicationDBContext CreateDbContext(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfiguration config = null;

            if (environment == "Development")
            {
                config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                    .Build();
            }

            var optionBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();

            var connectionString = config.GetValue<string>("ConnectionStrings:IdentityDb");
            optionBuilder.UseSqlServer(connectionString);

            return new ApplicationDBContext(optionBuilder.Options);
        }
    }
}
