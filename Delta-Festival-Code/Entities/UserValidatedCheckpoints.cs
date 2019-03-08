using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserValidatedCheckpoints

    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int CheckpointId { get; set; }

        public DateTime TimeChecked { get; set; }


        public int TeamId { get; set; }




    }
}
