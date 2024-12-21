using Lerua.Application.Categories.Commands.CreateCategory;
using Lerua.Application.Categories.Commands.DeleteCategory;
using Lerua.Application.Categories.Queries.GetCategoryById;
using Lerua.Application.Categories.Queries.GetCategoryList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lerua.API.Controllers
{
    /// <summary>
    /// Контроллер для управления категориями товаров.
    /// </summary>
    /// <remarks>
    /// Позволяет создавать новые категории и получать список существующих категорий.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Инициализирует экземпляр <see cref="CategoriesController"/>.
        /// </summary>
        /// <param name="mediator">Экземпляр <see cref="IMediator"/> для взаимодействия со слоем Application.</param>
        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Получить список всех категорий.
        /// </summary>
        /// <returns>Список категорий в формате <see cref="IActionResult"/> со статусом 200.</returns>
        /// <response code="200">Возвращает коллекцию категорий (возможно, пустую).</response>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _mediator.Send(new GetCategoryListQuery());
            return Ok(categories);
        }

        /// <summary>
        /// Получить данные о категории по её Id.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <returns>Объект категории, если найден.</returns>
        /// <response code="200">Если категория найдена.</response>
        /// <response code="404">Если категория с таким Id не найдена.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            try
            {
                var query = new GetCategoryByIdQuery { Id = id };
                var category = await _mediator.Send(query);
                return Ok(category);
            }
            catch (Exception ex)
            {
                // Либо NotFoundException, обработка через Middleware и т.д.
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Создать новую категорию.
        /// </summary>
        /// <param name="command">Команда, содержащая данные для создания категории.</param>
        /// <returns>Возвращает <see cref="CreatedAtActionResult"/> с идентификатором созданной категории.</returns>
        /// <response code="201">Если категория успешно создана.</response>
        /// <response code="400">Если входные данные невалидны (например, пустой <paramref name="command"/>).</response>
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            if (command == null)
                return BadRequest("Command cannot be null.");

            var categoryId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAllCategories), new { id = categoryId }, categoryId);
        }

        /// <summary>
        /// Удалить категорию по Id.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <returns>204 No Content, если удаление успешно.</returns>
        /// <response code="204">Если категория успешно удалена.</response>
        /// <response code="404">Если категория не найдена.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            try
            {
                var command = new DeleteCategoryCommand { Id = id };
                await _mediator.Send(command);
                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
