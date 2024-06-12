using Microsoft.EntityFrameworkCore;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaInfrastruktury.Data;
using RPGManager.WarstwaWprowadzania.Data;
using RPGManager.WarstwaWprowadzania.Services;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;
using RPGManager.WarstwaWprowadzania.Validators;
using System.Text.Json.Serialization;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllers()
    .AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICountryService, CountryService>();
//builder.Services.AddTransient<INPCValidator, NPCDataValidationService>();
builder.Services.AddTransient<INPCService, NPCService>();
builder.Services.AddTransient<INoteService, NoteService>();
builder.Services.AddTransient<IGoodsService, GoodsService>();
builder.Services.AddTransient<IValidator<NPC>, NPCValidator>();
builder.Services.AddTransient<IValidator<Country>, CountryValidator>();
builder.Services.AddTransient<IValidator<Note>, NoteValidator>();
builder.Services.AddTransient<IValidator<Goods>, GoodsValidator>();
builder.Services.AddTransient<IDataContext, DataContext>();

builder.Services.AddDbContext<DataContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("RPGManager.API")); // Zmiana migracji
});

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
