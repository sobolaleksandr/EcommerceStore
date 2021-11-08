namespace Store
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Позиция.
    /// </summary>
    public class LineItem
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Номер заказа.
        /// </summary>
        [Required]
        public int OrderNumber { get; set; }

        /// <summary>
        /// Id-продукта.
        /// </summary>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// Количество продуктов в одной позиции (положительное число).
        /// </summary>
        [Required]
        [Range(1, 999999999999, ErrorMessage = "Недопустимое значение. Количество должно быть больше нуля")]
        public int Quantity { get; set; }
    }
}