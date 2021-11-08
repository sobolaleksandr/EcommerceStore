namespace Store
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Продукт.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Цена (неотрицательное число).
        /// </summary>
        [Required]
        [Column(TypeName = "money")]
        [Range(0.01, 999999999999, ErrorMessage = "Недопустимое значение. Цена должна быть больше нуля")]
        public decimal Price { get; set; }
    }
}