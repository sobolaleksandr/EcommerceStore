namespace Store.Memory
{
    using System;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Store.Interfaces;

    /// <summary>
    /// Методы расширений для сервисов.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить EF репозитории.
        /// </summary>
        /// <param name="services"> Сервисы. </param>
        /// <param name="connectionString"> Строка подключений. </param>
        /// <returns> Коллекция сервисов. </returns>
        public static IServiceCollection AddEfRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EcommerceContext>(
                options => options.UseSqlServer(connectionString),
                ServiceLifetime.Transient
            );

            services.AddScoped<Dictionary<Type, EcommerceContext>>();
            services.AddSingleton<DbContextFactory>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();

            return services;
        }
    }
}