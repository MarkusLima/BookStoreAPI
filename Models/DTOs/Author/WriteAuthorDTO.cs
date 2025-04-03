namespace BookStoreAPI.Models.DTOs.Author
{
    public class WriteAuthorDTO
    {
        public string name { get; set; }

        public DateOnly dateOfBirth { get; set; }

        public string country { get; set; }
    }
}
