using System;
using System.Collections.Generic;

namespace game_store_platform.Data.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public IList<Game> Games { get; set; } = [];
    }
}