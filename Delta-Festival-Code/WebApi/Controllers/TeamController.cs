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
        // GET api/teams - Retourne la liste des équipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> Get()
        {
            return await _context.Teams.ToListAsync();
        }

        // GET api/teams/1 - Retourne une équipe selon un ID spécifique
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

        // POST /api/teams - Nouvelle équipe
        [HttpPost]
        public async Task<ActionResult<Team>> NewTeam(Team item)
        {
            _context.Teams.Add(item);
            await _context.SaveChangesAsync();
            // Content-type : application/json

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }
        [Route("api/merges")]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Team>>> Merges(int id)
        {
            var teams = await _context.Teams.FindAsync(id);

            if (teams == null)
            {
                return NotFound();
            }

            int members = teams.MembersCount;

            List<Team> teamList = await _context.Teams.ToListAsync();
            List<Team> mergeable = new List<Team>();

            foreach(Team index in teamList)
            {
                if(index.MembersCount + members <= 10)
                {
                    mergeable.Add(index);
                }
            }

            return mergeable;
        }

    }
}
