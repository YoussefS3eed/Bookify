namespace Libro.DAL.Configurations
{
    public class BookCopyConfig : IEntityTypeConfiguration<BookCopy>
    {
        public void Configure(EntityTypeBuilder<BookCopy> builder)
        {
            builder.Property(x => x.SerialNumber)
                .HasDefaultValueSql("NEXT VALUE FOR shared.SerialNumber");
        }
    }
}
