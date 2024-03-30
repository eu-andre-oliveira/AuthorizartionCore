using Application.Configurations.Options;
using Application.Services;
using Application.Services.Interfaces;
using Data.DataContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.Configure<AuthenticationBearerOptions>(
    builder.Configuration.GetSection(AuthenticationBearerOptions.SectionName));
builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddAuthentication(opt =>
 {
     opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
     opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
 }).AddJwtBearer(opt =>
 {
     opt.SaveToken = true;
     opt.TokenValidationParameters = new TokenValidationParameters()
     {
         ValidateAudience = false,
         ValidateIssuer = false,
         ValidateLifetime = true,
         IssuerSigningKey = new SymmetricSecurityKey(
             Encoding.UTF8.GetBytes(builder.Configuration["Authentication:PrivateKey"])),
         ClockSkew = TimeSpan.Zero
     };
 });

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
