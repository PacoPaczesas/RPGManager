using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;
using RPGManager.WarstwaWprowadzania.Services;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;
using RPGManager.WarstwaWprowadzania.Validators;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using RPGManager.WarstwaInfrastruktury.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllers();
/*    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
*/
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RPGManager API", Version = "v1" });

    // Konfiguracja autoryzacji
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Podaj token JWT w formacie: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Add services
builder.Services.AddTransient<ICountryService, CountryService>();
builder.Services.AddTransient<INPCService, NPCService>();
builder.Services.AddTransient<INoteService, NoteService>();
builder.Services.AddTransient<IGoodsService, GoodsService>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IPlayerCharacterService, PlayerCharacterService>();
builder.Services.AddTransient<IValidator<NPC>, NPCValidator>();
builder.Services.AddTransient<IValidator<Country>, CountryValidator>();
builder.Services.AddTransient<IValidator<Note>, NoteValidator>();
builder.Services.AddTransient<IValidator<Goods>, GoodsValidator>();
builder.Services.AddTransient<IValidator<Users>, UserValidator>();

// Register IDataContext as a service
builder.Services.AddScoped<IDataContext, DataContext>();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("RPGManager.API"));
});

// Add ASP.NET Core Identity
builder.Services.AddIdentity<Users, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

// Authorization Token
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RPGManager API v1");
        c.RoutePrefix = ""; // To serve the Swagger UI at the app's root
    });
}

app.UseHttpsRedirection();

// Authorization Token
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
