using AutoMapper;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using Entities = Test.Data.Entities;

namespace Test.Library
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PersistedGrant, Entities.Grant>()
                .ForPath(_ => _.Expiration, _ => _.MapFrom(__ => __.Expiration.Value));

            CreateMap<Entities.Grant, PersistedGrant>()
                .ForPath(_ => _.Expiration.Value, _ => _.MapFrom(__ => __.Expiration));
        }
    }
}
