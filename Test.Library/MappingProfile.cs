using AutoMapper;
using IdentityServer4.Models;
using Entities = Test.Data.Entities;

namespace Test.Library
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // TO DO: migrate Grant entity with a nullable expiration property
            CreateMap<PersistedGrant, Entities.Grant>()
                .ForMember(_ => _.Id, _ => _.Ignore());
        }
    }
}
