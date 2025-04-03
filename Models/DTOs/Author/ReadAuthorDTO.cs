namespace BookStoreAPI.Models.DTOs.Author
{
    public class ReadAuthorDTO
    {
        public int Id { get; set; }

        public string name { get; set; }

        public DateOnly dateOfBirth { get; set; }

        public string country { get; set; }
    }
}
