using System.Reflection;
using CellCultureBank.BLL.Services.BankFirst;
using CellCultureBank.DAL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IBankFirstService, BankFirstService>();
builder.Services.AddControllers();
// Add services to the container.

// This method gets called by the runtime. Use this method to add services to the container.


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0.0",
        Title = "Банк клеточных культур",
        Description = "Система, посредством которой производят последовательные серии продукции с использованием клеточных культур, принадлежащих одному и тому же главному банку клеток.",

    });

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