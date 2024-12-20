namespace Lerua.Domain
{
    public class Supplier
    {
        public Supplier()
        {
        }

        public Guid Id { get; set; } // Уникальный идентификатор
        public string Name { get; set; } = default!; // Название поставщика
        public string ContactInfo { get; set; } = default!; // Контактная информация
    }
}
