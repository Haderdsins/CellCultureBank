using System.ComponentModel.DataAnnotations;

namespace CellCultureBank.WEB.Models;

public class AccountViewModel
{
    public LoginViewModel? LoginViewModel { get; set; }
    public RegisterViewModel? RegisterViewModel { get; set; }
}

public class LoginViewModel
{
    [Required(ErrorMessage = "Данное поле обязательное")]
    public string? Login { get; set; }

    [Required(ErrorMessage = "Данное поле обязательное")]
    public string? Password { get; set; }
}

public class RegisterViewModel
{
    [Required(ErrorMessage = "Данное поле обязательное")]
    public string? FullName { get; set; }
    
    [Required(ErrorMessage = "Данное поле обязательное")]
    public string? Login { get; set; }

    [Required(ErrorMessage = "Данное поле обязательное")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Данное поле обязательное")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string? RepeatPassword { get; set; }
}