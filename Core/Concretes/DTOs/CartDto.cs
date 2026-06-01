namespace Core.Concretes.DTOs
{
    public class CartDto
    {
        public IEnumerable<CartItemDto> Items { get; set; } = [];
        public decimal TotalPrice => Items.Any() ? Items.Sum(x => x.TotalPrice) : 0;
        public decimal TotalDiscount => Items.Any() ? Items.Sum(x => x.TotalPrice - x.DiscountedTotal) : 0;
        public decimal TotalDue => TotalPrice - TotalDiscount;
    }

    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? ProductImage { get; set; }
        public decimal ListPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => ListPrice * Quantity;
        public decimal DiscountedTotal => DiscountedPrice * Quantity;
    }
}