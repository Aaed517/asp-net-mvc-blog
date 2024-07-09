using Repositories.Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
    where T : class, new()
    {
        protected readonly RepositoryContext _context;

        protected RepositoryBase(RepositoryContext context) => _context = context;

        public void Add(T? entity)
        {
            if (entity is not null)
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            }
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();

        }

        public IQueryable<T> GetAll(bool trackChanges)
        {
            return trackChanges ? _context.Set<T>() : _context.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            IQueryable<T> query = trackChanges
                ? _context.Set<T>()
                : _context.Set<T>().AsNoTracking();

            return query.Where(expression);
        }


        public void Update(T entity, int id)
        {
            var existingEntity = _context.Set<T>().Find(id);
    
            if (existingEntity == null)
            {
                throw new InvalidOperationException("Entity not found");
            }
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            _context.SaveChanges();
        }


    }
}