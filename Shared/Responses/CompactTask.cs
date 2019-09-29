using Shared.Prices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.Responses
{
    public class CompactTask
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public CompactCity PlaceCity { get; set; }
        public int Status { get; set; }
        public CompactOrganization OrganizationInitiator { get; set; }
        public CompactPerson PersonInitiator { get; set; }

        [Required]
        public CompactPerson ContactPerson { get; set; }

        [Required]
        public List<CompactNetworkType> Networks { get; set; }

        [Required]
        public List<CompactPrice> Prices  { get; set; } 
    }
}
