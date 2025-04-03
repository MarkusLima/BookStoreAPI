using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; } //  Id

        [Required]
        public string name { get; set; } // Nome

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
