using CellCultureBank.DAL.Database;
using Microsoft.EntityFrameworkCore;

namespace CellCultureBank.API.Extensions;

public static class DatabaseExtensions
{
    /// <summary>
    /// Добавление базы данных
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString">Тип подключения</param>
    /// <exception cref="Exception"></exception>
    public static void AddDatabase(this IServiceCollection services, string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception("Connection string is missing.");
        }

        services.AddDbContext<BankDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}