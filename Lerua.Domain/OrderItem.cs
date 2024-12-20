namespace Lerua.Domain
{
    public class OrderItem
    {
        public OrderItem()
        {
        }

        public Guid Id { get; set; } // Уникальный идентификатор
        public Guid OrderId { get; set; } // Ссылка на заказ
        public Guid ProductId { get; set; } // Ссылка на товар
        public int Quantity { get; set; } // Количество
        public decimal Price { get; set; } // Цена за единицу
    }
}
