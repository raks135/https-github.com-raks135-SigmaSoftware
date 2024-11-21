using SigmaSoftware.Application.Services;
using SigmaSoftware.Domain.Interfaces;
using SigmaSoftware.Infrastructure.Repositories;

namespace SigmaSoftware.API.Configurations
{
    public static class DIConfig
    {
        /// <summary>
        /// Configures services and registers dependencies into the application's service container.
        /// </summary>
        /// <param name="builder">The WebApplicationBuilder instance to configure services on.</param>
        /// <returns>Returns the configured <see cref="WebApplicationBuilder"/>.</returns>
        public static WebApplicationBuilder ConfigDI(this WebApplicationBuilder builder)
        {
            // Register repositories and services
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<ICandidateService, CandidateService>();
            return builder;
        }
    }
}
