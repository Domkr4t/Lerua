namespace Lerua.Domain
{
    public class Customer
    {
        public Customer()
        {
        }

        public Guid Id { get; set; } // Уникальный идентификатор
        public string FullName { get; set; } = default!; // Полное имя
        public string Email { get; set; } = default!; // Электронная почта
        public string PhoneNumber { get; set; } = default!; // Номер телефона
    }
}
