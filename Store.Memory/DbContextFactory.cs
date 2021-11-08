namespace Store.Memory
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Фрабика контекстов.
    /// </summary>
    internal class DbContextFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbContextFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Создать котекст.
        /// </summary>
        /// <param name="repositoryType"> Тип репозитория. </param>
        /// <returns> Возвращает контекст. </returns>
        public EcommerceContext Create(Type repositoryType)
        {
            var services = _httpContextAccessor.HttpContext.RequestServices;
            var dbContexts = services.GetService<Dictionary<Type, EcommerceContext>>();
            if (!dbContexts.ContainsKey(repositoryType))
                dbContexts[repositoryType] = services.GetService<EcommerceContext>();

            return dbContexts[repositoryType];
        }
    }
}