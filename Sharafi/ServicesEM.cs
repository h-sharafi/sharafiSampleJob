using Application.Interfaces;
using Application.UserService;
using Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharafi
{
    public static class ServicesEM
    {

        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IUSerService, UserServic>();
            return services;
        }
    }
}
