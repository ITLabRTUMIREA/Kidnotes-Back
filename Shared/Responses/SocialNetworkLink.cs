using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Responses
{
    public class CompactNetworkLink
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public CompactNetworkType Type { get; set; }

    }
}
