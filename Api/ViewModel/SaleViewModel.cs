using Business.Enums;
using System;
using System.Collections.Generic;

namespace Api.ViewModel
{
    public class SaleViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateRegister { get; set; }
        public decimal TotalValue { get; set; }
        public EFormOfPayment FormOfPayment { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Change { get; set; }
        public IList<ProductSaleViewModel> ProductSales { get; set; }
    }
}
