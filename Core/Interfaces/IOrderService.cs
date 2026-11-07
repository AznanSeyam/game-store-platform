using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game_store_platform.Core.DTOs.Order;

namespace game_store_platform.Core.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto?> BuyGameAsync(int userId, int gameId);

        Task<IEnumerable<OrderDto>> GetMyOrdersAsync(int userId);
    }
}