using Lerua.Application.Users.Commands.CreateUser;
using Lerua.Application.Users.Queries.GetUserList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lerua.API.Controllers
{
    /// <summary>
    /// Контроллер для управления пользователями (Users).
    /// </summary>
    /// <remarks>
    /// Позволяет создавать новых пользователей и получать список существующих.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UsersController"/>.
        /// </summary>
        /// <param name="mediator">Экземпляр <see cref="IMediator"/> для взаимодействия со слоем Application.</param>
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить список всех пользователей.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        /// <response code="200">Возвращает коллекцию пользователей (может быть пустой).</response>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _mediator.Send(new GetUserListQuery());
            return Ok(users);
        }

        /// <summary>
        /// Создать нового пользователя.
        /// </summary>
        /// <param name="command">Данные о пользователе (<see cref="CreateUserCommand"/>)</param>
        /// <returns>Возвращает <see cref="CreatedAtActionResult"/> с идентификатором созданного пользователя.</returns>
        /// <response code="201">Если пользователь успешно создан.</response>
        /// <response code="400">Если команда пуста или невалидна.</response>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            if (command == null)
                return BadRequest("Command cannot be null.");

            var userId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAllUsers), new { id = userId }, userId);
        }
    }
}
