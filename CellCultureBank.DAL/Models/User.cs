namespace CellCultureBank.DAL.Models;

public class User : Entity<int>
{
    
    
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Хеш пароля
    /// </summary>
    public string PasswordHash { get; set; }
    
    /// <summary>
    /// ФИО пользователя
    /// </summary>
    public string FullName { get; set; }

    public User(string login, string passwordHash, string fullName)
    {
        Login = login;
        PasswordHash = passwordHash;
        FullName = fullName;
    }
}