using System;

namespace Business.Model
{
    public class ProductSale
    {
        public Guid SaleId { get; set; }
        public Sale Sale { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}