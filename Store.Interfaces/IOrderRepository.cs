namespace Store.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Репозиторий <see cref="Order"/>
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Удалить заказ. 
        /// </summary>
        /// <param name="id"> Id-заказа. </param>
        /// <returns> Возвращает заказ, если получилось удалить, иначе null. </returns>
        Task<Order> Delete(int id);

        /// <summary>
        /// Получить заказ.
        /// </summary>
        /// <param name="number"> Id-заказа. </param>
        /// <returns> Возвращает заказ. </returns>
        Task<Order> GetByNumber(int number);

        /// <summary>
        /// Получить список заказов для указанного клиента, с указанием общей стоимости каждого.
        /// </summary>
        /// <param name="id"> Id-клиента. </param>
        /// <returns> Список заказов. </returns>
        Task<IEnumerable<CustomerOrders>> GetCustomerOrdersById(string id);

        /// <summary>
        /// Получить список клиентов, заказавших товара на сумму, превышающую указанную.
        /// </summary>
        /// <param name="sum"> Сумма. </param>
        /// <returns> Список клиентов. </returns>
        Task<IEnumerable<CustomerInvestmentsView>> GetCustomersBySum(decimal sum);

        /// <summary>
        /// Создать заказ.
        /// </summary>
        /// <param name="order"> Заказ. </param>
        /// <returns> True, если получилось создать. </returns>
        Task<bool> TryToCreate(OrderModel order);

        /// <summary>
        /// Обновить заказ.
        /// </summary>
        /// <param name="order"> Заказ. </param>
        /// <returns> True, если получилось обновить. </returns>
        Task<bool> Update(Order order);
    }
}