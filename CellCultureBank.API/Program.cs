using CellCultureBank.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBusinessServices();

builder.Services.AddControllers();

builder.Services.AddSwaggerDocumentation();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); 

builder.Services.AddDatabase(connectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();