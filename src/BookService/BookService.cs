using Common.Attributes;
using Common.Commands.Authors;
using Common.Commands.Books;
using Common.Commands.Database;
using Common.Events.Books;
using Common.Exceptions;
using Domain.Handlers;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookService
{
    [Service]
    class BookService : IRequestHandler<GetBooks, IEnumerable<Book>>,
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

        public async Task<IEnumerable<Book>> Handle(GetBooks request, CancellationToken cancellationToken) =>
            await BookEntityHandler.Get();

        public async Task<Book> Handle(GetBook request, CancellationToken cancellationToken) =>
            await BookEntityHandler.Get(request.Id);

        public async Task Handle(AddBook message, CancellationToken cancellationToken)
        {
            await CheckAndAddAuthors(message.Book, message.UserId);

            BookEntityHandler.Add(message.Book);

            await Mediator.Send(new SaveChanges());

            await Mediator.Publish(new BookAdded(message.Book, message.UserId));
        }

        public async Task Handle(UpdateBook message, CancellationToken cancellationToken)
        {
            var oldBook = await BookEntityHandler.Get(message.Book.Id);
            if (oldBook == null)
                throw new EntityNotFoundException<Book>(message.Book);

            await CheckAndAddAuthors(message.Book, message.UserId);

            var newBook = oldBook.Update(message.Book, DateTime.UtcNow);
            BookEntityHandler.Update(newBook);

            await Mediator.Send(new SaveChanges());

            await Mediator.Publish(new BookUpdated(newBook, oldBook, message.UserId));
        }

        public async Task Handle(DeleteBook message, CancellationToken cancellationToken)
        {
            var oldBook = await BookEntityHandler.Get(message.Id);
            if (oldBook == null)
                throw new EntityNotFoundException<Book>(message.Id);

            BookEntityHandler.Delete(oldBook);

            await Mediator.Send(new SaveChanges());

            await Mediator.Publish(new BookRemoved(oldBook, message.UserId));
        }

        async Task CheckAndAddAuthors(Book book, Guid userId)
        {
            foreach (var author in book.Authors)
                await CheckAndAddAuthor(author, userId);
        }

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