using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Thnyk.Users.Infrastructure;

namespace Thnyk.User.Api.StartpExtensions
{
    public static class DbContextExtension
    {
        public static  IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString"],
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            },
                       ServiceLifetime.Scoped
                   );

            var optionsBuilder = new DbContextOptionsBuilder<UsersDbContext>()
             .UseSqlServer(configuration["ConnectionString"]);

            using var dbContext = new UsersDbContext(optionsBuilder.Options);
            dbContext.Database.EnsureCreated();

            return services;
        }
    }
}
