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
    [Route("api/signup")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        
        private readonly EfContext _context;

        public SignupController(EfContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }
        
        // POST api/signup - Seule méthode accessible, pour éviter les problèmes de sécurité
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User item)
        {
            _context.Users.Add(item);
            await _context.SaveChangesAsync();
            // Content-type : application/json

            return CreatedAtAction(nameof(GetUser), new { id = item.Id }, item);
        }
    }
}
