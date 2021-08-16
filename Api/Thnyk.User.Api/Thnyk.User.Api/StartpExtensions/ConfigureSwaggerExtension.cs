using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Thnyk.User.Api.StartpExtensions
{
    public static class ConfigureSwaggerExtension
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "User API",
                    Description = "A simple example ASP.NET Core Web API"
                });
            });

            return services;
        }
    }
}
