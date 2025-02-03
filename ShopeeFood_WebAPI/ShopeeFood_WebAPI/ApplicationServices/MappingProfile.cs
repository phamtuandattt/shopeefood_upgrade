using AutoMapper;
using ShopeeFood_WebAPI.BLL.Dtos;
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
        }
    }
}
