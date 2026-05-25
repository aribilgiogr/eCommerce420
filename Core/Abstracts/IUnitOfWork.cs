using Core.Abstracts.IRepositories;
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

        IProductImageRepository ProductImages { get; }
        IProductFeatureRepository ProductFeatures { get; }

        Task<Reply> CommitAsync();
    }
}
