using System.ComponentModel.Design;

namespace Api.Extension
{
    using Api.Interfaces;
    using Api.Services;
    using API.Data;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    public static class ApplicationServisExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

           return services;
        }
    }
}
