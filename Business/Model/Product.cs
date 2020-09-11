using System.Collections.Generic;

namespace Business.Model
{
    public class Product : Entity
    {
        public string Description { get; set; }
        public string NoteDescription { get; set; }
        public string ExternalId { get; set; }
        public string MeasuredUnit { get;set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public string BarCode { get; set; }
        public bool Active { get; set; }
        public IList<ProductSale> ProductSales { get; set; }
    }
}
