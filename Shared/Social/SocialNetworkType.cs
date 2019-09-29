using Shared.Links;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Responses
{
    public class SocialNetworkType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public List<NetworkTypeToWork> WantedByWorks { get; set; }
    }
}
