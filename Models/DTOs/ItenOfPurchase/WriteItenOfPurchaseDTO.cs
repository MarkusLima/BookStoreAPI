namespace BookStoreAPI.Models.DTOs.ItenOfPurchase
{
    public class WriteItenOfPurchaseDTO
    {
        public int bookId { get; set; }

        public int purchaseId { get; set; }

        public int quantity { get; set; }
    }
}
