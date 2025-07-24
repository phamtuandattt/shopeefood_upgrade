using Newtonsoft.Json;
using ShopeeFood.BLL.DTOS.ShopDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.DTOS.BusinessDTOs
{
    public class ShopCityBusinessResponseDto
    {
        [JsonProperty("CityBusinesses")]
        public List<BusinessDto> Businesses { get; set; }

        [JsonProperty("ShopInTheCities")]
        public List<ShopResponseDtos> Shops { get; set; }
    }
}
