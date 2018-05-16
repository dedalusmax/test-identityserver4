using System.Threading.Tasks;

namespace Test.IdentityServer4.EFStores.Data
{
    public interface IDatabaseScope
    {
        /// <summary>
        /// Saves pending changes asynchronously.
        /// </summary>
        Task<int> SaveAsync();
    }
}
