﻿namespace Test.IdentityServer4.RealData.Services
{
    public class ProfileServices //: IProfileService
    {
        //public const string CLAIM_USERID = "test:userid";
        //public const string CLAIM_ENTERPRISEID = "test:enterpriseid";

        //private readonly UserManager<User> _userManager;

        //public ProfileService(UserManager<User> userManager)
        //{
        //    _userManager = userManager;
        //}

        //public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        //{
        //    var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

        //    var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;

        //    var user = await _userManager.FindByIdAsync(subjectId).ConfigureAwait(false);
        //    if (user == null)
        //    {
        //        throw new ArgumentException("Invalid subject identifier");
        //    }

        //    var claims = GetClaimsFromUser(user);
        //    context.IssuedClaims = claims.ToList();
        //}

        //public async Task IsActiveAsync(IsActiveContext context)
        //{
        //    var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

        //    var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;
        //    var user = await _userManager.FindByIdAsync(subjectId).ConfigureAwait(false);

        //    context.IsActive = false;

        //    if (user != null)
        //    {
        //        if (_userManager.SupportsUserSecurityStamp)
        //        {
        //            var security_stamp = subject.Claims.Where(c => c.Type == "security_stamp").Select(c => c.Value).SingleOrDefault();
        //            if (security_stamp != null)
        //            {
        //                var db_security_stamp = await _userManager.GetSecurityStampAsync(user).ConfigureAwait(false);
        //                if (db_security_stamp != security_stamp)
        //                {
        //                    return;
        //                }
        //            }
        //        }

        //        context.IsActive =
        //            !user.LockoutEnabled ||
        //            !user.LockoutEnd.HasValue ||
        //            user.LockoutEnd <= DateTime.Now;
        //    }
        //}

        //private IEnumerable<Claim> GetClaimsFromUser(User user)
        //{
        //    DateTime now = DateTime.Now;
        //    var claims = new List<Claim>
        //    {
        //        new Claim(CLAIM_USERID, user.Id),
        //        new Claim(CLAIM_ENTERPRISEID, user.EnterpriseId != null ? user.EnterpriseId.ToString() : string.Empty),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
        //    };

        //    if (!string.IsNullOrWhiteSpace(user.FirstName))
        //    {
        //        claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));
        //    }

        //    if (!string.IsNullOrWhiteSpace(user.LastName))
        //    {
        //        claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
        //    }

        //    // claims.Add(new Claim("dummy", "dummy"));
        //    if (_userManager.SupportsUserEmail)
        //    {
        //        claims.AddRange(new[]
        //        {
        //            new Claim(JwtClaimTypes.Email, user.Email),
        //            new Claim(JwtClaimTypes.EmailVerified, user.EmailConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
        //        });
        //    }

        //    if (_userManager.SupportsUserPhoneNumber && !string.IsNullOrWhiteSpace(user.PhoneNumber))
        //    {
        //        claims.AddRange(new[]
        //        {
        //            new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber),
        //            new Claim(JwtClaimTypes.PhoneNumberVerified, user.PhoneNumberConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
        //        });
        //    }

        //    var roles = _userManager.GetRolesAsync(user).Result;
        //    if (roles.Count > 0)
        //    {
        //        foreach (var role in roles)
        //        {
        //            claims.Add(new Claim(JwtClaimTypes.Role, role));
        //        }
        //    }

        //    return claims;
        //}
    }
}
