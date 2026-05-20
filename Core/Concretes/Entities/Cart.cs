using Core.Abstracts.Bases;

namespace Core.Concretes.Entities
{
    public class Cart : BaseEntity
    {
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<CartItem> Items { get; set; } = [];
        public bool Active { get; set; } = true;
    }
}
