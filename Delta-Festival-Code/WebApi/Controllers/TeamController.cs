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
    [Route("api/teams")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly EfContext _context;

        public TeamController(EfContext context)
        {
            _context = context;
        }
        // GET api/teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> Get()
        {
            return await _context.Teams.ToListAsync();
        }

        // GET api/teams/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetItem(int id)
        {
            var teams = await _context.Teams.FindAsync(id);

            if (teams == null)
            {
                return NotFound();
            }

            return teams;
        }
    }
}
