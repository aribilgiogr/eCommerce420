using Core.Abstracts.Bases;
using Core.Concretes.Enums;

namespace Core.Concretes.Entities
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public decimal TotalDue { get; set; }
        public decimal Total { get; set; }
        public decimal TotalDiscount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.PENDING;
        public virtual ICollection<OrderItem> Items { get; set; } = [];
    }

}
