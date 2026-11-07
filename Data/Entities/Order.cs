using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game_store_platform.Data.Enums;

namespace game_store_platform.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Completed;
    }
}