namespace Libro.DAL.Configurations
{
    public class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(x => x.Name)
                    .HasMaxLength(100);

            builder.Property(x => x.CreatedOn).HasDefaultValueSql("GETUTCDATE()");

            builder.HasIndex(x => x.Name)
                    .IsUnique();
        }
    }
}
