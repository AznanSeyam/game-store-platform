using System.Security.Claims;
using System.Threading.Tasks;
using game_store_platform.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace game_store_platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        [HttpPost("buy/{gameId:int}")]
        public async Task<IActionResult> BuyGame(int gameId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null) return Unauthorized();

            int userId = int.Parse(userIdString);

            var order = await orderService.BuyGameAsync(userId, gameId);

            if (order == null)
                return BadRequest("Purchase failed. Maybe insufficient balance, game not found, or already purchased.");

            return Ok(order);
        }

        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null) return Unauthorized();

            int userId = int.Parse(userIdString);

            var orders = await orderService.GetMyOrdersAsync(userId);
            return Ok(orders);
        }
    }
}