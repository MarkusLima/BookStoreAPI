using AutoMapper;
using BookStoreAPI.Models.DTOs.Purchase;
using BookStoreAPI.Models.Entities;

namespace BookStoreAPI.Profiles
{
    public class PurchaseProfile : Profile
    {
        public PurchaseProfile()
        {
            CreateMap<ReadPurchaseDTO, Purchase>().ReverseMap();
            CreateMap<WritePurchaseDTO, Purchase>().ReverseMap();
        }
    }

}
