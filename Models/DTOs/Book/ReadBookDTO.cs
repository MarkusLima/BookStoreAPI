using BookStoreAPI.Models.DTOs.Author;
using BookStoreAPI.Models.DTOs.Category;
using BookStoreAPI.Models.Entities;

namespace BookStoreAPI.Models.DTOs.Book
{
    public class ReadBookDTO
    {
        public int Id { get; set; }

        public string title { get; set; }

        public int authorId { get; set; }

        public int categoryId { get; set; }

        public DateOnly publicationDate { get; set; }

        public decimal price { get; set; }

        public int stockQuantity { get; set; }

        public ReadAuthorDTO author { get; set; }

        public ReadCategoryDTO category { get; set; }
    }
}
