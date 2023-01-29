using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]// api/users
    public class UsersController:ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            this._context = context;
        }

      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var Users = await this._context.Users.ToListAsync();
            return Users;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {   
            return await this._context.Users.FindAsync(id);
           
        }
    }
}
