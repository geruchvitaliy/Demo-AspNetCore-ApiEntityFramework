using Domain.Models;
using MediatR;
using System;

namespace Common.Commands.Authors
{
    public class UpdateAuthor : Command, IRequest
    {
        public UpdateAuthor(Author author, Guid userId)
            : base(userId)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
        }

        public Author Author { get; }
    }
}