using Lerua.Application.Orders.Commands.CreateOrder;
using Lerua.Application.Orders.Queries.GetOrderById;
using Lerua.Application.Orders.Queries.GetOrderList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lerua.API.Controllers
{
    /// <summary>
    /// Контроллер для управления заказами (Orders).
    /// </summary>
    /// <remarks>
    /// Позволяет создавать новые заказы и получать список существующих заказов.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Инициализирует экземпляр <see cref="OrdersController"/>.
        /// </summary>
        /// <param name="mediator">Экземпляр <see cref="IMediator"/> для взаимодействия со слоем Application.</param>
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        /// <summary>
        /// Получить список всех заказов.
        /// </summary>
        /// <returns>Возвращает <see cref="OkObjectResult"/> со списком заказов.</returns>
        /// <response code="200">Возвращает коллекцию заказов (может быть пустой).</response>
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _mediator.Send(new GetOrderListQuery());
            return Ok(orders);
        }

        /// <summary>
        /// Получить заказ по Id.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>Данные о заказе.</returns>
        /// <response code="200">Если найден.</response>
        /// <response code="404">Если не найден.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            try
            {
                var query = new GetOrderByIdQuery { Id = id };
                var order = await _mediator.Send(query);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Создать заказ на основе текущей корзины клиента.
        /// </summary>
        /// <param name="command">Команда, в которой содержится CustomerId</param>
        /// <returns>Возвращает GUID созданного заказа</returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrderFromCart([FromBody] CreateOrderCommand command)
        {
            if (command == null)
                return BadRequest("Command cannot be null.");

            var orderId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAllOrders), new { id = orderId }, orderId);
        }
    }
}
