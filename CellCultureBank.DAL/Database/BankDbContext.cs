using CellCultureBank.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CellCultureBank.DAL.Database;

public sealed class BankDbContext : DbContext
{
    public DbSet<BankFirst> BankFirsts { get; set; }
    public DbSet<BankSecond> BankSeconds { get; set; }
    
    public DbSet<User> Users { get; set; }
    public BankDbContext(DbContextOptions<BankDbContext> options)
        : base(options)
    { 
    }
}
