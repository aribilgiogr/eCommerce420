using Core.Abstracts.IRepositories;
using Core.Concretes.Entities;
using Data.Contexts;
using Tools.Helpers;

namespace Data.Repositories
{
    public class SubcategoryRepository : Repository<Subcategory>, ISubcategoryRepository
    {
        public SubcategoryRepository(ShopContext context) : base(context)
        {
        }
    }
}
