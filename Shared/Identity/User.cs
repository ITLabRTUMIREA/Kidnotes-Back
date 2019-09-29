
using Microsoft.AspNetCore.Identity;
using Shared.Links;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Identity
{
    public class User : IdentityUser<int>
    {
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public bool Verified { get; set; }

        public List<InterestToPerson> Interests { get; set; }

        public List<Work> WorksAsVolunteer { get; set; }
        public List<Work> WorksAsPublisher { get; set; }
        public List<Work> WorksAsContact { get; set; }
    }
}
