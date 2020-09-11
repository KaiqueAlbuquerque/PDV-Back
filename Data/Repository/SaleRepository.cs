using Business.Interfaces.Repository;
using Business.Model;
using Data.Context;

namespace Data.Repository
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public SaleRepository(MyDbContext context) : base(context) { }
    }
}
