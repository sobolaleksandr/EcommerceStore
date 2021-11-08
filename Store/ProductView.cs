
namespace Store
{
    /// <summary>
    /// Отображение для списка продуктов по популярности.
    /// </summary>
    public class ProductView
    {
        /// <summary>
        /// Наименование продукта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Популярность (1 - максимальная).
        /// </summary>
        public int Popularity { get; set; }

        /// <summary>
        /// Количество проданных единиц.
        /// </summary>
        public int Quantity { get; set; }
    }
}
