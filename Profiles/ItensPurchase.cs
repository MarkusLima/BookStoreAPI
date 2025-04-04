using AutoMapper;
using BookStoreAPI.Models.DTOs.ItenOfPurchase;
using BookStoreAPI.Models.Entities;

namespace BookStoreAPI.Profiles
{
    public class ItenPurchaseProfile : Profile
    {
        public ItenPurchaseProfile()
        {
            CreateMap<ReadItenOfPurchaseDTO, ItemOfPurchase>().ReverseMap();
            CreateMap<WriteItenOfPurchaseDTO, ItemOfPurchase>().ReverseMap();
        }
    }
}
