using AutoMapper;
using BookStoreAPI.Models.DTOs.Author;
using BookStoreAPI.Models.DTOs.Category;
using BookStoreAPI.Models.Entities;

namespace BookStoreAPI.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<ReadAuthorDTO, Author>().ReverseMap();
            CreateMap<WriteAuthorDTO, Author>().ReverseMap();
        }
    }
}
