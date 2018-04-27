using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test.IdentityServer4.Data.Entities
{
    public class Organisation
    {
        public Organisation()
        {
            OrganisationMembership = new List<Membership>();
        }

        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection<Membership> OrganisationMembership { get; set; }

        public string BaseUrl { get; set; }

        [Required]
        public AzureDeploymentStatus DeploymentStatus { get; set; }

    }

    public enum AzureDeploymentStatus
    {
        NotStarted = 0,
        InProgress = 1,
        Succeeded = 2,
        Failed = 3
    }
}
