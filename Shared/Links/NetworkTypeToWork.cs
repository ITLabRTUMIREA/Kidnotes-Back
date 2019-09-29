using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Links
{
    public class NetworkTypeToWork
    {
        public Work Work { get; set; }
        public int WorkId { get; set; }

        public SocialNetworkType SocialNetworkType { get; set; }
        public int SocialNetworkTypeId { get; set; }
    }
}
