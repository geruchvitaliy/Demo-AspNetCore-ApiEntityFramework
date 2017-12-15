using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public abstract class Base
    {
        public Base(Guid id,
            DateTime createDate,
            DateTime? updateDate)
        {
            Id = id;
            CreateDate = createDate;
            UpdateDate = updateDate;
        }

        [Key]
        public Guid Id { get; }
        public DateTime CreateDate { get; }
        public DateTime? UpdateDate { get; }
    }
}