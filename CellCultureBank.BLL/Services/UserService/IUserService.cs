using CellCultureBank.DAL.Models;

namespace CellCultureBank.BLL.Profile.Services.UserService;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers();
    
}