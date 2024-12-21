using Lerua.Application.Customers.Commands.CreateCustomer;
using Lerua.Application.Customers.Commands.DeleteCustomer;
using Lerua.Application.Customers.Queries.GetCustomerById;
using Lerua.Application.Customers.Queries.GetCustomerList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lerua.API.Controllers
{
    /// <summary>
    /// Контроллер для управления клиентами (Customers).
    /// </summary>
    /// <remarks>
    /// Позволяет создавать новых клиентов и получать список существующих клиентов.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Инициализирует экземпляр <see cref="CustomersController"/>.
        /// </summary>
        /// <param name="mediator">Экземпляр <see cref="IMediator"/> для взаимодействия со слоем Application.</param>
        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить список всех клиентов.
        /// </summary>
        /// <returns>Список клиентов.</returns>
        /// <response code="200">Возвращает коллекцию клиентов (может быть пустой).</response>
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _mediator.Send(new GetCustomerListQuery());
            return Ok(customers);
        }

        /// <summary>
        /// Получить данные о клиенте по Id.
        /// </summary>
        /// <param name="id">Идентификатор клиента.</param>
        /// <returns>Объект клиента.</returns>
        /// <response code="200">Если найден.</response>
        /// <response code="404">Если не найден.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            try
            {
                var query = new GetCustomerByIdQuery { Id = id };
                var customer = await _mediator.Send(query);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Создать нового клиента.
        /// </summary>
        /// <param name="command">Данные для создания клиента (<see cref="CreateCustomerCommand"/>)</param>
        /// <returns>Возвращает <see cref="CreatedAtActionResult"/> с идентификатором созданного клиента.</returns>
        /// <response code="201">Если клиент успешно создан.</response>
        /// <response code="400">Если команда пуста или невалидна.</response>
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            if (command == null)
                return BadRequest("Command cannot be null.");

            var customerId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAllCustomers), new { id = customerId }, customerId);
        }

        /// <summary>
        /// Удалить клиента и его корзину (если есть) с элементами.
        /// </summary>
        /// <param name="id">Идентификатор клиента.</param>
        /// <returns>204 No Content.</returns>
        /// <response code="204">Клиент и связанные сущности удалены.</response>
        /// <response code="404">Клиент не найден.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            try
            {
                var command = new DeleteCustomerCommand { Id = id };
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
