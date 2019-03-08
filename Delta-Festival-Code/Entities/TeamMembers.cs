using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  public   class TeamMembers
    {
        public int Id { get; set; }

        public int TeamId { get; set; }
        public int UserId { get; set; }


        DateTime JoinDate { get; set; }
        
        public bool IsActive { get; set; }
    }
}
