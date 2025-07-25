using AutoMapper;
using ShopeeFood_WebAPI.BLL.Dtos;
using ShopeeFood_WebAPI.BLL.Dtos.CityDtos;
using ShopeeFood_WebAPI.BLL.Dtos.CustomerDtos;
using ShopeeFood_WebAPI.BLL.Dtos.ShopDtos;
using ShopeeFood_WebAPI.DAL.ModelResonposeDtos.CityResponseDtos;
using ShopeeFood_WebAPI.DAL.ModelResonposeDtos.ShopResponseDtos;
using ShopeeFood_WebAPI.RequestModels.CityRequestDtos;

namespace ShopeeFood_WebAPI.ApplicationServices
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap();
        }
        protected void CreateMap()
        {
            CreateMap<City, CityDto>().ReverseMap()
                .ForMember(dest => dest.CityId, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcRemmeber) => srcRemmeber != null));
            CreateMap<AddCityRequestDto, CityDto>().ReverseMap();
            CreateMap<UpdateCityRequestDto, CityDto>().ReverseMap();
            CreateMap<CityBusinessFieldResponseDto, CityBusinessDto>().ReverseMap();
            CreateMap<ShopInTheCityDto, ShopInCityResponseDto>().ReverseMap();

            CreateMap<Shop, ShopInfoDto>().ReverseMap();
            CreateMap<MenuShop, ShopInfoResponseDto>()
                .ForMember(dest => dest.MenuDetailShops, opt => opt.MapFrom(src => src.MenuDetailShops));
            CreateMap<MenuDetailShop, CategoryItem>();

            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
