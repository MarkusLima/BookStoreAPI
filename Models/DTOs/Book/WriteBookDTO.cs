namespace BookStoreAPI.Models.DTOs.Book
{
    public class WriteBookDTO
    {
        public string title { get; set; }

        public int authorId { get; set; }

        public int categoryId { get; set; }

        public DateOnly publicationDate { get; set; }

        public decimal price { get; set; }

        public int stockQuantity { get; set; }
    }
}
