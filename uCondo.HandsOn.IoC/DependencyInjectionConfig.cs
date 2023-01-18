using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using uCondo.HandsOn.Infrastructure;
using uCondo.HandsOn.Infrastructure.Implamentations;
using uCondo.HandsOn.Infrastructure.Interfaces;
using uCondo.HandsOn.Service.Interfaces;
using uCondo.HandsOn.Service.TemplateService;

namespace uCondo.HandsOn.IoC
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration config, IHostEnvironment env)
        {
            services.AddSingleton(new RepositoryConfiguration
            {
                ConnectionString = config.GetValue<string>("DB_CONNECTION")
            });
            services.AddScoped<IPlanoContasService, PlanoContasService>();
            services.AddScoped<IPlanoContasRepository, PlanoContasRepository>();

            return services;
        }
    }
}
