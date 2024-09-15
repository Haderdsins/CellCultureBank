using CellCultureBank.BLL.Profile;
using CellCultureBank.BLL.Services.BankFirstCSV;
using CellCultureBank.BLL.Services.BankFirstEntity;
using CellCultureBank.BLL.Services.BankSecondCSV;
using CellCultureBank.BLL.Services.BankSecondEntity;
using CellCultureBank.DAL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IBankSecondEntityService, BankSecondEntityService>();
builder.Services.AddScoped<IBankSecondCsvService, BankSecondCsvService>();
builder.Services.AddScoped<IBankFirstEntityService, BankFirstEntityService>();
builder.Services.AddScoped<IBankFirstCsvService, BankFirstCsvService>();
builder.Services.AddControllers();
// Add services to the container.

// This method gets called by the runtime. Use this method to add services to the container.


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.1.1 Beta",
        Title = "Банк клеточных культур",
        Description = "Система, посредством которой производят последовательные серии продукции с использованием клеточных культур, принадлежащих одному и тому же главному банку клеток.",

    });
    //Как включить генерацию xml в меню не нашел, подключил вручную в файле проекта csproj
    //в csproj указан файл генерации xml api1.xml
    //также чтобы все работало подключается библиотека Swashbuckle. AspNetCore
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "api1.xml");
    options.IncludeXmlComments(filePath);
    
    
    
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); //считываем строку подключения
builder.Services.AddDbContext<BankDbContext>(opt =>
    opt.UseSqlServer(
        connectionString)); //регистрируеим контекст базы данных в контрейнер зависимости, также указываем что в качестве настройки используем субд sql сервер
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();