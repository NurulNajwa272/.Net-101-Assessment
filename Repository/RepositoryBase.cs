using Events;
using System.Linq.Expressions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public EventDbContext EventDbContext { get; set; }
        public RepositoryBase(EventDbContext eventDbContext)
        {
            EventDbContext = eventDbContext;
        }

        public IQueryable<T> FindAll()
        {
            return EventDbContext.Set<T>();     //.AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return EventDbContext.Set<T>().Where(expression).AsNoTracking();
        }
        public void Create(T entity) => EventDbContext.Set<T>().Add(entity);
        public void Update(T entity) => EventDbContext.Set<T>().Update(entity);
        public void Delete(T entity) => EventDbContext.Set<T>().Remove(entity);
    }
}