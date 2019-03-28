using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Inscription
    {
        public int Id { get; set; }
        
        public string identifiant { get; set; }

        public string pseudo { get; set; }

        public int MoodId { get; set; }
    }
}
