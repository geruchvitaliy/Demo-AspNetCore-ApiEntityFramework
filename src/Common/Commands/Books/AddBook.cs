using Domain.Models;
using MediatR;
using System;

namespace Common.Commands.Books
{
    public class AddBook : Command, IRequest
    {
        public AddBook(Book book, Guid userId)
            : base(userId)
        {
            Book = book ?? throw new ArgumentNullException(nameof(book));
        }

        public Book Book { get; }
    }
}