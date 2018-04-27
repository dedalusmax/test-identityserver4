using System.ComponentModel.DataAnnotations;

namespace Test.IdentityServer4.Login.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
