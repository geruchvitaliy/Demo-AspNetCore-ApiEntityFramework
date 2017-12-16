using System;

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

        public Guid Id { get; private set; }
        public DateTime CreateDate { get; protected set; }
        public DateTime? UpdateDate { get; protected set; }
        public bool IsActive { get; protected set; }

        public void Activate() =>
            IsActive = true;

        public void Remove() =>
            IsActive = false;
    }
}