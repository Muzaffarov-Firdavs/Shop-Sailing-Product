using ShopProductSailing.Domain.Commons;

namespace ShopSailingCRM.Domain.Entities
{
    public class Product : Auditable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
