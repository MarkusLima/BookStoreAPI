using BookStoreAPI.Models.DTOs.ItenOfPurchase;
using BookStoreAPI.Models.DTOs.Purchase;

namespace BookStoreAPI.Interface
{
    public interface IPurchaseSevice
    {
        Task<IEnumerable<ReadPurchaseDTO>> GetPurchasesAsync(int skip, int take);
        Task<ReadPurchaseDTO> GetPurchaseByIdAsync(int id);
        Task<ReadPurchaseDTO> CreatePurchaseAsync();
        Task<bool> UpdateStatusPurchaseAsync(int id, int status);
        Task<bool> UpdateItemPurchaseAsync(ReadItenOfPurchaseDTO itenOfPurchase);
        Task<bool> DeletePurchaseAsync(int id);
    }
}
