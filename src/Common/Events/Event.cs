using MediatR;
using System;

namespace Common.Events
{
    public abstract class Event : INotification
    {
        public Event(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}