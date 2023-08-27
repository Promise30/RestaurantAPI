using AutoMapper;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<State, StateDTO>().ReverseMap();
            CreateMap<Restaurant, DetailedRestaurantResponse>().ReverseMap();
            //CreateMap<MenuType, MenuTypeDTO>().ForMember(dest => dest.MenuItems, opt => opt.MapFrom(src => src.MenuItems));
            CreateMap<MenuItem, MenuItemDTO>().ReverseMap();
            CreateMap<LocalGovernment, LocalGovtDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<Delivery, DeliveryDTO>().ReverseMap();
            CreateMap<CreateMenuType, MenuTypeDTO>().ReverseMap();
            CreateMap<MenuType, MenuTypeDTO>().ReverseMap();
            CreateMap<CreateMenuItem, MenuItem>().ReverseMap();
            CreateMap<RestaurantResponse, Restaurant>().ReverseMap();
            CreateMap<RestaurantDTO, Restaurant>().ReverseMap();


        }
    }
}
