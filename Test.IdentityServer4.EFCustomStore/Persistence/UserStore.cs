using IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Test.Data;
using Entities = Test.Data.Entities;
using Test.Library.Services;

namespace Test.IdentityServer4.EFCustomStore.Persistence
{
    public class UserStore : IUserStore
    {
        private readonly IDatabaseScope _database;
        private readonly IGenericRepository<Entities.User> _entityRepository;

        public UserStore(IDatabaseScope database, IGenericRepository<Entities.User> entityRepository)
        {
            _database = database;
            _entityRepository = entityRepository;
        }

        public async Task<bool> ValidateCredentials(string username, string password)
        {
            var user = await _entityRepository
                .AsReadOnly()
                .SingleOrDefaultAsync(s => s.UserName == username);

            if (user != null)
            {
                return (String.IsNullOrEmpty(user.PasswordSalt) || String.IsNullOrEmpty(user.PasswordHash)) ? false 
                    : CryptographyService.PasswordValidation(user.PasswordHash, user.PasswordSalt, password);
            }
            else
            {
                return false;
            }
        }

        public async Task<Entities.User> FindBySubjectId(string subjectId)
        {
            return await _entityRepository
                .AsReadOnly()
                .Include(c => c.Claims)
                .SingleOrDefaultAsync(s => s.SubjectId == subjectId);
        }

        public async Task<Entities.User> FindByUsername(string username)
        {
            return await _entityRepository
                .AsReadOnly()
                .Include(c => c.Claims)
                .SingleOrDefaultAsync(s => s.UserName == username);
        }

        public async Task<Entities.User> FindByExternalProvider(string provider, string subjectId)
        {
            return await _entityRepository
                .AsReadOnly()
                .Include(c => c.Claims)
                .SingleOrDefaultAsync(s => s.ProviderName == provider && s.ProviderSubjectId == subjectId);
        }

        public async Task<Entities.User> AutoProvisionUser(string provider, string subjectId, List<Claim> claims)
        {
            // create a list of claims that we want to transfer into our store
            var filtered = new List<Claim>();

            foreach (var claim in claims)
            {
                // if the external system sends a display name - translate that to the standard OIDC name claim
                if (claim.Type == ClaimTypes.Name)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, claim.Value));
                }
                // if the JWT handler has an outbound mapping to an OIDC claim use that
                else if (JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.ContainsKey(claim.Type))
                {
                    filtered.Add(new Claim(JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap[claim.Type], claim.Value));
                }
                // copy the claim as-is
                else
                {
                    filtered.Add(claim);
                }
            }

            // if no display name was provided, try to construct by first and/or last name
            if (!filtered.Any(x => x.Type == JwtClaimTypes.Name))
            {
                var first = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.GivenName)?.Value;
                var last = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.FamilyName)?.Value;
                if (first != null && last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first + " " + last));
                }
                else if (first != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first));
                }
                else if (last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, last));
                }
            }

            // create a new unique subject id
            var sub = CryptoRandom.CreateUniqueId();

            // check if a display name is available, otherwise fallback to subject id
            var name = filtered.FirstOrDefault(c => c.Type == JwtClaimTypes.Name)?.Value ?? sub;

            // create new user
            var user = new Entities.User
            {
                SubjectId = sub,
                UserName = name,
                ProviderName = provider,
                ProviderSubjectId = subjectId,
                Claims = filtered
            };

            // store it and give it back
            await SaveAppUser(user);
            return user;
        }

        public async Task<bool> SaveAppUser(Entities.User user, string newPasswordToHash = null)
        {
            bool success = true;
            if (!String.IsNullOrEmpty(newPasswordToHash))
            {
                user.PasswordSalt = CryptographyService.PasswordSaltInBase64();
                user.PasswordHash = CryptographyService.PasswordToHashBase64(newPasswordToHash, user.PasswordSalt);
            }
            try
            {
                var entity = await _entityRepository
                    .AsReadOnly()
                    .SingleOrDefaultAsync(s => s.Id == user.Id);

                if (entity == null)
                {
                    entity = user;
                    _entityRepository.Insert(entity);
                }

                await _database.SaveAsync();
            }
            catch
            {
                success = false;
            }
            return success;
        }
    }
}
