using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProjetoFinal.Core.Interfaces;
using ProjetoFinal.Core.Services;
using ProjetoFinal.Infra.Data.Repository;
using ProjetoFinal_WebIII.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


var key = Encoding.ASCII.GetBytes(builder.Configuration["secretKey"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(key),
             ValidateIssuer = true,
             ValidIssuer = "APIClientes.com",
             ValidateAudience = true,
             ValidAudience = "APIEvents.com"
         };
     });



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options =>
   options.Filters.Add<LogGeneralExceptionFilter>()

);

builder.Services.AddScoped<ICityEventService, CityEventService>();
builder.Services.AddScoped<ICityEventRepository, CityEventRepository>();

builder.Services.AddScoped<LogGaranteEventoExisteActionFilter>();
//builder.Services.AddScoped<LogGaranteReservaExisteActionFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
