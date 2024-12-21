using Lerua.Application.Suppliers.Commands.CreateSupplier;
using Lerua.Application.Suppliers.Commands.DeleteSupplier;
using Lerua.Application.Suppliers.Queries.GetSupplierById;
using Lerua.Application.Suppliers.Queries.GetSupplierList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lerua.API.Controllers
{
    /// <summary>
    /// Контроллер для управления поставщиками (Suppliers).
    /// </summary>
    /// <remarks>
    /// Позволяет создавать новых поставщиков и получать список существующих поставщиков.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Инициализирует экземпляр <see cref="SuppliersController"/>.
        /// </summary>
        /// <param name="mediator">Экземпляр <see cref="IMediator"/> для взаимодействия со слоем Application.</param>
        public SuppliersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Получить список всех поставщиков.
        /// </summary>
        /// <returns>Список поставщиков в виде <see cref="OkObjectResult"/>.</returns>
        /// <response code="200">Возвращает коллекцию поставщиков (может быть пустой).</response>
        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var suppliers = await _mediator.Send(new GetSupplierListQuery());
            return Ok(suppliers);
        }

        /// <summary>
        /// Получить данные о поставщике по его Id.
        /// </summary>
        /// <param name="id">Идентификатор поставщика.</param>
        /// <returns>Объект поставщика.</returns>
        /// <response code="200">Если найден.</response>
        /// <response code="404">Если не найден.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierById(Guid id)
        {
            try
            {
                var query = new GetSupplierByIdQuery { Id = id };
                var supplier = await _mediator.Send(query);
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Создать нового поставщика.
        /// </summary>
        /// <param name="command">Команда, содержащая данные для создания поставщика (<see cref="CreateSupplierCommand"/>)</param>
        /// <returns>Возвращает <see cref="CreatedAtActionResult"/> с идентификатором созданного поставщика.</returns>
        /// <response code="201">Если поставщик успешно создан.</response>
        /// <response code="400">Если входные данные невалидны (null и т.д.).</response>
        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierCommand command)
        {
            if (command == null)
                return BadRequest("Command cannot be null.");

            var supplierId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAllSuppliers), new { id = supplierId }, supplierId);
        }

        /// <summary>
        /// Удалить поставщика по Id.
        /// </summary>
        /// <param name="id">Идентификатор поставщика.</param>
        /// <returns>204 No Content, если успешно.</returns>
        /// <response code="204">Поставщик удалён.</response>
        /// <response code="404">Поставщик не найден.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {
            try
            {
                var command = new DeleteSupplierCommand { Id = id };
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
