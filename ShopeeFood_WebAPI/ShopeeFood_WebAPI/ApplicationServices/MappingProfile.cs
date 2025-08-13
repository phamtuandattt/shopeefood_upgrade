using AutoMapper;
using ShopeeFood_WebAPI.BLL.Dtos;
using ShopeeFood_WebAPI.BLL.Dtos.CityDtos;
using ShopeeFood_WebAPI.BLL.Dtos.CustomerDtos;
using ShopeeFood_WebAPI.BLL.Dtos.PageDtos;
using ShopeeFood_WebAPI.BLL.Dtos.ShopDtos;
using ShopeeFood_WebAPI.DAL.ModelResonposeDtos.CityResponseDtos;
using ShopeeFood_WebAPI.DAL.ModelResonposeDtos.ShopResponseDtos;
using ShopeeFood_WebAPI.Infrastructure.Common.Email;
using ShopeeFood_WebAPI.RequestModels.CityRequestDtos;
using ShopeeFood_WebAPI.RequestModels.UserRequestDtos;

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
            CreateMap<CustomerAddress, CustomerAddressDto>().ReverseMap();
            CreateMap<CustomerExternalLogin, CustomerExternalLoginDto>().ReverseMap();

            CreateMap<CustomerAddressRequestDto, CustomerAddressDto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<EmailSetting, EmailSettingDto>().ReverseMap();
            CreateMap<EmailSettingDto, EmailSettings>().ReverseMap();

            CreateMap<PageSetting, PageSettingDto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
