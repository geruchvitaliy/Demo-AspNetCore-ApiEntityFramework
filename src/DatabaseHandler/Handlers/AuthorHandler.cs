﻿using DatabaseHandler.Entities;
using Domain.Handlers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Author = Domain.Models.Author;

namespace DatabaseHandler.Handlers
{
    public class AuthorHandler : IEntityHandler<Author>
    {
        public AuthorHandler(DatabaseDbContext dbContext)
        {
            DbContext = dbContext;
        }

        DatabaseDbContext DbContext { get; }

        public void Add(Author model) =>
            DbContext.Authors.Add(model.ToEntity());

        public void Delete(Author model)
        {
            var entity = model.ToEntity();
            entity.IsActive = false;

            DbContext.Authors.Attach(entity);
        }

        public async Task<IEnumerable<Author>> Get() =>
            await DbContext.Authors
                .Where(x => x.IsActive)
                .Select(x => x.ToModel())
                .AsNoTracking()
                .ToArrayAsync();

        public async Task<Author> Get(Guid id)
        {
            var entity = await DbContext.Authors
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
            if (entity == null || !entity.IsActive)
                return null;

            return entity.ToModel();
        }

        public void Update(Author model) =>
            DbContext.Authors.Attach(model.ToEntity());
    }
}