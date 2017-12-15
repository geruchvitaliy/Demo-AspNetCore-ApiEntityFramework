using MediatR;
using System;

namespace Common.Commands.Books
{
    public class DeleteBook : Command, IRequest
    {
        public DeleteBook(Guid id, Guid userId)
            : base(userId)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}