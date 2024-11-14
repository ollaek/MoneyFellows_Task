using MF_Task.Core.Interfaces;

namespace MF_Task.Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        // Get the repository for a specific entity
        public IBaseRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                // Create the repository and add it to the dictionary
                var repositoryInstance = new BaseRepository<T>(_context);
                _repositories[type] = repositoryInstance;
            }

            return (IBaseRepository<T>)_repositories[type];
        }

        // Commit the changes to the database
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // Dispose of the DbContext when done
        public void Dispose()
        {
            _context.Dispose();
        }
    }


}
