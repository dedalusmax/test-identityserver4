using System.ComponentModel.DataAnnotations;

namespace Test.IdentityServer4.Data.Entities
{
    public class Membership
    {
        [Key]
        public long Id { get; set; }

        public long OrganisationId { get; set; }

        public Organisation Organisation { get; set; }

        public long UserId { get; set; }

        public ApplicationUser User { get; set; }

        public MembershipPermission Permission { get; set; }

        public bool IsDefault { get; set; }
    }

    public enum MembershipPermission
    {
        None,
        Administrator,
        User
    }
}
