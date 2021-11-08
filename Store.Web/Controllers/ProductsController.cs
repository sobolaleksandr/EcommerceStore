namespace Store.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Store.Web.App;

    /// <summary>
    /// Контроллер <see cref="Product"/>
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Сервис <see cref="Product"/>
        /// </summary>
        private readonly ProductService _productService;

        /// <summary>
        /// Контроллер <see cref="Product"/>
        /// </summary>
        /// <param name="productService"> Сервис <see cref="Product"/> </param>
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Создать продукт.
        /// </summary>
        /// <param name="product"> Продукт. </param>
        /// <returns> Успех <see cref="GetProduct"/>,
        /// модель не прошла валидацию <see cref="BadRequestResult"/>,
        /// не получилось добавить в репозиторий <see cref="ConflictResult"/></returns>
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (await _productService.TryToCreate(product))
                return CreatedAtAction(nameof(GetProduct), new { id = product.Name }, product);

            return Conflict();
        }

        /// <summary>
        /// Удалить продукт. 
        /// </summary>
        /// <param name="id"> Id-продукта. </param>
        /// <returns> Успех <see cref="ObjectResult"/>,
        /// некорректный id <see cref="BadRequestResult"/>,
        /// не получилось удалить из репозитория <see cref="NotFoundResult"/></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            if (id == default)
                return BadRequest();

            var product = await _productService.Delete(id);
            if (product == null)
                return NotFound();

            return new ObjectResult(product);
        }

        /// <summary>
        /// Получить продукт.
        /// </summary>
        /// <param name="id"> Id-продукта. </param>
        /// <returns> Успех <see cref="ObjectResult"/>,
        /// некорректный id <see cref="BadRequestResult"/>,
        /// продукта нет в репозитории <see cref="NotFoundResult"/></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (id == default)
                return BadRequest();

            var product = await _productService.GetById(id);
            if (product == null)
                return NotFound();

            return new ObjectResult(product);
        }

        /// <summary>
        /// Получить список продуктов, отсортированных по популярности.
        /// </summary>
        /// <returns> Список отображений продуктов. </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductView>>> GetProducts()
        {
            return new ObjectResult(await _productService.GetAllByPopularity());
        }

        /// <summary>
        /// Обновить продукт.
        /// </summary>
        /// <param name="id"> Id-продукта. </param>
        /// <param name="product"> Продукт. </param>
        /// <returns> Успех <see cref="NoContentResult"/>,
        /// некорректный id <see cref="BadRequestResult"/>,
        /// не получилось обновить продукт в репозитории <see cref="NotFoundResult"/></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id || id == default)
                return BadRequest();

            if (await _productService.Update(id, product))
                return NoContent();

            return NotFound();
        }
    }
}