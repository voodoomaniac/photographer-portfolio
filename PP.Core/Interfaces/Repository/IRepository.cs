using System;
using System.Linq;
using System.Linq.Expressions;
using PP.Core.Entities;

namespace PP.Core.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Add(T entity);

        T Get(long id);

        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll();

        IQueryable<T> GetPage<TKey>(int itemsPerPage, int currentPage, Expression<Func<T, TKey>> orderByExpression);

        IQueryable<T> Where(Expression<Func<T, bool>> predicate);

        void Remove(T entity);
    }
}