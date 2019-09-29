using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Responses
{
    public class SocialNetworkLink
    {
        public int Id { get; set; }
        
        public Work TargetWork { get; set; }
        public int TargetWorkId { get; set; }

        public string Value { get; set; }

        public SocialNetworkType SocialNetworkType { get; set; }
        public int SocialNetworkTypeId { get; set; }

    }
}
