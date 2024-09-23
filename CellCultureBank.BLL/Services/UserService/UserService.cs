using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using CellCultureBank.BLL.Models.User;
using CellCultureBank.DAL;
using CellCultureBank.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CellCultureBank.BLL.Services.UserService;

public class UserService : IUserService
{
    private readonly BankDbContext _dbContext;
    private readonly IMapper _secondBankMapper;

    public UserService(BankDbContext dbContext, IMapper secondBankMapper)
    {
        _dbContext = dbContext;
        _secondBankMapper = secondBankMapper;
    }
    
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task Create(CreateUserModel model)
    {
        if (string.IsNullOrEmpty(model.Password))
        {
            throw new ArgumentException("Пароль не может быть пустым");
        }
        
        string passwordHash = HashPassword(model.Password);
        
        var newUser = _secondBankMapper.Map<User>(model);
        newUser.PasswordHash = passwordHash;

        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();
    }
    
    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}