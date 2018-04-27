using System.Threading.Tasks;

namespace Test.IdentityServer4.RealData.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
