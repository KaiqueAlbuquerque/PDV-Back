using Business.Interfaces;
using Business.Interfaces.Repository;
using Business.Interfaces.Service;
using Business.Notifications;
using Business.Services;
using Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductSaleRepository, ProductSaleRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISaleService, SaleService>();

            services.AddScoped<INotification, Notifier>();

            return services;
        }
    }
}
