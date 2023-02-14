using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.Design;
using System.Configuration;
using System.Text;

namespace Api.Extension
{
    public static class IdentityServisExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection service, IConfiguration config)
        {
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(Options =>
    {
        Options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
            return service;
        }
    }
}
