using Common.Attributes;
using Common.Commands.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseHandler.Handlers
{
    [Service]
    class DatabaseHandler : IRequestHandler<SaveChanges, bool>
    {
        public DatabaseHandler(DatabaseDbContext dbContext)
        {
            DbContext = dbContext;
        }

        DatabaseDbContext DbContext { get; }

        public async Task<bool> Handle(SaveChanges request, CancellationToken cancellationToken) =>
            await DbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}