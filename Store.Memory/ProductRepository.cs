namespace Store.Memory
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Store.Interfaces;

    internal class ProductRepository : IProductRepository
    {
        private readonly DbContextFactory _dbContextFactory;

        public ProductRepository(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Product> Delete(int id)
        {
            var context = _dbContextFactory.Create(typeof(ProductRepository));
            var product = await context.Products.FindAsync(id);
            if (product == null)
                return null;

            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAll()
        {
            var context = _dbContextFactory.Create(typeof(ProductRepository));
            return await context.Products.ToListAsync();
        }

        public async Task<IEnumerable<ProductView>> GetAllByPopularity()
        {
            var context = _dbContextFactory.Create(typeof(ProductRepository));

            return await context.Products
                .GroupJoin(context.LineItems, product => product.Id, item => item.ProductId,
                    (product, gj) => new { product, gj })
                .SelectMany(t => t.gj.DefaultIfEmpty(), (t, sub) => new { t.product.Name, sub.Quantity })
                .GroupBy(u => u.Name)
                .Select(g => new ProductView
                {
                    Name = g.Key,
                    Popularity = g.Count(),
                    Quantity = g.Sum(c => c.Quantity)
                })
                .OrderByDescending(g => g.Popularity)
                .ThenByDescending(g => g.Quantity)
                .ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            var context = _dbContextFactory.Create(typeof(ProductRepository));
            return await context.Products.FindAsync(id);
        }

        public async Task<bool> TryToCreate(Product product)
        {
            var context = _dbContextFactory.Create(typeof(ProductRepository));
            await context.Products.AddAsync(product);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Update(Product product)
        {
            var context = _dbContextFactory.Create(typeof(ProductRepository));
            context.Entry(product).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }
    }
}