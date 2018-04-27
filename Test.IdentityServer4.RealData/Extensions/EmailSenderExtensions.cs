using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Test.IdentityServer4.RealData.Services;

namespace Test.IdentityServer4.AspNetIdentity.RealData.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
