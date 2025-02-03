using AutoMapper;
using ShopeeFood_WebAPI.BLL.Dtos;

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
            CreateMap<CityDto, City>().ReverseMap();

        }
    }   
}
