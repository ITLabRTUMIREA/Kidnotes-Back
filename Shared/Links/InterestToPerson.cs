using Shared.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Links
{
    public class InterestToPerson
    {
        public Interest Interest { get; set; }
        public int InterestId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
