using CellCultureBank.BLL.Profile;
using CellCultureBank.BLL.Profile.Services.UserService;
using CellCultureBank.BLL.Services.BankSecondCSV;
using CellCultureBank.BLL.Services.BankSecondEntity;

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
        services.AddScoped<IBankSecondEntityService, BankSecondEntityService>();
        services.AddScoped<IBankSecondCsvService, BankSecondCsvService>();
        services.AddScoped<IUserService, UserService>();

    }
}