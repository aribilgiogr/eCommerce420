using Core.Abstracts.IRepositories;
using Core.Concretes.Entities;
using Data.Contexts;
using Tools.Helpers;

namespace Data.Repositories
{
    public class ProductFeatureRepository : Repository<ProductFeature>, IProductFeatureRepository
    {
        public ProductFeatureRepository(ShopContext context) : base(context)
        {
        }
    }
}
