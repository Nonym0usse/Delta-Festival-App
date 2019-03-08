using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        public int Id { get; set; }

        //  public string Email { get; set; }

        //  public string Password { get; set; }

        //  public string Pseudo { get; set; }

        public int UserRoleId { get; set; }
        public virtual UserRole Role { get; set; }

        public int MoodId { get; set; }
        public Mood ActualMood { get; set; }

        public ICollection<Publication> Publications { get; set; }

        public string TicketCode { get; set; }//index // unique

        public bool CanPublish { get; set; }

        public int Demission { get; set; } //resign

        public bool IsActive { get; set; }




    }
}
