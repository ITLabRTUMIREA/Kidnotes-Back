using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Work> Works { get; set; }
    }
}
