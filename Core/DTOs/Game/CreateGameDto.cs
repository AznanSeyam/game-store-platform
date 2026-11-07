namespace game_store_platform.Core.DTOs.Game
{
    public class CreateGameDto
    {
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public string DownloadUrl { get; set; } = string.Empty;
        public int GenreId { get; set; }
    }
}