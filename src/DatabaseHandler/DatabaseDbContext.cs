using Common;
using DatabaseHandler.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DatabaseHandler
{
    public class DatabaseDbContext : DbContext
    {
        public DatabaseDbContext(IConfiguration configuration) :
            base(new DbContextOptions<DatabaseDbContext>())
        {
            ConnectionString = configuration.Value("Database", "ConnectionString");
            Database.EnsureCreated();
        }

        internal DbSet<Author> Authors { get; set; }
        internal DbSet<Book> Books { get; set; }
        internal DbSet<BookAuthor> BookAuthors { get; set; }

        string ConnectionString { get; }

        internal void Reattach<T>(T entity) where T : Base
        {
            if (Entry(entity).State != EntityState.Detached)
                return;

            var oldEntity = ChangeTracker
                .Entries<T>()
                .SingleOrDefault(x => x.Entity.Id == entity.Id);
            if (oldEntity != null)
                oldEntity.State = EntityState.Detached;

            Attach(entity);
            Entry(entity).State = EntityState.Modified;
        }

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
            modelBuilder.Entity<Author>()
                .Property(x => x.Name)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Book>()
                .Property(x => x.Name)
                .IsRequired();

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