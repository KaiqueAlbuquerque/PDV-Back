using System;

namespace Api.ViewModel
{
    public class ProductSaleViewModel
    {
        public Guid SaleId { get; set; }
        public SaleViewModel Sale { get; set; }
        public Guid ProductId { get; set; }
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
