using Core.Concretes.DTOs;

namespace Core.Abstracts.IServices
{
    public interface IShopService
    {
        Task<IEnumerable<ProductListItemDto>> GetProducts();

        // Sepet Sistemi
        Task AddToCart(string userId, int productId, int quantity); // Ürün ekleme
        Task RemoveFromCart(string userId, int productId); // Ürün çıkarma (bir ürünün tümünü)
        Task IncreasingCart(string userId, int productId); // Sepetteki bir ürünün miktarını artırma
        Task DecreasingCart(string userId, int productId); // Sepetteki bir ürünün miktarını azaltma
    }
}
