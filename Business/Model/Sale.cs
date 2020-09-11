using Business.Enums;
using System.Collections.Generic;

namespace Business.Model
{
    public class Sale : Entity
    {
        public decimal TotalValue { get; set; }
        public EFormOfPayment FormOfPayment { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Change { get; set; }
        public IList<ProductSale> ProductSales { get; set; }
    }
}
