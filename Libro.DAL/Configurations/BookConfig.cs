namespace Libro.DAL.Configurations
{
    internal class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.Title).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Publisher).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Hall).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(5000);

            //builder.HasMany(b => b.Categories)
            //        .WithOne(bc => bc.Book)
            //        .HasForeignKey(bc => bc.BookId);

            //builder.Navigation(x => x.Categories).UsePropertyAccessMode(PropertyAccessMode.Field);
            //builder.Metadata.FindNavigation(nameof(Book.Categories))!.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasIndex(x => new { x.Title, x.AuthorId })
                    .IsUnique();
        }
    }
}
