using Lerua.Application.Products.Commands.CreateProduct;
using Lerua.Application.Products.Queries.GetProductsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lerua.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить список всех продуктов.
        /// </summary>
        /// <returns>Список продуктов.</returns>
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _mediator.Send(new GetProductListQuery());
            return Ok(products);
        }

        /// <summary>
        /// Создать новый продукт.
        /// </summary>
        /// <param name="command">Команда для создания продукта.</param>
        /// <returns>Идентификатор созданного продукта.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            if (command == null)
                return BadRequest("Command cannot be null.");

            var productId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProducts), new { id = productId }, productId);
        }
    }
}
