using Core.Abstracts;
using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;
using System.Security.Claims;

namespace Business.Services
{
    public class ShopService : IShopService
    {
        private readonly IUnitOfWork unitOfWork;

        public ShopService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private async Task<Cart> GetCartAsync(string userId)
        {
            var customer = await unitOfWork.Customers.ReadAsync(x => x.AccountId == userId);
            if (customer != null)
            {
                int customerId = customer.Id;
                var cart = await unitOfWork.Carts.ReadAsync(x => x.CustomerId == customerId && x.Active, "Items.Product.Images");
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
            else
            {
                throw new Exception("Customer not found.");
            }
        }

        public async Task AddToCart(string userId, int productId, int quantity = 1)
        {
            var cart = await GetCartAsync(userId);
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

        public async Task DecreasingCart(string userId, int productId)
        {
            var cart = await GetCartAsync(userId);
            var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);

            if (item == null) throw new Exception("Product not found in cart.");

            if (item.Quantity > 1)
            {
                item.Quantity -= 1;
                await unitOfWork.CartItems.UpdateAsync(item);
                var reply = await unitOfWork.CommitAsync();
                if (!reply.IsSuccess)
                {
                    throw new Exception(string.Join(", ", reply.Errors));
                }
            }
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

        public async Task IncreasingCart(string userId, int productId)
        {
            var cart = await GetCartAsync(userId);
            var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);

            if (item == null) throw new Exception("Product not found in cart.");

            item.Quantity += 1;
            await unitOfWork.CartItems.UpdateAsync(item);
            var reply = await unitOfWork.CommitAsync();
            if (!reply.IsSuccess)
            {
                throw new Exception(string.Join(", ", reply.Errors));
            }
        }

        public async Task RemoveFromCart(string userId, int productId)
        {
            var cart = await GetCartAsync(userId);
            var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                await unitOfWork.CartItems.DeleteAsync(item);
                var reply = await unitOfWork.CommitAsync();
                if (!reply.IsSuccess)
                {
                    throw new Exception(string.Join(", ", reply.Errors));
                }
            }
        }

        public async Task<CartDto> GetCurrentCartAsync(ClaimsPrincipal user)
        {

            var cart = await GetCartAsync(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var items = cart.Items.Select(x => new CartItemDto
            {
                ProductId = x.ProductId,
                ProductName = x.Product!.Name,
                Quantity = x.Quantity,
                ListPrice = x.Product.ListPrice,
                DiscountedPrice = x.Product.ListPrice * (100 - x.Product.DiscountRate) / 100,
                ProductImage = x.Product.Images.FirstOrDefault(i => i.IsCover)?.ImagePath
            }).ToList();

            return new CartDto { Items = items };
        }
    }
}
