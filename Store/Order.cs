namespace Store
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Заказ.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Дата заказа.
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Электронная почта клиента.
        /// </summary>
        [Required]
        public string CustomerEmail { get; set; }

        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Номер заказа.
        /// </summary>
        [Required]
        public int Number { get; set; }
    }
}