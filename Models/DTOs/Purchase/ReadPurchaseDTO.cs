using BookStoreAPI.Models.Entities;

namespace BookStoreAPI.Models.DTOs.Purchase
{
    public class ReadPurchaseDTO
    {
        public int Id { get; set; }

        public int status { get; set; }

        public int userId { get; set; } 

        public DateTime dateOfPurchase { get; set; }

    }
}
