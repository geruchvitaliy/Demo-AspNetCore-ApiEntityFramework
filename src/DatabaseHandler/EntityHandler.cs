using Domain.Handlers;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseHandler
{
    public class EntityHandler<T> : IEntityHandler<T> where T : Base
    {
        public EntityHandler(DatabaseDbContext dbContext)
        {
            DbContext = dbContext;
        }

        DatabaseDbContext DbContext { get; }
        DbSet<T> Set => DbContext.Set<T>();

        public async Task<IEnumerable<T>> Get() =>
            await Task.Run(() => (IEnumerable<T>)Set.Where(x => x.IsActive).ToArray());

        public async Task<IEnumerable<T>> Get(Func<T, bool> query) =>
            await Task.Run(() => (IEnumerable<T>)Set.Where(x => x.IsActive).Where(query).ToArray());

        public async Task<T> Get(Guid id) =>
            await Set.FindAsync(id);

        public void Add(T entity) =>
            Set.Add(entity);

        public void Update(T entity) =>
            Set.Attach(entity);

        public void Delete(Guid id) =>
            Set.Find(id).Remove();

        public async Task Save() =>
           await DbContext.SaveChangesAsync();
    }
}