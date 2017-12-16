using System;

namespace Domain.Models
{
    public class BookAuthor
    {
        public BookAuthor()
        { }

        public BookAuthor(Guid bookId,
            Guid authorId,
            Book book = null,
            Author author = null)
        {
            BookId = bookId;
            AuthorId = authorId;
            Book = book;
            Author = author;
        }

        public Guid BookId { get; private set; }
        public Book Book { get; private set; }
        public Guid AuthorId { get; private set; }
        public Author Author { get; private set; }
    }
}