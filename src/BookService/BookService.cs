﻿using Common.Commands.Authors;
using Common.Commands.Books;
using Domain.Handlers;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookService
{
    public class BookService : IRequestHandler<GetBooks, IEnumerable<Book>>,
        IRequestHandler<GetBook, Book>,
        IRequestHandler<AddBook>,
        IRequestHandler<UpdateBook>,
        IRequestHandler<DeleteBook>
    {
        public BookService(IEntityHandler<Book> bookEntityHandler, IMediator mediator)
        {
            BookEntityHandler = bookEntityHandler ?? throw new ArgumentNullException(nameof(bookEntityHandler));
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        IEntityHandler<Book> BookEntityHandler { get; }
        IMediator Mediator { get; }

        public async Task<IEnumerable<Book>> Handle(GetBooks request, CancellationToken cancellationToken)
        {
            var books = await BookEntityHandler.Get(x => x.BookAuthors);
            var authors = await Mediator.Send(new GetAuthors(request.UserId));

            return books;
        }

        public async Task<Book> Handle(GetBook request, CancellationToken cancellationToken)
        {
            var book = (await BookEntityHandler.Get(x => x.Id == request.Id, x => x.BookAuthors)).SingleOrDefault();
            if (book == null)
                return null;

            foreach (var ba in book.BookAuthors)
                await Mediator.Send(new GetAuthor(ba.AuthorId, request.UserId));

            return book;
        }

        public async Task Handle(AddBook message, CancellationToken cancellationToken)
        {
            await CheckAndAddAuthors(message.Book, message.UserId);
            BookEntityHandler.Add(message.Book);
            await BookEntityHandler.Save();
        }

        public async Task Handle(UpdateBook message, CancellationToken cancellationToken)
        {
            var existingBook = await BookEntityHandler.Get(message.Book.Id);
            existingBook.Update(message.Book, DateTime.UtcNow);

            await CheckAndAddAuthors(existingBook, message.UserId);
            BookEntityHandler.Update(existingBook);
            await BookEntityHandler.Save();
        }

        public async Task Handle(DeleteBook message, CancellationToken cancellationToken)
        {
            BookEntityHandler.Delete(message.Id);
            await BookEntityHandler.Save();
        }

        async Task CheckAndAddAuthors(Book book, Guid userId) =>
            await Task.WhenAll(book.Authors.Select(a => CheckAndAddAuthor(a, userId)));

        async Task CheckAndAddAuthor(Author author, Guid userId)
        {
            var existingAuthor = await Mediator.Send(new GetAuthor(author.Id, userId));
            if (existingAuthor == null)
                await Mediator.Send(new AddAuthor(author, userId));
            else
            {
                existingAuthor.Update(author, DateTime.UtcNow);
                await Mediator.Send(new UpdateAuthor(existingAuthor, userId));
            }
        }
    }
}