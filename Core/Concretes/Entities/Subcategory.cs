using Core.Abstracts.Bases;

namespace Core.Concretes.Entities
{
    public class Subcategory : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
