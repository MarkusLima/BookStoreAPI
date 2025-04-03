using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models.Entities
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; } //  Id

        public int status { get; set; } // Status da compra // 0-Aberta/1-Pago/2-Estornada

        [Required]
        public int userId { get; set; } // Id do usuário

        public User User { get; set; }

        [Required]
        public DateTime dateOfPurchase { get; set; }// Data da compra

        public ICollection<ItemOfPurchase> ItemsOfPurchase { get; set; }
    }
}
