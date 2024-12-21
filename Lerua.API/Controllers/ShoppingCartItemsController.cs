using Lerua.Application.ShoppingCartItems.Commands.CreateShoppingCartItem;
using Lerua.Application.ShoppingCartItems.Commands.DeleteShoppingCartItem;
using Lerua.Application.ShoppingCartItems.Queries.GetShoppingCartItemList;
using Lerua.Application.ShoppingCartItems.Queries.GetShoppingCartItemsByCartId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lerua.API.Controllers
{
    /// <summary>
    /// Контроллер для управления элементами корзины (ShoppingCartItems).
    /// </summary>
    /// <remarks>
    /// Позволяет создавать новые элементы корзины и получать список всех элементов (без фильтрации по корзине).
    /// Обычно это делают через вложенные эндпоинты, но здесь — отдельный контроллер.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ShoppingCartItemsController"/>.
        /// </summary>
        /// <param name="mediator">Экземпляр MediatR для слоя Application.</param>
        public ShoppingCartItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Получить список всех элементов корзины (всех корзин).
        /// </summary>
        /// <returns>Список элементов корзины.</returns>
        /// <response code="200">Возвращает коллекцию ShoppingCartItems.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllShoppingCartItems()
        {
            var items = await _mediator.Send(new GetShoppingCartItemListQuery());
            return Ok(items);
        }

        /// <summary>
        /// Получить все элементы корзины по Id корзины.
        /// </summary>
        /// <param name="cartId">Идентификатор корзины.</param>
        /// <returns>Коллекция элементов корзины.</returns>
        /// <response code="200">Возвращает список элементов. Может быть пустым.</response>
        [HttpGet("ByCart/{cartId}")]
        public async Task<IActionResult> GetShoppingCartItemsByCartId(Guid cartId)
        {
            var query = new GetShoppingCartItemsByCartIdQuery { CartId = cartId };
            var items = await _mediator.Send(query);
            return Ok(items);
        }

        /// <summary>
        /// Создать новый элемент корзины.
        /// </summary>
        /// <param name="command">Команда с данными о создаваемом элементе (<see cref="CreateShoppingCartItemCommand"/>)</param>
        /// <returns>Возвращает статус 201, если успешно.</returns>
        /// <response code="201">Элемент успешно создан.</response>
        /// <response code="400">Входные данные невалидны.</response>
        [HttpPost]
        public async Task<IActionResult> CreateShoppingCartItem([FromBody] CreateShoppingCartItemCommand command)
        {
            if (command == null)
                return BadRequest("Command cannot be null.");

            await _mediator.Send(command);
            // Так как мы не возвращаем Guid (у нас композитный ключ), можно просто вернуть 201 без тела.
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Удалить элемент корзины по паре (ShoppingCartId, ProductId).
        /// </summary>
        /// <param name="cartId">Id корзины.</param>
        /// <param name="productId">Id товара.</param>
        /// <returns>204 No Content.</returns>
        /// <response code="204">Если удалено.</response>
        /// <response code="404">Если не найдено.</response>
        [HttpDelete("ByCartProduct/{cartId}/{productId}")]
        public async Task<IActionResult> DeleteShoppingCartItem(Guid cartId, Guid productId)
        {
            try
            {
                var command = new DeleteShoppingCartItemCommand
                {
                    ShoppingCartId = cartId,
                    ProductId = productId
                };
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
