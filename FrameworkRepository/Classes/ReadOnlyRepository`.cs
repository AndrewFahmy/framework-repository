using System;
using System.Linq;
using System.Linq.Expressions;
using FrameworkRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FrameworkRepository
{
    public class ReadOnlyRepository<T, TDbContext> : IReadOnlyRepository<T, TDbContext>
        where T : class, new()
        where TDbContext : DbContext
    {
        private readonly TDbContext _context;

        public ReadOnlyRepository(TDbContext context)
        {
            _context = context;
        }



        public long Count(Expression<Func<T, bool>> predicate = null)
        {
            return _context.Set<T>().LongCount(predicate ?? (p => true));
        }

        public bool Exists(Expression<Func<T, bool>> predicate = null)
        {
            return _context.Set<T>().Any(predicate ?? (p => true));
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public DbSet<T> GetQueryable()
        {
            return _context.Set<T>();
        }

        public TResult Max<TResult>(Expression<Func<T, TResult>> selector)
        {
            if (_context.Set<T>().Count() <= 0) return default(TResult);

            return _context.Set<T>().Max(selector);
        }
    }
}