using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Book : Base
    {
        public Book(Guid id,
            string name,
            int numberOfPages,
            DateTime? dateOfPublication,
            DateTime createDate,
            DateTime? updateDate = null,
            IEnumerable<Author> authors = null)
            : base(id, createDate, updateDate)
        {
            Name = name;
            NumberOfPages = numberOfPages;
            DateOfPublication = dateOfPublication;
            Authors = authors ?? Array.Empty<Author>();
        }

        [Required]
        public string Name { get; }
        public int NumberOfPages { get; }
        public DateTime? DateOfPublication { get; }
        public IEnumerable<Author> Authors { get; }

        public Book Update(Book book, DateTime date) =>
            new Book(Id, book.Name, book.NumberOfPages, book.DateOfPublication, CreateDate, date, book.Authors);
    }
}