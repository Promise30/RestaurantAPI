namespace RestaurantAPI.DTOs
{
    public class MenuTypeDTO
    {
        public int MenuTypeId { get; set; }
        public string Name { get; set; }
        public int RestaurantId { get; set; }
        public ICollection<MenuItemDTO> MenuItems { get; set; }
    }
}
