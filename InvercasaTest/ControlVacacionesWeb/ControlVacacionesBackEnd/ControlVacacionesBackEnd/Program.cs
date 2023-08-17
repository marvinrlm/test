using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ControlVacacionesBackEnd.Data;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("DbContext");





var builder = WebApplication.CreateBuilder(args);



/*builder.Services.AddDbContext<ControlVacacionesBackEndContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ControlVacacionesBackEndContext") ?? throw new InvalidOperationException("Connection string 'ControlVacacionesBackEndContext' not found.")));
*/

builder.Services.AddDbContext<ControlVacacionesBackEndContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ControlVacacionesBackEndContext") ?? throw new InvalidOperationException("Connection string 'ControlVacacionesBackEndContext' not found.")));

// Add services to the container.
builder.Services.AddSingleton(configuration);//Para publicar el configuracion y poder llamar a la connection string desde los controllers

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
