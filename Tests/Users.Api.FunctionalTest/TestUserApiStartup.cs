using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Thnyk.User.Api;
using Thnyk.Users.Infrastructure;

namespace Users.Api.FunctionalTest
{
    public class TestUserApiStartup :Startup
    {
        public TestUserApiStartup(IConfiguration env) : base(env)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
        }

        public override IServiceCollection AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseSqlServer("Server=.;Initial Catalog=Thnyk.UserDb.Test;Integrated Security=true;",
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            },
                     ServiceLifetime.Scoped
                 );

            var optionsBuilder = new DbContextOptionsBuilder<UsersDbContext>()
             .UseSqlServer("Server=.;Initial Catalog=Thnyk.UserDb.Test;Integrated Security=true;");

            using var dbContext = new UsersDbContext(optionsBuilder.Options);
            dbContext.Database.EnsureCreated();
            return services;
        }
    }
}
