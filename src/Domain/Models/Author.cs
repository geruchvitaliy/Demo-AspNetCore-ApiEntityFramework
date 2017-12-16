using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Author : Base
    {
        public Author()
        {
            BookAuthors = Array.Empty<BookAuthor>();
        }

        public Author(Guid id,
                string name,
                DateTime createDate,
                DateTime? updateDate = null)
            : base(id, createDate, updateDate)
        {
            Name = name;
            BookAuthors = Array.Empty<BookAuthor>();
        }

        [Required]
        public string Name { get; private set; }
        public IEnumerable<BookAuthor> BookAuthors { get; private set; }
    }
}