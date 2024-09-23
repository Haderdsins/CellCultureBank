using CellCultureBank.DAL;
using CellCultureBank.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CellCultureBank.BLL.Profile.Services.UserService;

public class UserService : IUserService
{
    private readonly BankDbContext _dbContext;
    

    public UserService(BankDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }
}