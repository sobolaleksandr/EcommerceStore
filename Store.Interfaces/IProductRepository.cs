namespace Store.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Репозиторий <see cref="Product"/>
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Удалить продукт.
        /// </summary>
        /// <param name="id"> Id-продукта. </param>
        /// <returns></returns>
        Task<Product> Delete(int id);

        /// <summary>
        /// Получить список всех продуктов.
        /// </summary>
        /// <returns> Список продуктов. </returns>
        Task<List<Product>> GetAll();

        /// <summary>
        /// Получить список продуктов, отсортированных по популярности.
        /// </summary>
        /// <returns> Список отображений продуктов. </returns>
        Task<IEnumerable<ProductView>> GetAllByPopularity();

        /// <summary>
        /// Получить продукт.
        /// </summary>
        /// <param name="id"> Id-продукта. </param>
        /// <returns> Возвращает продукт, если получилось удалить, иначе null. </returns>
        Task<Product> GetById(int id);

        /// <summary>
        /// Создать продукт.
        /// </summary>
        /// <param name="product"> Продукт. </param>
        /// <returns> True, если получилось создать. </returns>
        Task<bool> TryToCreate(Product product);

        /// <summary>
        /// Обновить продукт.
        /// </summary>
        /// <param name="product"> Продукт. </param>
        /// <returns> True, если получилось обновить. </returns>
        Task<bool> Update(Product product);
    }
}