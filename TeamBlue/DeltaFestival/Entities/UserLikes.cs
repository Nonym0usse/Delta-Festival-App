using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class UserLikes
    {
        public int UserId { get; set; }

        public int UserLikedId { get; set; }

        public bool IsVoted { get; set; }
    }
}
