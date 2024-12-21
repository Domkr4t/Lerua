using Lerua.Application.Products.Commands.CreateProduct;
using Lerua.Application.Products.Commands.DeleteProduct;
using Lerua.Application.Products.Queries.GetProductById;
using Lerua.Application.Products.Queries.GetProductsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lerua.API.Controllers
{
    /// <summary>
    /// Контроллер для управления товарами (Products).
    /// </summary>
    /// <remarks>
    /// Позволяет создавать новые товары и получать список существующих товаров.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Инициализирует экземпляр <see cref="ProductsController"/>.
        /// </summary>
        /// <param name="mediator">Экземпляр <see cref="IMediator"/> для взаимодействия со слоем Application.</param>
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить список всех продуктов.
        /// </summary>
        /// <returns>Список продуктов в виде <see cref="OkObjectResult"/>.</returns>
        /// <response code="200">Возвращает коллекцию продуктов (может быть пустой).</response>
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _mediator.Send(new GetProductListQuery());
            return Ok(products);
        }

        /// <summary>
        /// Получить продукт по Id.
        /// </summary>
        /// <param name="id">Идентификатор продукта.</param>
        /// <returns>Объект продукта.</returns>
        /// <response code="200">Если найден.</response>
        /// <response code="404">Если не найден.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var query = new GetProductByIdQuery { Id = id };
                var product = await _mediator.Send(query);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Создать новый продукт.
        /// </summary>
        /// <param name="command">Данные о создаваемом продукте (<see cref="CreateProductCommand"/>)</param>
        /// <returns>Возвращает <see cref="CreatedAtActionResult"/> с идентификатором созданного продукта.</returns>
        /// <response code="201">Если продукт успешно создан.</response>
        /// <response code="400">Если команда пуста или невалидна.</response>
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            if (command == null)
                return BadRequest("Command cannot be null.");

            var productId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProducts), new { id = productId }, productId);
        }

        /// <summary>
        /// Удалить товар (Product) по Id.
        /// </summary>
        /// <param name="id">Идентификатор товара.</param>
        /// <returns>204 No Content, если успешно.</returns>
        /// <response code="204">Товар удалён.</response>
        /// <response code="404">Товар не найден.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                var command = new DeleteProductCommand { Id = id };
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
