namespace Store.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Store.Web.App;

    /// <summary>
    /// Контрллер <see cref="Order"/>
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        /// <summary>
        /// Сервис <see cref="Order"/>
        /// </summary>
        private readonly OrderService _orderService;

        /// <summary>
        /// Контрллер <see cref="Order"/>
        /// </summary>
        /// <param name="orderService"> Сервис <see cref="Order"/> </param>
        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Создать заказ.
        /// </summary>
        /// <param name="order"> Заказ. </param>
        /// <returns> Модель заказа, если получилось создать, иначе <see cref="BadRequestResult"/>. </returns>
        [HttpPost]
        public async Task<ActionResult<OrderModel>> CreateOrder(OrderModel order)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (await _orderService.TryToCreate(order))
                return Ok();

            return BadRequest();
        }

        /// <summary>
        /// Удалить заказ.
        /// </summary>
        /// <param name="id"> Id-заказа. </param>
        /// <returns> Модель заказа, если получилось удалить, иначе <see cref="NotFoundResult"/></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderModel>> DeleteOrder(int id)
        {
            var order = await _orderService.Delete(id);
            if (order == null)
                return NotFound();

            return new ObjectResult(order);
        }

        /// <summary>
        /// Получить список заказов для указанного клиента.
        /// </summary>
        /// <param name="id"> Id-клиента. </param>
        /// <returns> Список отображений заказов клиента, при неправильном Id <see cref="BadRequestResult"/>. </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CustomerOrders>>> GetCustomerOrders(string id)
        {
            if (id == default)
                return BadRequest();

            return new ObjectResult(await _orderService.GetCustomerOrdersById(id));
        }

        /// <summary>
        /// Получить список клиентов, заказавших товара на сумму, превышающую указанную.
        /// </summary>
        /// <param name="sum"> Сумма заказов. </param>
        /// <returns> Список отображений клиентов, при отрицательной сумме <see cref="BadRequestResult"/></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerInvestmentsView>>> GetCustomersInvestmentsBySum(decimal sum)
        {
            if (sum >= 0)
                return new ObjectResult(await _orderService.GetCustomerInvestmentsBySum(sum));

            return BadRequest();
        }

        /// <summary>
        /// Обновить заказ.
        /// </summary>
        /// <param name="id"> Id-заказа. </param>
        /// <param name="order"> Заказ. </param>
        /// <returns> Если получилось бновить <see cref="NoContentResult"/>,
        /// в случае несоответсвия Id-заказа <see cref="BadRequestResult"/>,
        /// если возникла ошибка при обновлении <see cref="NotFoundResult"/> </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
                return BadRequest();

            if (await _orderService.Update(order))
                return NoContent();

            return NotFound();
        }
    }
}