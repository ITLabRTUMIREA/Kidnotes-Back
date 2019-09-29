using Shared.Links;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Identity
{
    public class Interest
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<InterestToPerson> Persons { get; set; }
    }
}
