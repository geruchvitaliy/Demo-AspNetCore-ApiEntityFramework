using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace Common.Commands.Books
{
    public class GetBooks : Command, IRequest<IEnumerable<Book>>
    {
        public GetBooks(Guid userId)
            : base(userId)
        { }
    }
}