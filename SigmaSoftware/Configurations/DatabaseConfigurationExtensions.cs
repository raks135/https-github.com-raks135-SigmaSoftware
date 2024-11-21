using Microsoft.EntityFrameworkCore;
using SigmaSoftware.Infrastructure.DBContext;
namespace SigmaSoftware.API.Configurations
{
    public static class DatabaseConfigurationExtensions
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseProvider = configuration["DatabaseProvider"]; // Get database type from configuration
            switch (databaseProvider)
            {
                case "SqlServer":
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("SqlServer")));
                    break;
                case "CosmosDb":
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseCosmos(configuration.GetConnectionString("CosmosDb") ,
                                          configuration["CosmosDb:DatabaseName"]));
                    break;
                case "Oracle":
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseOracle(configuration.GetConnectionString("Oracle")));
                    break;
                default:
                    throw new Exception("Database provider not configured correctly.");
            }
        }
    }
}
