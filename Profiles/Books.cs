using AutoMapper;
using BookStoreAPI.Models.DTOs.Book;
using BookStoreAPI.Models.DTOs.Category;
using BookStoreAPI.Models.Entities;

namespace BookStoreAPI.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<ReadBookDTO, Book>().ReverseMap();
            CreateMap<WriteBookDTO, Book>().ReverseMap();
        }
    }
}
