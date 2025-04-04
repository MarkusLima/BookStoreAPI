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

        public async Task<IEnumerable<ReadPurchaseDTO>> GetPurchasesAsync(int skip, int take, int userId = 0)
        {
            if (userId != 0)
            {
                var resultClient = await _context.Purchases
                    .Where(p => p.userId == userId)
                    .Skip(skip).Take(take).ToListAsync();
                return _mapper.Map<List<ReadPurchaseDTO>>(resultClient);
            }

            var results = await _context.Purchases.Skip(skip).Take(take).ToListAsync();
            return _mapper.Map<List<ReadPurchaseDTO>>(results);
        }

        public async Task<ReadPurchaseDTO> GetPurchaseByIdAsync(int id, int userId = 0)
        {
            if (userId != 0) {
                var resultClient = await _context.Purchases
                    .Where(p => p.userId == userId && p.Id == id).FirstOrDefaultAsync();
                if (resultClient == null) throw new ExceptionsCode("Purchase not found", 404);
                return _mapper.Map<ReadPurchaseDTO>(resultClient);
            }

            var result = await _context.Purchases.FirstOrDefaultAsync(b => b.Id == id);
            if (result == null) throw new ExceptionsCode("Purchase not found", 404);

            return _mapper.Map<ReadPurchaseDTO>(result);
        }

        public async Task<bool> UpdateStatusPurchaseAsync(int id, int status, int userId = 0)
        {

            Purchase result;

            if (userId != 0) result = await _context.Purchases.FirstOrDefaultAsync(p => p.userId == userId && p.Id == id);
            else result = await _context.Purchases.FirstOrDefaultAsync(p => p.Id == id);

            if (result == null) throw new ExceptionsCode("Purchase not found", 404);

            var itemsOfPurchase = await _context.ItemsOfPurchases
                .Where(i => i.purchaseId == id)
                .ToListAsync();

            if (status == result.status) throw new ExceptionsCode("Purchase is already in this status", 400);

            if (status == 1) await this.DownOrUpStock(itemsOfPurchase, true);
            else await this.DownOrUpStock(itemsOfPurchase, false);

            result.status = status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ReadPurchaseDTO> CreatePurchaseAsync(int userId)
        {
            var purchase = new Purchase
            {
                userId = userId, status = 0, dateOfPurchase = DateTime.Now
            };

            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReadPurchaseDTO>(purchase);
        }

        public async Task<bool> DeletePurchaseAsync(int id, int userId = 0)
        {
            var result = await _context.Purchases.FindAsync(id);
            if (result == null) throw new ExceptionsCode("Purchase not found", 404);

            _context.Purchases.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateItemPurchaseAsync(WriteItenOfPurchaseDTO itenOfPurchase, int userId = 0)
        {
            Purchase result;

            if (userId != 0) result = await _context.Purchases.FirstOrDefaultAsync(i => i.userId == userId && i.Id == itenOfPurchase.purchaseId);
            else result = await _context.Purchases.FirstOrDefaultAsync(i => i.Id == itenOfPurchase.purchaseId);

            if (result == null) throw new ExceptionsCode("Purchase not found", 404);

            if (result.status != 0) throw new ExceptionsCode("Purchase not editabled", 404);

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == itenOfPurchase.bookId);
            if (book == null) throw new ExceptionsCode("Book not found", 404);

            if (book.stockQuantity == 0) throw new ExceptionsCode("Quantity must be greater than 0", 400);

            if (itenOfPurchase.quantity > book.stockQuantity) throw new ExceptionsCode("Quantity must be less than stock", 400);

            var itemOfPurchase = await _context.ItemsOfPurchases
                .FirstOrDefaultAsync(i => i.purchaseId == itenOfPurchase.purchaseId && i.bookId == itenOfPurchase.bookId);

            if (itemOfPurchase != null)
            {
                itemOfPurchase.quantity = itenOfPurchase.quantity;
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

        public async Task<IEnumerable<ReadItenOfPurchaseDTO>> GetItensPurchasesAsync(int id, int userId = 0)
        {

            if (userId != 0)
            {
                var resultClient = await _context.ItemsOfPurchases
                    .Where(i => i.Puchases.userId == userId && i.Puchases.Id == id).ToListAsync();

                return _mapper.Map<List<ReadItenOfPurchaseDTO>>(resultClient);
            }
       
            var results = await _context.ItemsOfPurchases
                .Where(i => i.Puchases.Id == id).ToListAsync();

            return _mapper.Map<List<ReadItenOfPurchaseDTO>>(results);
        }

        private async Task<bool> DownOrUpStock(List<ItemOfPurchase> itemsOfPurchase, bool operation)
        {
            foreach (var item in itemsOfPurchase)
            {
                var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == item.bookId);
                if (book == null) continue;

                if (operation)
                    book.stockQuantity -= item.quantity;
                else
                    book.stockQuantity += item.quantity;
  
                _context.Books.Update(book);
            }
            return true;
        }
    }
}
