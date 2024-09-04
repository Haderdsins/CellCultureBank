using CellCultureBank.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CellCultureBank.DAL.Database;

public sealed class BankDbContext : DbContext
{
    public DbSet<BankFirst> BankFirsts { get; set; }
    
    public BankDbContext(DbContextOptions<BankDbContext> options)
        : base(options)
    { 
    }
}
