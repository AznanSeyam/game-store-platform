using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace game_store_platform.Core.DTOs.Game
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public string GenreName { get; set; } = string.Empty;
    }
}