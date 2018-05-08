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
            CreateMap<Claim, Entities.Claim>()
                .ForMember(_ => _.Issuer, _ => _.MapFrom(__ => __.Issuer))
                .ForMember(_ => _.OriginalIssuer, _ => _.MapFrom(__ => __.OriginalIssuer))
                .ForMember(_ => _.Subject, _ => _.MapFrom(__ => __.Subject))
                .ForMember(_ => _.Type, _ => _.MapFrom(__ => __.Type))
                .ForMember(_ => _.Value, _ => _.MapFrom(__ => __.Value))
                .ForMember(_ => _.ValueType, _ => _.MapFrom(__ => __.ValueType));

            CreateMap<Entities.Claim, Claim>()
                .ForMember(_ => _.Issuer, _ => _.MapFrom(__ => __.Issuer))
                .ForMember(_ => _.OriginalIssuer, _ => _.MapFrom(__ => __.OriginalIssuer))
                .ForMember(_ => _.Subject, _ => _.MapFrom(__ => __.Subject))
                .ForMember(_ => _.Type, _ => _.MapFrom(__ => __.Type))
                .ForMember(_ => _.Value, _ => _.MapFrom(__ => __.Value))
                .ForMember(_ => _.ValueType, _ => _.MapFrom(__ => __.ValueType));

            CreateMap<PersistedGrant, Entities.Grant>()
                .ForMember(_ => _.Key, _ => _.MapFrom(__ => __.Key))
                .ForMember(_ => _.ClientId, _ => _.MapFrom(__ => __.ClientId))
                .ForMember(_ => _.CreationTime, _ => _.MapFrom(__ => __.CreationTime))
                .ForMember(_ => _.Data, _ => _.MapFrom(__ => __.Data))
                .ForPath(_ => _.Expiration, _ => _.MapFrom(__ => __.Expiration.Value))
                .ForMember(_ => _.SubjectId, _ => _.MapFrom(__ => __.SubjectId))
                .ForMember(_ => _.Type, _ => _.MapFrom(__ => __.Type));

            CreateMap<Entities.Grant, PersistedGrant>()
                .ForMember(_ => _.Key, _ => _.MapFrom(__ => __.Key))
                .ForMember(_ => _.ClientId, _ => _.MapFrom(__ => __.ClientId))
                .ForMember(_ => _.CreationTime, _ => _.MapFrom(__ => __.CreationTime))
                .ForMember(_ => _.Data, _ => _.MapFrom(__ => __.Data))
                .ForPath(_ => _.Expiration.Value, _ => _.MapFrom(__ => __.Expiration))
                .ForMember(_ => _.SubjectId, _ => _.MapFrom(__ => __.SubjectId))
                .ForMember(_ => _.Type, _ => _.MapFrom(__ => __.Type));
        }
    }
}
