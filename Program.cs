using Choix_des_technos_et_infras_de_développement___TP1.Application;
using Choix_des_technos_et_infras_de_développement___TP1.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Ajouter le contexte de base de données avec SQLite
builder.Services.AddDbContext<TP1Context>(options =>
    options.UseSqlite("Data Source=TP1.db"));

// Ajouter le UserService pour l'injection de dépendance
builder.Services.AddScoped<UserService>();

// Add services to the container.

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
