using System.Threading.Tasks;

namespace Test.IdentityServer4.Login.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
