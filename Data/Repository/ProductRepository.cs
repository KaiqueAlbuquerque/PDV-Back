using Business.Interfaces.Repository;
using Business.Model;
using Data.Context;

namespace Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MyDbContext context) : base(context) { }
    }
}
