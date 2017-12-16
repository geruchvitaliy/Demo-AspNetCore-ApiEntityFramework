using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Models
{
    public class Book : Base
    {
        public Book()
        {
            BookAuthors = Array.Empty<BookAuthor>();
        }

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
            BookAuthors = authors != null ? authors.Select(x => new BookAuthor(id, x.Id, null, x)) : Array.Empty<BookAuthor>();
        }

        [Required]
        public string Name { get; private set; }
        public int NumberOfPages { get; private set; }
        public DateTime? DateOfPublication { get; private set; }
        [JsonIgnore]
        public IEnumerable<BookAuthor> BookAuthors { get; private set; }
        public IEnumerable<Author> Authors => BookAuthors
            .Select(x => x.Author)
            .Where(x => x.IsActive);

        public Book Update(Book book, DateTime date) =>
            new Book(Id, book.Name, book.NumberOfPages, book.DateOfPublication, CreateDate, date, book.Authors)
            {
                IsActive = IsActive
            };
    }
}