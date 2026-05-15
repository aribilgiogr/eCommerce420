using Core.Abstracts.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;
        public virtual ICollection<Subcategory> Subcategories { get; set; } = [];
    }

    public class Subcategory : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<Product> Products { get; set; } = [];
    }

    public class Brand : BaseEntity
    {
        public string Name { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
