using Core.Abstracts.Bases;

namespace Core.Concretes.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int SubcategoryId { get; set; }
        public virtual Subcategory? Subcategory { get; set; }
        public int BrandId { get; set; }
        public virtual Brand? Brand { get; set; }
        public decimal ListPrice { get; set; }
        public decimal DiscountRate { get; set; }
        public int StockQuantity { get; set; }
        public bool Active { get; set; } = true;

        public virtual ICollection<CartItem> CartItems { get; set; } = [];
        public virtual ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
