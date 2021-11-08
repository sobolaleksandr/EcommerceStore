namespace Store
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Модель позиции.
    /// </summary>
    public class LineItemModel
    {
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