using Core.Abstracts.IRepositories;
using Core.Concretes.Entities;
using Data.Contexts;
using Tools.Helpers;

namespace Data.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(ShopContext context) : base(context)
        {
        }
    }
}
