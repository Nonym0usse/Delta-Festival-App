using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Place
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public virtual PlaceCategory Category { get; set; }


        //todo add geometry / polygone
    }
    
}
