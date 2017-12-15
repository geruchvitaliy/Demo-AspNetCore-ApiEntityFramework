using Domain.Models;
using MediatR;
using System;

namespace Common.Commands.Books
{
    public class GetBook : Command, IRequest<Book>
    {
        public GetBook(Guid id, Guid userId)
            : base(userId)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}