namespace Store.Web.App
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Store.Interfaces;

    /// <summary>
    /// Сервис <see cref="Order"/>
    /// </summary>
    public class OrderService
    {
        /// <summary>
        /// Репозиторий <see cref="Order"/>
        /// </summary>
        private readonly IOrderRepository _orderRepository;

        /// <summary>
        /// Репозиторий <see cref="Product"/>
        /// </summary>
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Сервис <see cref="Order"/>
        /// </summary>
        /// <param name="orderRepository"> Репозиторий <see cref="Order"/> </param>
        /// <param name="productRepository"> Репозиторий <see cref="Product"/> </param>
        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Удалить заказ. 
        /// </summary>
        /// <param name="id"> Id-заказа. </param>
        /// <returns></returns>
        public async Task<Order> Delete(int id)
        {
            return await _orderRepository.Delete(id);
        }

        /// <summary>
        /// Получить список клиентов, заказавших товара на сумму, превышающую указанную.
        /// </summary>
        /// <param name="sum"> Сумма заказа. </param>
        /// <returns> Список клиентов. </returns>
        public async Task<IEnumerable<CustomerInvestmentsView>> GetCustomerInvestmentsBySum(decimal sum)
        {
            return await _orderRepository.GetCustomersBySum(sum);
        }

        /// <summary>
        /// Получить список заказов для указанного клиента, с указанием общей стоимости каждого.
        /// </summary>
        /// <param name="id"> Id-клиента. </param>
        /// <returns> Список заказов. </returns>
        public async Task<IEnumerable<CustomerOrders>> GetCustomerOrdersById(string id)
        {
            return await _orderRepository.GetCustomerOrdersById(id);
        }

        /// <summary>
        /// Создать заказ.
        /// </summary>
        /// <param name="order"> Заказ. </param>
        /// <returns> True, если получилось создать. </returns>
        public async Task<bool> TryToCreate(OrderModel order)
        {
            if (!await ValidateOrder(order))
                return false;

            // Проверяем есть ли заказ в базе с таким номером
            if (await _orderRepository.GetByNumber(order.Number) != null)
                return false;

            if (order.Created == default)
                order.Created = DateTime.Today;

            return await _orderRepository.TryToCreate(order);
        }

        /// <summary>
        /// Обновить заказ.
        /// </summary>
        /// <param name="order"> Заказ. </param>
        /// <returns> True, если получилось обновить. </returns>
        public async Task<bool> Update(Order order)
        {
            return await _orderRepository.Update(order);
        }

        /// <summary>
        /// Проверить все ли позиции уникальны в заказе и есть ли в БД такие продукты.
        /// </summary>
        /// <param name="order"> Заказ. </param>
        /// <returns> True, если проверка пройдена. </returns>
        private async Task<bool> ValidateOrder(OrderModel order)
        {
            if (!order.Items.Any())
                return false;

            var products = await _productRepository.GetAll();
            var orderedProducts = order.Items.Select(item => products.FirstOrDefault(i => i.Id == item.ProductId));
            foreach (var product in orderedProducts)
            {
                if (product == null)
                    return false;

                products.Remove(product);
            }

            return true;
        }
    }
}