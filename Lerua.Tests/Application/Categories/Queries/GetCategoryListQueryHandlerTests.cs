using AutoMapper;
using FluentAssertions;
using Lerua.Application.Categories.Queries.GetCategoryList;
using Lerua.Domain;
using Lerua.Persistance;
using Lerua.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Tests
{
    public class GetCategoryListQueryHandlerTests
    {
        [Fact]
        public async Task GetCategoryList_ShouldReturnAllCategories()
        {
            var options = new DbContextOptionsBuilder<LeruaDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new LeruaDbContext(options);

            var cat1 = new Category { Id = Guid.NewGuid(), Name = "Cat1", Description = "Desc1" };
            var cat2 = new Category { Id = Guid.NewGuid(), Name = "Cat2", Description = "Desc2" };
            context.Categories.AddRange(cat1, cat2);
            await context.SaveChangesAsync();

            IMapper mapper = TestMapperFactory.CreateTestMapper();
            var handler = new GetCategoryListQueryHandler(context, mapper);

            var query = new GetCategoryListQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            var names = new List<string> { result[0].Name, result[1].Name };
            names.Should().Contain("Cat1");
            names.Should().Contain("Cat2");
        }

        [Fact]
        public async Task GetCategoryList_ShouldReturnEmpty_WhenNoCategories()
        {
            var options = new DbContextOptionsBuilder<LeruaDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new LeruaDbContext(options);

            IMapper mapper = TestMapperFactory.CreateTestMapper();
            var handler = new GetCategoryListQueryHandler(context, mapper);

            var query = new GetCategoryListQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull().And.BeEmpty();
        }
    }
}
