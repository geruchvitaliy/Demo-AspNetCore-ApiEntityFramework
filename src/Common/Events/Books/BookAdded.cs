using Domain.Models;
using MediatR;
using System;

namespace Common.Events.Books
{
    public class BookAdded : Event, INotification
    {
        public BookAdded(Book book, Guid userId)
            : base(userId)
        {
            Book = book ?? throw new ArgumentNullException(nameof(book));
        }

        public Book Book { get; }
    }
}