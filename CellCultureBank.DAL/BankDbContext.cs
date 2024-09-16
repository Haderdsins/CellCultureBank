using CellCultureBank.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CellCultureBank.DAL.Database;

public sealed class BankDbContext : DbContext
{
    /// <summary>
    /// Набор данных для работы с первой моделью банков клеток
    /// </summary>
    public DbSet<BankFirst> BankFirsts { get; set; }
    
    /// <summary>
    /// Набор данных для работы со второй моделью банков клеток
    /// </summary>
    public DbSet<BankSecond> BankSeconds { get; set; }
    
    /// <summary>
    /// Набор данных для работы с сущностями пользователя 
    /// </summary>
    public DbSet<User> Users { get; set; }
    
    /// <summary>
    /// Конструктор контекста базы данных
    /// </summary>
    /// <param name="options">Параметры конфигурации контекста базы данных</param>
    public BankDbContext(DbContextOptions<BankDbContext> options)
        : base(options)
    { 
    }
}
