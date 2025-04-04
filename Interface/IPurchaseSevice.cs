using BookStoreAPI.Models.DTOs.ItenOfPurchase;
using BookStoreAPI.Models.DTOs.Purchase;

namespace BookStoreAPI.Interface
{
    public interface IPurchaseSevice
    {
        Task<IEnumerable<ReadPurchaseDTO>> GetPurchasesAsync(int skip, int take, int userId = 0);
        Task<ReadPurchaseDTO> GetPurchaseByIdAsync(int id, int userId = 0);
        Task<ReadPurchaseDTO> CreatePurchaseAsync(int userId);
        Task<bool> UpdateStatusPurchaseAsync(int id, int status, int userId = 0);
        Task<bool> UpdateItemPurchaseAsync(WriteItenOfPurchaseDTO itenOfPurchase, int userId = 0);
        Task<bool> DeletePurchaseAsync(int id, int userId = 0);
        Task<IEnumerable<ReadItenOfPurchaseDTO>> GetItensPurchasesAsync(int id, int userId = 0);
    }
}
