using Microsoft.EntityFrameworkCore;

namespace Lerua.Persistance
{
    public class DbInitialize
    {
        public static void Initialize(LeruaDbContext context)
        {
            context.Database.EnsureCreated();

            context.Database.Migrate();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Domain.Category { Id = Guid.NewGuid(), Name = "Строительные материалы", Description = "Категория строительных материалов" },
                    new Domain.Category { Id = Guid.NewGuid(), Name = "Инструменты", Description = "Категория инструментов" }
                );
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Domain.Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Цемент",
                        Description = "Цемент для строительства",
                        Price = 350,
                        StockQuantity = 100,
                        CategoryId = context.Categories.First().Id,
                        SupplierId = Guid.NewGuid()
                    },
                    new Domain.Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Кирпич",
                        Description = "Кирпич для стен",
                        Price = 15,
                        StockQuantity = 1000,
                        CategoryId = context.Categories.First().Id,
                        SupplierId = Guid.NewGuid()
                    }
                );
            }

            context.SaveChanges();
        }
    }
}
