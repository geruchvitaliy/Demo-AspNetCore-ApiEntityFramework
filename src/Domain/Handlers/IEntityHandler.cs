using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public interface IEntityHandler<T> where T : Base
    {
        Task<IEnumerable<T>> Get();
        Task<IEnumerable<T>> Get<TProperty>(Expression<Func<T, TProperty>> includePropertyPath);
        Task<IEnumerable<T>> Get(Func<T, bool> query);
        Task<IEnumerable<T>> Get<TProperty>(Func<T, bool> query, Expression<Func<T, TProperty>> includePropertyPath);
        Task<T> Get(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
        Task Save();
    }
}