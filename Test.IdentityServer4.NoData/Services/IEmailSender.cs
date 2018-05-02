using System.Threading.Tasks;

namespace Test.IdentityServer4.NoData.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
