using Domain.Models;
using MediatR;
using System;

namespace Common.Commands.Authors
{
    public class GetAuthor : Command, IRequest<Author>
    {
        public GetAuthor(Guid id, Guid userId)
            : base(userId)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}