using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace Common.Commands.Authors
{
    public class GetAuthors : Command, IRequest<IEnumerable<Author>>
    {
        public GetAuthors(Guid userId)
            : base(userId)
        { }
    }
}