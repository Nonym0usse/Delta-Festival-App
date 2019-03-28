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
        [HttpGet("api/user/{pseudo}")]
        public async Task<ActionResult<User>> GetUser(string identifiant)
        {
            var user = _context.Users.First(p => p.identifiant == identifiant);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST api/signup - Inscription d'un nouvel utilisateur
        [HttpPost("api/signup")]
        public async Task<ActionResult<User>> CreateUser(User item)
        {
            _context.Users.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = item.Id, identifiant = item.identifiant  }, item);
        }

        // PUT api/signup-infos - Création des infos utilisateur
        [HttpPut("api/signup-infos")]
        public async Task<ActionResult<User>> UpdateUser(User item)
        {
            var IdentifiantCheck = _context.Users.Find(item.Identifiant);

            if (_context.Users.Find(IdentifiantCheck) == null) 
            {
                return NotFound();
            }

            Mood mood = item.Mood;
            _context.Mood.Update(mood);
            await _context.SaveChangesAsync();

            User user = { item.Identifiant, item.Pseudo, item.Password };
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

        }
    }
}
