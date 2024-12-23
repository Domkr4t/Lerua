using FluentAssertions;
using Lerua.Application.Categories.Commands.CreateCategory;
using Lerua.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Tests.Application.Categories.Commands
{
    public class CreateCategoryCommandHandlerTests
    {
        [Fact]
        public async Task CreateCategory_ShouldCreateCategoryInDb()
        {
            var options = new DbContextOptionsBuilder<LeruaDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new LeruaDbContext(options);

            var handler = new CreateCategoryCommandHandler(context);

            var command = new CreateCategoryCommand
            {
                Name = "Какая-то категория",
                Description = "Какое-то описание"
            };

            var createdId = await handler.Handle(command, CancellationToken.None);

            createdId.Should().NotBeEmpty();

            var category = await context.Categories.FindAsync(createdId);
            category.Should().NotBeNull();
            category!.Name.Should().Be("Какая-то категория");
            category.Description.Should().Be("Какое-то описание");
        }
    }
}
