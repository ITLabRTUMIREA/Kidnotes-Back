using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class Organization
    {
        public int Id { get; set; }
        public string ShortName { get; set;}
        public string FullName { get; set; }
        public string Description { get; set; }

        public List<Work> Works;
    }
}
