using BookStoreAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models.DTOs.User
{
    public class WriteUserDTO
    {

        public int roleId { get; set; }

        public string email { get; set; }

        public string name { get; set; }

        public string password { get; set; }

    }
}
