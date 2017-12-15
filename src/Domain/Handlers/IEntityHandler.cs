using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public interface IEntityHandler<T> where T : Base
    {
        Task<IEnumerable<T>> Get();
        Task<IEnumerable<T>> Get(Func<T, bool> query);
        Task<T> Get(Guid id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
    }
}