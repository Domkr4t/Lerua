namespace Lerua.Domain
{
    public class Category
    {
        public Category()
        {
        }

        public Guid Id { get; set; } // Уникальный идентификатор
        public string Name { get; set; } = default!; // Название категории
        public string Description { get; set; } = default!; // Описание категории
    }
}
