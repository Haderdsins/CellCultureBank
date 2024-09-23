using Microsoft.OpenApi.Models;

namespace CellCultureBank.API.Extensions;

public static class SwaggerExtensions
{
    /// <summary>
    /// Добавление swagger 
    /// </summary>
    /// <param name="services"></param>
    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1.1.2",
                Title = "Банк клеточных культур",
                Description = "Система, посредством которой производят последовательные серии продукции с использованием клеточных культур, принадлежащих одному и тому же главному банку клеток.",
            });

            // Путь к XML-документации (указывается в .csproj)
            var filePath = Path.Combine(AppContext.BaseDirectory, "api1.xml");
            options.IncludeXmlComments(filePath);
        });
    }
}