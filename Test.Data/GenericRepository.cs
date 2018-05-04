using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private AppDbContext _context { get; }

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public void Delete(TEntity item)
        {
            _context.Set<TEntity>().Remove(item);
        }

        public TEntity Insert(TEntity item)
        {
            _context.Set<TEntity>().Add(item);
            return item;
        }

        public void InsertMultiple(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public IQueryable<TEntity> AsReadOnly()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public IEnumerable<TEntity> GetPage(int startRecord, int maxRecords, Func<TEntity, object> sort, bool? asc = true)
        {
            IEnumerable<TEntity> query = _context.Set<TEntity>();
            if (sort != null)
            {
                query = (asc.HasValue && asc.Value) ? query.OrderBy(sort) : query.OrderByDescending(sort);
            }
            return query.Skip(startRecord).Take(maxRecords);
        }
    }
}
