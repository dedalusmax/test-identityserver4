using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Data.Entities;

namespace Test.IdentityServer4.EFCustomStore.Persistence
{
    public interface IUserStore
    {
        Task<bool> ValidateCredentials(string username, string password);
        Task<User> FindBySubjectId(string subjectId);
        Task<User> FindByUsername(string username);
        Task<User> FindByExternalProvider(string provider, string subjectId);
        Task<User> AutoProvisionUser(string provider, string subjectId, List<System.Security.Claims.Claim> claims);
        Task<bool> SaveAppUser(User user, string newPasswordToHash = null);
    }
}
