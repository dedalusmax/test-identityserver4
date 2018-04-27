using System;

namespace Test.IdentityServer4.Data.Interfaces
{
    public interface IDatedEntity
    {
        DateTime Created { get; set; }

        DateTime Updated { get; set; }

        string ModifiedBy { get; set; }
    }
}
