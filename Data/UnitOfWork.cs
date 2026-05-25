using Core.Abstracts;
using Core.Abstracts.IRepositories;
using Data.Contexts;
using Data.Repositories;
using Tools.Responses;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopContext context;

        public UnitOfWork(ShopContext context)
        {
            this.context = context;
        }

        private ICustomerRepository? customers;
        public ICustomerRepository Customers => customers ??= new CustomerRepository(context);


        private IProductRepository? products;
        public IProductRepository Products => products ??= new ProductRepository(context);


        private ICategoryRepository? categories;
        public ICategoryRepository Categories => categories ??= new CategoryRepository(context);


        private ISubcategoryRepository? subcategories;
        public ISubcategoryRepository Subcategories => subcategories ??= new SubcategoryRepository(context);


        private IBrandRepository? brands;
        public IBrandRepository Brands => brands ??= new BrandRepository(context);


        private ICartRepository? carts;
        public ICartRepository Carts => carts ??= new CartRepository(context);


        private ICartItemRepository? cartItems;
        public ICartItemRepository CartItems => cartItems ??= new CartItemRepository(context);


        private IOrderRepository? orders;
        public IOrderRepository Orders => orders ??= new OrderRepository(context);


        private IOrderItemRepository? orderItems;
        public IOrderItemRepository OrderItems => orderItems ??= new OrderItemRepository(context);


        private IProductImageRepository? productImages;
        public IProductImageRepository ProductImages => productImages ??= new ProductImageRepository(context);


        private IProductFeatureRepository? productFeatures;
        public IProductFeatureRepository ProductFeatures => productFeatures ??= new ProductFeatureRepository(context);


        public async Task<Reply> CommitAsync()
        {
            try
            {
                await context.SaveChangesAsync();
                return Reply.Success();
            }
            catch (Exception ex)
            {
                return Reply.Fail(ex.Message);
            }
        }

        public async ValueTask DisposeAsync() => await context.DisposeAsync();
    }
}
