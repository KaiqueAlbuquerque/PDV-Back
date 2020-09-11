using System;

namespace Api.ViewModel
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateRegister { get; set; }
        public string Description { get; set; }
        public string NoteDescription { get; set; }
        public string ExternalId { get; set; }
        public string MeasuredUnit { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public string BarCode { get; set; }
        public bool Active { get; set; }
    }
}
