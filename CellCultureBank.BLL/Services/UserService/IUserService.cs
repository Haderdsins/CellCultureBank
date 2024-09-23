using CellCultureBank.BLL.Models.User;
using CellCultureBank.DAL.Models;

namespace CellCultureBank.BLL.Services.UserService;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers();

    Task Create(CreateUserModel model);

}