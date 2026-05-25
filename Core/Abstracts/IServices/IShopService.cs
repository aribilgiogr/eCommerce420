using Core.Concretes.DTOs;

namespace Core.Abstracts.IServices
{
    public interface IShopService
    {
        Task<IEnumerable<ProductListItemDto>> GetProducts();

        // Sepet Sistemi
        Task AddToCart(int customerId, int productId, int quantity); // Ürün ekleme
        Task RemoveFromCart(int customerId, int productId); // Ürün çıkarma (bir ürünün tümünü)
        Task IncreasingCart(int customerId, int productId); // Sepetteki bir ürünün miktarını artırma
        Task DecreasingCart(int customerId, int productId); // Sepetteki bir ürünün miktarını azaltma
    }
}
