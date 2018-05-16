﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Test.IdentityServer4.EFConfigStore.Data.Entities;

namespace Test.IdentityServer4.EFStores.Persistence
{
    public interface IUserStore
    {
        Task<bool> ValidateCredentials(string username, string password);

        Task<User> FindBySubjectId(string subjectId);

        Task<User> FindByUsername(string username);

        Task<User> FindByExternalProvider(string provider, string subjectId);

        Task<User> AutoProvisionUser(string provider, string subjectId, List<Claim> claims);

        Task<bool> SaveAppUser(User user, string newPasswordToHash = null);
    }
}
