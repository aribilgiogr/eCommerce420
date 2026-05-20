using Core.Abstracts.IRepositories;
using Core.Concretes.Entities;
using Data.Contexts;
using Tools.Helpers;

namespace Data.Repositories
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ShopContext context) : base(context)
        {
        }
    }
}
