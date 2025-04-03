using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace BookStoreAPI.Models.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; } //  Id

        [Required]
        public string name { get; set; } // Nome

        [Required]
        public DateOnly dateOfBirth { get; set; }// Data de Nascimento

        [Required]
        public string country { get; set; }// País

        public ICollection<Book> Books { get; set; }
    }

}