using System;

namespace Common.Events
{
    public abstract class Event
    {
        public Event(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}