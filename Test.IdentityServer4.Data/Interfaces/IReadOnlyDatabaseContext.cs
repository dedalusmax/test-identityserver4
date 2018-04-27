using Microsoft.EntityFrameworkCore;

namespace Test.IdentityServer4.Data.Interfaces
{
    public interface IReadOnlyDatabaseContext
    {
        DbSet<TEntity> DataSet<TEntity>()
            where TEntity : class;
    }
}
