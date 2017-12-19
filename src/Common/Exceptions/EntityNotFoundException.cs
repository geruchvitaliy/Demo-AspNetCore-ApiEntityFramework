using Domain.Models;
using System;

namespace Common.Exceptions
{
    public sealed class EntityNotFoundException<T> : Exception where T : Base
    {
        public EntityNotFoundException()
            : base($"Entity '{typeof(T).Name}' is not found")
        {
        }

        public EntityNotFoundException(Guid id)
            : base($"Entity '{typeof(T).Name}' with Id '{id}' is not found")
        {
        }

        public EntityNotFoundException(T entity)
            : this(entity.Id)
        {
        }
    }
}