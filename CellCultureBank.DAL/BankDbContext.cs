using CellCultureBank.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CellCultureBank.DAL;

public sealed class BankDbContext : DbContext
{
    /// <summary>
    /// Набор данных для работы моделью банков клеток
    /// </summary>
    public DbSet<BankOfCell> BankOfCells { get; set; }
    
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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настройка связи между BankSecond и User для FrozenByUserId
        modelBuilder.Entity<BankOfCell>()
            .HasOne(bs => bs.FrozenByUser)
            .WithMany(u => u.FrozenBankSeconds)
            .HasForeignKey(bs => bs.FrozenByUserId);

        // Настройка связи между BankSecond и User для DefrostedByUserId
        modelBuilder.Entity<BankOfCell>()
            .HasOne(bs => bs.DefrostedByUser)
            .WithMany(u => u.DefrostedBankSeconds)
            .HasForeignKey(bs => bs.DefrostedByUserId);
    }
}
