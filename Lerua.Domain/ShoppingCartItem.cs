namespace Lerua.Domain
{
    public class ShoppingCartItem
    {
        public ShoppingCartItem()
        {
        }

        public Guid ShoppingCartId { get; set; }    // FK -> ShoppingCart
        public Guid ProductId { get; set; }         // FK -> Product
        public int Quantity { get; set; }
    }
}
