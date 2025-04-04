using BookStoreAPI.Data;
using BookStoreAPI.Interface;
using BookStoreAPI.Midlewares;
using BookStoreAPI.Models.DTOs.ItenOfPurchase;
using BookStoreAPI.Models.DTOs.Purchase;
using BookStoreAPI.Models.Entities;
using BookStoreAPI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemPurchaseController : Controller
    {
        private readonly IPurchaseSevice _purchaseService;
        private readonly UserContextService _userContextService;

        public ItemPurchaseController(IPurchaseSevice purchaseService, UserContextService userContextService)
        {
            _purchaseService = purchaseService;
            _userContextService = userContextService;
        }


        [HttpPost]
        [Authorize]
        [RoleMiddleware("Adm", "Client")]
        public async Task<ActionResult> UpdateItemPurchaseAsync(WriteItenOfPurchaseDTO itenOfPurchase)
        {
            try
            {
                if (_userContextService.roleName == "Client")
                {
                    await _purchaseService.UpdateItemPurchaseAsync(itenOfPurchase, _userContextService.userId);
                    return NoContent();
                }
   
                await _purchaseService.UpdateItemPurchaseAsync(itenOfPurchase);
                return NoContent();
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        
        }

        [HttpGet("{id}")]
        [Authorize]
        [RoleMiddleware("Adm", "Client")]
        public async Task<ActionResult<ReadPurchaseDTO>> GetPurchaseById(int id)
        {
            try
            {
                if (_userContextService.roleName == "Client")
                {
                    var resultClient = await _purchaseService.GetItensPurchasesAsync(id, _userContextService.userId);
                    return Ok(resultClient);
                }

                var result = await _purchaseService.GetItensPurchasesAsync(id);
                return Ok(result);
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }


    }
}
