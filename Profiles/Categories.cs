﻿using AutoMapper;
using BookStoreAPI.Models.DTOs.Category;
using BookStoreAPI.Models.Entities;

namespace BookStoreAPI.Profiles
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<Category, ReadCategoryDTO>();
            CreateMap<ReadCategoryDTO, Category>();
        }
    }
}
