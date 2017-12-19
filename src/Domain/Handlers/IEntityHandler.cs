using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public interface IEntityHandler<T> where T : Base
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(Guid id);
        void Add(T model);
        void Update(T model);
        void Delete(T model);
    }
}