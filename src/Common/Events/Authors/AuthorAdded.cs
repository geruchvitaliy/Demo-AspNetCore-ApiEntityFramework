using Domain.Models;
using System;

namespace Common.Events.Authors
{
    public class AuthorAdded : Event
    {
        public AuthorAdded(Author author, Guid userId)
            : base(userId)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
        }

        public Author Author { get; }
    }
}