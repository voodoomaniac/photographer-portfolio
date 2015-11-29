using System;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using PP.Core.Entities;
using PP.Core.Interfaces.Repository;

namespace PP.Data.EF.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;

        private readonly DbSet<T> _entities;

        public Repository(DbContext context)
        {
            this._context = context;
            this._entities = this._context.Set<T>();
        }

        public void Add(T entity)
        {
            this._entities.Add(entity);
            this._context.SaveChanges();
        }

        public T Get(long id)
        {
            return this._entities.Find(id);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return this._entities.FirstOrDefault(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return this._entities;
        }

        public IQueryable<T> GetPage<TKey>(int itemsPerPage, int currentPage, Expression<Func<T, TKey>> orderByExpression)
        {
            return this._entities.OrderBy(orderByExpression).Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return this._entities.Where(predicate);
        }

        public void Remove(T entity)
        {
            this._entities.Remove(entity);
            this._context.SaveChanges();
        }
    }
}