using Api.Interfaces;
using API.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _Key;
        public TokenService(IConfiguration config)
        {
            _Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            var Claims = new List<Claim>
          {
              new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
          };
         var creds=new SigningCredentials(_Key,SecurityAlgorithms.HmacSha256);
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var TokenHandler = new JwtSecurityTokenHandler();
            var token=TokenHandler.CreateJwtSecurityToken(TokenDescriptor);
            return TokenHandler.WriteToken(token);
        }
    }
}
