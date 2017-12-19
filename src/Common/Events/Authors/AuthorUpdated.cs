using Domain.Models;
using System;

namespace Common.Events.Authors
{
    public class AuthorUpdated : Event
    {
        public AuthorUpdated(Author newAuthor, Author oldAuthor, Guid userId)
            : base(userId)
        {
            NewAuthor = newAuthor ?? throw new ArgumentNullException(nameof(newAuthor));
            OldAuthor = oldAuthor ?? throw new ArgumentNullException(nameof(oldAuthor));
        }

        public Author NewAuthor { get; }
        public Author OldAuthor { get; }
    }
}