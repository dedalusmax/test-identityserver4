using AutoMapper;
using IdentityServer4.Models;
using Entities = Test.Data.Entities;

namespace Test.Library
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PersistedGrant, Entities.Grant>()
                .ForMember(_ => _.Id, _ => _.Ignore());
        }
    }
}
