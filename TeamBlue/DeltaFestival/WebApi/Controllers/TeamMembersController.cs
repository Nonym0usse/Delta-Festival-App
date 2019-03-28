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
    public class TeamMembersController : ControllerBase
    {

        private readonly TeamContext _context;

        public TeamMembersController(TeamContext context)
        {
            _context = context;
        }
        public static string GetRandomAlphaNumeric()
        {
            Random random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(chars.Select(c => chars[random.Next(chars.Length)]).Take(8).ToArray());
        }
        // GET api/team_members - On retourne la liste de tous les utilisateurs et de leur team associée
        [HttpGet("team_members")]
        public async Task<ActionResult<IEnumerable<TeamMembers>>> Get()
        {
            return await _context.TeamMembers.ToListAsync();
        }
        // GET /api/team_members/{team_id} - On retourne tous les utilisateurs faisant partie de la team ID
        [HttpGet("team_members/{team_id}")]
        public async Task<ActionResult<IEnumerable<TeamMembers>>> GetTeamUsers(int team_id)
        {
            Team teams = await _context.Teams.FindAsync(team_id);

            if (teams == null)
            {
                return NotFound();
            }
            List<TeamMembers> teamMembers = await _context.TeamMembers.ToListAsync();
            List<TeamMembers> members = new List<TeamMembers>();
            foreach(TeamMembers index in teamMembers)
            {
                if(index.TeamId == team_id)
                {
                    members.Add(index);
                }
            }


            return members;
        }

        //// DELETE /api/team_member/{id_user} - On supprime l'utilisateur qui a cet ID
        //[HttpDelete("team_member/{id_user}")]
        //public async Task<ActionResult<TeamMembers>> DeleteMember(int id_user)
        //{
        //    TeamMembers deleted = await _context.TeamMembers.FindAsync(id_user);

        //    if (deleted == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TeamMembers.Remove(deleted);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        // PUT /api/change_team/{id} - On change la team de l'utilisateur en question
        [HttpPut("change_team/{id}")]
        public async Task<IActionResult> ChangeTeam(long id, TeamMembers item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET /api/team_member/{id} - On renvoie le team member qui a cet ID
        [HttpGet("team_member/{id}")]
        public async Task<ActionResult<TeamMembers>> GetUser(int id)
        {
            var teamMember = await _context.TeamMembers.FindAsync(id);

            if (teamMember == null)
            {
                return NotFound();
            }

            return teamMember;
        }

        // POST /api/team_members - Ajout d'un utilisteur à une team
        [HttpPost("team_members")]
        public async Task<ActionResult<TeamMembers>> AddUser (TeamMembers item)
        {
            item.JoinDate = DateTime.Now;
            _context.TeamMembers.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = item.Id }, item);
        }
        // PUT /api/team_member/{id} - On retire un utilisateur d'une équipe
        [HttpPut("team_member/{id}")]
        public async Task<IActionResult> Dismiss(long id, TeamMembers item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }


            _context.Entry(item).State = EntityState.Modified;

            User user = await _context.Users.FindAsync(item.UserId);

            user.Demission++;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET /api/recrues - Consulter la liste des personnes sans équipes avec moins de 2 démissions
        [HttpGet("recrues")]
        public async Task<ActionResult<List<User>>> Recrues()
        {
            List<User> users = await _context.Users.ToListAsync();
            List<User> recrues = new List<User>();

            List<TeamMembers> members = await _context.TeamMembers.ToListAsync();

            foreach(User index in users)
            {
                if (index.Demission != 2)
                {
                    recrues.Add(index);
                }
                foreach (TeamMembers member in members)
                {
                    if (member.TeamId != 0 && member.UserId == index.Id)
                    {
                        recrues.Remove(index);
                    }
                }
                
            }
            

            return recrues;
        }
    }
}
