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
    [Route("api")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamContext _context;

        public TeamController(TeamContext context)
        {
            _context = context;
        }
        // GET api/teams - Retourne la liste des équipes
        [HttpGet("teams")]
        public async Task<ActionResult<IEnumerable<Team>>> Get()
        {
            return await _context.Teams.ToListAsync();
        }

        // GET api/teams/1 - Retourne une équipe selon un ID spécifique
        [HttpGet("teams/{id}")]
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
        [HttpPost("teams")]
        public async Task<ActionResult<Team>> NewTeam(Team item)
        {
            item.Score = 0;
            _context.Teams.Add(item);
            await _context.SaveChangesAsync();
            // Content-type : application/json

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        // GET /api/merges/{id} - Liste des équipes avec lesquelles l'équipe avec cet ID peut fusionner
        [HttpGet("merges/{id}")]
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
                if(index.Id == id)
                {
                    mergeable.Remove(index);
                }
            }
            return mergeable;
        }

        // PUT /api/teams/{id} - Changer les propriétés de l'équipe
        [HttpPut("teams/{id}")]
        public async Task<IActionResult> Dismiss(long id, Team item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }


            _context.Entry(item).State = EntityState.Modified;

            Team team = await _context.Teams.FindAsync(item.Id);

            _context.Entry(team).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /api/delete_team/{id} - Supprime une team
        [HttpDelete("delete_team/{id}")]
        public async Task<ActionResult<Team>> Delete(int id)
        {
            Team deleted = await _context.Teams.FindAsync(id);

            if (deleted == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(deleted);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //POST /api/merge/{id_team1}/{id_team2} - Fusion de 2 équipes
        [HttpPost("merge/{id_team1}/{id_team2}")]
        public async Task<ActionResult<Team>> Merge(int id_team1, int id_team2)
        {
            Team team1 = await _context.Teams.FindAsync(id_team1);
            Team team2 = await _context.Teams.FindAsync(id_team2);

            Team merged_team = new Team();
            merged_team.MembersCount = team1.MembersCount + team2.MembersCount;
            merged_team.Name = team1.Name + " & " + team2.Name;
            if(merged_team.MembersCount == 10)
            {
                merged_team.IsActive = false;
            }
            await NewTeam(merged_team);
            await Delete(id_team1);
            await Delete(id_team2);
            return merged_team;
            
        }
    }
}