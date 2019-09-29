using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Identity
{
    public class UserModel
    {
        public string Name { get; set; }
        public List<string> Roles { get; set; }
    }
}
