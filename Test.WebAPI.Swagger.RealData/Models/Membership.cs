using Test.IdentityServer4.Data.Entities;

namespace Test.WebAPI.Swagger.RealData.Models
{
    public class Membership
    {
        public long OrganisationId { get; set; }

        public long UserId { get; set; }

        public MembershipPermission Permission { get; set; }

        public bool IsDefault { get; set; }
    }
}
