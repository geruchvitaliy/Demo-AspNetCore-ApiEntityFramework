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
            BookAuthors = new List<BookAuthor>();
        }

        public Book(Guid id,
            string name,
            int numberOfPages,
            DateTime? dateOfPublication,
            DateTime createDate,
            IEnumerable<Author> authors = null)
            : base(id, createDate, null)
        {
            Name = name;
            NumberOfPages = numberOfPages;
            DateOfPublication = dateOfPublication;
            BookAuthors = authors != null ? authors.Select(x => new BookAuthor(id, x.Id, null, x)).ToList() : new List<BookAuthor>();
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

        public void Update(Book book, DateTime date)
        {
            Name = book.Name;
            NumberOfPages = book.NumberOfPages;
            DateOfPublication = book.DateOfPublication;
            BookAuthors = book.Authors != null ? book.Authors.Select(x => new BookAuthor(Id, x.Id, null, x)).ToList() : new List<BookAuthor>();
            UpdateDate = date;
        }
    }
}