using Domain.Models;
using MediatR;
using System;

namespace Common.Events.Books
{
    public class BookRemoved : Event, INotification
    {
        public BookRemoved(Book book, Guid userId)
            : base(userId)
        {
            Book = book ?? throw new ArgumentNullException(nameof(book));
        }

        public Book Book { get; }
    }
}