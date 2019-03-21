using Database;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly EfContext _context;

        public UserController(EfContext context)
        {
            _context = context;
        }
        // GET api/users - On retourne la liste de tous les utilisateurs
        [HttpGet("api/users")]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _context.Users.ToListAsync();
        }
        // GET /api/user/{id} - On retourne un user avec l'ID spécifique
        [HttpGet("api/user/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }
        
        // POST api/signup - Inscription d'un nouvel utilisateur
        //[HttpPost("api/signup")]
        //public async Task<ActionResult<User>> CreateUser(User item)
        //{
        //    _context.Users.Add(item);
        //    await _context.SaveChangesAsync();
        //    // Content-type : application/json

        //    return CreatedAtAction(nameof(GetUser), new { id = item.Id }, item);
        //}
    }
}
