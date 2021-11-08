namespace Store.Web.App
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Store.Interfaces;

    /// <summary>
    /// Сервис <see cref="Product"/>
    /// </summary>
    public class ProductService
    {
        /// <summary>
        /// Репозиторий <see cref="Product"/>
        /// </summary>
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Сервис <see cref="Product"/>
        /// </summary>
        /// <param name="productRepository"> Репозиторий <see cref="Product"/> </param>
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Удалить продукт.
        /// </summary>
        /// <param name="id"> Id-продукта. </param>
        /// <returns> True, если получилось удалить. </returns>
        public async Task<Product> Delete(int id)
        {
            return await _productRepository.Delete(id);
        }

        /// <summary>
        /// Получить список продуктов, отсортированных по популярности.
        /// Для каждого продукта указано общее количество проданных единиц.
        /// </summary>
        /// <returns> Список отображений продуктов. </returns>
        public async Task<IEnumerable<ProductView>> GetAllByPopularity()
        {
            return await _productRepository.GetAllByPopularity();
        }

        /// <summary>
        /// Получить продукт по Id.
        /// </summary> 
        /// <param name="id"> Id-продукта. </param>
        /// <returns> Продукт. </returns>
        public async Task<Product> GetById(int id)
        {
            return await _productRepository.GetById(id);
        }

        /// <summary>
        /// Создать продукт.
        /// </summary>
        /// <param name="product"> Продукт. </param>
        /// <returns> True, если получилось создать. </returns>
        public async Task<bool> TryToCreate(Product product)
        {
            return await _productRepository.TryToCreate(product);
        }

        /// <summary>
        /// Обновить продукт.
        /// </summary>
        /// <param name="id"> Id-продукта. </param>
        /// <param name="product"> Продукт. </param>
        /// <returns> True, если получилось обновить. </returns>
        public async Task<bool> Update(int id, Product product)
        {
            return await _productRepository.Update(product);
        }
    }
}