using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookStoreAPI.Models.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; } // Id

        [Required]
        public string title { get; set; } // Título

        [Required]
        public int authorId { get; set; } // Id do Autor

        public Author Author { get; set; }

        [Required]
        public int categoryId { get; set; } // Id da Categoria

        public Category Category { get; set; }

        [Required]
        public DateOnly publicationDate { get; set; } // Data de Publicação

        [Required]
        public decimal price { get; set; } // Preço

        [Required]
        public int stockQuantity { get; set; } // Quantidade em Estoque

        public ICollection<ItemOfPurchase> ItemsOfPurchase { get; set; }

    }
}
