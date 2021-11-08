namespace Store.Memory
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Store.Interfaces;

    internal class OrderRepository : IOrderRepository
    {
        private readonly DbContextFactory _dbContextFactory;

        public OrderRepository(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Order> Delete(int id)
        {
            var context = _dbContextFactory.Create(typeof(OrderRepository));
            var order = await context.Orders.FindAsync(id);
            if (order == null)
                return null;

            context.Orders.Remove(order);
            await context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> GetByNumber(int number)
        {
            var context = _dbContextFactory.Create(typeof(OrderRepository));
            return await context.Orders.Where(o => o.Number == number).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CustomerOrders>> GetCustomerOrdersById(string id)
        {
            var context = _dbContextFactory.Create(typeof(OrderRepository));

            return await context.Orders
                .Join(context.LineItems, order => order.Number, item => item.OrderNumber,
                    (order, item) => new { order, item })
                .Join(context.Products, t => t.item.ProductId, product => product.Id,
                    (t, product) => new { t, product })
                .Where(t => t.t.order.CustomerEmail == id)
                .Select(t => new { t.t.order.Number, t.product.Price, t.t.item.Quantity })
                .GroupBy(o => o.Number)
                .Select(g => new CustomerOrders
                {
                    Number = g.Key,
                    Sum = g.Sum(selector => selector.Price * selector.Quantity)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CustomerInvestmentsView>> GetCustomersBySum(decimal sum)
        {
            var context = _dbContextFactory.Create(typeof(OrderRepository));

            return await context.Customers
                .Join(context.Orders, customer => customer.Email, order => order.CustomerEmail,
                    (customer, order) => new { customer, order })
                .Join(context.LineItems, t => t.order.Number, item => item.OrderNumber,
                    (t, item) => new { t, item })
                .Join(context.Products, t => t.item.ProductId, product => product.Id,
                    (t, product) => new { t, product })
                .Where(t => t.product.Price * t.t.item.Quantity > sum)
                .Select(t => new CustomerInvestmentsView
                {
                    Email = t.t.t.customer.Email,
                    Sum = t.product.Price * t.t.item.Quantity
                })
                .ToListAsync();
        }

        public async Task<bool> TryToCreate(OrderModel order)
        {
            var context = _dbContextFactory.Create(typeof(OrderRepository));

            await CreateItems(order);
            await CreateCustomer(order);

            await context.Orders.AddAsync(new Order
            {
                Number = order.Number,
                CustomerEmail = order.Customer.Email,
                Created = order.Created
            });

            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Order order)
        {
            var context = _dbContextFactory.Create(typeof(OrderRepository));
            context.Entry(order).State = EntityState.Modified;

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

        /// <summary>
        /// Создать клиента для заказа.
        /// </summary>
        /// <param name="order"> Заказ. </param>
        private async Task CreateCustomer(OrderModel order)
        {
            var context = _dbContextFactory.Create(typeof(OrderRepository));
            var customer = await context.Customers.FindAsync(order.Customer.Email);
            if (customer == null)
                await context.Customers.AddAsync(order.Customer);
        }

        /// <summary>
        /// Добавить позиции заказа в БД.
        /// </summary>
        /// <param name="order"> Заказ. </param>
        private async Task CreateItems(OrderModel order)
        {
            var context = _dbContextFactory.Create(typeof(OrderRepository));
            foreach (var item in order.Items)
            {
                await context.LineItems.AddAsync(new LineItem
                {
                    OrderNumber = order.Number,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            }
        }
    }
}