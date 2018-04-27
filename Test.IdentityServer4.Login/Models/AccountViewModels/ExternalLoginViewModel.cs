using System.ComponentModel.DataAnnotations;

namespace Test.IdentityServer4.Login.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
