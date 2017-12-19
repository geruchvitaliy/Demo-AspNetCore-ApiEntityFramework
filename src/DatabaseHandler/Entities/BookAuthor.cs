using System;

namespace DatabaseHandler.Entities
{
    class BookAuthor
    {
        public Guid BookId { get; set; }
        public virtual Book Book { get; set; }
        public Guid AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}