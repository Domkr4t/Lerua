using Lerua.Application.ShoppingCarts.Commands.CreateShoppingCart;
using Lerua.Application.ShoppingCarts.Queries.GetShoppingCartById;
using Lerua.Application.ShoppingCarts.Queries.GetShoppingCartList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lerua.API.Controllers
{
    /// <summary>
    /// Контроллер для управления корзинами (ShoppingCarts).
    /// </summary>
    /// <remarks>
    /// Позволяет создавать новые корзины и получать список существующих.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ShoppingCartsController"/>.
        /// </summary>
        /// <param name="mediator">Экземпляр MediatR для взаимодействия с Application-слоем.</param>
        public ShoppingCartsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Получить список всех корзин.
        /// </summary>
        /// <returns>Список корзин в формате JSON.</returns>
        /// <response code="200">Возвращает коллекцию корзин (может быть пустой).</response>
        [HttpGet]
        public async Task<IActionResult> GetAllShoppingCarts()
        {
            var carts = await _mediator.Send(new GetShoppingCartListQuery());
            return Ok(carts);
        }

        /// <summary>
        /// Получить корзину по Id.
        /// </summary>
        /// <param name="id">Идентификатор корзины.</param>
        /// <returns>Данные о корзине.</returns>
        /// <response code="200">Если найдена.</response>
        /// <response code="404">Если не найдена.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShoppingCartById(Guid id)
        {
            try
            {
                var query = new GetShoppingCartByIdQuery { Id = id };
                var cart = await _mediator.Send(query);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Создать новую корзину.
        /// </summary>
        /// <param name="command">Команда с данными для создания корзины (<see cref="CreateShoppingCartCommand"/>)</param>
        /// <returns>Возвращает идентификатор созданной корзины.</returns>
        /// <response code="201">Корзина успешно создана.</response>
        /// <response code="400">Входные данные невалидны.</response>
        [HttpPost]
        public async Task<IActionResult> CreateShoppingCart([FromBody] CreateShoppingCartCommand command)
        {
            if (command == null)
                return BadRequest("Command cannot be null.");

            var cartId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAllShoppingCarts), new { id = cartId }, cartId);
        }
    }
}
