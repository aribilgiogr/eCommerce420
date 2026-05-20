using Core.Abstracts.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Responses;

namespace Core.Abstracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICustomerRepository Customers { get; }
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        ISubcategoryRepository Subcategories { get; }
        IBrandRepository Brands { get; }
        ICartRepository Carts { get; }
        ICartItemRepository CartItems { get; }
        IOrderRepository Orders { get; }
        IOrderItemRepository OrderItems { get; }

        Task<Reply> CommitAsync();
    }
}
