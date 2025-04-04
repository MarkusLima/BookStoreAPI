using AutoMapper;
using BookStoreAPI.Models.DTOs.Category;
using BookStoreAPI.Models.Entities;

namespace BookStoreAPI.Profiles
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<ReadCategoryDTO, Category>().ReverseMap();
            CreateMap<WriteCategoryDTO, Category>().ReverseMap();
        }
    }
}
