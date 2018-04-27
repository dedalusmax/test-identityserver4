using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Test.IdentityServer4.Data.Entities;

namespace Test.WebAPI.Swagger.RealData.Models
{
    public abstract class Organisation
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "ORGANISATION_CANNOT_BE_NAMELESS"), StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public string BaseUrl { get; set; }

        public AzureDeploymentStatus DeploymentStatus { get; set; }
    }

    public class OrganisationDetails : Organisation
    {
        public ICollection<Membership> OrganisationMembership { get; set; }
    }

    public class OrganisationView : Organisation
    {
        public int Users { get; set; }
    }
}
