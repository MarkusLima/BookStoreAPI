using BookStoreAPI.Interface;
using BookStoreAPI.Models.DTOs.ItenOfPurchase;
using BookStoreAPI.Tools;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemPurchaseController : Controller
    {
        private readonly IPurchaseSevice _purchaseService;

        public ItemPurchaseController(IPurchaseSevice purchaseService)
        {
            _purchaseService = purchaseService;
        }


        [HttpPost]
        //[RoleMiddleware("admin")]
        public async Task<ActionResult> UpdateItemPurchaseAsync(ReadItenOfPurchaseDTO itenOfPurchase)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _purchaseService.UpdateItemPurchaseAsync(itenOfPurchase);
                return NoContent();
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
