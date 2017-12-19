using Domain.Models;
using MediatR;
using System;

namespace Common.Events.Authors
{
    public class AuthorAdded : Event, INotification
    {
        public AuthorAdded(Author author, Guid userId)
            : base(userId)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
        }

        public Author Author { get; }
    }
}