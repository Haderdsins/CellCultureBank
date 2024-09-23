using CellCultureBank.BLL.Models.User;
using CellCultureBank.BLL.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace CellCultureBank.API.Controllers;

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Создать пользователя
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser(CreateUserModel model)
    {
        try
        {
            await _userService.Create(model);
            return Ok("Пользователь успешно создан");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}