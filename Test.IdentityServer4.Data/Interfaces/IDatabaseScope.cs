using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Test.IdentityServer4.Data.Interfaces
{
    public interface IDatabaseScope
    {
        Task<int> SaveAsync();
    }
}
