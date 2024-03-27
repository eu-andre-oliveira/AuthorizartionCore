using Application.Configurations.Options;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.Configure<AuthenticationBearerOptions>(
    builder.Configuration.GetSection(AuthenticationBearerOptions.SectionName));
builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddAuthentication("Bearer").AddJwtBearer();
//builder.Services.AddAuthentication(opt =>
// {
//     opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(opt =>
// {
//     opt.TokenValidationParameters = new TokenValidationParameters()
//     {
//         ValidateAudience = true,
//         ValidateIssuer = true,
//         ValidateLifetime = true,

//         ValidIssuer = builder.Configuration[new AuthenticationBearerOptions().Schemes.Bearer.ValidIssuer],
//         ValidAudience = builder.Configuration[new AuthenticationBearerOptions().Schemes.Bearer.ValidAudiences[0]],
//         IssuerSigningKey = new SymmetricSecurityKey(
//             Encoding.UTF8.GetBytes(builder.Configuration["Authentication:Schemes:Bearer:ValidIssuer"])),
//         ClockSkew = TimeSpan.Zero

//     };
// });

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
