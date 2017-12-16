using Common;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHandler
{
    public class DatabaseDbContext : DbContext
    {
        public DatabaseDbContext(IConfiguration configuration, DbContextOptions<DatabaseDbContext> options) :
            base(options)
        {
            ConnectionString = configuration.Value("Database", "ConnectionString");
            Database.EnsureCreated();
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

        string ConnectionString { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Book>()
                .Ignore(x => x.Authors)
                .HasKey(x => x.Id);

            modelBuilder.Entity<BookAuthor>()
                .HasKey(x => new { x.BookId, x.AuthorId });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(x => x.Book)
                .WithMany(x => x.BookAuthors)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(x => x.Author)
                .WithMany(x => x.BookAuthors)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}