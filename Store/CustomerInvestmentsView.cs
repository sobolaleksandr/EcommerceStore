namespace Store
{
    /// <summary>
    /// Отображение для суммы заказов клиента.
    /// </summary>
    public class CustomerInvestmentsView
    {
        /// <summary>
        /// Адрес электронной почты.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Сумма заказов.
        /// </summary>
        public decimal Sum { get; set; }
    }
}