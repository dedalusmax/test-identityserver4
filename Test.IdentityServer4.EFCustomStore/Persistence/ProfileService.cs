using AutoMapper;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Test.IdentityServer4.EFCustomStore.Persistence
{
    public class ProfileService : IProfileService
    {
        protected readonly IUserStore _userstore;

        public ProfileService(IUserStore injectedUserStore)
        {
            _userstore = injectedUserStore;
        }

        public virtual async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            if (context.RequestedClaimTypes.Any())
            {
                var user = await _userstore.FindBySubjectId(context.Subject.GetSubjectId());
                if (user != null)
                {
                    var reqClaim = user.Claims;

                    List<Claim> mappedClaims = new List<Claim>();

                    foreach (Data.Entities.Claim claim in reqClaim)
                    {
                        var mappedClaim = Mapper.Map<Data.Entities.Claim, Claim>(claim);

                        mappedClaims.Add(mappedClaim);
                    }

                    context.AddRequestedClaims(mappedClaims);
                }
            }
            return;
        }

        public virtual async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userstore.FindBySubjectId(context.Subject.GetSubjectId());
            context.IsActive = !(user is null); // TODO check indicators like account status
            return;
        }

    }
}
