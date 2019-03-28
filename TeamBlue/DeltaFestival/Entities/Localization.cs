using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Localization
    {

        public Guid Id { get; set; }

        //todo 
        //log
        //lat
        //date
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int UserTeamId { get; set; }

        public int ZoneId { get; set; }

        public virtual Zone Zone {get;set;}

    


    }
}
