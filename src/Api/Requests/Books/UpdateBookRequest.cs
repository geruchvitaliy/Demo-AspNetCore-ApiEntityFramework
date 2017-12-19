using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Api.Requests.Books
{
    public class UpdateBookRequest
    {
        [Required]
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public DateTime? DateOfPublication { get; set; }
        public IEnumerable<string> Authors { get; set; }

        public Book ToBook(Guid id) =>
            new Book(id, Name, NumberOfPages, DateOfPublication, new DateTime(), DateTime.UtcNow, Authors?.Select(x => new Author(Guid.NewGuid(), x, DateTime.UtcNow)).ToArray());
    }
}