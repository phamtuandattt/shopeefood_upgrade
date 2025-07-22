using AutoMapper;
using ShopeeFood.BLL.DTOS.CityDTOs;
using ShopeeFood.BLL.DTOS.ShopDTOs;
using ShopeeFood_WebApp.Models.Shops;

namespace ShopeeFood_WebApp.Areas
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap();
        }

        protected void CreateMap()
        {
            CreateMap<ShopResponseDtos, ShopViewModel>().ReverseMap();
        }
    }
}
