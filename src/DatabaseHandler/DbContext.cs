using Common;
using Domain.Models;
using System.Data.Entity;

namespace DatabaseHandler
{
    public class DatabaseDbContext : DbContext
    {
        public DatabaseDbContext(Configuration configuration) :
            base(configuration.Database.ConnectionString)
        { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}