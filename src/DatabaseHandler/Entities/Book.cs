using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseHandler.Entities
{
    class Book : Base
    {
        public Book()
        {
            BookAuthors = new List<BookAuthor>();
        }

        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public DateTime? DateOfPublication { get; set; }
        public virtual IEnumerable<BookAuthor> BookAuthors { get; set; }
    }

    static class BookExtensions
    {
        public static Domain.Models.Book ToModel(this Book book) =>
            new Domain.Models.Book(book.Id,
                book.Name,
                book.NumberOfPages,
                book.DateOfPublication,
                book.CreateDate,
                book.UpdateDate,
                book.BookAuthors.Where(a => a.Author.IsActive).Select(a => a.Author.ToModel()));

        public static IEnumerable<Domain.Models.Book> ToModels(this IEnumerable<Book> book) =>
            book.Select(ToModel);

        public static Book ToEntity(this Domain.Models.Book book, bool isActive = true) =>
            new Book
            {
                Id = book.Id,
                CreateDate = book.CreateDate,
                UpdateDate = book.UpdateDate,
                IsActive = isActive,
                Name = book.Name,
                NumberOfPages = book.NumberOfPages,
                DateOfPublication = book.DateOfPublication
            };
    }
}