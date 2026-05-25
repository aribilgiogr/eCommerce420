using Core.Abstracts;
using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;

namespace Business.Services
{
    public class ShopService : IShopService
    {
        private readonly IUnitOfWork unitOfWork;

        public ShopService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private async Task<Cart> GetCartAsync(int customerId)
        {
            var cart = await unitOfWork.Carts.ReadAsync(x => x.CustomerId == customerId && x.Active, "Items");
            if (cart == null)
            {
                cart = new Cart { CustomerId = customerId };
                await unitOfWork.Carts.CreateAsync(cart);
                var reply = await unitOfWork.CommitAsync();
                if (!reply.IsSuccess)
                {

                    throw new Exception(string.Join(", ", reply.Errors));
                }
            }
            return cart;
        }

        public async Task AddToCart(int customerId, int productId, int quantity = 1)
        {
            var cart = await GetCartAsync(customerId);
            if (cart.Items.Any(x => x.ProductId == productId))
            {
                var item = cart.Items.First(x => x.ProductId == productId);
                item.Quantity += quantity;
                await unitOfWork.CartItems.UpdateAsync(item);
                var reply = await unitOfWork.CommitAsync();
                if (!reply.IsSuccess)
                {
                    throw new Exception(string.Join(", ", reply.Errors));
                }
            }
            else
            {
                var item = new CartItem { CartId = cart.Id, ProductId = productId, Quantity = quantity };
                await unitOfWork.CartItems.CreateAsync(item);
                var reply = await unitOfWork.CommitAsync();
                if (!reply.IsSuccess)
                {
                    throw new Exception(string.Join(", ", reply.Errors));
                }
            }
        }

        public Task DecreasingCart(int customerId, int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductListItemDto>> GetProducts()
        {
            var products = await unitOfWork.Products.ReadManyAsync(x => x.Active, "Subcategory.Category", "Brand", "Images");

            return from p in products
                   select new ProductListItemDto
                   {
                       Id = p.Id,
                       Name = p.Name,
                       BrandId = p.BrandId,
                       BrandName = p.Brand!.Name,
                       CategoryId = p.Subcategory!.CategoryId,
                       CategoryName = p.Subcategory!.Category!.Name,
                       SubcategoryId = p.SubcategoryId,
                       SubcategoryName = p.Subcategory.Name,
                       Price = p.ListPrice,
                       DiscountRate = p.DiscountRate,
                       StockQuantitiy = p.StockQuantity,
                       DiscountedPrice = p.ListPrice * (100 - p.DiscountRate) / 100,
                       CoverImage = p.Images.FirstOrDefault(x => x.IsCover)?.ImagePath
                   };
        }

        public Task IncreasingCart(int customerId, int productId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromCart(int customerId, int productId)
        {
            throw new NotImplementedException();
        }
    }
}
