using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Author : Base
    {
        public Author(Guid id,
                string name,
                DateTime createDate,
                DateTime? updateDate = null)
            : base(id, createDate, updateDate)
        {
            Name = name;
        }

        [Required]
        public string Name { get; }

        protected override IEnumerable<object> EqualityCheckAttributes =>
            new object[] { Id, CreateDate, UpdateDate, Name };

        public Author Update(Author author, DateTime date) =>
            new Author(Id, author.Name, CreateDate, date);
    }
}