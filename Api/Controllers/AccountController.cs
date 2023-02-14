using Api.DTOs;
using Api.Interfaces;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            this._context = context;
            this._tokenService = tokenService;
        }

        [HttpPost("register")] //Post: api/Account/register
        public async Task<ActionResult<UserDto>> register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName)) return BadRequest("UserName Is Taken");

            using var hmac = new HMACSHA512();
            var User = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                Username = User.UserName,
                Token = _tokenService.CreateToken(User)
            };
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if (User == null) return Unauthorized("Invalid UserName");
            using var hmac = new HMACSHA512(User.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computeHash.Length; i++)
            {

                if (computeHash[i] != User.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            return new UserDto
            {
                Username = User.UserName,
                Token = _tokenService.CreateToken(User)
            };
        }
        private async Task<Boolean> UserExists(String UsreName)
        {
            return await _context.Users.AnyAsync(x => x.UserName == UsreName.ToLower());
        }
    }
}
