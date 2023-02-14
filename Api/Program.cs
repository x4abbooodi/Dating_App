

using Api.Extension;
using Api.Interfaces;
using Api.Services;
using API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

//These two line shold be in the same order and the same postion, after the app.useCors and before MapControllers
app.UseAuthentication();
app.UseAuthorization();
//////////////////////////////////////////
app.MapControllers();

app.Run();
