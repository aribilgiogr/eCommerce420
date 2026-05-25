using Core.Abstracts.Bases;

namespace Core.Concretes.Entities
{
    // Ürün galerisi ve özellikleri için ayrı sınıflar oluşturulabilir, böylece ürün detayları daha esnek bir şekilde yönetilebilir.
    public class ProductImage : BaseEntity
    {
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public string ImagePath { get; set; } = null!;
        public bool IsCover { get; set; } = false;
    }
}
