using Business.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class SaleMapping : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.TotalValue)
                .HasColumnType("decimal(65,2)")
                .IsRequired();

            builder.Property(s => s.FormOfPayment)
                .IsRequired();

            builder.Property(s => s.AmountPaid)
                .HasColumnType("decimal(65,2)")
                .IsRequired();

            builder.Property(s => s.Change)
                .HasColumnType("decimal(65,2)")
                .IsRequired();

            builder.ToTable("Sales");
        }
    }
}
