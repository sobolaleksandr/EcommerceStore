namespace Store
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Модель заказа.
    /// </summary>
    public class OrderModel
    {
        /// <summary>
        /// Дата заказа.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Клиент.
        /// </summary>
        [Required]
        public Customer Customer { get; set; }

        /// <summary>
        /// Список позиций.
        /// </summary>
        [Required]
        public IEnumerable<LineItemModel> Items { get; set; }

        /// <summary>
        /// Номер заказа.
        /// </summary>
        [Required]
        public int Number { get; set; }
    }
}