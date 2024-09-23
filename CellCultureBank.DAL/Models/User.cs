using Newtonsoft.Json;

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
    
    /// <summary>
    /// Список клеточных линий, замороженных этим пользователем
    /// </summary>
    [JsonIgnore]
    public ICollection<BankOfCell> FrozenBankSeconds { get; set; } = new List<BankOfCell>();
    
    /// <summary>
    /// Список клеточных линий, размороженных этим пользователем
    /// </summary>
    [JsonIgnore]
    public ICollection<BankOfCell> DefrostedBankSeconds { get; set; } = new List<BankOfCell>();
    
    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public User() { }
    public User(string login, string passwordHash, string fullName)
    {
        Login = login;
        PasswordHash = passwordHash;
        FullName = fullName;
        
    }
}