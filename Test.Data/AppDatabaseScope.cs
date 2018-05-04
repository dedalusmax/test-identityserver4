using System.Threading.Tasks;

namespace Test.Data
{
    public class AppDatabaseScope : IDatabaseScope
    {
        private AppDbContext _context { get; }

        public AppDatabaseScope(AppDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
