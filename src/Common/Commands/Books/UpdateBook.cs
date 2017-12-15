using Domain.Models;
using MediatR;
using System;

namespace Common.Commands.Books
{
    public class UpdateBook : Command, IRequest
    {
        public UpdateBook(Book book, Guid userId)
            : base(userId)
        {
            Book = book;
        }

        public Book Book { get; }
    }
}