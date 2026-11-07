namespace game_store_platform.Core.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string GameTitle { get; set; } = string.Empty;
        public decimal AmountPaid { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}