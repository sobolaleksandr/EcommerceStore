namespace Store.Memory
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Контекст приложения.
    /// </summary>
    public sealed class EcommerceContext : DbContext
    {
        /// <summary>
        /// Контекст приложения.
        /// </summary>
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Таблица клиентов.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Таблица позиций.
        /// </summary>
        public DbSet<LineItem> LineItems { get; set; }

        /// <summary>
        /// Таблица заказов.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Таблица продуктов. 
        /// </summary>
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<OrderModel>();
            modelBuilder.Ignore<ProductView>();
            modelBuilder.Ignore<LineItemModel>();
            modelBuilder.Ignore<CustomerInvestmentsView>();

            BuildProducts(modelBuilder);
            BuildCustomers(modelBuilder);
            BuildOrders(modelBuilder);
        }

        /// <summary>
        /// Заполняем данными таблицу клиентов.
        /// </summary>
        private static void BuildCustomers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(action => action.HasData(
                new Customer { Name = "Tom", Email = "Tomt@yahoo.com" },
                new Customer { Name = "Bob", Email = "bob00@gmail.com" },
                new Customer { Name = "Bill", Email = "Bill2@yandex.ru" }
            ));
        }

        /// <summary>
        /// Устанавливаем альтернативный ключ (номер заказа уникален) для таблицы заказов.
        /// </summary>
        private static void BuildOrders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(action => action.HasAlternateKey(u => new { u.Number }));
        }

        /// <summary>
        /// Устанавливаем альтернативный ключ (имя продукта уникально) и заполняем данными таблицу продуктов.
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void BuildProducts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(action =>
            {
                action.HasAlternateKey(u => new { u.Name });

                action.HasData(
                    new Product { Id = 1, Name = "mango", Price = 230 },
                    new Product { Id = 2, Name = "banana", Price = 206 },
                    new Product { Id = 3, Name = "apple", Price = 100 }
                );
            });
        }
    }
}