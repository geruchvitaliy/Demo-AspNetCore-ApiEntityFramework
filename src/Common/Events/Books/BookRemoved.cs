using Domain.Models;
using System;

namespace Common.Events.Books
{
    public class BookRemoved : Event
    {
        public BookRemoved(Book book, Guid userId)
            : base(userId)
        {
            Book = book ?? throw new ArgumentNullException(nameof(book));
        }

        public Book Book { get; }
    }
}