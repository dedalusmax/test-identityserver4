using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Test.IdentityServer4.Data.Entities;

namespace Test.IdentityServer4.NoData.Services
{
    public class EfLoginService : ILoginService<ApplicationUser>
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public void EFLoginService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUser> FindByEmail(string user)
        {
            return await _userManager.FindByEmailAsync(user).ConfigureAwait(false);
        }

        public async Task<bool> ValidateCredentials(ApplicationUser user, string password)
        {
            return await Task.Run(() =>
            {
                var passwordHash = CryptographyService.CreateHash(password, user.PasswordSalt);
                if (passwordHash != user.PasswordHash)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }).ConfigureAwait(false);
        }

        public Task SignIn(ApplicationUser user)
        {
            return _signInManager.SignInAsync(user, true);
        }
    }
}
