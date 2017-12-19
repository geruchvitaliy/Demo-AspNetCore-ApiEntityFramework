using DatabaseHandler.Entities;
using Domain.Handlers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book = Domain.Models.Book;

namespace DatabaseHandler.Handlers
{
    public class BookHandler : IEntityHandler<Book>
    {
        public BookHandler(DatabaseDbContext dbContext)
        {
            DbContext = dbContext;
        }

        DatabaseDbContext DbContext { get; }

        public void Add(Book model)
        {
            AddOrRemoveBookAuthors(model);
            DbContext.Books.Add(model.ToEntity());
        }

        public void Delete(Book model)
        {
            var entity = model.ToEntity();
            entity.IsActive = false;

            DbContext.Books.Attach(entity);
        }

        public async Task<IEnumerable<Book>> Get() =>
            await DbContext.Books
                .Include(x => x.BookAuthors)
                .ThenInclude(x => x.Author)
                .Where(x => x.IsActive)
                .Select(x => x.ToModel())
                .AsNoTracking()
                .ToArrayAsync();

        public async Task<Book> Get(Guid id)
        {
            var entity = await DbContext.Books
                .Include(x => x.BookAuthors)
                .ThenInclude(x => x.Author)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
            if (entity == null || !entity.IsActive)
                return null;

            return entity.ToModel();
        }

        public void Update(Book model)
        {
            AddOrRemoveBookAuthors(model);
            DbContext.Books.Attach(model.ToEntity());
        }

        void AddOrRemoveBookAuthors(Book model)
        {
            var bookAuthors = DbContext.BookAuthors
                .Where(x => x.BookId == model.Id)
                .ToArray();

            var newBookAuthors = model.Authors
                .Where(x => !bookAuthors.Select(ba => ba.AuthorId).Contains(x.Id))
                .ToArray();
            foreach (var author in newBookAuthors)
            {
                var bookAuthorEntity = new BookAuthor
                {
                    AuthorId = author.Id,
                    BookId = model.Id
                };
                DbContext.BookAuthors.Add(bookAuthorEntity);
            }

            var oldBookAuthors = bookAuthors
                .Where(x => !model.Authors.Select(a => a.Id).Contains(x.AuthorId))
                .ToArray();
            foreach (var author in oldBookAuthors)
                DbContext.BookAuthors.Remove(author);
        }
    }
}