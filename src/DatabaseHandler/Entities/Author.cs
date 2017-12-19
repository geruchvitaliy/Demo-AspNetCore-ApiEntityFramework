using System.Collections.Generic;
using System.Linq;

namespace DatabaseHandler.Entities
{
    class Author : Base
    {
        public Author()
        {
            BookAuthors = new List<BookAuthor>();
        }

        public string Name { get; set; }
        public virtual IEnumerable<BookAuthor> BookAuthors { get; set; }
    }

    static class AuthorExtensions
    {
        public static Domain.Models.Author ToModel(this Author author) =>
            new Domain.Models.Author(author.Id,
                author.Name,
                author.CreateDate,
                author.UpdateDate);

        public static IEnumerable<Domain.Models.Author> ToModels(this IEnumerable<Author> authors) =>
            authors.Select(ToModel);

        public static Author ToEntity(this Domain.Models.Author author, bool isActive = true) =>
            new Author
            {
                Id = author.Id,
                CreateDate = author.CreateDate,
                UpdateDate = author.UpdateDate,
                IsActive = isActive,
                Name = author.Name
            };
    }
}