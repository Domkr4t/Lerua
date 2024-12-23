using AutoMapper;
using FluentAssertions;
using Lerua.Application.Categories.Queries.GetCategoryById;
using Lerua.Domain;
using Lerua.Persistance;
using Lerua.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Tests.Application.Categories.Queries
{
    public class GetCategoryByIdQueryHandlerTests
    {
        [Fact]
        public async Task GetCategoryById_ShouldReturnDto_WhenCategoryExists()
        {
            var options = new DbContextOptionsBuilder<LeruaDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new LeruaDbContext(options);

            var existingCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Существующая категория",
                Description = "Существующее описание"
            };
            context.Categories.Add(existingCategory);
            await context.SaveChangesAsync();

            IMapper mapper = TestMapperFactory.CreateTestMapper();

            var handler = new GetCategoryByIdQueryHandler(context, mapper);
            var query = new GetCategoryByIdQuery
            {
                Id = existingCategory.Id
            };

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(existingCategory.Id);
            result.Name.Should().Be("Существующая категория");
            result.Description.Should().Be("Существующее описание");
        }

        [Fact]
        public async Task GetCategoryById_ShouldThrow_WhenNotFound()
        {
            var options = new DbContextOptionsBuilder<LeruaDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new LeruaDbContext(options);

            IMapper mapper = TestMapperFactory.CreateTestMapper();
            var handler = new GetCategoryByIdQueryHandler(context, mapper);

            var query = new GetCategoryByIdQuery
            {
                Id = Guid.NewGuid()
            };

            Func<Task> act = async () =>
                await handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("*not found*");
        }
    }
}
