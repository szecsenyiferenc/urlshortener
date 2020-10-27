using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Repository;
using UrlShortener.Repository.Impl;
using UrlShortener.Validators;
using UrlShortener.Validators.Impl;

namespace UrlShortener.DependencyInjection
{
    public static class DependencyInjectionExtensionMethod
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IUrlValidator, UrlValidator>();
            services.AddScoped<IUrlRepository, UrlRepository>();
        }
    }
}
