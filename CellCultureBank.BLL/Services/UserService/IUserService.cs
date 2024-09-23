using CellCultureBank.DAL.Models;

namespace CellCultureBank.BLL.Services.UserService;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers();
    
}