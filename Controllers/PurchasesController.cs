using BookStoreAPI.Data;
using BookStoreAPI.Interface;
using BookStoreAPI.Midlewares;
using BookStoreAPI.Models.DTOs.ItenOfPurchase;
using BookStoreAPI.Models.DTOs.Purchase;
using BookStoreAPI.Models.Entities;
using BookStoreAPI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasesController : Controller
    {
        private readonly IPurchaseSevice _purchaseService;
        private readonly UserContextService _userContextService;

        public PurchasesController(IPurchaseSevice purchaseService, UserContextService userContextService)
        {
            _purchaseService = purchaseService;
            _userContextService = userContextService;
        }

        [HttpGet]
        [Authorize]
        [RoleMiddleware("Adm")]
        public async Task<ActionResult<IEnumerable<ReadPurchaseDTO>>> GetPurchasesAsync([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            try
            {
                if (_userContextService.roleName == "Client")
                {
                    var resultClient = await _purchaseService.GetPurchasesAsync(skip, take, _userContextService.userId);
                    return Ok(resultClient);
                }
                var result = await _purchaseService.GetPurchasesAsync(skip, take);
                return Ok(result);
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
                    var resultClient = await _purchaseService.GetPurchaseByIdAsync(id, _userContextService.userId);
                    return Ok(resultClient);
                }

                var result = await _purchaseService.GetPurchaseByIdAsync(id);
                return Ok(result);
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        [RoleMiddleware("Adm", "Client")]
        public async Task<ActionResult> UpdateStatusPurchaseAsync(int id, int status)
        {
            try
            {
                if (_userContextService.roleName == "Client")
                {
                    await _purchaseService.UpdateStatusPurchaseAsync(id, status, _userContextService.userId);
                    return NoContent();
                }

                await _purchaseService.UpdateStatusPurchaseAsync(id, status);
                return NoContent();
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [RoleMiddleware("Adm", "Client")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreatePurchaseAsync()
        {
            try
            {
                var result = await _purchaseService.CreatePurchaseAsync(_userContextService.userId);
                return CreatedAtAction(nameof(GetPurchaseById), new { id = result.Id }, result);
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        [RoleMiddleware("Adm", "Client")]
        public async Task<ActionResult> DeletePurchase(int id)
        {
            try
            {
                var result = await _purchaseService.DeletePurchaseAsync(id);
                return NoContent();
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

    }
}
