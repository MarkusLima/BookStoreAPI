using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models.Entities
{
    [Index(nameof(email), Name = "IX_Unique_Email", IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; } // Id

        [Required]
        public int roleId { get; set; } // Id da Função

        public Role Role { get; set; }

        [Required]
        public string email { get; set; } // Email

        [Required]
        public string name { get; set; } // Nome

        [Required]
        [StringLength(100)]
        public string password { get; set; } // Senha

        [Required]
        public DateTime createdAt { get; set; } // Data de Criação

        public ICollection<Purchase> Purchases { get; set; }
    }
}
