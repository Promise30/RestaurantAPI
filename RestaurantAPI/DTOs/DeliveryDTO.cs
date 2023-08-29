namespace RestaurantAPI.DTOs
{
    public class DeliveryDTO
    {
        public bool OffersDelivery { get; set; }
        public decimal DeliveryFee { get; set; } = 0;
        public string DeliveryTime { get; set; } = string.Empty;
    }
}
