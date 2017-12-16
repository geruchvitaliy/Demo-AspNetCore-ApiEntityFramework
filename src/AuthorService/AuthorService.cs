using Common.Commands.Authors;
using Domain.Handlers;
using Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AuthorService
{
    public class AuthorService : IRequestHandler<GetAuthor, Author>,
        IRequestHandler<AddAuthor>
    {
        public AuthorService(IEntityHandler<Author> authorEntityHandler)
        {
            AuthorEntityHandler = authorEntityHandler ?? throw new ArgumentNullException(nameof(authorEntityHandler));
        }

        public IEntityHandler<Author> AuthorEntityHandler { get; }

        public async Task<Author> Handle(GetAuthor request, CancellationToken cancellationToken) =>
            await AuthorEntityHandler.Get(request.Id);

        public async Task Handle(AddAuthor message, CancellationToken cancellationToken)
        {
            AuthorEntityHandler.Add(message.Author);
            await AuthorEntityHandler.Save();
        }
    }
}