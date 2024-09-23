namespace CellCultureBank.BLL.Models.User;

public class CreateUserModel
{
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Хеш пароля
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// ФИО пользователя
    /// </summary>
    public string FullName { get; set; }
}