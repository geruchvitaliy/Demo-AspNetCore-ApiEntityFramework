using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public abstract class Base
    {
        public Base()
        { }

        public Base(Guid id,
            DateTime createDate,
            DateTime? updateDate)
            : this()
        {
            Id = id;
            CreateDate = createDate;
            UpdateDate = updateDate;
            IsActive = true;
        }

        [Key]
        public Guid Id { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool IsActive { get; protected set; }

        public void Remove() =>
            IsActive = false;
    }
}