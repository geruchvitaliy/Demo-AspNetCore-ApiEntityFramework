using Common.Commands.Authors;
using Domain.Handlers;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AuthorService
{
    public class AuthorService : IRequestHandler<GetAuthors, IEnumerable<Author>>,
        IRequestHandler<GetAuthor, Author>,
        IRequestHandler<AddAuthor>,
        IRequestHandler<UpdateAuthor>
    {
        public AuthorService(IEntityHandler<Author> authorEntityHandler)
        {
            AuthorEntityHandler = authorEntityHandler ?? throw new ArgumentNullException(nameof(authorEntityHandler));
        }

        IEntityHandler<Author> AuthorEntityHandler { get; }

        public async Task<IEnumerable<Author>> Handle(GetAuthors request, CancellationToken cancellationToken) =>
            await AuthorEntityHandler.Get();

        public async Task<Author> Handle(GetAuthor request, CancellationToken cancellationToken) =>
            await AuthorEntityHandler.Get(request.Id);

        public async Task Handle(AddAuthor message, CancellationToken cancellationToken)
        {
            AuthorEntityHandler.Add(message.Author);
            await AuthorEntityHandler.Save();
        }

        public async Task Handle(UpdateAuthor message, CancellationToken cancellationToken)
        {
            AuthorEntityHandler.Update(message.Author);
            await AuthorEntityHandler.Save();
        }
    }
}