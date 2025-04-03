using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; } // Id

        [Required]
        [MaxLength(50)]
        public string name { get; set; } // Nome

        public ICollection<User> Users { get; set; }

    }
}
