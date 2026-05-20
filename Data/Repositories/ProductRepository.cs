using Core.Abstracts.IRepositories;
using Core.Concretes.Entities;
using Data.Contexts;
using Tools.Helpers;

namespace Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ShopContext context) : base(context)
        {
        }
    }
}
