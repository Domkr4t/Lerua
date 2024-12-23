using FluentAssertions;
using Lerua.Application.Categories.Commands.DeleteCategory;
using Lerua.Domain;
using Lerua.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Tests
{
    public class DeleteCategoryCommandHandlerTests
    {
        [Fact]
        public async Task DeleteCategory_ShouldRemoveCategory_WhenExists()
        {
            var options = new DbContextOptionsBuilder<LeruaDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new LeruaDbContext(options);

            var existingCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Категория для удаления",
                Description = "Должна быть удалена"
            };
            context.Categories.Add(existingCategory);
            await context.SaveChangesAsync();

            var handler = new DeleteCategoryCommandHandler(context);

            var command = new DeleteCategoryCommand
            {
                Id = existingCategory.Id
            };

            await handler.Handle(command, CancellationToken.None);

            var categoryInDb = await context.Categories.FindAsync(existingCategory.Id);
            categoryInDb.Should().BeNull(); 
        }

        [Fact]
        public async Task DeleteCategory_ShouldThrow_WhenCategoryNotFound()
        {
            var options = new DbContextOptionsBuilder<LeruaDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new LeruaDbContext(options);

            var handler = new DeleteCategoryCommandHandler(context);

            var nonExistingId = Guid.NewGuid();
            var command = new DeleteCategoryCommand
            {
                Id = nonExistingId
            };

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<Exception>()
                     .WithMessage($"*{nonExistingId}*");
        }
    }
}
