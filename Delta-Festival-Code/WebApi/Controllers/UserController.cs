﻿using Database;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

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
        [HttpGet("api/user/{identifiant}")]
        public async Task<ActionResult<User>> GetUser(string identifiant)
        {
            User user = _context.Users.Single(x => x.identifiant == identifiant);

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

            return CreatedAtAction(nameof(GetUser), new { item.identifiant  }, item);
        }

        // PUT api/signup-infos - Création des infos utilisateur
        [HttpPut("api/signup/{identifiant}")]
        public async Task<ActionResult<Inscription>> UpdateUser(User item)
        {
            var user = _context.Users.SingleOrDefault(x => x.identifiant == item.identifiant);
            //default
            if (user == null) 
            {
                return NotFound();
            }

            user.pseudo = item.pseudo;
            user.password = item.password;
            user.ActualMood = item.ActualMood;
            await _context.SaveChangesAsync();

            //List<string> userLight = new List<string>();
            //userLight.Add(item.Id.ToString());
            //userLight.Add(item.identifiant);
            //userLight.Add(item.ActualMood.Id.ToString());
            //userLight.Add(item.pseudo);
            Inscription inscription = new Inscription();
            inscription.Id = item.Id;
            inscription.identifiant = item.identifiant;
            inscription.MoodId = item.MoodId;
            inscription.pseudo = item.pseudo;

            return inscription;
        }
    }
}