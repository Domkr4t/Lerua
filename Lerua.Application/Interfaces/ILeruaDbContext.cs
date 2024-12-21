using Lerua.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Application.Interfaces
{
    public interface ILeruaDbContext
    {
        DbSet<Product> Products { get; }
        DbSet<Category> Categories { get; }
        DbSet<Customer> Customers { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }
        DbSet<Supplier> Suppliers { get; }
        DbSet<User> Users { get; }
        DbSet<ShoppingCart> ShoppingCarts { get; }
        DbSet<ShoppingCartItem> ShoppingCartItems { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
