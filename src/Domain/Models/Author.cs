using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Author : Base
    {
        public Author()
        {
            BookAuthors = new List<BookAuthor>();
        }

        public Author(Guid id,
                string name,
                DateTime createDate)
            : base(id, createDate, null)
        {
            Name = name;
            BookAuthors = new List<BookAuthor>();
        }

        [Required]
        public string Name { get; private set; }
        [JsonIgnore]
        public IEnumerable<BookAuthor> BookAuthors { get; private set; }

        public void Update(Author author, DateTime date)
        {
            Name = author.Name;
            UpdateDate = date;
        }
    }
}