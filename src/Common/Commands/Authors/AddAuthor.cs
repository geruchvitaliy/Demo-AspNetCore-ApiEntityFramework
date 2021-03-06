﻿using Domain.Models;
using MediatR;
using System;

namespace Common.Commands.Authors
{
    public class AddAuthor : Command, IRequest
    {
        public AddAuthor(Author author, Guid userId)
            : base(userId)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
        }

        public Author Author { get; }
    }
}