namespace Lerua.Domain
{
    public class Product
    {
        public Product()
        {
        }

        public Guid Id { get; set; } // Уникальный идентификатор
        public string Name { get; set; } = default!; // Название товара
        public string Description { get; set; } = default!; // Описание товара
        public decimal Price { get; set; } // Цена товара
        public Guid CategoryId { get; set; } // Ссылка на категорию
        public int StockQuantity { get; set; } // Количество на складе
        public Guid SupplierId { get; set; } // Ссылка на поставщика
    }
}
