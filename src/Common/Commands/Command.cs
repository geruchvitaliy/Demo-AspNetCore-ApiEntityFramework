using System;

namespace Common.Commands
{
    public abstract class Command
    {
        public Command(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}