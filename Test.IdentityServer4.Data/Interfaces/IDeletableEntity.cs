using System;

namespace Test.IdentityServer4.Data.Interfaces
{
    public interface IDeletableEntity
    {
        DateTime? HasBeenDeleted { get; set; }
    }
}
