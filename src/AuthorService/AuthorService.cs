using Common.Attributes;
using Common.Commands.Authors;
using Common.Commands.Database;
using Common.Events.Authors;
using Common.Exceptions;
using Domain.Handlers;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AuthorService
{
    [Service]
    class AuthorService : IRequestHandler<GetAuthors, IEnumerable<Author>>,
        IRequestHandler<GetAuthor, Author>,
        IRequestHandler<AddAuthor>,
        IRequestHandler<UpdateAuthor>
    {
        public AuthorService(IEntityHandler<Author> authorEntityHandler, IMediator mediator)
        {
            AuthorEntityHandler = authorEntityHandler ?? throw new ArgumentNullException(nameof(authorEntityHandler));
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        IEntityHandler<Author> AuthorEntityHandler { get; }
        IMediator Mediator { get; }

        public async Task<IEnumerable<Author>> Handle(GetAuthors request, CancellationToken cancellationToken) =>
            await AuthorEntityHandler.Get();

        public async Task<Author> Handle(GetAuthor request, CancellationToken cancellationToken) =>
            await AuthorEntityHandler.Get(request.Id);

        public async Task Handle(AddAuthor message, CancellationToken cancellationToken)
        {
            AuthorEntityHandler.Add(message.Author);

            await Mediator.Send(new SaveChanges());

            await Mediator.Publish(new AuthorAdded(message.Author, message.UserId));
        }

        public async Task Handle(UpdateAuthor message, CancellationToken cancellationToken)
        {
            var oldAuthor = await AuthorEntityHandler.Get(message.Author.Id);
            if (oldAuthor == null)
                throw new EntityNotFoundException<Author>(message.Author);

            var newAuthor = oldAuthor.Update(message.Author, DateTime.UtcNow);
            AuthorEntityHandler.Update(newAuthor);

            await Mediator.Send(new SaveChanges());

            await Mediator.Publish(new AuthorUpdated(newAuthor, oldAuthor, message.UserId));
        }
    }
}