using System;

namespace Domain.Models
{
    public abstract class Base : ValueObject<Base>
    {
        public Base()
        { }

        public Base(Guid id,
            DateTime createDate,
            DateTime? updateDate = null)
            : this()
        {
            Id = id;
            CreateDate = createDate;
            UpdateDate = updateDate;
        }

        public Guid Id { get; }
        public DateTime CreateDate { get; }
        public DateTime? UpdateDate { get; }
    }
}