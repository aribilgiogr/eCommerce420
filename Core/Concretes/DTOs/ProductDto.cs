using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concretes.DTOs
{
    public class ProductListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; } = null!;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal DiscountRate { get; set; }
        public int StockQuantitiy { get; set; }
        public string? CoverImage { get; set; }
    }

    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
