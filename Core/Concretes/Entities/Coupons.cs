using Core.Abstracts.Bases;
using Tools.Helpers;

namespace Core.Concretes.Entities
{
    public class Coupon : BaseEntity
    {
        // Kupon kodu özel olarak gönderilmezse , otomatik olarak 12 karakter uzunluğunda rastgele bir kod oluşturulur.
        public string Code { get; set; } = RandomCodeGenerator.Generate(12);
        // Kupon indirim miktarı veya yüzdesi, indirim türüne göre belirlenir. İndirim türü "Amount" ise DiscountAmount kullanılır, "Percentage" ise DiscountRate kullanılır.
        public decimal DiscountAmount { get; set; } = 0; // İndirim miktarı (örneğin: 10.00)
        public decimal DiscountRate { get; set; } = 0; // İndirim yüzdesi (örneğin: 15.00)
        public string DiscountType { get; set; } = "Amount"; // İndirim türü ("Amount" veya "Percentage")
        public DateTime ExpirationDate { get; set; } // Kuponun geçerlilik süresi
        public bool Active { get; set; } = true; // Kuponun aktif olup olmadığını belirten özellik
    }
}
