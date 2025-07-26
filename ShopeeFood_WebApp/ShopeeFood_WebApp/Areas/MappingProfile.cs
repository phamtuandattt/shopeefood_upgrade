using AutoMapper;
using ShopeeFood.BLL.DTOS.BusinessDTOs;
using ShopeeFood.BLL.DTOS.CityDTOs;
using ShopeeFood.BLL.DTOS.CustomerDTOs;
using ShopeeFood.BLL.DTOS.ShopDTOs;
using ShopeeFood_WebApp.Models;
using ShopeeFood_WebApp.Models.Customers;
using ShopeeFood_WebApp.Models.Shops;
using System.Runtime.CompilerServices;

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

            CreateMap<ShopCityBussinessJsonResponse, ShopCityBusinessResponseDto>().ReverseMap();

            CreateMap<CustomerResponseDto, CustomerProfileViewModel>().ReverseMap();
        }
    }
}
