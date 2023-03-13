using ShopProductSailing.Domain.Commons;

namespace ShopProductSailing.Domain.Entities
{
    public class SoldProduct : Auditable
    {
        public DateTime SoldAt { get; set; }
    }
}
