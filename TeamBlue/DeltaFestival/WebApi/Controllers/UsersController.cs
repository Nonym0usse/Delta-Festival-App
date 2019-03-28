using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Database;
using Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TeamContext _context;

        public UsersController(TeamContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }


        // FONCTIONNALITÉ CUSTOM -- Système de likes entre personnes pour mesurer la popularité et l'intégration des personnes
        // au Festival

        // GET /api/users/likes -- Liste les personnes ayant déjà liké quelqu'un

        [HttpGet("likes")]
        public async Task<ActionResult<IEnumerable<UserLikes>>> GetLikes()
        {
            return await _context.UserLikes.ToListAsync();
        }


        // Cette fonction est pas nécessaire pour l'API, elle est là pour la route /api/likes en POST
        public async Task<ActionResult<UserLikes>> GetLike(int id)
        {
            var like = await _context.UserLikes.FindAsync(id);

            if (like == null)
            {
                return NotFound();
            }

            return like;
        }

        //POST /api/likes - Nouveau like
        [HttpPost("likes")]
        public async Task<ActionResult<UserLikes>> NewLike(UserLikes like)
        {
            _context.UserLikes.Add(like);
            User liked = await _context.Users.FindAsync(like.UserLikedId);
            liked.LikeScore++;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLike", new { id = like.UserId }, like);
        }

        [HttpDelete("likes/{id}")]
        public async Task<ActionResult<UserLikes>> DeleteLike(int id)
        {
            var like = await _context.UserLikes.FindAsync(id);
            if (like == null)
            {
                return NotFound();
            }

            _context.UserLikes.Remove(like);
            await _context.SaveChangesAsync();

            return like;
        }
    }
}
