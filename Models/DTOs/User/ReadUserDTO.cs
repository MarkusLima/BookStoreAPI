using BookStoreAPI.Models.DTOs.Role;
using BookStoreAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models.DTOs.User
{
    public class ReadUserDTO
    {
        public int Id { get; set; }

        public int roleId { get; set; }

        public string email { get; set; }

        public string name { get; set; }

        public DateTime createdAt { get; set; }

    }
}
