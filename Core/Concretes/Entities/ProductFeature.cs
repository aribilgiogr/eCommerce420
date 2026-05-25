using Core.Abstracts.Bases;

namespace Core.Concretes.Entities
{
    public class ProductFeature : BaseEntity
    {
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
