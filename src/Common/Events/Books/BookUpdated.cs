using Domain.Models;
using MediatR;
using System;

namespace Common.Events.Books
{
    public class BookUpdated : Event, INotification
    {
        public BookUpdated(Book newBook, Book oldBook, Guid userId)
            : base(userId)
        {
            NewBook = newBook ?? throw new ArgumentNullException(nameof(newBook));
            OldBook = oldBook ?? throw new ArgumentNullException(nameof(oldBook));
        }

        public Book NewBook { get; }
        public Book OldBook { get; }
    }
}