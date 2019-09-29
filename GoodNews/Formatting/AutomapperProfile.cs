using AutoMapper;
using Shared;
using Shared.Identity;
using Shared.Links;
using Shared.Prices;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodNews.Formatting
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            Requests();
            Responses();
        }
        private void Requests()
        {
            CreateMap<RegisterModel, User>()
                .ForMember(u => u.UserName, map => map.MapFrom(r => r.FirstName));
        }
        public void Responses()
        {
            CreateMap<User, CompactPerson>()
                .ForMember(cp => cp.Name, map => map.MapFrom(u => u.UserName));            



            CreateMap<User, CompactOrganization>()
                .ForMember(cp => cp.Name, map => map.MapFrom(u => u.UserName));

            CreateMap<Organization, CompactOrganization>()
                .ForMember(cp => cp.Name, map => map.MapFrom(u => u.ShortName));


            CreateMap<Work, CompactTask>()
                .ForMember(ct => ct.Networks, map => map.MapFrom(w => w.WantedRecources))
                .ForMember(ct => ct.Status, map => map.MapFrom(w => (int)w.WorkType));

            CreateMap<Work, FullTask>()
                .ForMember(ct => ct.Networks, map => map.MapFrom(w => w.WantedRecources))
                .ForMember(ct => ct.NetworkLinks, map => map.MapFrom(w => w.PublishedResources))
                .ForMember(ct => ct.Status, map => map.MapFrom(w => (int)w.WorkType));


            CreateMap<SocialNetworkLink, CompactNetworkLink>()
                .ForMember(cnl => cnl.Type, map => map.MapFrom(snl => snl.SocialNetworkType));
            CreateMap<SocialNetworkType, CompactNetworkType>();

            CreateMap<NetworkTypeToWork, CompactNetworkType>()
                .ForMember(cnt => cnt.Id, map => map.MapFrom(nttw => nttw.SocialNetworkTypeId))
                .ForMember(cnt => cnt.Title, map => map.MapFrom(nttw => nttw.SocialNetworkType.Title))
                .ForMember(cnt => cnt.Url, map => map.MapFrom(nttw => nttw.SocialNetworkType.Url));

            CreateMap<City, CompactCity>();
            CreateMap<Price, CompactPrice>();
        }
    }
}
