using Lerua.Application.OrderItems.Commands.CreateOrderItem;
using Lerua.Application.OrderItems.Commands.DeleteOrderItem;
using Lerua.Application.OrderItems.Queries.GetOrderItemList;
using Lerua.Application.OrderItems.Queries.GetOrderItemsByOrderId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lerua.API.Controllers
{
    /// <summary>
    /// Контроллер для управления элементами заказа (OrderItems).
    /// </summary>
    /// <remarks>
    /// Позволяет создавать новые позиции заказа и получать список всех позиций.
    /// Обычно это делают как вложенный ресурс внутри Order, но здесь отдельный контроллер.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Инициализирует экземпляр <see cref="OrderItemsController"/>.
        /// </summary>
        /// <param name="mediator">Экземпляр MediatR для слоя Application.</param>
        public OrderItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить список всех элементов заказа.
        /// </summary>
        /// <returns>Список элементов заказа в формате JSON.</returns>
        /// <response code="200">Возвращает коллекцию элементов (может быть пустой).</response>
        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems()
        {
            var items = await _mediator.Send(new GetOrderItemListQuery());
            return Ok(items);
        }

        /// <summary>
        /// Получить все позиции (items) для определённого заказа.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <returns>Коллекция элементов заказа.</returns>
        /// <response code="200">Список элементов. Может быть пустым.</response>
        [HttpGet("ByOrder/{orderId}")]
        public async Task<IActionResult> GetOrderItemsByOrderId(Guid orderId)
        {
            var query = new GetOrderItemsByOrderIdQuery { OrderId = orderId };
            var items = await _mediator.Send(query);
            return Ok(items);
        }

        /// <summary>
        /// Создать новый элемент заказа.
        /// </summary>
        /// <param name="command">Команда с данными о создаваемом элементе (<see cref="CreateOrderItemCommand"/>)</param>
        /// <returns>Возвращает <see cref="CreatedAtActionResult"/> с идентификатором созданного элемента.</returns>
        /// <response code="201">Элемент заказа успешно создан.</response>
        /// <response code="400">Входные данные невалидны.</response>
        [HttpPost]
        public async Task<IActionResult> CreateOrderItem([FromBody] CreateOrderItemCommand command)
        {
            if (command == null)
                return BadRequest("Command cannot be null.");

            var orderItemId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAllOrderItems), new { id = orderItemId }, orderItemId);
        }

        /// <summary>
        /// Удалить элемент заказа (OrderItem) по его Id.
        /// </summary>
        /// <param name="id">Идентификатор элемента заказа.</param>
        /// <returns>204 No Content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(Guid id)
        {
            try
            {
                var command = new DeleteOrderItemCommand { Id = id };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
