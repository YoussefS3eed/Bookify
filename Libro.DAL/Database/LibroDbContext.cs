using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace Libro.DAL.Database
{
    public class LibroDbContext : IdentityDbContext
    {
        public LibroDbContext(DbContextOptions<LibroDbContext> options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("SerialNumber", schema: "shared")
                .StartsAt(1000001);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
