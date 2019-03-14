using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int MembersCount { get; set; }

        public DateTime ? WinDate { get; set; }
        public int ? Score {get; set;}
    }
}
