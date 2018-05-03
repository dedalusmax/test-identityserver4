using System.Threading.Tasks;

namespace Test.IdentityServer4.NoData.Services
{
    public interface ILoginService<T>
    {
        Task<bool> ValidateCredentials(T user, string password);

        Task<T> FindByEmail(string user);

        Task SignIn(T user);
    }
}
