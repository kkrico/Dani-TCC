using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Dani_TCC.Configurations
{
    public static class WebApiServiceCollectionExtensions
    {
        private static IMvcBuilder AddWebApi(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            IMvcCoreBuilder builder = services.AddMvcCore();
            builder.AddJsonFormatters();
            builder.AddApiExplorer();
            builder.AddCors();

            return new MvcBuilder(builder.Services, builder.PartManager);
        }

        public static IMvcBuilder AddWebApi(this IServiceCollection services, Action<MvcOptions> setupAction)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            IMvcBuilder builder = services.AddWebApi();
            builder.Services.Configure(setupAction);

            return builder;
        }
    }
}