using System;
using System.Collections.Generic;

namespace game_store_platform.Data.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public string DownloadUrl { get; set; } = string.Empty;
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}