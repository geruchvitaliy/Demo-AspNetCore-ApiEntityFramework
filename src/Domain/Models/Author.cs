using System;
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
    }
}