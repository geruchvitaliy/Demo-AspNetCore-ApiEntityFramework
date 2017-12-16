﻿using Domain.Handlers;
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

        public async Task Add(T entity)
        {
            Set.Add(entity);
            await Save();
        }

        public async Task Delete(Guid id)
        {
            var entity = await Set.FindAsync(id);
            entity.Remove();
            await Save();
        }

        public async Task<IEnumerable<T>> Get() =>
            await Task.Run(() => (IEnumerable<T>)Set.Where(x => x.IsActive).ToArray());

        public async Task<IEnumerable<T>> Get(Func<T, bool> query) =>
            await Task.Run(() => (IEnumerable<T>)Set.Where(x => x.IsActive).Where(query).ToArray());

        public Task<T> Get(Guid id) =>
            Set.FindAsync(id);

        public async Task Update(T entity)
        {
            Set.Attach(entity);
            await Save();
        }

        async Task Save() =>
            await DbContext.SaveChangesAsync();
    }
}