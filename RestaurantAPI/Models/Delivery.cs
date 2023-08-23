namespace RestaurantAPI.Models
{
    public class Delivery
    {
        public int DeliveryId { get; set; }
        public bool OffersDelivery { get; set; }
        public decimal DeliveryFee { get; set; }
        public string DeliveryTime { get; set; }

    }
}
