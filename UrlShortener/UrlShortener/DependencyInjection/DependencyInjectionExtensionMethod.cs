using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Providers;
using UrlShortener.Providers.Impl;
using UrlShortener.Repository;
using UrlShortener.Repository.Impl;
using UrlShortener.Services;
using UrlShortener.Services.Impl;
using UrlShortener.Utils;
using UrlShortener.Utils.Impl;
using UrlShortener.Validators;
using UrlShortener.Validators.Impl;

namespace UrlShortener.DependencyInjection
{
    public static class DependencyInjectionExtensionMethod
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IUrlValidator, UrlValidator>();
            services.AddSingleton<IUrlRepository, UrlRepository>();
            services.AddSingleton<IIdProvider, IdProvider>();
            services.AddSingleton<IUrlService, UrlService>();
            services.AddSingleton<INumbericBaseConverter, NumbericBaseConverter>();
        }
    }
}
