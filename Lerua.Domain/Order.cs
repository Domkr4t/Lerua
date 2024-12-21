namespace Lerua.Domain
{
    public class Order
    {
        public Order()
        {
        }

        public Guid Id { get; set; } // Уникальный идентификатор
        public Guid CustomerId { get; set; } // Ссылка на клиента
        public DateTime OrderDate { get; set; } // Дата заказа
        public decimal TotalAmount { get; set; } // Общая сумма заказа

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
