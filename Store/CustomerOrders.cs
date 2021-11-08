namespace Store
{
    /// <summary>
    /// Отображение для списка заказов клиента.
    /// </summary>
    public class CustomerOrders
    {
        /// <summary>
        /// Номер заказа.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Сумма.
        /// </summary>
        public decimal Sum { get; set; }
    }
}