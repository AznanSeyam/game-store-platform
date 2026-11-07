using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using game_store_platform.Core.DTOs.Order;
using game_store_platform.Core.Interfaces;
using game_store_platform.Data;
using game_store_platform.Data.Entities;
using game_store_platform.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace game_store_platform.Core.Services
{
    public class OrderService(GameStoreDBContext context, IMapper mapper) : IOrderService
    {
        public async Task<OrderDto?> BuyGameAsync(int userId, int gameId)
        {

            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var game = await context.Games.FirstOrDefaultAsync(g => g.Id == gameId);

            if (user == null || game == null)
            {
                return null;
            }

            var alreadyPurchased = await context.Orders
                .AnyAsync(o => o.UserId == userId && o.GameId == gameId);

            if (alreadyPurchased)
            {
                return null;
            }

            if (user.WalletBalance < (decimal)game.Price)
            {
                return null;
            }

            user.WalletBalance -= (decimal)game.Price;

            var order = new Order
            {
                UserId = userId,
                GameId = gameId,
                AmountPaid = (decimal)game.Price,
                PurchaseDate = DateTime.UtcNow,
                Status = OrderStatus.Completed
            };

            context.Orders.Add(order);
            await context.SaveChangesAsync();

            order.Game = game;

            return mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetMyOrdersAsync(int userId)
        {
            var orders = await context.Orders
                .Include(o => o.Game)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.PurchaseDate)
                .ToListAsync();

            return mapper.Map<IEnumerable<OrderDto>>(orders);
        }
    }
}