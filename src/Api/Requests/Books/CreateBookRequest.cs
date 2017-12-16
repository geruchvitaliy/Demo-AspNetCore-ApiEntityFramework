using Domain;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Api.Requests.Books
{
    public class CreateBookRequest : ValueObject<CreateBookRequest>
    {
        public static CreateBookRequest Null =>
            new CreateBookRequest();

        public static CreateBookRequest Empty =>
            new CreateBookRequest
            {
                Name = "No Name"
            };

        [Required]
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public DateTime? DateOfPublication { get; set; }
        public IEnumerable<string> Authors { get; set; }

        protected override IEnumerable<object> EqualityCheckAttributes =>
            new object[] { Name, NumberOfPages, DateOfPublication, Authors };

        public Book ToBook(Guid id) =>
            new Book(id, Name, NumberOfPages, DateOfPublication, DateTime.UtcNow, Authors?.Select(x => new Author(Guid.NewGuid(), x, DateTime.UtcNow)));
    }
}