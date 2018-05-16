using AutoMapper;
using IdentityServer4.Models;
using System.Linq;

namespace Test.IdentityServer4.EFStores.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PersistedGrant, Entities.Grant>()
                .ForMember(_ => _.Id, _ => _.Ignore())
                ;

            //CreateMap<IdentityResource, Entities.IdentityResourceEntity>()
            //    .ForMember(_ => _.IdentityResource, _ => _.Ignore());

            //CreateMap<ApiResource, Entities.ApiResourceEntity>()
            //    .ForMember(_ => _.ApiResource, _ => _.Ignore())
            //    .ForMember(_ => _.UserClaims, _ => _.Ignore())
            //    .ForMember(_ => _.Scopes, _ => _.Ignore())
            //    ;

            CreateMap<IdentityResource, Entities.IdentityResourceEntity>()
                .ForMember(_ => _.Id, _ => _.Ignore())
                .ForMember(_ => _.IdentityResource, _ => _.Ignore())
                .ForMember(_ => _.IdentityResourceData, _ => _.Ignore());

            CreateMap<ApiResource, Entities.ApiResourceEntity>()
                .ForMember(_ => _.Id, _ => _.Ignore())
                .ForMember(_ => _.ApiResource, _ => _.Ignore())
                .ForMember(_ => _.ApiResourceData, _ => _.Ignore());

            CreateMap<Client, Entities.ClientEntity>()
                .ForMember(_ => _.ClientSecrets, _ => _.MapFrom(__ => __.ClientSecrets))
                .ForMember(_ => _.AllowedGrantTypes, _ => _.Ignore());// _ => _.MapFrom(__ => __.AllowedGrantTypes.ToList<string>()))
                ;
            //    .ForMember(_ => _.ClientClaims, _ => _.Ignore())
            //    .ForMember(_ => _.ClientScopes, _ => _.Ignore())
            //    .ForMember(_ => _.RedirectUris, _ => _.MapFrom(__ => __.RedirectUris))
            //    ;

            ////CreateMap<Entities.ApiResourceEntity, ApiResource>()
            ////    .ForMember(_ => _.UserClaims, _ => _.Ignore())
            ////    .ForMember(_ => _.Scopes, _ => _.Ignore())
            ////    ;

            //CreateMap<IdentityResource, Entities.IdentityResourceEntity>()
            //    .ForMember();
        }
    }
}
