using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Thnyk.Core.Application.CQRS.Command.AddUser;
using Thnyk.Core.Application.Infrastructure;
using Thnyk.Core.Application.Mapping;
using Thnyk.Core.Domain.Interfaces;
using Thnyk.User.Api.Filters;
using Thnyk.Users.Infrastructure.Repositories;

namespace Thnyk.User.Api.StartpExtensions
{
    public static class AddCustomApplicationServices
    {
        public static IServiceCollection AddApplicationServiceRegistration(this IServiceCollection services)
        {

            services.AddMediatR(typeof(AddUserCommand).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperGeneralMapping).GetTypeInfo().Assembly });

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            }));


            services
            .AddMvc(options =>
            {
                options.Filters.Add(typeof(ApiValidationAttribute));
                options.Filters.Add(typeof(CustomExceptionFilter));
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(AddUserValidator).GetTypeInfo().Assembly))
            .AddApplicationPart(typeof(Startup).Assembly);

            return services;
        }
    }
}
