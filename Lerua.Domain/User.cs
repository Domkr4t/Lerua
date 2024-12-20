namespace Lerua.Domain
{
    public class User
    {
        public User()
        {
        }

        public Guid Id { get; set; } // Уникальный идентификатор
        public string Username { get; set; } = default!; // Имя пользователя
        public string PasswordHash { get; set; } = default!; // Хэш пароля
        public string Role { get; set; } = default!; // Роль пользователя (Admin, Manager)
    }
}
