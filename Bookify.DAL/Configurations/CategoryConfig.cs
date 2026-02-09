using Bookify.DAL.Entities;

namespace Bookify.DAL.Configurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name)
                    .HasMaxLength(100);

            builder.Property(x => x.CreatedOn).HasDefaultValueSql("GETUTCDATE()");

            builder.HasIndex(x => x.Name)
                    .IsUnique();
        }
    }
}
