using BookStoreAPI.Interface;
using BookStoreAPI.Models.DTOs.ItenOfPurchase;
using BookStoreAPI.Models.DTOs.Purchase;
using BookStoreAPI.Tools;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasesController : Controller
    {
        private readonly IPurchaseSevice _purchaseService;

        public PurchasesController(IPurchaseSevice purchaseService)
        {
            _purchaseService = purchaseService;
        }

        public async Task<ActionResult<IEnumerable<ReadPurchaseDTO>>> GetBooks([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            try
            {
                var result = await _purchaseService.GetPurchasesAsync(skip, take);
                return Ok(result);
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


        [HttpGet("{id}")]
        //[RoleMiddleware("admin")]
        public async Task<ActionResult<ReadPurchaseDTO>> GetPurchaseById(int id)
        {
            try
            {
                var result = await _purchaseService.GetPurchaseByIdAsync(id);
                return Ok(result);
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

        [HttpPut("{id}")]
        //[RoleMiddleware("admin")]
        public async Task<ActionResult> UpdateStatusPurchaseAsync(int id, int status)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _purchaseService.UpdateStatusPurchaseAsync(id, status);
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

        [HttpPost]
        //[RoleMiddleware("admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreatePurchaseAsync()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _purchaseService.CreatePurchaseAsync();
                return CreatedAtAction(nameof(GetPurchaseById), new { id = result.Id }, result);
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

        [HttpDelete("{id}")]
        //[RoleMiddleware("admin")]
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
