namespace Lerua.Domain
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
        }

        public Guid Id { get; set; } // Уникальный идентификатор
        public Guid CustomerId { get; set; } // Ссылка на клиента
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>(); // Список товаров
    }
}
