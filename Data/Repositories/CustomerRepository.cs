using Core.Abstracts.IRepositories;
using Core.Concretes.Entities;
using Data.Contexts;
using Tools.Helpers;

namespace Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ShopContext context) : base(context)
        {
        }
    }
}
