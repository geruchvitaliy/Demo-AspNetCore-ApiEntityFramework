using Domain.Models;
using System;

namespace Common.Events.Books
{
    public class BookAdded : Event
    {
        public BookAdded(Book book, Guid userId)
            : base(userId)
        {
            Book = book ?? throw new ArgumentNullException(nameof(book));
        }

        public Book Book { get; }
    }
}