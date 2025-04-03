using AutoMapper;
using BookStoreAPI.Data;
using BookStoreAPI.Interface;
using BookStoreAPI.Models.DTOs.ItenOfPurchase;
using BookStoreAPI.Models.DTOs.Purchase;
using BookStoreAPI.Models.Entities;
using BookStoreAPI.Tools;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Services
{
    public class PurchaseService : IPurchaseSevice
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PurchaseService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadPurchaseDTO>> GetPurchasesAsync(int skip, int take)
        {
            var results = await _context.Purchases.Skip(skip).Take(take).ToListAsync();
            return _mapper.Map<List<ReadPurchaseDTO>>(results);
        }

        public async Task<ReadPurchaseDTO> GetPurchaseByIdAsync(int id)
        {
            var result = await _context.Purchases.FirstOrDefaultAsync(b => b.Id == id);

            if (result == null) throw new ExceptionsCode("Purchase not found", 404);

            return _mapper.Map<ReadPurchaseDTO>(result);
        }

        public async Task<bool> UpdateStatusPurchaseAsync(int id, int status)
        {

            var result = await _context.Purchases
                .FirstOrDefaultAsync( p => p.userId == 1 && p.Id == id);

            if (result == null) throw new ExceptionsCode("Purchase not found", 404);

            result.status = status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ReadPurchaseDTO> CreatePurchaseAsync()
        {
            var purchase = new Purchase
            {
                userId = 1, status = 0, dateOfPurchase = DateTime.Now
            };

            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReadPurchaseDTO>(purchase);
        }

        public async Task<bool> DeletePurchaseAsync(int id)
        {
            var result = await _context.Purchases.FindAsync(id);
            if (result == null) throw new ExceptionsCode("Purchase not found", 404);

            _context.Purchases.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateItemPurchaseAsync(ReadItenOfPurchaseDTO itenOfPurchase)
        {
            // to do pegar o id do usuario pelo token
            var result = await _context.Purchases.FirstOrDefaultAsync(i => i.userId == 1 && i.Id == itenOfPurchase.purchaseId);
            if (result == null) throw new ExceptionsCode("Purchase not found", 404);

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == itenOfPurchase.bookId);
            if (book == null) throw new ExceptionsCode("Book not found", 404);

            if (book.stockQuantity == 0) throw new ExceptionsCode("Quantity must be greater than 0", 400);

            if (itenOfPurchase.quantity > book.stockQuantity) throw new ExceptionsCode("Quantity must be less than stock", 400);

            var itemOfPurchase = await _context.ItemsOfPurchases
                .FirstOrDefaultAsync(i => i.purchaseId == itenOfPurchase.purchaseId && i.bookId == itenOfPurchase.bookId);

            if (itemOfPurchase != null)
            {
                itemOfPurchase.quantity += itenOfPurchase.quantity;
                _context.ItemsOfPurchases.Update(itemOfPurchase);
            }

            if (itemOfPurchase == null)
            {
                itemOfPurchase = new ItemOfPurchase
                {
                    bookId = itenOfPurchase.bookId,
                    purchaseId = itenOfPurchase.purchaseId,
                    quantity = itenOfPurchase.quantity
                };
                _context.ItemsOfPurchases.Add(itemOfPurchase);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
