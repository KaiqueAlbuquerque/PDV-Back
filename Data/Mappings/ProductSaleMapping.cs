using Business.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class ProductSaleMapping : IEntityTypeConfiguration<ProductSale>
    {
        public void Configure(EntityTypeBuilder<ProductSale> builder)
        {
            builder.HasKey(ps => new { ps.SaleId, ps.ProductId});

            builder
                .HasOne<Product>(ps => ps.Product)
                .WithMany(p => p.ProductSales)
                .HasForeignKey(ps => ps.ProductId);

            builder
                .HasOne<Sale>(ps => ps.Sale)
                .WithMany(s => s.ProductSales)
                .HasForeignKey(ps => ps.SaleId);

            builder.Property(ps => ps.Quantity)
                .IsRequired();

            builder.Property(ps => ps.Total)
                .HasColumnType("decimal(65,2)")
                .IsRequired();
        }
    }
}
