using Core.Abstracts;
using Core.Abstracts.IServices;
using Core.Concretes.DTOs;

namespace Business.Services
{
    public class ShopService : IShopService
    {
        private readonly IUnitOfWork unitOfWork;

        public ShopService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductListItemDto>> GetProducts()
        {
            var products = await unitOfWork.Products.ReadManyAsync(x => x.Active, "Subcategory.Category", "Brand");

            return from p in products
                   select new ProductListItemDto
                   {
                       Id = p.Id,
                       Name = p.Name,
                       BrandId = p.BrandId,
                       BrandName = p.Brand.Name,
                       CategoryId = p.Subcategory.CategoryId,
                       CategoryName = p.Subcategory.Category.Name,
                       SubcategoryId = p.SubcategoryId,
                       SubcategoryName = p.Subcategory.Name,
                       Price = p.ListPrice,
                       DiscountRate = p.DiscountRate,
                       StockQuantitiy = p.StockQuantity,
                       DiscountedPrice = p.ListPrice * (100 - p.DiscountRate) / 100
                   };
        }
    }
}
