using Business.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ExternalId)
                .IsRequired();

            builder.Property(p => p.Description)
                .IsRequired();

            builder.Property(p => p.NoteDescription)
                .HasColumnType("varchar(17)");

            builder.Property(p => p.Quantity);

            builder.Property(p => p.Value)
                .HasColumnType("decimal(65,2)")
                .IsRequired();

            builder.Property(p => p.BarCode)
                .IsRequired();

            builder.Property(p => p.MeasuredUnit)
                .IsRequired();

            builder.Property(p => p.Active)
                .IsRequired();

            builder.ToTable("Products");
        }
    }
}
