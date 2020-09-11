using Business.Interfaces.Repository;
using Business.Model;
using Data.Context;

namespace Data.Repository
{
    public class ProductSaleRepository : Repository<ProductSale>, IProductSaleRepository
    {
        public ProductSaleRepository(MyDbContext context) : base(context) { }
    }
}
