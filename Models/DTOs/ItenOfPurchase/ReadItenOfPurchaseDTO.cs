
namespace BookStoreAPI.Models.DTOs.ItenOfPurchase
{
    public class ReadItenOfPurchaseDTO
    {
        public int Id { get; set; }

        public int bookId { get; set; }

        public int purchaseId { get; set; }

        public int quantity { get; set; }
    }
}
