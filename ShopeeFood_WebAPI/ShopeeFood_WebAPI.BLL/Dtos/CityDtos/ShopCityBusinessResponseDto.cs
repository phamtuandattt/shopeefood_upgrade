using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.BLL.Dtos.CityDtos
{
    public class ShopCityBusinessResponseDto
    {
        public List<CityBusinessDto> CityBusinesses { get; set; }
        public List<ShopInTheCityDto> ShopInTheCities { get; set; }
    }
}
