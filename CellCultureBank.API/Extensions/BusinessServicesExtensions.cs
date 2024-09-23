using CellCultureBank.BLL;
using CellCultureBank.BLL.Services.BankCSV;
using CellCultureBank.BLL.Services.BankEntity;
using CellCultureBank.BLL.Services.UserService;

namespace CellCultureBank.API.Extensions;

public static class BusinessServicesExtensions
{
    /// <summary>
    /// Добавление DI реализаций интерфейсов
    /// </summary>
    /// <param name="services"></param>
    public static void AddBusinessServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BankProfile));
        services.AddScoped<IBankEntityService, BankEntityService>();
        services.AddScoped<IBankCsvService, BankCsvService>();
        services.AddScoped<IUserService, UserService>();

    }
}