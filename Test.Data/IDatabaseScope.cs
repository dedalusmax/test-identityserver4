using System.Threading.Tasks;

namespace Test.Data
{
    public interface IDatabaseScope
    {
        /// <summary>
        /// Saves pending changes asynchronously.
        /// </summary>
        Task<int> SaveAsync();
    }
}
