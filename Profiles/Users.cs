using AutoMapper;
using BookStoreAPI.Models.DTOs.Category;
using BookStoreAPI.Models.DTOs.User;
using BookStoreAPI.Models.Entities;

namespace BookStoreAPI.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<ReadUserDTO, User>().ReverseMap();
            CreateMap<LoginUserDTO, User>().ReverseMap();
            CreateMap<WriteUserDTO, User>().ReverseMap();
        }
    }
}
