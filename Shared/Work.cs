using Shared.Identity;
using Shared.Links;
using Shared.Prices;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared
{
    public class Work
    {
        public int Id { get; set; }
        public WorkType WorkType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TaskText { get; set; }
        public string Address { get; set; }
        public DateTime Deadline { get; set; }

        public User Publisher { get; set; }
        public int? PublisherId { get; set; }


        public City PlaceCity { get; set; }
        public int PlaceCityId { get; set; }

        public List<NetworkTypeToWork> WantedRecources { get; set; }
        public List<SocialNetworkLink> PublishedResources { get; set; }

        public List<Content> Content { get; set; }

        public User ContactPerson { get; set; }
        public int ContactPersonId { get; set; }


        public List<Price> Prices { get; set; }


        public Organization OrganizationInitiator { get; set; }
        public int? OrganizationInitiatorId { get; set; }
        public User PersonInitiator { get; set; }
        public int? PersonInitiatorId { get; set; }
    }
}
