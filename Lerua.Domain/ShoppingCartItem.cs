namespace Lerua.Domain
{
    public class ShoppingCartItem
    {
        public Guid ProductId { get; set; } // Ссылка на товар
        public int Quantity { get; set; } // Количество
    }
}
