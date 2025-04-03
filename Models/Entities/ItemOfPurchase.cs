using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models.Entities
{
    public class ItemOfPurchase
    {
        [Key]
        public int Id { get; set; } //  Id

        [Required]
        public int bookId { get; set; } // Id do Livro

        public Book Books { get; set; }

        [Required]
        public int purchaseId { get; set; } // Id da Compra

        [Required]
        public int quantity { get; set; } // Quantidade

        public Purchase Puchases { get; set; }
    }
}
